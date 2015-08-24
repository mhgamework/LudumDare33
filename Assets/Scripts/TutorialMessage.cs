using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class TutorialMessage : MonoBehaviour
{
    private BoxCollider collider;

    [SerializeField]
    private float ShowTime = 3f;
    [SerializeField]
    private UIFader TutorialMessageToShow = null;

    private bool tutorialFired;
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
        while (!GameStateManager.SimulationEnabled)
        {
            yield return null;
        }

        TutorialMessageToShow.Fade(1, 0.5f, EasingFunctions.TYPE.In);
        yield return new WaitForSeconds(ShowTime);
        TutorialMessageToShow.Fade(0, 0.5f, EasingFunctions.TYPE.Out);
    }
}
