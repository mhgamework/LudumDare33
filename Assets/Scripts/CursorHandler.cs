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
        Cursor.visible = CursorVisible;
        Cursor.lockState = LockCursor ? CursorLockMode.Locked : CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
