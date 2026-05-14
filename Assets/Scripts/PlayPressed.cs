using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayPressed : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(PlayGame);
        transform.parent.GetComponentInParent<Canvas>().enabled = true;
        if(FindAnyObjectByType<GameManagerScript>().prestigeLevel > 0)
        {
            GameObject.Find("PlayTxt").GetComponent<TextMeshProUGUI>().text = "Prestige Onwards!";
        }
    }
    public void PlayGame()
    {
        GameObject.Find("Manager").GetComponentInParent<GameManagerScript>().gameStartTime = Time.time;
        transform.parent.GetComponentInParent<Canvas>().enabled = false;
    }
}