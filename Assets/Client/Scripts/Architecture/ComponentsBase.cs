using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Architecture
{
    public sealed class ComponentsBase<T> where T : IArchitectureComponent
    {

        private Dictionary<Type, T> componentsMap;


        public ComponentsBase(string[] classReferences)
        {
            this.componentsMap = CreateInstances<T>(classReferences);
        }

        private Dictionary<Type, T> CreateInstances<T>(string[] classReferences) where T : IArchitectureComponent
        {
            var createdMap = new Dictionary<Type, T>();

            foreach (var reference in classReferences)
            {
                var type = Type.GetType(reference);
                var result = Activator.CreateInstance(type);
                var resultComponent = (T)result;
                createdMap[type] = resultComponent;
            }

            return createdMap;
        }



        #region MESSAGES

        public void SendMessageOnCreate()
        {
            var allComponents = componentsMap.Values.ToArray();
            foreach (var component in allComponents)
                component.OnCreate();
        }

        public void SendMessageOnInitialize()
        {
            var allComponents = componentsMap.Values.ToArray();
            foreach (var component in allComponents)
                component.OnInitialize();
        }

        public void SendMessageOnStart()
        {
            var allComponents = componentsMap.Values.ToArray();
            foreach (var component in allComponents)
                component.OnStart();
        }

        #endregion



        #region INITIALIZING

        public Coroutine InitializeAllComponents()
        {
            return CustomTools.Coroutines.Start(InitializeAllComponentsRoutine());
        }

        private IEnumerator InitializeAllComponentsRoutine()
        {
            var allComponents = componentsMap.Values.ToArray();
            foreach (var component in allComponents)
            {
                if (!component.isInitialized)
                    yield return component.InitializeWithRoutine();
            }
        }

        #endregion

        public List<T> GetComponents()
        {
            var allComponents = new List<T>(this.componentsMap.Values);

            return allComponents;
        }
        public bool HaveComponent<P>() where P : IArchitectureComponent
        {
            var type = typeof(P);
            var founded = componentsMap.TryGetValue(type, out var resultComponent);

            if (founded)
                return true;

            var allComponents = this.componentsMap.Values;
            foreach (var component in allComponents)
            {
                if (component is P resultComponent2)
                    return true;
            }

            return false;
        }
        public TP GetComponent<TP>() where TP : T
        {
            var type = typeof(TP);
            var founded = componentsMap.TryGetValue(type, out var resultComponent);

            if (founded)
                return (TP)resultComponent;

            var allComponents = this.componentsMap.Values;
            foreach (var component in allComponents)
            {
                if (component is TP resultComponent2)
                    return resultComponent2;
            }

            throw new KeyNotFoundException($"Key: {type}");
        }
    }
}
