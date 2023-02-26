using System;
using System.Collections;
using System.Collections.Generic;
using CustomTools;


namespace Architecture
{

    public abstract class Game
    {

        #region EVENTS

        public static event Action OnGameInitializedEvent;

        #endregion


        public static ArchitectureComponentState state { get; private set; } = ArchitectureComponentState.NotInitialized;
        public static bool isInitialized => state == ArchitectureComponentState.Initialized;
        public static ISceneManager sceneManager { get; private set; }
        public static IGameSettings gameSettings { get; private set; }
        public static SaveDataController saveController => sceneManager.saveController;
        public static EnvironmentSettings CurrentEnvironment => sceneManager.CurrentEnvironment;

        #region GAME RUNNING

        public static void Run()
        {
            Coroutines.Start(RunGameRoutine());
        }

        private static IEnumerator RunGameRoutine()
        {

            state = ArchitectureComponentState.Initializing;
            yield return null;

            InitSceneManager();
            yield return null;

            yield return sceneManager.InitializeCurrentScene();

            state = ArchitectureComponentState.Initialized;
            OnGameInitializedEvent?.Invoke();
        }

        private static void InitSceneManager()
        {
            sceneManager = new SceneManager();
            
        }

        #endregion

        public static List<IJsonSerializable> GetSerializableObjects()
        {
            return sceneManager.GetSerializableObjects();
        }
        public static T GetInteractor<T>() where T : IInteractor
        {
            return sceneManager.sceneActual.GetInteractor<T>();
        }
        public static T GetRepository<T>() where T : IRepository
        {
            return sceneManager.sceneActual.GetRepository<T>();
        }
        public static T GetUIController<T>() where T : class, IUIController
        {
            return sceneManager.sceneActual.GetUIController<T>();
        }
        public static bool HaveComponent<T>() where T : IArchitectureComponent
        {
            return sceneManager.sceneActual.HaveComponent<T>();
        }
    }
}

