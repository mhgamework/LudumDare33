using UnityEngine;
using System.Collections;

public class CursorHandler : MonoBehaviour
{
    [SerializeField]
    private bool CursorVisible = false;
    [SerializeField]
    private bool LockCursor = true;

    void Start()
    {
        
    }

    void Update()
    {
        Cursor.visible = CursorVisible;
        Cursor.lockState = LockCursor ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
