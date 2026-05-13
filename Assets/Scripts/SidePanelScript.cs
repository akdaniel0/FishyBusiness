using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SidePanelScript : MonoBehaviour
{
    public GameManagerScript manager;
    public float[] upgradeCosts;
    public bool isUpgrade1;
    public bool isUpgrade2;
    public bool isUpgrade3;

    [Header("Attributes")]
    public float craneAccel = 1.1f;
    public float _craneDrag = 0.9f;
    public float conveyorSpeed = 0.9f;
    public float verticalCraneSpeed = 1.25f;

    public Color32 baseTextColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isUpgrade1 = false;
        isUpgrade2 = false;
        isUpgrade3 = false;

        manager = GameObject.Find("Manager").GetComponent<GameManagerScript>();
        
        transform.GetChild(4).GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(doUpgrade1);
        transform.GetChild(3).GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(doUpgrade2);
        transform.GetChild(2).GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(doUpgrade3);

        transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().overrideColorTags = true;
    }

    // Update is called once per frame
    void Update()
    {
        // update top button text color to red if cannot afford that upgrade
        if (manager.money <= upgradeCosts[0]) {
            transform.GetChild(4).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 0, 0, 255);
        } else {
            transform.GetChild(4).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
        // color update for middle button
        if (manager.money <= upgradeCosts[1]) {
            transform.GetChild(3).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 0, 0, 255);
        } else {
            transform.GetChild(3).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
        // color update for bottom button
        if (manager.money <= upgradeCosts[2]) {
            transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 0, 0, 255);
        } else {
            transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
    }

    // attempt to buy upgrade
    public void doUpgrade1() {
        if (manager.money >= upgradeCosts[2]) {
            isUpgrade1 = true;
            float upgrade1 = GameObject.Find("ConveyorA").GetComponent<ConveyorAScript>().conveyorSpeed *= conveyorSpeed;
            manager.AddMoney(-1f * upgradeCosts[2]);
        }
        isUpgrade1 = false;
    }
    
    // attempt to buy upgrade
    public void doUpgrade2() {
        if (manager.money >= upgradeCosts[1]) {
            isUpgrade2 = true;
            float upgrade2 = GameObject.Find("FishTankCraneCenter").GetComponent<FishTankCraneScript>().verticalSpeed *= verticalCraneSpeed;
            manager.AddMoney(-1f * upgradeCosts[1]);
        }
        isUpgrade2 = false;
    }
    
    // attempt to buy upgrade
    public void doUpgrade3() {
        if (manager.money >= upgradeCosts[0]) {
            isUpgrade3 = true;
            GameObject.Find("FishTankCraneCenter").GetComponent<FishTankCraneScript>().craneAcceleration *= 1.1f;
            GameObject.Find("FishTankCraneCenter").GetComponent<FishTankCraneScript>().craneDrag *= _craneDrag;
            GameObject.Find("ConveyorCraneCenter").GetComponent<ConveyorCraneScript>().craneAcceleration *= 1.1f;
            GameObject.Find("ConveyorCraneCenter").GetComponent<ConveyorCraneScript>().craneDrag *= 1.05f; 
            manager.AddMoney(-1f * upgradeCosts[0]);
        }
        isUpgrade3 = false;
    }
}
