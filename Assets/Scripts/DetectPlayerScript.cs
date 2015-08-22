using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class DetectPlayerScript : MonoBehaviour
{

    private FirstPersonController Player { get { return GameState.Player; } }
    public GameStateManagerScript GameState { get { return GameStateManagerScript.Get; } }
    public GameObject Eye;
    public float DamageMultiplier = 1;

    public float enterTime;
    private Collider countingCollider = null;

    // Use this for initialization
    void Start()
    {

    }

    public void Awake()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (countingCollider != null)
            GameState.Player.GetComponent<PlayerScript>().takeDamage(Time.deltaTime * DamageMultiplier);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Player.gameObject) return;


        var eye = Eye.transform.position;

        var ray = new Ray(eye, Vector3.Normalize(other.gameObject.transform.position - eye));

        RaycastHit hitInfo;

        if (!Physics.Raycast(ray, out hitInfo)) throw new InvalidOperationException();

        if (hitInfo.collider != other)
            return; // very well hidden sir

        countingCollider = other;
        enterTime = Time.realtimeSinceStartup;
        GameState.DamageCanvasControl.showDamage();
    }

    void OnTriggerExit(Collider other)
    {
        if (countingCollider != other) return;
        countingCollider = null;
    }

}
