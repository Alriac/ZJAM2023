using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleText : MonoBehaviour
{
    int sense  = 1;
    public float speed = 1.5f;
    public float current_offset;
    public float offset = 0.5f;
    public float current_lifetime;
    public float lifetime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        current_offset = 0.0f;
        current_lifetime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (current_lifetime < lifetime) {
            current_lifetime += Time.deltaTime;
            current_offset += speed * Time.deltaTime;
            if (current_offset > offset) {
                sense *= -1;
                current_offset = 0;
            } else {
                transform.position += new Vector3(0.0f, speed * sense, 0.0f) * Time.deltaTime;
            }
        //} else {
        //    Destroy(gameObject);
        //    current_lifetime = 0.0f;
        //}
    }
}
