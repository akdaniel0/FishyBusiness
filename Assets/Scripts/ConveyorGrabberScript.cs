using UnityEngine;

public class ConveyorGrabberScript : MonoBehaviour
{
    private Animator animator;
    ConveyorCraneScript craneScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.animator = GetComponent<Animator>();
        craneScript = GetComponentInParent<ConveyorCraneScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    // detect collisions
    void OnTriggerStay2D(Collider2D collision) {
        // fish collision detection is here
        if (collision.gameObject.tag == "Fish" && craneScript.isReaching) {
            // animate grab
            if (craneScript.grabbedObj == null) {
                this.animator.Play("GrabAnim");
            }

            // remove obj from conveyor
            if (collision.transform.parent != null) {
                UnityEditor.ArrayUtility.Remove(ref GameObject.Find("ConveyorA").GetComponent<ConveyorAScript>().grabbedObjs, collision.gameObject);
                UnityEditor.ArrayUtility.Remove(ref GameObject.Find("ConveyorB").GetComponent<ConveyorAScript>().grabbedObjs, collision.gameObject);
            }
            
            craneScript.grabbedObj = collision.gameObject;
            craneScript.isReaching = false;
            collision.transform.SetParent(transform);
            collision.transform.localPosition = new Vector3(0, -0.2f, 0.2f);
        }
    }
}
