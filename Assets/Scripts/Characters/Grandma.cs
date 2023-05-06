using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : MonoBehaviour
{

    private float current_cooldown;
    float cooldown = 2.0f;
    GameObject TextBubble;
    private GameObject GeneratedTextBubble;

    // Start is called before the first frame update
    void Start()
    {
        current_cooldown = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_cooldown < cooldown) {
            if (Random.Range(0.0f, 1.0f) != 0.0f) {
                GeneratedTextBubble = Instantiate(TextBubble, transform.position + new Vector3(0.0f, 0.5f, 0.0f), Quaternion.identity);
                current_cooldown = 0.0f;
            }
        } else {
            current_cooldown += Time.deltaTime;
        }
    }
}
