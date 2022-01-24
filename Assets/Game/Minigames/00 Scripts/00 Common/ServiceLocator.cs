using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.Common
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        private Dictionary<Type, Type> singletons = new Dictionary<Type, Type>();
        private Dictionary<Type, object> singletonInstances = new Dictionary<Type, object>();

        
        static ServiceLocator()
        {
            _instance = new ServiceLocator();
        }

        public static ServiceLocator Instance => _instance;

        public T Resolve<T>(params object[] parameters) where T : class
        {
            var result = default(T);
            if (singletons.TryGetValue(typeof(T), out var concreteType))
            {
                // If we don't have this concrete's instance, create a new one
                if (!singletonInstances.TryGetValue(concreteType, out var r))
                {
                    if (concreteType.IsSubclassOf(typeof(MonoBehaviour)))
                    {
                        var go = new GameObject();
                        r = go.AddComponent(concreteType);
                        go.name = concreteType.ToString() + " (singleton)";
                    }
                    else
                    {
                        r = Activator.CreateInstance(concreteType, parameters);
                    }
                    singletonInstances[concreteType] = r;
                }
                result = (T) r;
            }

            return result;
        }
        
        public void RegisterSingleton<TConcrete>() {
            singletons[typeof(TConcrete)] = typeof(TConcrete);
        }
        
        public void RegisterSingleton<TConcrete>(TConcrete instance)
        {
            singletons[typeof(TConcrete)] = typeof(TConcrete);
            singletonInstances[typeof(TConcrete)] = instance;
        }
        
    }

}


