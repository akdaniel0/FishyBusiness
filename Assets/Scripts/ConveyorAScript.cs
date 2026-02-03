using UnityEngine;

public class ConveyorAScript : MonoBehaviour
{
    public GameObject[] grabbedObjs;
    GameObject arrowVisual;

    public float conveyorSpeed;
    public Animator C_animation;
    public bool hasArrowChild;
    private Vector3 ogpos;
    public float bounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grabbedObjs = new GameObject[0];
        //C_animation.speed = Mathf.Abs(conveyorSpeed) * 1.95f;
        
        if (hasArrowChild) {
            arrowVisual = transform.GetChild(0).gameObject;
            this.ogpos = arrowVisual.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Canvas").GetComponent<Canvas>().enabled) { return; }
        for (int i = 0; i < grabbedObjs.Length; i++) {
            // remove deleted fish from array
            if (grabbedObjs[i] == null) {
                UnityEditor.ArrayUtility.Remove(ref grabbedObjs, grabbedObjs[i]);
            } else {
                grabbedObjs[i].transform.position = new Vector3(grabbedObjs[i].transform.position.x + (-conveyorSpeed * Time.deltaTime), grabbedObjs[i].transform.position.y, grabbedObjs[i].transform.position.z);
            }
        }

        //this.conveyor_head.localPosition = Vector3.MoveTowards(this.conveyor_head.localPosition, new Vector3(-0.882f, 0f), this.conveyorSpeed * 0.05f);
        

        // move conveyor visual if enabled
        if (hasArrowChild) {
            arrowVisual.transform.position = new Vector3(arrowVisual.transform.position.x + (-conveyorSpeed * Time.deltaTime), arrowVisual.transform.position.y, arrowVisual.transform.position.z);
            if ((this.conveyorSpeed > 0f && arrowVisual.transform.localPosition.x <= this.bounds) || (this.conveyorSpeed < 0f && arrowVisual.transform.localPosition.x >= this.bounds))
            {
                this.arrowVisual.transform.position = this.ogpos;
            }
        }
        
    }
    // detect collisions
    void OnTriggerStay2D(Collider2D collision) {
        // if fish and not had, get
        if (collision.gameObject.tag == "Fish" && !UnityEditor.ArrayUtility.Contains<GameObject>(grabbedObjs, collision.gameObject) && collision.transform.parent == null) {
            // grab fish
            UnityEditor.ArrayUtility.Add(ref grabbedObjs, collision.gameObject);
            collision.transform.SetParent(transform);
            
        }
    }
}
