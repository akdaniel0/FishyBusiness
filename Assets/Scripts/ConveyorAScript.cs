using UnityEngine;

public class ConveyorAScript : MonoBehaviour
{
    public GameObject[] grabbedObjs;
    GameObject arrowVisual;

    public float conveyorSpeed;
    public Animator C_animation;
    public bool hasArrowChild;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grabbedObjs = new GameObject[0];
        C_animation.speed = Mathf.Abs(conveyorSpeed) * 1.95f;
        
        if (hasArrowChild) {
            arrowVisual = transform.GetChild(0).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < grabbedObjs.Length; i++) {
            grabbedObjs[i].transform.position = new Vector3(grabbedObjs[i].transform.position.x + (-conveyorSpeed * Time.deltaTime), grabbedObjs[i].transform.position.y, grabbedObjs[i].transform.position.z);
        }

        // move conveyor visual if enabled
        if (hasArrowChild) {
            arrowVisual.transform.position = new Vector3(arrowVisual.transform.position.x + (-conveyorSpeed * Time.deltaTime), arrowVisual.transform.position.y, arrowVisual.transform.position.z);
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
