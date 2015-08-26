using System;
using UnityEngine;
using System.Collections;
using Assets.Helpers;
using UnityStandardAssets.Characters.FirstPerson;

public class DetectPlayerScript : MonoBehaviour
{

    private FirstPersonController Player { get { return GameState.Player; } }
    public GameStateManagerScript GameState { get { return GameStateManagerScript.Get; } }
    public GameObject Eye;
    public float DamageMultiplier = 1;

    //public float enterTime;
    private Collider countingCollider = null;

    private DamageCanvasControl damageCanvasControl;
    // Use this for initialization
    void Start()
    {
        damageCanvasControl = this.GetSingleton<DamageCanvasControl>();
    }

    public void Awake()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (countingCollider == null) return;
        if (GameState.PlayerScript.IsAlive)
        {
            damageCanvasControl.blinkDamage();

            GameState.Player.GetComponent<PlayerScript>().takeDamage(Time.deltaTime * DamageMultiplier);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter: " + other.name);
        if (other.gameObject != Player.gameObject) return;


        var eye = Eye.transform.position;

        var ray = new Ray(eye, Vector3.Normalize(other.gameObject.transform.position - eye));

        RaycastHit hitInfo;

        if (!Physics.Raycast(ray, out hitInfo)) throw new InvalidOperationException();
        
        if (hitInfo.collider != other)
            return; // very well hidden sir

        countingCollider = other;
        //enterTime = Time.realtimeSinceStartup;
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit: " +other.name);
        if (countingCollider != other) return;
        countingCollider = null;
    }

    public void DisableLight()
    {
        countingCollider = null;

    }

    public void EnableLight()
    {
    }
}
