using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    //private BoxCollider2D coll;
    private CircleCollider2D coll;

    private bool canJump;
    private bool canBounce;
    private bool canLand;
    private bool canLandUp;
    [SerializeField] private float jumpForceY = 10.0f;
    [SerializeField] private float jumpForceX = 10.0f;
    [SerializeField] private float bounceForce = 1.4f;
    [SerializeField] private float landForce = 1.3f;
    [SerializeField] private float landUpForce = 1.3f;
    [SerializeField] private float rotateMult = -2.5f;
    private float rotateAmount;
    private float rotationDirection = 1;
    private float rotationSway = 0;

    private bool canGoRight = true;
    private bool canGoLeft = true;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask bouncableWall;
    [SerializeField] private LayerMask bouncableCeiling;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //coll = GetComponent<BoxCollider2D>();
        coll = GetComponent<CircleCollider2D>();

        //canJump = false;
        canBounce = true;
    }

    // Update is called once per frame
    void Update()
    {
        //rotation
        RotationFunction();

        //allow for bouncing off grounds
        if (!IsGrounded())
        {
            canLand = true;
        }

        //allow for bouncing off ceilings
        if (!IsCeiling())
        {
            canLandUp = true;
        }

        //bounce if landing on ground
        if (canLand && IsGrounded())
        {
            canLand = false;
            rb.AddForce(Vector2.up * Mathf.Abs(rb.velocity.y) * landForce, ForceMode2D.Impulse);
            print("groundBounce");
        }

        //bounce if bumping into ceiling
        if (canLandUp && IsCeiling())
        {
            canLandUp = false;
            rb.AddForce(Vector2.down * Mathf.Abs(rb.velocity.y) * landUpForce, ForceMode2D.Impulse);
            print("ceilingBounce");
        }

        //slow down player on landing
        if (IsGrounded() && rb.velocity.y == 0f)
        {
            rb.velocity = new Vector3(rb.velocity.x * 0.99f, 0f, 0f);
        }

        if (IsWalled())
        {
            print("isWalled");
            //bounce off walls
            if (canBounce)
            {
                print("canBounce");
                if (rb.velocity.x > 0)
                {
                    print("bounce left");
                    rb.AddForce(Vector2.left * Mathf.Abs(rb.velocity.x) * bounceForce, ForceMode2D.Impulse);
                    StartCoroutine(bounceTimer());
                   
                }
                else
                {
                    print("bounce right");
                    rb.AddForce(Vector2.right * Mathf.Abs(rb.velocity.x) * bounceForce, ForceMode2D.Impulse);
                    StartCoroutine(bounceTimer());
                }
            }
        }

        InputFunction();
    }


    private void RotationFunction()
    {
        //swaying back and forth
        if (rotationDirection == 1)
        {
            rotationSway += .08f;
        }
        else
        {
            rotationSway -= .08f;
        }

        //don't sway if grounded
        /*
        if (IsGrounded())
        {
            rotateAmount = (rotateMult * rb.velocity.x);
        }
        else
        {
            rotateAmount = (rotateMult * rb.velocity.x) + (-1 * rotationSway);
        }
        */

        //FIRST = SWAYING, SECOND = NOT
        rotateAmount = (rotateMult * rb.velocity.x) + (-1 * rotationSway);
        //rotateAmount = (rotateMult * rb.velocity.x);

        //change sway direction
        if (rotateAmount > (rotateMult * rb.velocity.x) + 5)
        {
            rotationDirection *= -1;
        }
        else if (rotateAmount < (rotateMult * rb.velocity.x) - 5)
        {
            rotationDirection *= -1;
        }
       
        //set rotation
        transform.eulerAngles = new Vector3(0f, 0f, rotateAmount);
    }

    private void InputFunction()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && canGoRight)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            rb.AddForce(Vector2.up * jumpForceY, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * jumpForceX / 4.0f, ForceMode2D.Impulse);
            print("right");

            StartCoroutine(goRightTimer());
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && canGoLeft)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            rb.AddForce(Vector2.up * jumpForceY, ForceMode2D.Impulse);
            rb.AddForce(Vector2.left * jumpForceX / 4.0f, ForceMode2D.Impulse);
            print("left");

            StartCoroutine(goLeftTimer());
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private bool IsWalled()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, .05f, bouncableWall) || Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, .05f, bouncableWall);
    }

    private bool IsCeiling()
    {    
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.up, .05f, bouncableCeiling);
    }

    private IEnumerator bounceTimer()
    {
        canBounce = false;
        yield return new WaitForSeconds(0.1f);
        canBounce = true;
    }

    private IEnumerator goRightTimer()
    {
        canGoRight = false;
        yield return new WaitForSeconds(0.1f);
        canGoRight = true;
    }

    private IEnumerator goLeftTimer()
    {
        canGoLeft = false;
        yield return new WaitForSeconds(0.1f);
        canGoLeft = true;
    }
}