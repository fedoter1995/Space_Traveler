using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture
{
    public sealed class Scene : IScene
    {
        public string Name => sceneConfig.name;
        public SceneConfig sceneConfig { get; }
        public UI<IUIController> UI { get; }
        public ComponentsBase<IRepository> repositoriesBase { get; }
        public ComponentsBase<IInteractor> interactorsBase { get; }

        public Scene(SceneConfig config)
        {
            sceneConfig = config;
            repositoriesBase = new ComponentsBase<IRepository>(sceneConfig.repositoriesReferences);
            interactorsBase = new ComponentsBase<IInteractor>(sceneConfig.interactorsReferences);
            UI = new UI<IUIController>(sceneConfig.uiControllersReferences);
            
        }



        #region ONCREATE



        public void SendMessageOnCreate()
        {
            repositoriesBase.SendMessageOnCreate();
            interactorsBase.SendMessageOnCreate();
            UI.controllers.SendMessageOnCreate();
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
        }

        #endregion

        #region START

        public void Start()
        {
            repositoriesBase.SendMessageOnStart();
            interactorsBase.SendMessageOnStart();
            UI.controllers.SendMessageOnStart();
        }


        #endregion


        public T GetRepository<T>() where T : IRepository
        {
            return repositoriesBase.GetComponent<T>();
        }

        public T GetInteractor<T>() where T : IInteractor
        {
            return interactorsBase.GetComponent<T>();
        }
        public T GetUIController<T>() where T : class, IUIController
        {
            return UI.controllers.GetComponent<T>();
        }

        public IEnumerator BuildUI()
        {
            yield return UI.controllers.InitializeAllComponents();
            UI.controllers.SendMessageOnInitialize();
        }

        public List<IJsonSerializable> GetSerializableObjects()
        {
            var objectsList = new List<IJsonSerializable>(repositoriesBase.GetComponents());
            objectsList.AddRange(interactorsBase.GetComponents());

            return objectsList;
        }
    }
}

