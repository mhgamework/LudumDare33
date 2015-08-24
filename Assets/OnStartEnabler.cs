using UnityEngine;
using System.Collections;

public class OnStartEnabler : MonoBehaviour
{

    [SerializeField]
    private GameObject ObjectToEnable = null;

    void Start()
    {
        if (ObjectToEnable != null)
            ObjectToEnable.SetActive(true);
    }
}
