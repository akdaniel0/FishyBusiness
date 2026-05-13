using Unity.Properties;
using UnityEngine;

public class PrestigeScript : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] private float currentMultiplier = 1f;
    [SerializeField] private float addMulti = .25f;
    [SerializeField] private float moneyCon = 100f;
    [SerializeField] private float moneyConADD = 50f;
    [SerializeField] private bool moneyConBOOL;
    [SerializeField] private int orderCon = 15;
    [SerializeField] private int orderConADD = 5;
    [SerializeField] private bool orderConBOOL;


    [Header("Stuff")]
    [SerializeField] private GameObject Y_button;
    [SerializeField] private bool completeCon;

    [Header("References")]
    [SerializeField] private GameManagerScript gameManagerScript;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        completeCon = false;
        orderConBOOL = false;
        moneyConBOOL = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.money >= moneyCon)
            moneyConBOOL = true;

        if (orderConBOOL)
            orderConBOOL = true;


        if (moneyConBOOL && orderConBOOL)
        {
            completeCon = true; 
        }

    }

    public void MoneyMulti()
    {   
        gameManagerScript.money = 30;   
        currentMultiplier += addMulti;
        completeCon = false;
        orderConBOOL = false;
        moneyConBOOL = false;
    }

    public void Go()
    {
        if (completeCon)
            MoneyMulti();

    }
}
