using UnityEngine;
using TMPro;
using System.Collections.Generic;

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
        this.quantity = Random.Range(1, 6);
        this.maxquant = this.quantity;
        this.holdpos = Vector3.zero;
        this.UpdateQuant();
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
        if (this.prev_children != base.transform.childCount)
        {
            FishAiScript[] fishes = base.GetComponentsInChildren<FishAiScript>();
            this.holdquant = fishes.Length;
            int tempquant = this.maxquant;
            foreach (FishAiScript fish in fishes)
            {
                if (fish.type == this.fish)
                {
                    tempquant--;
                }
            }
            this.quantity = tempquant;
            this.UpdateQuant();
        }
        this.prev_children = base.transform.childCount;


        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(1) && !this.done)
        {
            if (Mathf.Abs(base.transform.position.x - mousePos.x) <= 0.5f && Mathf.Abs(base.transform.position.y - mousePos.y) <= 0.5f)
            {
                if (this.quantity == 0)
                {
                    this.moveSpeed *= 5;
                    this.done = true;
                }
            }
        }
        
    }


    /* private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1) && !this.done)
        {
            if (this.quantity == 0)
            {
                this.moveSpeed *= 5;
                this.done = true;
            }
        }
    }*/

    private void UpdateQuant()
    {
        if (this.quantity > 1)
        {
            this.quantity_text.text = this.quantity.ToString();
        }
        else
        {
            this.quantity_text.text = string.Empty;
        }
        if (this.quantity == 0)
        {
            this.display.enabled = false;
        }
        else
        {
            this.display.enabled = true;
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
            collision.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            collision.transform.parent = base.transform;
            collision.transform.localPosition = this.holdpos + new Vector3(0f, 0.1f * this.holdquant);
            collision.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
            collision.GetComponent<SpriteRenderer>().flipX = false;
        } else if (collision.transform.name == "ConveyorB") {
            moveSpeed = collision.GetComponent<ConveyorAScript>().conveyorSpeed;
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish") && collision.transform.parent != base.transform)
        {
            FishAiScript fishscr = collision.GetComponent<FishAiScript>();
                if (fishscr.type == this.fish)
                {
                    if (this.quantity == 0)
                    {
                        this.display.color = this.semi;
                    }
                    this.quantity++;
                    this.UpdateQuant();
                }
                this.holdquant--;
            this.prev_id = -1;
        }
    }*/

    public int fish;
    public Sprite[] sprites;
    public SpriteRenderer display;
    public float moveSpeed;
    private TextMeshPro quantity_text;
    private int quantity;
    private int maxquant;
    private Color semi = new Color(1f, 1f, 1f, 0.4f);
    public int holdquant = 0;
    private Vector3 holdpos;
    public List<FishAiScript> fishAis;
    private int prev_children;
    private bool done;
}
