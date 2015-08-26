using System;
using System.Linq;
using Assets.Helpers;
using UnityEngine;

namespace Assets.Scripts.GameSystems
{
    public class GameStateScript : MonoBehaviour
    {
        private static GameStateScript instance;
        public static GameStateScript Get
        {
            get
            {
                if (instance == null)
                {
                    var obj = new GameObject();
                    obj.AddComponent<GameStateScript>();
                    instance = obj.GetComponent<GameStateScript>();
                }
                return instance;
            }
        }


        public AbstractGameState StartState;

        private AbstractGameState activeState;

        public void Start()
        {
            if (instance != null && instance != this) throw new InvalidOperationException();
            instance = this;
            
        }

        public void Update()
        {
            if (activeState == null)
            {
                PauseGameSimulation(); // Reset!
                activeState = StartState;
                StartState.ActivateState(this);
            }
        }
        
        public void ChangeState(AbstractGameState state)
        {
            if(state == null) throw new InvalidOperationException("State can't be null");
            if (activeState != null)
                activeState.DeactivateState(this);
            state.ActivateState(this);
            activeState = state;
        }


        public void PauseGameSimulation()
        {
            foreach (var obj in InterfaceHelper.GetAll<IPausable>()) obj.Pause();
        }

        public void ContinueGameSimulation()
        {
            foreach (var obj in InterfaceHelper.GetAll<IPausable>()) obj.Unpause();
        }



    }
}