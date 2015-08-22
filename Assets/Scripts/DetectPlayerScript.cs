using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DetectPlayerScript : MonoBehaviour
{

    public FirstPersonController Player;
    public GameStateManagerScript GameState;
    public GameObject Eye;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Player.gameObject) return;


        var eye = Eye.transform.position;

        var ray = new Ray(eye, Vector3.Normalize( other.gameObject.transform.position- eye));

        RaycastHit hitInfo;

        if (!Physics.Raycast(ray,out hitInfo)) throw new InvalidOperationException();

        if (hitInfo.collider != other)
            return; // very well hidden sir

        GameState.RestoreCheckpoint();
    }
}
