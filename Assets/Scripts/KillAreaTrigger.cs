using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class KillAreaTrigger : MonoBehaviour
{
    public class OnKillAreaEnteredEventHandler : UnityEvent { }
    public OnKillAreaEnteredEventHandler OnKillAreaEntered;

    private BoxCollider collider;

    [SerializeField]
    private BoxCollider PreyCollider = null;

    [SerializeField]
    private AudioSource DinnerTimeAudioSource = null;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other != PreyCollider)
            return;

        //print("KillArea entered");

        PlayDinnerTimeSound();

        if (OnKillAreaEntered != null)
            OnKillAreaEntered.Invoke();
    }

    private void PlayDinnerTimeSound()
    {
        if (DinnerTimeAudioSource == null)
            return;

        DinnerTimeAudioSource.Play();
    }

}
