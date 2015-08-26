using Assets.Scripts.GameSystems;
using UnityEngine;

namespace Assets.Scripts.GameStates
{
    public class PlayState :  AbstractGameState
    {
        [SerializeField]
        private CrossHairScript CrossHair;
        public override void ActivateState(GameStateScript mgr)
        {
            mgr.ContinueGameSimulation();
            CrossHair.Show();
        }

        public override void DeactivateState(GameStateScript mgr)
        {
            mgr.PauseGameSimulation();
            CrossHair.Hide();
        }
    }
}