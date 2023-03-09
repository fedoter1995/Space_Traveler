using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Architecture
{
    public interface IScene
    {

        public event Action OnCreateEvent;
        public event Action OnInitializeEvent;
        public event Action OnStartEvent;
        string Name { get; }
        SceneConfig sceneConfig { get; }
        ComponentsBase<IRepository> repositoriesBase { get; }
        ComponentsBase<IInteractor> interactorsBase { get; }
        ComponentsBase<IUIController> uiControllers { get; }
        EnvironmentSettings DefaultEnvironment { get; }

        void SendMessageOnCreate();
        Coroutine InitializeAsync();
        void Start();
        IEnumerator BuildUI();

        T GetRepository<T>() where T : IRepository;
        List<IJsonSerializable> GetSerializableObjects();
        T GetInteractor<T>() where T : IInteractor;
        T GetUIController<T>() where T : class, IUIController;
        bool HaveComponent<T>() where T : IArchitectureComponent;

    }
}
