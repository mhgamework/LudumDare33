using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EscapeQuitter : MonoBehaviour
{
    [SerializeField]
    private bool GoToLevelSelect;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
    }

    public void QuitGame()
    {
        if (GoToLevelSelect)
            Application.LoadLevel("TitleScene");
        else
            Application.Quit();
    }
}
