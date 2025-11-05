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
        int type;
        int rand = Random.Range(1, 5);
        type = FishSpawnerScript.GetFishType(rand, true);
        ordscr.fish = type;
    }

    public GameObject order;
    private float cooldown;
    private float timer;

    GameManagerScript manager;
    public bool isRunning;
    public float cooldownMultiplier;
}
