using UnityEngine;

namespace Assets.Scripts.GameSystems
{
    public abstract class AbstractGameState : MonoBehaviour
    {
        public abstract void ActivateState(GameStateScript mgr);
        public abstract void DeactivateState(GameStateScript mgr);

    }
}