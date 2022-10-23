using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;
using UnityEngine.SceneManagement;

namespace IsakUtils
{
    public class ComponentInjector : Singleton<ComponentInjector>
    {
        private void OnEnable()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += SceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneLoaded;
        }

        public void InjectOnMonoBehavioursOnGameObject(GameObject gameObject)
        {
            MonoBehaviour[] monoBehaviours = gameObject.GetComponentsInChildren<MonoBehaviour>();
            foreach (MonoBehaviour monoBehaviour in monoBehaviours)
            {
                InjectSingleComponentsFromGameObject(monoBehaviour);
                InjectSingleComponentsFromParent(monoBehaviour);
                InjectSingleComponentsFromChildren(monoBehaviour);
                InjectArrayComponentsFromParent(monoBehaviour);
                InjectArrayComponentsFromChildren(monoBehaviour);
            }
        }

        private IEnumerable<FieldInfo> GetFieldsWithAttribute(Type attributeType, MonoBehaviour monoBehaviour)
        {
            var fields = monoBehaviour.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => field.GetCustomAttributes(attributeType, true).FirstOrDefault() != null);

            return fields;
        }

        private FieldInfo[] GetFields(Type type, MonoBehaviour monoBehaviour)
        {
            return GetFieldsWithAttribute(type, monoBehaviour).ToArray();
        }

        private void InjectSingleComponentsFromGameObject(MonoBehaviour monoBehaviour)
        {
            InjectSingleComponents(monoBehaviour, typeof(ComponentAttribute), (type) => monoBehaviour.GetComponent(type));
        }

        private void InjectSingleComponentsFromParent(MonoBehaviour monoBehaviour)
        {
            InjectSingleComponents(monoBehaviour, typeof(ComponentInParentAttribute), (type) => monoBehaviour.GetComponentInParent(type));
        }
        private void InjectSingleComponentsFromChildren(MonoBehaviour monoBehaviour)
        {
            InjectSingleComponents(monoBehaviour, typeof(ComponentInChildrenAttribute), (type) => monoBehaviour.GetComponentInChildren(type));
        }

        private void InjectArrayComponentsFromParent(MonoBehaviour monoBehaviour)
        {
            InjectArrayComponents(monoBehaviour, typeof(ComponentsInParentAttribute), (type) => monoBehaviour.GetComponentsInParent(type));
        }
        private void InjectArrayComponentsFromChildren(MonoBehaviour monoBehaviour)
        {
            InjectArrayComponents(monoBehaviour, typeof(ComponentsInChildrenAttribute), (type) => monoBehaviour.GetComponentsInChildren(type));
        }


        private void InjectSingleComponents(MonoBehaviour monoBehaviour, Type attributeType,  Func<Type, Component> getComponent)
        {
            var fields = GetFields(attributeType, monoBehaviour);
            foreach (var field in fields)
            {
                var type = field.FieldType;
                var component = getComponent.Invoke(type);
                if (component == null)
                {
                    Debug.LogWarning("GetComponent/GetComponentInParent/GetComponentInChildren typeof(" + type.Name + ") on game object '" + monoBehaviour.gameObject.name + "' is null");
                    continue;
                }
                field.SetValue(monoBehaviour, component);
            }
        }

        private void InjectArrayComponents(MonoBehaviour monoBehaviour, Type attributeType, Func<Type, Component[]> getComponent)
        {
            var fields = GetFields(attributeType, monoBehaviour);
            foreach (var field in fields)
            {
                var type = field.FieldType.GetElementType();
                var components = getComponent.Invoke(type);
                var componentsAsCorrectType = Array.CreateInstance(type, components.Length);
                Array.Copy(components, componentsAsCorrectType, components.Length);
                if (components.Length == 0)
                {
                    Debug.LogWarning("GetComponentsInParent/GetComponentsInChildren typeof(" + type.Name + ") on game object '" + monoBehaviour.gameObject.name + "' returns empty list");
                    continue;
                }
                field.SetValue(monoBehaviour, componentsAsCorrectType);
            }
        }

        private void StartInjection(Scene scene)
        {
            var gameObjects = scene.GetRootGameObjects();
            foreach (GameObject gameObject in gameObjects)
            {
                InjectOnMonoBehavioursOnGameObject(gameObject);
            }
        }

        public void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            StartInjection(scene);
        }
    }
}