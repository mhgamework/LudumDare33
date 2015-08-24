using UnityEngine;
using System.Collections;

public class EscapeQuitter : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
