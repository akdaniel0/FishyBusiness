/****************************************************************************
 *  File Name: FishAiScript.cs
 *  Author: Gavin Muller
 *  DigiPen Email: gavin.muller@digipen.edu
 *  Course: GAM100
 *
 *  Description: This is a script used for the ai of the fish in the tank
 *
 ****************************************************************************/

using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;

public class FishAiScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.max = base.transform.position + new Vector3(11.9f, 0f);
        //Debug.Log(this.max);
        this.origin = base.transform.position;
        this.phase = 0;
        this.sprite = base.GetComponent<SpriteRenderer>();
        this.lap = this.origin + new Vector3(1f, 0f);
        this.outside = this.origin - new Vector3(2f, 0f);
        int rand = Random.Range(0, 2);
        if (rand == 0) { this.pullup = false; } else { this.pullup = true; }
        this.sprite.sprite = this.types[this.type];
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // revive when water
        if (isDead && transform.parent == null && transform.position.x < 3f && mousePos.y > 0.5f) {
            isDead = false;
        }
        
        // if grabbed no swim
        if (transform.parent != null) {
            isDead = true;
            return;
        } else if (isDead) {
            // fall when out of water
            transform.position = new Vector3(transform.position.x, transform.position.y - fallSpeed * Time.deltaTime, transform.position.z);
            fallSpeed += 2 * Time.deltaTime;
            return;
        }

        float speed = this.speed * 0.005f;
        float ymove;
        this.checker += Time.deltaTime;
        if (this.pullup)
        {
            ymove = Random.Range(0f, 0.8f);
        }
        else
        {
            ymove = Random.Range(-0.8f, 0f);
        }
        if (this.checker >= 1f / this.speed)
        {
            this.pullup = !this.pullup;
            float difference = base.transform.position.y - this.origin.y;
            float predict = difference + ymove;
            if (predict >= 0.4f)
            {
                float subtract = predict - 0.4f;
                ymove -= subtract;
            }
            else if (predict <= -0.4f)
            {
                float add = predict + 0.4f;
                ymove += add;
            }
            this.checker = 0f;
        }
        if (phase == 0)
        {
            base.transform.position = Vector3.MoveTowards(base.transform.position, this.max + new Vector3(0f, ymove), speed);
            if (Mathf.Abs(base.transform.position.x - this.max.x) <= 0.5f)
            {
                if(Random.Range(0, 3) == 0 && !this.debug)
                {
                    this.phase = 2;
                }
                else
                {
                    this.phase = 1;
                }
                this.sprite.flipX = true;
            }
        }
        if (phase == 1)
        {
            base.transform.position = Vector3.MoveTowards(base.transform.position, this.lap + new Vector3(0f, ymove), speed);
            //Debug.Log(Mathf.Abs(base.transform.position.x - dest.x));
            if (Mathf.Abs(base.transform.position.x - this.lap.x) <= 1f)
            {
                this.phase = 0;
                this.sprite.flipX = false;
            }
        }
        if (phase == 2)
        {
            base.transform.position = Vector3.MoveTowards(base.transform.position, this.outside + new Vector3(0f, ymove), speed);
            if (Mathf.Abs(base.transform.position.x - this.outside.x) <= 0.5f)
            {
                Destroy(base.gameObject);
            }
        }
    }

    private Vector3 max;
    private Vector3 origin;
    private SpriteRenderer sprite;
    private int phase;
    [SerializeField]
    private bool pullup;
    public float speed = 2;
    [Tooltip("Should the fish not go off screen?")]
    public bool debug;
    private Vector3 outside;
    private Vector3 lap;
    private float checker;
    public Sprite[] types;
    public int type = 0;

    public bool isDead;
    float fallSpeed;
    Vector3 mousePos;
}
