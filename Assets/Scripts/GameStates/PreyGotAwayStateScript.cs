using System;
using System.Collections.Generic;
using Assets.Scripts.GameSystems;
using UnityEngine;

namespace Assets.Scripts.GameStates
{
    public class PreyGotAwayStateScript : AbstractGameState
    {
        private UIFader uiFader;

        public float fadeDuration;
        public float stayDuration;


        // Use this for initialization
        private void Start()
        {
            uiFader = this.GetComponent<UIFader>();
            uiFader.Fade(0, 0, EasingFunctions.TYPE.Out);
        }

        private IEnumerable<YieldInstruction> fade(GameStateScript mgr)
        {
            uiFader.Fade(1, fadeDuration, EasingFunctions.TYPE.In);
            yield return new WaitForSeconds(fadeDuration + stayDuration);
            uiFader.Fade(0, fadeDuration, EasingFunctions.TYPE.Out);
            mgr.ChangeState(FindObjectOfType<PlayState>());
        }

        // Update is called once per frame
        private void Update()
        {

        }

        public void triggerLose()
        {
            throw new InvalidOperationException();
            uiFader.Fade(1, fadeDuration, EasingFunctions.TYPE.In);
            //StartCoroutine(fade().GetEnumerator());
        }

        public void Hide()
        {
            throw new InvalidOperationException();
            uiFader.Fade(0, fadeDuration, EasingFunctions.TYPE.Out);
            //throw new System.NotImplementedException();
        }

        public override void ActivateState(GameStateScript mgr)
        {
            StartCoroutine(fade(mgr).GetEnumerator());
        }

        public override void DeactivateState(GameStateScript mgr)
        {
            StopAllCoroutines();
            uiFader.Fade(0, fadeDuration, EasingFunctions.TYPE.Out);
        }
    }
}