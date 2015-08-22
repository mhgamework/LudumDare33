using UnityEngine;
using System.Collections;

public class PlayerInteractionScript : MonoBehaviour
{
    public float RaycastDistance = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F))
        { 
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));

            RaycastHit hitInfo;
            if (!Physics.Raycast(ray, out hitInfo, RaycastDistance)) return;

            var lss = hitInfo.collider.transform.GetComponentInParent<LightSwitchScript>();
            if (lss != null)
            {
                lss.Interact();
            }

        }
	}
}
