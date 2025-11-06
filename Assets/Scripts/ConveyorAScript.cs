using UnityEngine;

public class ConveyorAScript : MonoBehaviour
{
    public GameObject[] grabbedObjs;

    public float conveyorSpeed;
    public Animator C_animation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grabbedObjs = new GameObject[0];
        C_animation.speed = conveyorSpeed * 1.95f;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < grabbedObjs.Length; i++) {
            grabbedObjs[i].transform.position = new Vector3(grabbedObjs[i].transform.position.x + (-conveyorSpeed * Time.deltaTime), grabbedObjs[i].transform.position.y, grabbedObjs[i].transform.position.z);
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
