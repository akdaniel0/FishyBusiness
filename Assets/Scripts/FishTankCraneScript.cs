using UnityEngine;

public class FishTankCraneScript : MonoBehaviour
{
    public float[] xyLimits; // [ymin, ymax, xmin, xmax], [0.5f, 4f, -9f, 4f]
    public float craneDrag;
    public float craneAcceleration;
    public float verticalSpeed;

    public GameObject grabbedObj;
    
    Vector3 mousePos;
    Vector2 craneVelocity;
    float xdist;
    float ydist;

    public bool isReaching;
    public bool isColliding;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Disable VSync to use targetFrameRate
        QualitySettings.vSyncCount = 0;

        // Set target frame rate to 120 FPS
        Application.targetFrameRate = 120;
    }

    // Update is called once per frame
    void Update()
    {
        // update line between grabber and center
        gameObject.GetComponent<LineRenderer>().SetPosition(0, new Vector3(transform.GetChild(0).transform.position.x, transform.GetChild(0).transform.position.y + 0.6f, 5));
        gameObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(transform.position.x, transform.position.y, 5));
        

        float xpos = transform.localPosition.x;
        float ypos = transform.GetChild(0).localPosition.y;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // action when space pressed
        if(mousePos.y > 0.5f && Input.GetKeyDown(KeyCode.Space) && ypos > -1f) {
            // grab object if no object grabbed
            if (grabbedObj == null) {
                isReaching = true;
            } else {
                // drop object
                if (grabbedObj != null) {
                    gameObject.GetComponentInChildren<Animator>().Play("ReleaseAnim");
                }
                grabbedObj.transform.SetParent(null);
                grabbedObj = null;
            }
        }

        // enable crane movement when above limit
        if (mousePos.y > 0.5f && Input.GetKey(KeyCode.C)) {
            xdist = -1 * (xpos - mousePos.x);
            //ydist = -1 * (ypos + transform.localPosition.y - mousePos.y);
            
        } else {
            xdist = 0;
            ydist = 0;
        }
        // apply drag to crane velocity
        craneVelocity /= (1f + Time.deltaTime * craneDrag);

        if(isReaching && ypos > xyLimits[2]) {
            ydist = -0.5f * verticalSpeed;
        } else if (ypos < -1f){
            ydist = 0.5f * verticalSpeed;
            isReaching = false;
        } else {
            ydist = 0;
        }

        
        // move axes that aren't out of bounds if cursor not at same spot as grabber
        if (Mathf.Abs(xdist) > 0.2 || Mathf.Abs(ydist) > 0.2) {
            if (xpos <= xyLimits[1] && xpos >= xyLimits[0] && ypos <= xyLimits[3] && ypos >= xyLimits[2] && !isColliding) {
                craneVelocity += new Vector2(xdist, ydist);
            } else {
                // stop crane if out of bounds
                craneVelocity = new Vector2(0,0);
            }
            // reset collision state for next frame
            isColliding = false;
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

        // drop animation when grab puffer
        if (grabbedObj != null && grabbedObj.GetComponent<FishAiScript>().type == 0) {
            gameObject.GetComponentInChildren<Animator>().Play("ReleaseAnim");
        }
    
    }
}
