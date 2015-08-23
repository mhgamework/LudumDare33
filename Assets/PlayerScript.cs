using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour
{

    public GameStateManagerScript GameStateManagerScript { get { return GameStateManagerScript.Get; } }
    public float PreyKillDistance = 5;
    public PreyWalkScript Prey { get { return GameStateManagerScript.Prey; } }

    public float defaultWalkingSpeed = 5;
    public float defaultRunningSped = 10;
    public float damageSpeedModifier = 0.5f;
    public float slowDuration = 1;

    public float losingDistance = 100;

    private float currentSlowDuration = 0;

    [SerializeField] private AudioSource monsterDeath = null;

    public float health = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Vector3.Distance(GameStateManagerScript.Prey.transform.position, gameObject.transform.position) > losingDistance && !GameStateManagerScript.isLost)
	    {
	        GameStateManagerScript.loseGame();
	    }
	    if (currentSlowDuration > 0)
	    {
	        currentSlowDuration -= Time.deltaTime;
            GetComponent<FirstPersonController>().m_RunSpeed = defaultRunningSped *
	                                                           damageSpeedModifier;
            GetComponent<FirstPersonController>().m_WalkSpeed = defaultWalkingSpeed *
	                                                            damageSpeedModifier;
	    }
	    else
	    {
            GetComponent<FirstPersonController>().m_RunSpeed = defaultRunningSped;
            GetComponent<FirstPersonController>().m_WalkSpeed = defaultWalkingSpeed;
	    }
	}

    public bool CanKillPrey()
    {
        return (Prey.transform.position - transform.position).magnitude < PreyKillDistance;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        //Debug.Log(health);
        if (health <= 0)
        {
            monsterDeath.Play();
            GameStateManagerScript.PlayerDeath();
        }
        currentSlowDuration = slowDuration;
    }

    public void OnWaterEnter()
    {

    }

    public void OnWaterExit()
    {

    }
}