using UnityEngine;
using TMPro;

public class OrderScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.quantity_text = base.GetComponentInChildren<TextMeshPro>();
        if(this.fish < this.sprites.Length)
        {
            this.display.sprite = this.sprites[this.fish];
        }
        this.display.color = this.semi;
        this.outside = base.transform.position + new Vector3(15f, 0f);
        this.quantity = Random.Range(1, 6);
        this.maxquant = this.quantity;
        this.UpdateQuant();
        this.holdpos = base.transform.Find("StackPos").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //base.transform.position = Vector3.MoveTowards(base.transform.position, this.outside, 0.001f);
        base.transform.position = new Vector3(base.transform.position.x + (moveSpeed * Time.deltaTime), base.transform.position.y, base.transform.position.z);
        
        //if (Vector3.Distance(base.transform.position, this.outside) <= 0.01f)
        // check if passed end without regard for exact y and z position
        if (transform.position.x > 6)
        {
            // change moneys
            if(this.quantity > 0)
            {
                GameObject.Find("Manager").GetComponent<GameManagerScript>().money -= (this.Worth() * this.quantity);
            }
            else
            {
                GameObject.Find("Manager").GetComponent<GameManagerScript>().money += (this.Worth() * this.maxquant);
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
            }
            this.holdquant++;
            collision.transform.parent = base.transform;
            collision.transform.position = this.holdpos + new Vector3(0f, 0.053f * (this.holdquant - 1));
            collision.transform.localScale = new Vector3(0.4f, 0.4f);
        } else if (collision.transform.name == "ConveyorB") {
            moveSpeed = collision.GetComponent<ConveyorAScript>().conveyorSpeed;
        }
    }

    public int fish;
    public Sprite[] sprites;
    public SpriteRenderer display;
    public float moveSpeed;
    private Vector3 holdpos;
    private TextMeshPro quantity_text;
    private Vector3 outside;
    private int quantity;
    private int maxquant;
    private Color semi = new Color(1, 1, 1, 0.4f);
    public int holdquant = 0;
}
