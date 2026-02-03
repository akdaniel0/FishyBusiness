using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public float money;
    public float gameStartTime;
    public int totalfish;
    public int fishcaught;

    float profitIndicatorGreen;
    float profitIndicatorRed;
    //public SidePanelScript sidepanel;
    

    public float profitIndicatorOpacity; // default 0, reset to 255 on profit earned
    public float fadeSpeed; // opacity change rate
    private TextMeshProUGUI profitIndicator;
    private GameObject prestigeBackground;
    private TextMeshProUGUI fish_text;
    private TextMeshProUGUI money_need;
    public int prestigeLevel = 0;
    private int fish_needed;
    private float money_needed;


    // when button press, toggle Prestige Menu
    public void TogglePrestigeMenu()
    {
        this.prestigeBackground.SetActive(!this.prestigeBackground.activeSelf);
        if(this.prestigeBackground.activeSelf)
        {
            Button yes = this.prestigeBackground.transform.Find("Yes").GetComponent<Button>();
            yes.interactable = (this.money >= this.money_needed) && (this.fishcaught >= this.fish_needed);
            this.fish_text.text = this.fishcaught + " / " + this.fish_needed;
            this.money_need.text = this.money + " / " + this.money_needed;
        }
    }
    
    public void Prestige() // YOOOOOOOOOOOOOOOOO
    {
        this.prestigeLevel++;
        this.money = 30f;
        this.fish_needed = (this.prestigeLevel * 25) + 10;
        this.money_needed = (this.prestigeLevel * 100f) + 100f;
        MiscUiScript.Restart();
    }

    private void AssignMenus()
    {
        this.prestigeBackground = GameObject.Find("Canvas_game").transform.Find("PrestigeBackground").gameObject;
        this.prestigeBackground.SetActive(true); // Done to let GameObject.Find work
        this.profitIndicator = GameObject.Find("ProfitIndicator").GetComponent<TextMeshProUGUI>();
        this.fish_text = GameObject.Find("Caught_text").GetComponent<TextMeshProUGUI>();
        this.money_need = GameObject.Find("Money_need").GetComponent<TextMeshProUGUI>();
        GameObject.Find("PrestigeButton").GetComponent<Button>().onClick.AddListener(this.TogglePrestigeMenu);
        GameObject.Find("No").GetComponent<Button>().onClick.AddListener(this.TogglePrestigeMenu);
        GameObject.Find("Yes").GetComponent<Button>().onClick.AddListener(this.Prestige);
        this.prestigeBackground.SetActive(false);
    }

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Manager").Length == 1)
        {
            Debug.Log("No destroy!");
            DontDestroyOnLoad(base.gameObject);
        }
        else
        {
            Destroy(base.gameObject);
            return;
        }
        //sidepanel = GameObject.Find("Canvas").GetComponent<SidePanelScript>();
        // define child object/component for profitIndicator
        this.fish_needed = (this.prestigeLevel * 25) + 10;
        this.money_needed = (this.prestigeLevel * 100f) + 100f;
    }

    // Update is called once per frame
    void Update()
    {
        // debug test
        if (Input.GetKeyDown(KeyCode.K)) {
            AddMoney(5);
        }

        // update display rounded to tenth place
        GameObject.Find("Total").GetComponent<TextMeshProUGUI>().text = ("$" + Mathf.Round(money * 10f) / 10f);

        if(this.profitIndicator == null)
        {
            this.AssignMenus();
        }

        // set floor for opacity
        if (profitIndicatorOpacity < 0) {
            profitIndicatorOpacity = 0;
        }
        // fade out indicator by decreasing opacity by fadeSpeed
        if (profitIndicatorOpacity > 0) {
            profitIndicatorOpacity -= Time.deltaTime * fadeSpeed;
        }

        
        // update opacity
        profitIndicator.color = new Color32((byte)profitIndicatorRed, (byte)profitIndicatorGreen, (byte)profitIndicator.color.b, (byte)profitIndicatorOpacity);
    }

    // method for adding profit instead of changing variable, which will set profitIndicator's opacity to 255 and correct it's color
    public void AddMoney(float moneyAdded)
    {
        if(moneyAdded > 0f)
        {
            // If money is being added, multiply it by the multiplier (10%)
            moneyAdded *= Mathf.Pow(1.1f, this.prestigeLevel);
        }
        money += moneyAdded;
        profitIndicator.text = moneyAdded.ToString();
        profitIndicatorOpacity = 255;
        
        if (moneyAdded >= 0) { // green (positive)
            profitIndicator.color = new Color32(0, 255, 0, 255);
            profitIndicatorGreen = 255;
            profitIndicatorRed = 0;
        } else { // red
            profitIndicator.color = new Color32(255, 0, 0, 255);
            profitIndicatorRed = 255;
            profitIndicatorGreen = 0;
        }
    }

}

