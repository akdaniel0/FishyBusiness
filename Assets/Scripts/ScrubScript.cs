using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrubScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.water = base.GetComponent<SpriteRenderer>();
        this.start = this.scrub.color;
        this.bluwater = this.water.color; // new Color(63f, 107f, 145f, 66f);
        //Debug.Log(this.bluwater);
        this.dirt = new Color(206f, 255f, 0f, 250f) / 255f;
        this.diff = this.dirt - this.bluwater;
    }

    // Update is called once per frame
    void Update()
    {
        this.grime = Mathf.Clamp(this.grime, 0f, 500f);
        this.percent = this.grime / 500f;
        this.water.color = this.bluwater + (this.diff * this.percent);
        if(this.on)
        {
            if (this.check)
            {
                this.mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                this.check = false;
            }
            else
            {
                this.prev = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                this.check = true;
            }
            if(isHovering(base.transform))
            {
                this.CheckMouse();
            }
        }
    }

    public static bool isHovering(Transform obj)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool left = mousePos.x >= -(obj.lossyScale.x / 2f) + obj.position.x;
        bool right = mousePos.x <= (obj.lossyScale.x / 2f) + obj.position.x;
        bool up = mousePos.y <= (obj.lossyScale.y / 2f) + obj.position.y;
        bool down = mousePos.y >= -(obj.lossyScale.y / 2f) + obj.position.y;
        return left && right && up && down;
    }

    public static bool inBounds(Transform obj)
    {
        return obj.position.x > -10f && obj.position.x < 6f;
    }

    private void CheckMouse()
    {
        float diff = (Mathf.Abs(prev.x - mousePos.x) + Mathf.Abs(prev.y - mousePos.y)) * 0.25f;
        this.grime -= diff;
    }

    public void Toggle()
    {
        this.on = !this.on;
        if (this.on)
        {
            Cursor.SetCursor(this.curspic, Vector2.zero, CursorMode.Auto);
            this.scrub.color = Color.red;
            this.scrub.GetComponentInChildren<TextMeshProUGUI>().text = "Scrub: ON";
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            this.scrub.color = this.start;
            this.scrub.GetComponentInChildren<TextMeshProUGUI>().text = "Scrub: OFF";
        }
        
    }
    public bool on;
    public Texture2D curspic;
    private Color start;
    private Color bluwater;
    private Color dirt;
    private SpriteRenderer water;
    public Image scrub;
    public float grime;
    private Color diff;
    private Vector3 mousePos;
    private Vector3 prev;
    private bool check;
    public float percent;
}
