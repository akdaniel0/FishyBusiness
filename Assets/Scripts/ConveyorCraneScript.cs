using UnityEngine;

public class ConveyorCraneScript : MonoBehaviour
{
    public float[] xyLimits; // [ymin, ymax, xmin, xmax], [0.5f, 4f, -9f, 4f]
    public float[] rotationLimits;
    public float craneDrag;
    public float craneAcceleration;
    
    Vector3 mousePos;
    Vector2 craneVelocity;
    float xdist;
    float ydist;
    //float rotatio;
    float rotarget;
    bool isRotating;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // update line between grabber and center
        gameObject.GetComponent<LineRenderer>().SetPosition(0, new Vector3(transform.GetChild(1).transform.position.x, transform.GetChild(1).transform.position.y, 5));
        gameObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(transform.position.x, transform.position.y, 5));

        float xpos = transform.localPosition.x;
        float ypos = transform.localPosition.y;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // enable crane movement when above limit
        if (mousePos.y < 0.5f && Input.GetKey(KeyCode.C)) {
            xdist = -1 * (xpos - mousePos.x);
            ydist = -1 * (ypos - mousePos.y);
        } else {
            xdist = 0;
            ydist = 0;
        }
        // apply drag to crane velocity
        craneVelocity /= (1f + Time.deltaTime * craneDrag);

        
        // move axes that aren't out of bounds if cursor not at same spot as grabber
        if (Mathf.Abs(xdist) > 0.2 || Mathf.Abs(ydist) > 0.2) {
            if (xpos <= xyLimits[1] && xpos >= xyLimits[0]) {
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

        // rotate crane center if incorrect
        if (ydist > 0) { // target 180 euler
            if (rotarget == 0) {
                isRotating = true;
                rotarget = 180;
            }
        } else if (ydist < 0) { // target 0 euler
            if (rotarget == 180) {
                isRotating = true;
                rotarget = 0;
            }
        }

        if (Mathf.Abs((transform.eulerAngles.z % 360f) - rotarget) > 2f) {
            //if ((transform.eulerAngles.z % 360f) < rotarget) {
            transform.Rotate(0, 0, 10);
            //}
        } else {
            isRotating = false;
        }

        

        //Debug.Log("cv: " + craneVelocity);
        //Debug.Log("ydist: " + ydist);
        //Debug.Log("tea: " + transform.eulerAngles);
        //Debug.Log("q: " + transform.rotation);
        
        // apply position offset from velocity to position
        xpos += craneVelocity.x * Time.deltaTime * craneAcceleration;
        //ypos += craneVelocity.y * Time.deltaTime * craneAcceleration;
        // parent horizontal position
        transform.localPosition = new Vector3(xpos, transform.localPosition.y, transform.localPosition.z);
        // grabber vertical position
        //transform.GetChild(0).localPosition = new Vector3(transform.GetChild(0).localPosition.x, ypos, transform.GetChild(0).localPosition.z);
    }
}
