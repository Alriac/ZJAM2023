using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : MonoBehaviour
{

    public float current_cooldown;
    public float cooldown = 2.0f;

    int sense = 1;
    public float bubble_current_offset = 0.0f;
    public float bubble_offset = 2.0f;
    public GameObject TextBubble;
    public GameObject GeneratedTextBubble;

    // Start is called before the first frame update
    void Start()
    {
        current_cooldown = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_cooldown >= cooldown) {
            Destroy(GeneratedTextBubble);
            if (Random.Range(0.0f, 100.0f) > 60.0f) {
                GeneratedTextBubble = Instantiate(TextBubble, transform.position + new Vector3(1.0f, 0.5f, 0.0f), Quaternion.identity);
                current_cooldown    = 0.0f;
            }
        } else {
            current_cooldown += Time.deltaTime;
            if (bubble_current_offset >= bubble_offset) {
                sense *= -1;
                bubble_current_offset = 0;
            }
            bubble_current_offset += 0.5f * 2.0f * Time.deltaTime;
            GeneratedTextBubble.transform.position += new Vector3(0.0f, 0.5f * sense, 0.0f) * 2.0f * Time.deltaTime;            
        }
    }
}
