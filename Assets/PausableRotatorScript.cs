using UnityEngine;
using System.Collections;
using Assets.Scripts.GameSystems;

public class PausableRotatorScript : MonoBehaviour, IPausable
{

    private bool rotating = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
            transform.Rotate(new Vector3(0, Time.deltaTime * 180, 0));
    }

    public void Pause()
    {
        rotating = false;
    }

    public void Unpause()
    {
        rotating = true;
    }
}
