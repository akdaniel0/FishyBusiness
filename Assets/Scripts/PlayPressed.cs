using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayPressed : MonoBehaviour
{
    private void Start()
    {
        {
            //Time.timeScale = 0f;
            gameObject.GetComponent<Button>().onClick.AddListener(PlayGame);
            transform.parent.GetComponent<Canvas>().enabled = true;
        }   
    }
    public void PlayGame()
    {
        GameObject.Find("Manager").GetComponent<GameManagerScript>().gameStartTime = Time.time;
        transform.parent.GetComponent<Canvas>().enabled = false;
       // Time.timeScale = 1f;
    }
}