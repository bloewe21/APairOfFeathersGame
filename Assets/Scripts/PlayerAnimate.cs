using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    [SerializeField] private bool isSinglePlayer = true;

    [SerializeField] private GameObject leftWing;
    [SerializeField] private GameObject rightWing;
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerMovement2 pm;

    private bool unfrozen = true;
    private bool zeroYVelocity = false;

    [SerializeField] private LayerMask jumpableGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement2>();
    }

    // Update is called once per frame
    void Update()
    {
        //edge case of super small y velocity
        if (Mathf.Abs(rb.velocity.y) < .0001f)
        {
            //print("fact");
            zeroYVelocity = true;
        }
        else
        {
            zeroYVelocity = false;
        }

        //print(rb.velocity.y);


        if (pm.isFrozen)
        {
            leftWing.GetComponent<Animator>().SetTrigger("leftinvis");
            rightWing.GetComponent<Animator>().SetTrigger("rightinvis");
        }

        if (pm.isFrozen && unfrozen)
        {
            if (isSinglePlayer)
            {
                PlayerPrefs.SetInt("singleSavedStuns", PlayerPrefs.GetInt("singleSavedStuns") + 1);
            }
            else
            {
                PlayerPrefs.SetInt("multiSavedStuns", PlayerPrefs.GetInt("multiSavedStuns") + 1);
            }

            anim.SetTrigger("stunned");
            leftWing.GetComponent<Animator>().SetTrigger("leftinvis");
            rightWing.GetComponent<Animator>().SetTrigger("rightinvis");

            unfrozen = false;
        }

        else if (zeroYVelocity && !pm.isFrozen)
        {
            //print("groundward");
            anim.SetTrigger("onground");
            leftWing.GetComponent<Animator>().SetTrigger("leftinvis");
            rightWing.GetComponent<Animator>().SetTrigger("rightinvis");

            unfrozen = true;
        }

        else if (rb.velocity.y != 0f && !pm.isFrozen)
        {
            //print(rb.velocity.y);
            anim.SetTrigger("inair");
            leftWing.GetComponent<Animator>().SetTrigger("leftidle");
            rightWing.GetComponent<Animator>().SetTrigger("rightidle");

            unfrozen = true;
        }

        //else if (rb.velocity.y == 0f && !pm.isFrozen)
        //else if (Mathf.Approximately(rb.velocity.y, 0f) && !pm.isFrozen)
        // else if (zeroYVelocity && !pm.isFrozen)
        // {
        //     print("groundward");
        //     anim.SetTrigger("onground");
        //     leftWing.GetComponent<Animator>().SetTrigger("leftinvis");
        //     rightWing.GetComponent<Animator>().SetTrigger("rightinvis");

        //     unfrozen = true;
        // }

        // if (pm.isFrozen)
        // {
        //     anim.SetTrigger("stunned");
        //     leftWing.GetComponent<Animator>().SetTrigger("leftinvis");
        //     rightWing.GetComponent<Animator>().SetTrigger("rightinvis");
        // }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, .51f, jumpableGround);
    }
}
