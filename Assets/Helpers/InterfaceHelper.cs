using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameSystems;
using UnityEngine;

namespace Assets.Helpers
{
    public class InterfaceHelper
    {
        public static IEnumerable<T> GetAll<T>()
        {
            return Object.FindObjectsOfType(typeof (MonoBehaviour)).OfType<T>();
        }
    }
}