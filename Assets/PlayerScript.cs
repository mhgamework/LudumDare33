using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour
{

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

    public float health = 3;
    private bool isInWater;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Check distance to prey, lose if too far
        distanceToPrey();
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
            print("applying water debuff.");

            GetComponent<FirstPersonController>().m_RunSpeed = defaultRunningSped*
                                                               waterSpeedModifier;
            GetComponent<FirstPersonController>().m_WalkSpeed = defaultWalkingSpeed*
                                                                waterSpeedModifier;
        }
    }

    private void healthRegen()
    {
        if (health < 3 && currentSlowDuration <= 0)
        {
            health += healthRegenRatio*Time.deltaTime;
            if (health > 3)
            {
                health = 3;
            }
        }
    }

    private void distanceToPrey()
    {
        if (Vector3.Distance(GameStateManagerScript.Prey.transform.position, gameObject.transform.position) > losingDistance &&
            !GameStateManagerScript.isLost)
        {
            GameStateManagerScript.loseGame();
        }
    }

    public bool CanKillPrey()
    {
        return (Prey.transform.position - transform.position).magnitude < PreyKillDistance;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // Check death
            GameStateManagerScript.PlayerDeath();
        }
        currentSlowDuration = slowDuration;
    }

    public void OnWaterEnter()
    {
        isInWater = true;
    }

    public void OnWaterExit()
    {
        isInWater = false;
        currentSlowDuration = slowDuration;
    }
}