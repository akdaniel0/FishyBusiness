using UnityEngine;
using UnityEngine.UI;

public class PlayPressed : MonoBehaviour
{
    private void Start()
    {
        {
            gameObject.GetComponent<Button>().onClick.AddListener(PlayGame);
        }
    }
    public void PlayGame()
    {
        GameObject.Find("Manager").GetComponent<GameManagerScript>().gameStartTime = Time.time;
        transform.parent.GetComponent<Canvas>().enabled = false;
    }
}