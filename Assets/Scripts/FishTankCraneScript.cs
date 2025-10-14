using UnityEngine;

public class FishTankCraneScript : MonoBehaviour
{
    public float[] xyLimits; // [ymin, ymax, xmin, xmax], [0.5f, 4f, -9f, 4f]
    public float craneDrag;
    public float craneAcceleration;
    
    Vector3 mousePos;
    Vector2 craneVelocity;
    float xdist;
    float ydist;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xpos = transform.localPosition.x;
        float ypos = transform.GetChild(0).localPosition.y;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // enable crane movement when above limit
        if (mousePos.y > 0.5f && Input.GetKey(KeyCode.C)) {
            xdist = -1 * (xpos - mousePos.x);
            ydist = -1 * (ypos + transform.localPosition.y - mousePos.y);
        } else {
            xdist = 0;
            ydist = 0;
        }
        // apply drag to crane velocity
        craneVelocity /= (1f + Time.deltaTime * craneDrag);

        
        // move axes that aren't out of bounds if cursor not at same spot as grabber
        if (Mathf.Abs(xdist) > 0.2 || Mathf.Abs(ydist) > 0.2) {
            if (xpos <= xyLimits[1] && xpos >= xyLimits[0] && ypos <= xyLimits[3] && ypos >= xyLimits[2]) {
                craneVelocity += new Vector2(xdist, ydist);
            } else {
                // stop crane if out of bounds
                craneVelocity = new Vector2(0,0);
            }
        }
        
        // if x position out of bounds, correct
        if (xpos > xyLimits[1]) {
            xpos = xyLimits[1] - 0.1f;
        } else if (xpos < xyLimits[0]) {
            xpos = xyLimits[0] + 0.1f;
        }

        // if y position out of bounds, correct
        if (ypos > xyLimits[3]) {
            ypos = xyLimits[3] - 0.1f;
        } else if (ypos < xyLimits[2]) {
            ypos = xyLimits[2] + 0.1f;
        }
        //Debug.Log("cv: " + craneVelocity);
        //Debug.Log("ydist: " + ydist);
        
        // apply position offset from velocity to position
        xpos += craneVelocity.x * Time.deltaTime * craneAcceleration;
        ypos += craneVelocity.y * Time.deltaTime * craneAcceleration;
        // parent horizontal position
        transform.localPosition = new Vector3(xpos, transform.localPosition.y, transform.localPosition.z);
        // grabber vertical position
        transform.GetChild(0).localPosition = new Vector3(transform.GetChild(0).localPosition.x, ypos, transform.GetChild(0).localPosition.z);
    }
}
