using System;
using System.Linq;
using Assets.Helpers;
using UnityEngine;

namespace Assets.Scripts.GameSystems
{
    public class CheckpointingScript : MonoBehaviour
    {
        private static CheckpointingScript instance;
        public static CheckpointingScript Get
        {
            get
            {
                if (instance == null)
                {
                    var obj = new GameObject();
                    obj.AddComponent<CheckpointingScript>();
                    instance = obj.GetComponent<CheckpointingScript>();
                }
                return instance;
            }
        }

        public void Start()
        {
            if (instance != null && instance != this) throw new InvalidOperationException();
            instance = this;
        }

        public void MakeCheckpoint()
        {
            foreach (var obj in InterfaceHelper.GetAll<ICheckpointable>())
            {
                obj.SaveCheckpoint();
            }
        }

        public void RestoreCheckpoint()
        {
            foreach (var obj in InterfaceHelper.GetAll<ICheckpointable>())
            {
                obj.RestoreCheckpoint();
            }

        }
    }
}