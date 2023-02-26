using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Architecture
{
    public delegate void SceneManagerHandler(SceneConfig config);

    public interface ISceneManager
    {

        #region EVENTS

        event SceneManagerHandler OnSceneLoadStartedEvent;
        event SceneManagerHandler OnSceneLoadCompletedEvent;
        event Action OnUIInitialized;


        T GetRepository<T>() where T : IRepository;
        T GetInteractor<T>() where T : IInteractor;

        #endregion

        IScene sceneActual { get; }
        Dictionary<string, SceneConfig> scenesConfigMap { get; }
        SaveDataController saveController { get; }
        EnvironmentSettings CurrentEnvironment { get; }
        List<IJsonSerializable> GetSerializableObjects();
        Coroutine LoadScene(string sceneName, UnityAction<SceneConfig> sceneLoadedCallback = null);
        Coroutine InitializeCurrentScene(UnityAction<SceneConfig> sceneLoadedCallback = null);


    }
}
