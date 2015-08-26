using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Helpers;
using Assets.Scripts.GameStates;
using Assets.Scripts.GameSystems;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private UIFader DistanceWarningUI = null;

    public GameStateManagerScript GameStateManagerScript { get { return GameStateManagerScript.Get; } }
    public float PreyKillDistance = 5;
    public PreyWalkScript Prey { get { return GameStateManagerScript.Prey; } }

    public bool IsAlive { get { return health > 0; } }

    public float defaultWalkingSpeed = 5;
    public float defaultRunningSped = 10;
    public float damageSpeedModifier = 0.5f;
    public float waterSpeedModifier = 0.2f;
    public float slowDuration = 1;

    public float healthRegenRatio;

    public float losingDistance = 100;

    private float currentSlowDuration = 0;

    public PlayerSoundScript SoundScript;

    [SerializeField]
    private List<AudioClip> drySteps = new List<AudioClip>();
    [SerializeField]
    private List<AudioClip> wetSteps = new List<AudioClip>();

    [SerializeField]
    private KillAreaTrigger KillArea = null;

    public float health = 3;
    private bool isInWater;

    public bool Invincible = false;

    private bool PreyHasEnteredKillZone;
    private bool ShowingWarningUi;

    private GameStateScript gameStateScript;
    private BurnedGameStateScript burnedState;

    // Use this for initialization
    void Start()
    {
        gameStateScript = this.GetSingleton<GameStateScript>();
        burnedState = this.GetSingleton<BurnedGameStateScript>();
        if (KillArea != null)
        {
            KillArea.OnKillAreaEntered = new KillAreaTrigger.OnKillAreaEnteredEventHandler();
            KillArea.OnKillAreaEntered.AddListener(() => PreyHasEnteredKillZone = true);
        }

        StartCoroutine("UpdateCheckPreyDistance");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            Invincible = !Invincible;

        //Set movement speed to water movement speed if in water
        waterDebuff();
        //Slow debuff from taking damage, water ignores this.
        slowDebuff();
        //Regen health if out of slow
        healthRegen();
    }

    private void slowDebuff()
    {
        if (currentSlowDuration > 0)
        {
            currentSlowDuration -= Time.deltaTime;
            if (!isInWater)
            {
                GetComponent<FirstPersonController>().m_RunSpeed = defaultRunningSped *
                                                               damageSpeedModifier;
                GetComponent<FirstPersonController>().m_WalkSpeed = defaultWalkingSpeed *
                                                                    damageSpeedModifier;
            }
        }
        else
        {
            if (!isInWater)
            {
                GetComponent<FirstPersonController>().m_RunSpeed = defaultRunningSped;
                GetComponent<FirstPersonController>().m_WalkSpeed = defaultWalkingSpeed;
            }
        }
    }

    private void waterDebuff()
    {
        if (isInWater)
        {
            //print("applying water debuff.");

            GetComponent<FirstPersonController>().m_RunSpeed = defaultRunningSped *
                                                               waterSpeedModifier;
            GetComponent<FirstPersonController>().m_WalkSpeed = defaultWalkingSpeed *
                                                                waterSpeedModifier;
        }
    }

    private void healthRegen()
    {
        if (health < 3 && currentSlowDuration <= 0)
        {
            health += healthRegenRatio * Time.deltaTime;
            if (health > 3)
            {
                health = 3;
            }
        }
    }

    public bool CanKillPrey()
    {
        if (KillArea != null && !PreyHasEnteredKillZone)
            return false;

        return (Prey.transform.position - transform.position).magnitude < PreyKillDistance;
    }

    public void takeDamage(float damage)
    {
        if (Invincible) return;
        health -= damage;
        if (health <= 0)
        {
            // Check death
            this.GetSingleton<GameStateScript>().ChangeState(this.GetSingleton<BurnedGameStateScript>());
            
        }
        currentSlowDuration = slowDuration;
    }

    public void OnWaterEnter()
    {
        isInWater = true;
        GetComponent<FirstPersonController>().m_FootstepSounds = wetSteps.ToArray();
    }

    public void OnWaterExit()
    {
        isInWater = false;
        currentSlowDuration = slowDuration;
        GetComponent<FirstPersonController>().m_FootstepSounds = drySteps.ToArray();
    }

    private IEnumerator UpdateCheckPreyDistance()
    {
        while (true)
        {
            var distance_to_prey = Vector3.Distance(GameStateManagerScript.Prey.transform.position, gameObject.transform.position);

            //Check distance to prey, lose if too far
            if (distance_to_prey > losingDistance && !GameStateManagerScript.isLost)
            {
                StopCoroutine("FlashWarningUi");
                ShowingWarningUi = false;
                if (DistanceWarningUI != null)
                    DistanceWarningUI.Fade(0, 0f);

                this.GetSingleton<GameStateScript>().ChangeState(this.GetSingleton<PreyGotAwayStateScript>());
            }

            if (distance_to_prey > losingDistance * 0.5f)
            {
                if (!ShowingWarningUi)
                {
                    StartCoroutine("FlashWarningUi");
                    ShowingWarningUi = true;
                }
            }
            else
            {
                StopCoroutine("FlashWarningUi");
                ShowingWarningUi = false;
                if (DistanceWarningUI != null)
                    DistanceWarningUI.Fade(0, 0);
            }

            yield return null;
        }
    }

    private IEnumerator FlashWarningUi()
    {
        if (DistanceWarningUI == null)
            yield break;

        float min_flash_duration = 0.1f;
        float max_flash_duration = 2f;

        DistanceWarningUI.Fade(0, 0);
        float to_alpha = 1f;
        while (true)
        {
            var prey_distance = Vector3.Distance(GameStateManagerScript.Prey.transform.position, gameObject.transform.position);
            var flash_duration = Mathf.Lerp(min_flash_duration, max_flash_duration, 1f - prey_distance / losingDistance);

            DistanceWarningUI.Fade(to_alpha, flash_duration);
            yield return new WaitForSeconds(flash_duration);
            to_alpha = to_alpha == 0 ? 1 : 0;
        }
    }
}