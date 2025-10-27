using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public float money;
    public float gameStartTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // update display rounded to tenth place
        gameObject.GetComponent<TMP_Text>().text = ("$" + Mathf.Round(money * 10f) / 10f);
        
    }
}

