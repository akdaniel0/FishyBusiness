using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public float money;
    public float gameStartTime;
    //public SidePanelScript sidepanel;
    

    public float profitIndicatorOpacity; // default 0, reset to 255 on profit earned
    public float fadeSpeed; // opacity change rate
    // TODO: child object/component for profitIndicator text here

    void Start()
    {
        //sidepanel = GameObject.Find("Canvas").GetComponent<SidePanelScript>();
        // define child object/component for profitIndicator
    }

    // Update is called once per frame
    void Update()
    {
        // update display rounded to tenth place
        gameObject.GetComponent<TextMeshProUGUI>().text = ("$" + Mathf.Round(money * 10f) / 10f);

        // set floor for opacity
        if (profitIndicatorOpacity < 0) {
            profitIndicatorOpacity = 0;
        }
        // fade out indicator by decreasing opacity by fadeSpeed
        while (profitIndicatorOpacity > 0) {
            profitIndicatorOpacity -= Time.deltaTime * fadeSpeed;
        }
        
    }
}

