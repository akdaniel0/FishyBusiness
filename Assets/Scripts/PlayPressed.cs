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
        transform.parent.GetComponent<Canvas>().enabled = false;
    }
}