using UnityEngine;
using System.Collections;
using Assets.Scripts.GameSystems;

[RequireComponent(typeof(BoxCollider))]
public class TutorialMessage : MonoBehaviour,IPausable
{
    private BoxCollider collider;

    [SerializeField]
    private float ShowTime = 3f;
    [SerializeField]
    private UIFader TutorialMessageToShow = null;

    private bool tutorialFired;
    private bool simulationEnabled = false;
    private GameStateManagerScript GameStateManager { get { return GameStateManagerScript.Get; } }

    void Start()
    {
        collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != GameStateManager.Player.gameObject || tutorialFired)
            return;

        tutorialFired = true;
        StartCoroutine("ShowTutorial");
    }

    IEnumerator ShowTutorial()
    {
        while (!simulationEnabled)
        {
            yield return null;
        }

        TutorialMessageToShow.Fade(1, 0.5f, EasingFunctions.TYPE.In);
        yield return new WaitForSeconds(ShowTime);
        TutorialMessageToShow.Fade(0, 0.5f, EasingFunctions.TYPE.Out);
    }

    public void Pause()
    {
        simulationEnabled = false;
    }

    public void Unpause()
    {
        simulationEnabled = true;
    }
}
