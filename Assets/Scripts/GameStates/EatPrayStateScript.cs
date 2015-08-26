using System.Collections.Generic;
using Assets.Scripts.GameSystems;
using UnityEngine;

namespace Assets.Scripts.GameStates
{
    [RequireComponent(typeof(AudioSource))]
    public class EatPrayStateScript : AbstractGameState
    {

        [SerializeField]
        private UIFader uiFader;
        [SerializeField]
        private UIFader textFader;

        

        [SerializeField] private PlayState playState;

        public float CreditsDuration;


        public AudioClip MonsterChewing;
        public AudioClip KillTensionBuildup;

        // Use this for initialization
        void Start()
        {
            uiFader = this.GetComponent<UIFader>();
            uiFader.Fade(0, 0, EasingFunctions.TYPE.Out);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowCredits()
        {
            GetComponent<AudioSource>().clip = MonsterChewing;
            GetComponent<AudioSource>().Play();

            //GameStateManager.PlayerScript.SoundScript.PlayMonsterChewing();

            textFader.Fade(1, 2, EasingFunctions.TYPE.In);
        }

        public void Hide()
        {
            uiFader.Fade(0, 4f, EasingFunctions.TYPE.In);
        }

        public void PlayKillSound()
        {
            GetComponent<AudioSource>().clip = KillTensionBuildup;
            GetComponent<AudioSource>().Play();
        }

        public void FadeToBlack()
        {
            textFader.Fade(0, 0, EasingFunctions.TYPE.Out);
            uiFader.Fade(1, 0.3f, EasingFunctions.TYPE.In);
        }

        public override void ActivateState(GameStateScript mgr)
        {
            StartCoroutine(simulate(mgr).GetEnumerator());
        }

        private IEnumerable<YieldInstruction> simulate(GameStateScript mgr)
        {
            FadeToBlack();
            PlayKillSound();
            yield return new WaitForSeconds(18);
            ShowCredits();
            yield return new WaitForSeconds(CreditsDuration);
            Hide();
            yield return new WaitForSeconds(4);
            mgr.ChangeState(playState);
        }

        public override void DeactivateState(GameStateScript mgr)
        {
            StopAllCoroutines();
            Hide();
            GetComponent<AudioSource>().Stop();


        }
    }
}
