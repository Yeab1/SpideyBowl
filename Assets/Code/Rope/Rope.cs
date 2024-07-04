using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject[] ropeSegments;
    public int numLinks = 5;
    // Start is called before the first frame update
    void Start()
    {
        generate_rope();
    }

    void generate_rope() {
        Rigidbody2D prev_body = hook;
        for (int i = 0; i < numLinks; i++) {
            int idx = Random.Range(0, ropeSegments.Length);
            GameObject newSeg = Instantiate(ropeSegments[idx]);

            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;

            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prev_body;

            prev_body = newSeg.GetComponent<Rigidbody2D>();
        }
    }

    
}
