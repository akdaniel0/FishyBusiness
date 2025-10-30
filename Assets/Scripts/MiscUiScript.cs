using UnityEngine;
using UnityEngine.SceneManagement;

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
}
