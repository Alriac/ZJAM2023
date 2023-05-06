using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    public float speed = 10.0f;

    Animator a;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && GameEvents.Ins.OnPlayerActionKey != null)
            GameEvents.Ins.OnPlayerActionKey();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Object")
        {
            if (Input.GetKey("e"))
            {
                Debug.Log("Interactuo?");
            }
        }
    }


}
