using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public static Rope instance;
    public GameObject hook;
    public GameObject[] ropeSegments;
    public int numLinks = 5;
    public bool should_generate_rope = false;
    public bool is_rope_in_use = false;
    public List<GameObject> createdRopeSegments = new List<GameObject>();

    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        generate_rope();
    }

    void Update() {
        if (should_generate_rope) {
            generate_rope();
        } else if (!is_rope_in_use && createdRopeSegments.Count > 0) {
            delete_rope();
        }
    }

    void generate_rope() {
        Debug.Log("Generating New Segments: " + numLinks);
        Rigidbody2D prev_body = hook.GetComponent<Rigidbody2D>();
        for (int i = 0; i < numLinks; i++) {
            int idx = Random.Range(0, ropeSegments.Length);
            GameObject newSeg = Instantiate(ropeSegments[idx]);

            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;

            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prev_body;

            prev_body = newSeg.GetComponent<Rigidbody2D>();
            createdRopeSegments.Add(newSeg);
        }
        should_generate_rope = false;
    }

    void delete_rope() {
        Debug.Log("Deleting Segments");
        foreach (GameObject go in createdRopeSegments) {
            Destroy(go);
        }
        createdRopeSegments.Clear();
    }
}
