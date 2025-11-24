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

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }


    public GameObject prompt;
    public Canvas prestigeBackground;
    public Button prestigeButton;

    void Start() {
        // make happen when click
        prestigeBackground = GameObject.Find("PrestigeBackground").GetComponent<Canvas>();
        GameObject.Find("PrestigeButton").GetComponent<Button>().onClick.AddListener(togglePrestigeMenu);
    }

    // when button press, toggle Prestige Menu
    public void togglePrestigeMenu() {
        if (prestigeBackground.enabled) {
            prestigeBackground.enabled = false;
        } else {
            prestigeBackground.enabled = true;
        }
    }

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
    }
}
