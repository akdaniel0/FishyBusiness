using UnityEngine;
using System.Collections.Generic;

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

    private void QueueFish()
    {
        int com = 0; // Common chance (Fish 1-3)
        int med = 0; // Medium chance (Fish 4-7)
        float cooldown;
        int type;
        // Gold chosen otherwise (Fish 8-10)
        // Type zero yields a pufferfish
        if(Random.Range(0, 25) == 0)
        {
            this.timers.Add(0f);
            cooldown = Random.Range(30f, 60f);
            this.cooldowns.Add(cooldown);
            this.types.Add(0);
            return;
        }
        switch (this.tier)
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
        if(com == 0) // not common
        {
            if(med == 0) // not medium (gold)
            {
                cooldown = Random.Range(40f, 60f);
                type = Random.Range(8, 11);
            }
            else // if medium
            {
                cooldown = Random.Range(15f, 40f);
                type = Random.Range(4, 8);
            }
        }
        else // if common
        {
            cooldown = Random.Range(5f, 20f);
            type = Random.Range(1, 4);
        }
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
