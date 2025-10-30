using UnityEngine;

public class OrderSpawnScript : MonoBehaviour
{

    void Start()
    {
        this.cooldown = 10f;
        manager = GameObject.Find("Manager").GetComponent<GameManagerScript>();
    }


    void Update()
    {
        // if started, mark running
        if (manager.gameStartTime != 0) {
            isRunning = true;
        }

        // if running, run
        if (isRunning) {
            this.timer += Time.deltaTime;
            if (this.timer >= this.cooldown)
            {
                this.GenerateOrder();
                this.ResetCooldown();
            }
        }
    }

    private void ResetCooldown()
    {
        // Minutes
        int elapsed = Mathf.RoundToInt(Time.time / 60f);
        float newcool;
        if(elapsed >= 10)
        {
            newcool = Random.Range(20f, 90f);
        }
        else if (elapsed >= 7)
        {
            newcool = Random.Range(30f, 120f);
        }
        else if (elapsed >= 4)
        {
            newcool = Random.Range(60f, 120f);
        }
        else
        {
            newcool = Random.Range(40f, 120f);
        }
        this.cooldown = newcool * cooldownMultiplier;
        this.timer = 0f;
    }    

    private void GenerateOrder()
    {
        GameObject order = Instantiate(this.order, new Vector3(-9.4f, -4.485f), Quaternion.identity);
        OrderScript ordscr = order.GetComponent<OrderScript>();
        int com = 0; // Common chance (Fish 1-3)
        int med = 0; // Medium chance (Fish 4-7)
        int type;
        int rand = Random.Range(1, 5);
        switch (rand)
        {
            // May be adjusted as needed
            case 1: // Bottom tier
                com = Random.Range(0, 5);
                med = Random.Range(0, 3);
                break;
            case 2:
                com = Random.Range(0, 40);
                med = Random.Range(0, 15);
                break;
            case 3:
                com = Random.Range(0, 80);
                med = Random.Range(0, 25);
                break;
            case 4:
                com = Random.Range(0, 160);
                med = Random.Range(0, 40);
                break;
        }
        if (com == 0) // not common
        {
            if (med == 0) // not medium (gold)
            {
                type = Random.Range(8, 11);
            }
            else // if medium
            {
                type = Random.Range(4, 8);
            }
        }
        else // if common
        {
            type = Random.Range(1, 4);
        }
        ordscr.fish = type;
    }

    public GameObject order;
    private float cooldown;
    private float timer;

    GameManagerScript manager;
    public bool isRunning;
    public float cooldownMultiplier;
}
