using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class OrderScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.quantity_text = base.GetComponentInChildren<TextMeshPro>();
        if (this.fish < this.sprites.Length)
        {
            this.display.sprite = this.sprites[this.fish];
        }
        this.display.color = this.semi;
        int max;
        if (this.fish >= 8)
        {
            max = Random.Range(1, 3);
        }
        else
        {
            max = Random.Range(1, 4);
        }
        /*else
        {
            max = Random.Range(1, 4);
        }*/
        this.quantity = max;
        this.maxquant = this.quantity;
        this.holdpos = Vector3.zero;
        this.UpdateQuant();
        this.move_mult = 1f;
        this.platedisp = GameObject.Find("Canvas_game").transform.Find("Plate_render").gameObject;
        this.scrub = GameObject.Find("Water").GetComponent<ScrubScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //base.transform.position = Vector3.MoveTowards(base.transform.position, this.outside, 0.001f);
        base.transform.position = new Vector3(base.transform.position.x - (moveSpeed * Time.deltaTime * this.move_mult), base.transform.position.y, base.transform.position.z);
        GameManagerScript manager = GameObject.Find("Manager").GetComponent<GameManagerScript>();
        //if (Vector3.Distance(base.transform.position, this.outside) <= 0.01f)
        // check if passed end without regard for exact y and z position
        if (transform.position.x > 6)
        {
            // change moneys
            if (this.quantity > 0)
            {
                manager.AddMoney(-0.5f * (this.Worth() * this.quantity));
            }
            else
            {
                manager.AddMoney((this.Worth() * this.maxquant));
            }
            Destroy(base.gameObject);
        }
        else if (!this.half && base.transform.position.x >= -1.4f)
        {
            this.half = true;
            int rand = Random.Range(0, 3);
            if(rand <= 1)
            {
                rand = Random.Range(1, 4);
                for (int i = 0; i < rand; i++)
                {
                    FishSpawnerScript spawner = FindAnyObjectByType<FishSpawnerScript>();
                    float cool = Random.Range(5f, 20f);
                    spawner.QueueFish(this.fish, cool);
                }
                Debug.Log(rand + "x Fish Help Spawning...");
            }
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


        //Debug.Log(Mathf.Abs(base.transform.position.y - mousePos.y));
        if (!this.scrub.on && ScrubScript.isHovering(base.transform))
        {
            if (Input.GetMouseButtonDown(1) && !this.done)
            {
                if (this.quantity == 0)
                {
                    this.done = true;
                    this.star.SetActive(false);
                }
            }
            if (!this.highlight.activeSelf && this.selected <= 1)
            {
                this.platedisp.SetActive(true);
                OrderScript[] other = FindObjectsByType<OrderScript>(FindObjectsSortMode.None);
                this.selected = 0;
                if(other.Length == 1)
                {
                    this.ShowPlate();
                    this.highlight.SetActive(true);
                    this.selected = 1;
                }
                else
                {
                    foreach (OrderScript order in other)
                    {
                        if (order.gameObject != base.gameObject)
                        {
                            GameObject hl = order.transform.Find("Highlight").gameObject;
                            if (ScrubScript.isHovering(order.transform))
                            {
                                // Both plates are highlighted
                                this.selected++;
                                if (this.platedisp.transform.Find("Qty").GetComponent<TextMeshProUGUI>().text != this.quantity.ToString())
                                {
                                    if (!hl.activeSelf)
                                    {
                                        hl.SetActive(true);
                                        this.highlight.SetActive(false);
                                    }
                                }
                            }
                            else
                            {
                                hl.SetActive(false);
                                if (!this.highlight.activeSelf)
                                {
                                    this.ShowPlate();
                                    this.selected = 1;
                                    this.highlight.SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
        }
        else if(this.highlight.activeSelf)
        {
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //if(Mathf.Abs(base.transform.position.y - mousePos.y) > 0.5f)
            this.selected--;
            if (this.selected == 0)
            {
                this.platedisp.SetActive(false);
            }
            this.highlight.SetActive(false);
        }

        if (this.done)
        {
            this.move_mult *= 1.05f;
        }
    }

    private void ShowPlate()
    {
        this.platedisp.transform.Find("Display").GetComponent<Image>().sprite = this.sprites[this.fish];
        this.platedisp.transform.Find("Qty").GetComponent<TextMeshProUGUI>().text = this.quantity.ToString();
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
        if (this.quantity == 0)
        {
            this.display.enabled = false;
            this.star.SetActive(true);
            this.quantity_text.text = string.Empty;
        }
        else
        {
            this.display.enabled = true;
            this.star.SetActive(false);
            if (this.quantity == 1)
            {
                this.quantity_text.text = string.Empty;
            }
            else
            {
                this.quantity_text.text = this.quantity.ToString();
            }
        }
    }

    private int Worth()
    {
        if (this.fish >= 9)
        {
            return 25;
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
        if (collision.CompareTag("Fish") && collision.transform.parent == null)
        {
            collision.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            collision.transform.parent = base.transform;
            collision.transform.localPosition = this.holdpos + new Vector3(0f, 0.1f * this.holdquant);
            collision.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
            collision.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (collision.transform.name == "ConveyorB")
        {
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
    public int quantity;
    private int maxquant;
    private Color semi = new Color(1f, 1f, 1f, 0.4f);
    public int holdquant = 0;
    private Vector3 holdpos;
    public List<FishAiScript> fishAis;
    private int prev_children;
    private bool done;
    private float move_mult;
    public GameObject star;
    private bool half;
    private GameObject platedisp;
    public GameObject highlight;
    private ScrubScript scrub;
    private int selected;
}
