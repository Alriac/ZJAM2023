using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : MonoBehaviour
{

    public float current_cooldown;
    public float cooldown = 3.0f;
    public GameObject TextBubble;
    public GameObject GeneratedTextBubble;

    // Start is called before the first frame update
    void Start()
    {
        current_cooldown = Random.Range(0.0f, 200.0f)/100;
    }

    // Update is called once per frame
    void Update()
    {
        if (GeneratedTextBubble == null) {
            if (current_cooldown < cooldown) {
                current_cooldown += Time.deltaTime;
            } else {
                if (Random.Range(0.0f, 100.0f) > 60.0f) {
                    GeneratedTextBubble = Instantiate(TextBubble, new Vector3(transform.position.x + 1.0f, transform.position.y + 0.75f, 1.0f), Quaternion.identity);
                    current_cooldown = 0;
                }
            }
        }
    }
}
