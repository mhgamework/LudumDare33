using System;
using System.Collections.Generic;
using Assets.Scripts.GameSystems;
using UnityEngine;

namespace Assets.Scripts.GameStates
{
    public class BurnedGameStateScript :  AbstractGameState
    {

        [SerializeField]
        private UIFader uiFader;

        public float fadeDuration;
        public float stayDuration;
        [SerializeField]
        private PlayState playState;

        [SerializeField]
        private DamageCanvasControl damageCanvas;

        [SerializeField]
        private CheckpointingScript checkpointingScript;

        // Use this for initialization
        void Start()
        {
            uiFader = this.GetComponent<UIFader>();
            uiFader.Fade(0, 0, EasingFunctions.TYPE.Out);
        }

        IEnumerable<YieldInstruction> fade(GameStateScript mgr)
        {
            GetComponent<AudioSource>().Play();

            //Hack
            //EndGameCanvas.gameObject.SetActive(false);
            damageCanvas.ShowDamagePermanent();

            uiFader.Fade(1, fadeDuration, EasingFunctions.TYPE.In);
            yield return new WaitForSeconds(fadeDuration);
            yield return new WaitForSeconds(stayDuration);

            checkpointingScript.RestoreCheckpoint();
            damageCanvas.ResetDamagePermanent();
            mgr.ChangeState(playState);

            uiFader.Fade(0, fadeDuration, EasingFunctions.TYPE.Out);

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void Show()
        {
            throw new NotImplementedException();
            //StartCoroutine(fade().GetEnumerator());

        }

        public void Hide()
        {
            throw new NotImplementedException();
            //uiFader.Fade(0, fadeDuration, EasingFunctions.TYPE.Out);

        }

        public override void ActivateState(GameStateScript mgr)
        {
            StartCoroutine(fade(mgr).GetEnumerator());

        }

        public override void DeactivateState(GameStateScript mgr)
        {
            StopAllCoroutines();
            damageCanvas.ResetDamagePermanent();
            uiFader.Fade(0, 0, EasingFunctions.TYPE.Out);
            GetComponent<AudioSource>().Stop();
        }
    }
}
