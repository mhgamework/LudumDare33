using System.Collections.Generic;
using Assets.Scripts.GameSystems;
using UnityEngine;

namespace Assets.Scripts.GameStates
{
    public class StartGameStateScript : AbstractGameState
    {
        [SerializeField]
        private UIFader uiFader;

        public UIFader textFader;



        public GameStateManagerScript GameStateManager { get { return GameStateManagerScript.Get; } }

        [SerializeField]
        private PlayState PlayState;

        // Use this for initialization
        void Start()
        {
            Hide();
        }

        private void Hide()
        {
            uiFader.Fade(0, 0f, EasingFunctions.TYPE.Out);
            textFader.Fade(0, 0f, EasingFunctions.TYPE.Out);
        }

        IEnumerable<YieldInstruction> simulate(GameStateScript mgr)
        {
            uiFader.Fade(1, 0f, EasingFunctions.TYPE.Out);
            textFader.Fade(0, 0f, EasingFunctions.TYPE.Out);

            textFader.Fade(1, 1, EasingFunctions.TYPE.In);
            yield return new WaitForSeconds(1);
            while (!Input.GetKey(KeyCode.S)) yield return null;
            uiFader.Fade(0, 3f, EasingFunctions.TYPE.Out);
            yield return new WaitForSeconds(3);

            mgr.ChangeState(PlayState);
            //GameStateManager.PlayerScript.SoundScript.PlayDinnerTime();

        }

        public void Show()
        {
            //if (isVisible) return;
            //uiFader.Fade(1, 0f, EasingFunctions.TYPE.Out);
            //textFader.Fade(0, 0f, EasingFunctions.TYPE.Out);

            //isVisible = true;
            //StartCoroutine(simulate().GetEnumerator());
        }

        public override void ActivateState(GameStateScript mgr)
        {
            StartCoroutine(simulate(mgr).GetEnumerator());
        }

        public override void DeactivateState(GameStateScript mgr)
        {
            StopAllCoroutines();
            uiFader.Fade(0, 3f, EasingFunctions.TYPE.Out);
        }
    }
}
