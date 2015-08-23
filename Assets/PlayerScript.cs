using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

    public GameStateManagerScript GameStateManagerScript;
    public float PreyKillDistance = 5;
    public PreyWalkScript Prey;

    [SerializeField] private AudioSource monsterDeath = null;

    public float health = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
    }

    public void OnWaterEnter()
    {
        
    }

    public void OnWaterExit()
    {
        
    }
}