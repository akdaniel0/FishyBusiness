using UnityEngine;
using UnityEngine.SceneManagement;

public class MiscUiScript : MonoBehaviour
{ 
    // Toggles pause menu
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
        Time.timeScale = 1f;
    }


    public GameObject prompt;

    void Update() {
        // open/close pause menu with esc
        if (Input.GetKeyDown(KeyCode.Escape) && !GameObject.Find("Canvas").GetComponent<Canvas>().enabled) {
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
            this.fast_forward = true;
            this.ff_obj.SetActive(true);

        }
        else if(this.fast_forward)
        {
            Time.timeScale = 1f;
            this.fast_forward = false;
            this.ff_obj.SetActive(false);
        }
    }

    private bool fast_forward;
    private GameObject ff_obj => GameObject.Find("Canvas_game").transform.Find("Fast Forward").gameObject;
}
