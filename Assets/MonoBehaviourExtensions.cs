using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Helpers
{
    public static class MonoBehaviourExtensions
    {
        public static T GetSingleton<T>(this MonoBehaviour b) where T : Object
        {
            var objs = GameObject.FindObjectsOfType<T>();
            if (!objs.Any()) throw new InvalidOperationException("No singleton object of type exists: " + typeof(T).Name);
            if (objs.Count() == 2) throw new InvalidOperationException("Multiple objects of given type exists: " + typeof(T).Name);
            return objs.First();
        }
    }
}