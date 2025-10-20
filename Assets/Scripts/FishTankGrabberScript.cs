using UnityEngine;

public class FishTankGrabberScript : MonoBehaviour
{
    private Animator animator;
    FishTankCraneScript craneScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.animator = GetComponent<Animator>();
        craneScript = GetComponentInParent<FishTankCraneScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // detect collisions
    void OnTriggerStay2D(Collider2D collision) {
        // if colliding with the barrier, bounce off and return up
        if (collision.transform.name == "Barrier") {
            craneScript.isColliding = true;
            craneScript.isReaching = false;
            
            // update positions of self and parent with offsets
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
            // move away depending on what side colliding with
            if (transform.position.x - collision.transform.position.x > 0) {
                transform.parent.position = new Vector3(transform.parent.position.x + 0.05f, transform.parent.position.y, transform.parent.position.z);
            } else {
                transform.parent.position = new Vector3(transform.parent.position.x - 0.05f, transform.parent.position.y, transform.parent.position.z);
            }
        }
        // fish collision detection is here
        if (collision.gameObject.tag == "Fish" && craneScript.isReaching) {
            // animate grab
            if (craneScript.grabbedObj == null) {
                this.animator.Play("GrabAnim");
            }
            
            craneScript.grabbedObj = collision.gameObject;
            craneScript.isReaching = false;
            collision.transform.SetParent(transform);
            collision.transform.localPosition = new Vector3(0, -0.4f, 0.2f);
            
        }
    }
}
