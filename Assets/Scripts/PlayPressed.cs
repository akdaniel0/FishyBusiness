using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayPressed : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}