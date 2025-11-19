using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class FishSpawnerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int rand = Random.Range(1, 8);
        for(int i = 0; i < rand; i++)
        {
            this.QueueFish();
        }
        this.queuecool = Random.Range(30f, 90f);
    }

    public static int GetFishType(int tier, bool order)
    {
        //int com = 0; // Common chance (Fish 1-3)
        //int med = 0; // Medium chance (Fish 4-7)
        float odds;
        int type;
        float mult = 1f;
        float max = 0f;
        // Gold chosen otherwise (Fish 8-10)
        // Type zero yields a pufferfish
        float puffer = 1f / ((1f - GameObject.Find("Water").GetComponent<ScrubScript>().percent) * 8.5f + 1.05f); // (10% - 97%)
        Debug.Log(Mathf.Ceil(puffer * 100f) + "%");
        if (!order && Random.Range(0f, 1f) <= puffer)
        {
            Debug.Log("Pufferfish spawning with " + Mathf.Ceil(puffer * 100f) + "% chance!");
            return 0;
        }
        if(order)
        {
            mult = 0.8f;
        }
        #region Old
        /*
        switch (tier)
        {
            // May be adjusted as needed
            case 1: // Bottom tier
                com = Random.Range(0, Mathf.RoundToInt(5 * mult));
                med = Random.Range(0, Mathf.RoundToInt(3 * mult));
                break;
            case 2:
                com = Random.Range(0, Mathf.RoundToInt(10 * mult));
                med = Random.Range(0, Mathf.RoundToInt(5 * mult));
                break;
            case 3:
                com = Random.Range(0, Mathf.RoundToInt(40 * mult));
                med = Random.Range(0, Mathf.RoundToInt(20 * mult));
                break;
            case 4:
                com = Random.Range(0, Mathf.RoundToInt(80 * mult));
                med = Random.Range(0, Mathf.RoundToInt(40 * mult));
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
        */
        #endregion
        switch (tier)
        {
            // May be adjusted as needed
            case 1: // Bottom tier
                max = 5f;
                break;
            case 2:
                max = 10f;
                break;
            case 3:
                max = 40f;
                break;
            case 4:
                max = 80f;
                break;
        }
        odds = Random.Range(0f, max);
        odds *= mult;
        if (odds <= max / 10f) // gold
        {
            type = Random.Range(8, 11);
        }
        else if(odds <= max / 2.5f) // if medium
        {
            type = Random.Range(4, 8);
        }
        else // if common
        {
            type = Random.Range(1, 4);
        }
        return type;
    }

    private void QueueFish()
    {
        float cooldown;
        int type;
        type = GetFishType(this.tier, false);
        if(type == 0) // pufferfish
        {
            cooldown = Random.Range(30f, 60f);
        }
        else if (type >= 4) // not common
        {
            if(type >= 8) // not medium (gold)
            {
                cooldown = Random.Range(35f, 60f);
                //type = Random.Range(8, 11);
            }
            else // if medium
            {
                cooldown = Random.Range(15f, 40f);
                //type = Random.Range(4, 8);
            }
        }
        else // if common
        {
            cooldown = Random.Range(5f, 20f);
            //type = Random.Range(1, 4);
        }
        this.timers.Add(0f);
        this.cooldowns.Add(cooldown);
        this.types.Add(type);
    }

    public void QueueFish(int type, float cooldown) // Manual overhaul
    {
        this.timers.Add(0f);
        this.cooldowns.Add(cooldown);
        this.types.Add(type);
    }

    void Update()
    {
        for(int i = 0; i < this.timers.Count; i++)
        {
            if (this.timers[i] <= this.cooldowns[i])
            {
                this.timers[i] += Time.deltaTime;
            }
            else
            {
                this.Spawn(i);
                i--;
            }
        }
        if (this.queuer < this.queuecool)
        {
            this.queuer += Time.deltaTime;
        }
        else
        {
            this.QueueFish();
            this.queuer = 0;
            this.queuecool = Random.Range(0f, 30f);
        }
    }

    private void Spawn(int index)
    {
        GameObject fishy = Instantiate(this.fish, base.transform.position, Quaternion.identity);
        FishAiScript fishai = fishy.GetComponent<FishAiScript>();
        switch(this.tier)
        {
            case 1:
                fishai.speed = Random.Range(2f, 7f);
                break;
            case 2:
                fishai.speed = Random.Range(2f, 4f);
                break;
            case 3:
                fishai.speed = Random.Range(0.5f, 3f);
                break;
            case 4:
                fishai.speed = Random.Range(0.5f, 1.5f);
                break;
        }
        fishai.type = this.types[index];
        this.timers.RemoveAt(index);
        this.cooldowns.RemoveAt(index);
        this.types.RemoveAt(index);
    }
    public GameObject fish;
    [SerializeField]
    private List<float> timers;
    [SerializeField]
    private List<float> cooldowns;
    [SerializeField]
    private List<int> types;
    [SerializeField]
    private float queuer;
    [SerializeField]
    private float queuecool;
    public int tier; // 1: Bottom, 2: 2nd to bottom, 3: 2nd to Top, 4: Top.
}
