using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture
{
    public sealed class Scene : IScene
    {

        public event Action OnCreateEvent;
        public event Action OnInitializeEvent;
        public event Action OnStartEvent;
        public string Name => sceneConfig.name;
        public SceneConfig sceneConfig { get; }
        public ComponentsBase<IUIController> uiControllers { get; }
        public ComponentsBase<IRepository> repositoriesBase { get; }
        public ComponentsBase<IInteractor> interactorsBase { get; }

        public EnvironmentSettings DefaultEnvironment => sceneConfig.environmentSettings;

        public Scene(SceneConfig config)
        {
            sceneConfig = config;
            repositoriesBase = new ComponentsBase<IRepository>(sceneConfig.repositoriesReferences);
            interactorsBase = new ComponentsBase<IInteractor>(sceneConfig.interactorsReferences);
            uiControllers = new ComponentsBase<IUIController>(sceneConfig.uiControllersReferences);

        }



        #region ONCREATE



        public void SendMessageOnCreate()
        {
            repositoriesBase.SendMessageOnCreate();
            interactorsBase.SendMessageOnCreate();
            uiControllers.SendMessageOnCreate();
            OnStartEvent?.Invoke();
        }

        #endregion


        #region INITIALIZE

        public Coroutine InitializeAsync()
        {
            return CustomTools.Coroutines.Start(InitializeAsyncRoutine());
        }

        private IEnumerator InitializeAsyncRoutine()
        {
            yield return repositoriesBase.InitializeAllComponents();
            yield return interactorsBase.InitializeAllComponents();
            repositoriesBase.SendMessageOnInitialize();
            interactorsBase.SendMessageOnInitialize();
            OnInitializeEvent?.Invoke();
        }

        #endregion

        #region START

        public void Start()
        {
            repositoriesBase.SendMessageOnStart();
            interactorsBase.SendMessageOnStart();
            uiControllers.SendMessageOnStart();
            OnStartEvent?.Invoke();
        }


        #endregion


        public T GetRepository<T>() where T : IRepository
        {
            return repositoriesBase.GetComponent<T>();
        }
        public bool HaveComponent<T>() where T : IArchitectureComponent
        {
            var ui = uiControllers.HaveComponent<T>();
            var interactors = interactorsBase.HaveComponent<T>();
            var repositories = repositoriesBase.HaveComponent<T>();

            if (ui || interactors || repositories)
                return true;

            return false;
        }
        public T GetInteractor<T>() where T : IInteractor
        {
            return interactorsBase.GetComponent<T>();
        }
        public T GetUIController<T>() where T : class, IUIController
        {
            return uiControllers.GetComponent<T>();
        }

        public IEnumerator BuildUI()
        {
            yield return uiControllers.InitializeAllComponents();
            uiControllers.SendMessageOnInitialize();
        }

        public List<IJsonSerializable> GetSerializableObjects()
        {
            var objectsList = new List<IJsonSerializable>(repositoriesBase.GetComponents());
            objectsList.AddRange(interactorsBase.GetComponents());

            return objectsList;
        }
    }
}

