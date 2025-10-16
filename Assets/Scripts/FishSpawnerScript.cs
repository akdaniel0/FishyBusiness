using UnityEngine;

public class FishSpawnerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.cooldown = Random.Range(0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.timer <= this.cooldown)
        {
            this.timer += Time.deltaTime;
        }
        else
        {
            this.Spawn();
        }
    }

    private void Spawn()
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
        
        this.timer = 0f;
        this.cooldown = Random.Range(5f, 20f);
    }
    public GameObject fish;
    private float timer;
    private float cooldown;
    public int tier;
}
