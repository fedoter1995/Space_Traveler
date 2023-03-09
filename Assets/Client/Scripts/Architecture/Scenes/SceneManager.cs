using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomTools;

namespace Architecture
{
    public class SceneManager : ISceneManager
    {

        #region CONSTANTS
        private const string CONFIG_FOLDER = "SceneConfigs";
        #endregion

        public event SceneManagerHandler OnSceneLoadStartedEvent;
        public event SceneManagerHandler OnSceneLoadCompletedEvent;

        public event Action OnUIInitialized;

        public Dictionary<string, SceneConfig> scenesConfigMap { get; }
        public SaveDataController saveController { get; private set; }

        public IScene sceneActual { get; private set; }
        public bool isLoading { get; private set; }
        public EnvironmentSettings DefaultEnvironment => sceneActual.DefaultEnvironment;
        public SceneManager()
        {
            scenesConfigMap = new Dictionary<string, SceneConfig>();

            saveController = new SaveDataController();
            saveController.Initialize();

            InitializeSceneConfigs();
        }

        private void InitializeSceneConfigs()
        {
            var allSceneConfigs = Resources.LoadAll<SceneConfig>(CONFIG_FOLDER);
            foreach (var sceneConfig in allSceneConfigs)
                scenesConfigMap[sceneConfig.sceneName] = sceneConfig;
        }



        public Coroutine LoadScene(string sceneName, UnityAction<SceneConfig> sceneLoadedCallback = null)
        {
            return LoadAndInitializeScene(sceneName, sceneLoadedCallback, true);
        }

        public Coroutine InitializeCurrentScene(UnityAction<SceneConfig> sceneLoadedCallback = null)
        {
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            return LoadAndInitializeScene(sceneName, sceneLoadedCallback, false);
        }


        protected Coroutine LoadAndInitializeScene(string sceneName, UnityAction<SceneConfig> sceneLoadedCallback,
            bool loadNewScene)
        {
            scenesConfigMap.TryGetValue(sceneName, out SceneConfig config);

            if (config == null)
                throw new NullReferenceException($"There is no scene ({sceneName}) in the scenes list. The name is wrong or you forget to add it o the list.");

            return Coroutines.Start(LoadSceneRoutine(config, sceneLoadedCallback, loadNewScene));
        }


        protected virtual IEnumerator LoadSceneRoutine(SceneConfig config, UnityAction<SceneConfig> sceneLoadedCallback, bool loadNewScene = true)
        {
            isLoading = true;

            if (loadNewScene)
                yield return Coroutines.Start(LoadSceneAsyncRoutine(config));
            yield return Coroutines.Start(InitializeSceneRoutine(config, sceneLoadedCallback));

            yield return new WaitForSecondsRealtime(1f);
            isLoading = false;
            sceneLoadedCallback?.Invoke(config);

        }

        protected IEnumerator LoadSceneAsyncRoutine(SceneConfig config)
        {
            var asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(config.sceneName);
            asyncOperation.allowSceneActivation = false;

            var progressDivider = 0.9f;
            var progress = asyncOperation.progress / progressDivider;

            while (progress < 1f)
            {
                yield return null;
                progress = asyncOperation.progress / progressDivider;
            }

            asyncOperation.allowSceneActivation = true;
        }
        protected void LoadScene(SceneConfig config)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(config.sceneName);
        }

        protected virtual IEnumerator InitializeSceneRoutine(SceneConfig config, UnityAction<SceneConfig> sceneLoadedCallback)
        {

            sceneActual = new Scene(config);

            sceneActual.OnCreateEvent += OnSceneCreate;
            sceneActual.OnInitializeEvent += OnSceneInitialize;
            sceneActual.OnStartEvent += OnSceneStart;
            yield return null;

            sceneActual.SendMessageOnCreate();
            yield return null;

            yield return sceneActual.BuildUI();
            yield return null;

            yield return sceneActual.InitializeAsync();
            yield return null;

            OnUIInitialized?.Invoke();

            sceneActual.Start();
        }

        public T GetRepository<T>() where T : IRepository
        {
            return sceneActual.GetRepository<T>();
        }

        public T GetInteractor<T>() where T : IInteractor
        {
            return sceneActual.GetInteractor<T>();
        }

        public List<IJsonSerializable> GetSerializableObjects()
        {
            return sceneActual.GetSerializableObjects();
        }
        public bool HaveComponent<T>() where T : IArchitectureComponent
        {
            return sceneActual.HaveComponent<T>();
        }
        private void OnSceneCreate()
        {
         
        }
        private void OnSceneInitialize()
        {
            saveController.OnSceneInitialize();
        }
        private void OnSceneStart()
        {

        }
    }
}