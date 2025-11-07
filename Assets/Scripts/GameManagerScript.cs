using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public float money;
    public float gameStartTime;
    //public SidePanelScript sidepanel;
    

    public float profitIndicatorOpacity; // default 0, reset to 255 on profit earned
    public float fadeSpeed; // opacity change rate
    TextMeshProUGUI profitIndicator;

    void Start()
    {
        //sidepanel = GameObject.Find("Canvas").GetComponent<SidePanelScript>();
        // define child object/component for profitIndicator
        profitIndicator = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
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

        // update opacity
        //profitIndicator.color = new Color32(profitIndicator.color.r, profitIndicator.color.g, profitIndicator.color.b, profitIndicatorOpacity);
        
    }

    // method for adding profit instead of changing variable, which will set profitIndicator's opacity to 255 and correct it's color
    public void AddMoney(float moneyAdded) {
        money += moneyAdded;
        profitIndicatorOpacity = 255;
        
        if (moneyAdded >= 0) { // green (positive)
            //profitIndicator.color = new Color32(0, 255, 0, (byte)profitIndicatorOpacity); // fix typecast
        } else { // red
            //profitIndicator.color = new Color32(255, 0, 0, (byte)profitIndicatorOpacity); // fix typecast
        }
    }

}

