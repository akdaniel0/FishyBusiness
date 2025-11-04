using UnityEngine;
using UnityEngine.UI;


public class SidePanelScript : MonoBehaviour
{
    public GameManagerScript manager;
    public float[] upgradeCosts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<GameManagerScript>();
        
        transform.GetChild(2).GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(doUpgrade1);
        transform.GetChild(3).GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(doUpgrade2);
        transform.GetChild(4).GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(doUpgrade3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // attempt to buy upgrade
    void doUpgrade1() {
        if (manager.money > upgradeCosts[0]) {
            GameObject.Find("FishTankCraneCenter").GetComponent<FishTankCraneScript>().craneAcceleration *= 1.1f;
            GameObject.Find("FishTankCraneCenter").GetComponent<FishTankCraneScript>().craneDrag *= 1.05f;
            GameObject.Find("ConveyorCraneCenter").GetComponent<ConveyorCraneScript>().craneAcceleration *= 1.1f;
            GameObject.Find("ConveyorCraneCenter").GetComponent<ConveyorCraneScript>().craneDrag *= 1.05f; 
            manager.money -= upgradeCosts[0];
        }
    }
    
    // attempt to buy upgrade
    void doUpgrade2() {
        if (manager.money > upgradeCosts[1]) {
            GameObject.Find("FishTankCraneCenter").GetComponent<FishTankCraneScript>().verticalSpeed *= 1.25f;
            manager.money -= upgradeCosts[1];
        }
    }
    
    // attempt to buy upgrade
    void doUpgrade3() {
        if (manager.money > upgradeCosts[2]) {
            GameObject.Find("ConveyorA").GetComponent<ConveyorAScript>().conveyorSpeed *= 0.9f;
            manager.money -= upgradeCosts[2];
        }
    }
}
