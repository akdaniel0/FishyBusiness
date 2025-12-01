using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class MiscUiScript : MonoBehaviour
{ 
    public void TogglePrompt(bool status)
    {
        this.prompt.SetActive(status);
        if(status)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public static void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }


    public GameObject prompt;

    void Update() {
        // open/close pause menu with esc
        if (Input.GetKeyDown(KeyCode.Escape)) {
            // check if menu and make it not what it is
            if (Time.timeScale == 0f) {
                TogglePrompt(false);
            } else {
                TogglePrompt(true);
            }
        }

        if(Input.GetKey(KeyCode.B))
        {
            Time.timeScale = 4f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
