using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InputCaptor : MonoBehaviour
{
    public bool HasFocus()
    {
        if (EventSystem.current.IsPointerOverGameObject() || (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
        {
            //print(EventSystem.current.currentSelectedGameObject);
            return EventSystem.current.currentSelectedGameObject == gameObject;
        }
        return false;
    }
}
