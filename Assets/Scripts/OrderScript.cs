using UnityEngine;
using TMPro;

public class OrderScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.quantity_text = base.GetComponentInChildren<TextMeshPro>();
        this.display.sprite = this.sprites[this.fish];
        this.display.color = this.semi;
        this.outside = base.transform.position + new Vector3(15f, 0f);
        this.quantity = Random.Range(1, 6);
        this.maxquant = this.quantity;
        this.UpdateQuant();
    }

    // Update is called once per frame
    void Update()
    {
        base.transform.position = Vector3.MoveTowards(base.transform.position, this.outside, 0.001f);
        if (Vector3.Distance(base.transform.position, this.outside) <= 0.01f)
        {
            // SKYE, CHANGE THIS TO MATCH YOUR GAMEOBJECT AND SCRIPT
            if(this.quantity > 0)
            {
                // GameObject.Find("Manager").GetComponent<GameManagerScript>().money -= (this.Worth() * this.quantity);
            }
            else
            {
                // GameObject.Find("Manager").GetComponent<GameManagerScript>().money += (this.Worth() * this.maxquant);
            }
            Destroy(base.gameObject);
        }
    }

    private void UpdateQuant()
    {
        if (this.quantity > 1)
        {
            this.quantity_text.text = this.quantity.ToString();
        }
        else
        {
            Destroy(this.quantity_text);
        }
    }

    private int Worth()
    {
        if (this.fish >= 9)
        {
            return 20;
        }
        else if (this.fish >= 6)
        {
            return 15;
        }
        else if (this.fish >= 3)
        {
            return 10;
        }
        else
        {
            return 5;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Fish") && collision.transform.parent == null)
        {
            FishAiScript fishscr = collision.GetComponent<FishAiScript>();
            if(this.quantity > 0)
            {
                if (fishscr.type == this.fish)
                {
                    this.quantity--;
                    this.UpdateQuant();
                    if (this.quantity <= 0)
                    {
                        this.display.color = Color.white; // Full opacity
                    }
                }
                else
                {
                    // TODO: Figure this out
                }
            }    
            Destroy(collision.gameObject);
        }
    }

    public int fish;
    public Sprite[] sprites;
    public SpriteRenderer display;
    private TextMeshPro quantity_text;
    private Vector3 outside;
    private int quantity;
    private int maxquant;
    private Color semi = new Color(1, 1, 1, 0.4f);
}
