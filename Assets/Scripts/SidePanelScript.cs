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
        
        transform.GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(doUpgrade1);
        transform.GetChild(5).gameObject.GetComponent<Button>().onClick.AddListener(doUpgrade2);
        transform.GetChild(6).gameObject.GetComponent<Button>().onClick.AddListener(doUpgrade3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // attempt to buy upgrade
    void doUpgrade1() {
        if (manager.money > upgradeCosts[0]) {
            GameObject.Find("FishTankCraneCenter").GetComponent<FishTankCraneScript>().craneAcceleration *= 1.1f;
        }
    }
    
    // attempt to buy upgrade
    void doUpgrade2() {
        if (manager.money > upgradeCosts[1]) {
            GameObject.Find("FishTankCraneCenter").GetComponent<FishTankCraneScript>().verticalSpeed *= 1.25f;
        }
    }
    
    // attempt to buy upgrade
    void doUpgrade3() {
        if (manager.money > upgradeCosts[2]) {
            GameObject.Find("ConveyorA").GetComponent<ConveyorAScript>().conveyorSpeed *= 0.9f;
        }
    }
}
