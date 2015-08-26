using UnityEngine;
using System.Collections;
using Assets.Scripts.GameStates;
using Assets.Scripts.GameSystems;

public class GameStateTesterScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Alpha1))
	        FindObjectOfType<GameStateScript>().ChangeState(FindObjectOfType<StartGameStateScript>());
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            FindObjectOfType<GameStateScript>().ChangeState(FindObjectOfType<BurnedGameStateScript>());
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            FindObjectOfType<GameStateScript>().ChangeState(FindObjectOfType<EatPrayStateScript>());
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            FindObjectOfType<GameStateScript>().ChangeState(FindObjectOfType<PreyGotAwayStateScript>());
        else if (Input.GetKeyDown(KeyCode.Alpha0))
            FindObjectOfType<GameStateScript>().ChangeState(FindObjectOfType<PlayState>());
	}
}

