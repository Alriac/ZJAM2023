using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    public float speed = 2.0f;
    Animator a;
    SpriteRenderer sr;
    Rigidbody2D rb;

    bool stopMovement = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        GameEvents.Ins.OnGameEnded += OnGameEnded;
        PlayerPrefs.SetFloat("total time played", 0);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal_move = Input.GetAxis("Horizontal");
        float vertical_move = Input.GetAxis("Vertical");
        if (horizontal_move == 0 || stopMovement)
        {
            a.SetBool("HorWalk", false);
        }
        else
        {
            a.SetBool("HorWalk", true);
            if (horizontal_move > 0)
            {
                if (!sr.flipX)
                {
                    sr.flipX = true;
                }
            }
            else if (horizontal_move < 0)
            {
                if (sr.flipX)
                {
                    sr.flipX = false;
                }
            }
        }
        if (vertical_move == 0 || stopMovement)
        {
            a.SetBool("VerWalkDown", false);
            a.SetBool("VerWalkUp", false);
        }
        else
        {
            if (vertical_move > 0)
            {
                a.SetBool("VerWalkUp", true);
            }
            else
            {
                a.SetBool("VerWalkDown", true);
            }
        }

        // transform.position += new Vector3(horizontal_move, vertical_move, 0) * speed * Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.Space)) && GameEvents.Ins.OnPlayerActionKey != null && !stopMovement)
        {
            GameEvents.Ins.OnPlayerActionKey();
        }
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (Mathf.Abs(x) < 0.25 && Mathf.Abs(y) < 0.25)
        {
            rb.velocity = Vector2.zero;
        }
        else if (!stopMovement)
        {
            rb.velocity = new Vector2(x, y).normalized * speed;
        }
    }

    void OnGameEnded(EnumGameEndingReason ending)
    {
        stopMovement = true;
    }


}
