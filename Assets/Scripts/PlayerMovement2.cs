using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public Rigidbody2D rb;
    private CircleCollider2D coll;

    [SerializeField] private bool isSinglePlayer;
    [SerializeField] private GameObject endingObject;

    [SerializeField] private GameObject leftWing;
    [SerializeField] private GameObject rightWing;
    [SerializeField] private GameObject leftParticles;
    [SerializeField] private GameObject rightParticles;
    [SerializeField] private GameObject stunStars;

    private bool canBounce;
    private bool canLand;
    private bool canLandUp;
    private bool canLandConveyor;
    public bool inHeat;
    [SerializeField] private float jumpForceY = 10.0f;
    [SerializeField] private float jumpForceX = 10.0f;
    [SerializeField] private float bounceForce = 1.4f;
    [SerializeField] private float landForce = 1.3f;
    [SerializeField] private float landUpForce = 1.3f;
    [SerializeField] private float rotateMult = -2.5f;
    [SerializeField] private float conveyorForce = 0.2f;
    [SerializeField] private float conveyorLimit = 1.0f;
    [SerializeField] private float terminalVelocity = -10.0f;
    private float defaultTerminal;
    private float rotateAmount;

    private bool canGoRight = true;
    private bool canGoLeft = true;
    public bool isFrozen = false;
    public bool isPaused = false;
    private bool resumeFreeze = true;
    private float timeGrounded = 0f;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask bouncableWall;
    [SerializeField] private LayerMask slopedGround;
    [SerializeField] private LayerMask conveyorRight;
    [SerializeField] private LayerMask conveyorLeft;
    [SerializeField] private LayerMask lavaLayer;
    [SerializeField] private LayerMask freezeLayer;

    [SerializeField] private GameObject bounceParticles;
    [SerializeField] private GameObject splashParticles;

    [SerializeField] private AudioSource flapSound;
    [SerializeField] private AudioSource wallSound;
    [SerializeField] private AudioSource ceilingSound;
    [SerializeField] private AudioSource groundSound;
    [SerializeField] private AudioSource stunSound;
    [SerializeField] private AudioSource lavaSound;
    [SerializeField] private AudioSource splashSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();

        canBounce = true;
        defaultTerminal = terminalVelocity;

        if (isSinglePlayer && !PlayerPrefs.HasKey("singleSavedRights"))
        {
            PlayerPrefs.SetInt("singleSavedRights", 0);
            PlayerPrefs.SetInt("singleSavedLefts", 0);
            PlayerPrefs.SetInt("singleSavedStuns", 0);
        }
        
        else if (!isSinglePlayer && !PlayerPrefs.HasKey("multiSavedRights"))
        {
            PlayerPrefs.SetInt("multiSavedRights", 0);
            PlayerPrefs.SetInt("multiSavedLefts", 0);
            PlayerPrefs.SetInt("multiSavedStuns", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerPrefs.GetInt("singleSavedFrozen") == 1 && resumeFreeze && isSinglePlayer && Time.timeScale == 1)
        {
            PlayerPrefs.SetInt("singleSavedStuns", PlayerPrefs.GetInt("singleSavedStuns") - 1);
            freezeInputs();
            resumeFreeze = false;
        }
        else if (PlayerPrefs.GetInt("multiSavedFrozen") == 1 && resumeFreeze && !isSinglePlayer && Time.timeScale == 1)
        {
            PlayerPrefs.SetInt("multiSavedStuns", PlayerPrefs.GetInt("multiSavedStuns") - 1);
            freezeInputs();
            resumeFreeze = false;
        }

        //dont move if paused
        if (Time.timeScale == 0)
        {
            return;
        }
        //Max falling velocity
        if (rb.velocity.y < terminalVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, terminalVelocity);
        }

        if (isFrozen)
        {
            stunStars.SetActive(true);
        }
        else
        {
            stunStars.SetActive(false);
        }

        if (!IsConveyorLeft() && !IsConveyorRight() && (IsGrounded() || Mathf.Abs(rb.velocity.y) < .00001f))
        {
            beginGrounded();
        }
        else
        {
            canLand = true;
            endGrounded();
        }

        if (timeGrounded > 1f)
        {
            unfreezeInputs();
        }

        if (!IsCeiling())
        {
            canLandUp = true;
        }

        if (canLand && IsGrounded())
        {
            groundSound.Play();

            canLand = false;
            rb.AddForce(Vector2.up * Mathf.Abs(rb.velocity.y) * landForce, ForceMode2D.Impulse);

            bounceParticles.transform.position = transform.position - new Vector3(0f, .5f, 0f);
            bounceParticles.transform.eulerAngles = new Vector3(0f, 0f, 15f);
            var main = bounceParticles.GetComponent<ParticleSystem>().main;
            main.startSpeed = Mathf.Abs(rb.velocity.y) * .1f;
            bounceParticles.GetComponent<ParticleSystem>().Play();
        }

        if (canLandUp && IsCeiling())
        {
            ceilingSound.Play();

            canLandUp = false;
            rb.AddForce(Vector2.down * Mathf.Abs(rb.velocity.y) * landUpForce, ForceMode2D.Impulse);

            bounceParticles.transform.position = transform.position + new Vector3(0f, .5f, 0f);
            bounceParticles.transform.eulerAngles = new Vector3(0f, 0f, 195f);
            var main = bounceParticles.GetComponent<ParticleSystem>().main;
            main.startSpeed = Mathf.Abs(rb.velocity.y) * .1f;
            bounceParticles.GetComponent<ParticleSystem>().Play();
        }

        if (IsWalled())
        {
            //bounce off walls
            if (canBounce)
            {
                if (Mathf.Abs(rb.velocity.x) > 0.5f)
                {
                    wallSound.Play();
                }
                // wallSound.Play();

                if (rb.velocity.x > 0)
                {
                    bounceParticles.transform.position = transform.position + new Vector3(.5f, 0f, 0f);
                    bounceParticles.transform.eulerAngles = new Vector3(0f, 0f, 105f);
                    var main = bounceParticles.GetComponent<ParticleSystem>().main;
                    main.startSpeed = Mathf.Abs(rb.velocity.x) * .1f;
                    if (Mathf.Abs(rb.velocity.x) > 0.1f) {
                        bounceParticles.GetComponent<ParticleSystem>().Play();
                    }

                    rb.AddForce(Vector2.left * Mathf.Abs(rb.velocity.x) * bounceForce, ForceMode2D.Impulse);
                    StartCoroutine(bounceTimer());
                   
                }
                else
                {
                    bounceParticles.transform.position = transform.position - new Vector3(.5f, 0f, 0f);
                    bounceParticles.transform.eulerAngles = new Vector3(0f, 0f, -105f);
                    var main = bounceParticles.GetComponent<ParticleSystem>().main;
                    main.startSpeed = Mathf.Abs(rb.velocity.x) * .1f;
                    if (Mathf.Abs(rb.velocity.x) > 0.1f) {
                        bounceParticles.GetComponent<ParticleSystem>().Play();
                    }

                    rb.AddForce(Vector2.right * Mathf.Abs(rb.velocity.x) * bounceForce, ForceMode2D.Impulse);
                    StartCoroutine(bounceTimer());
                }
            }
        }

        if (IsConveyorRight())
        {
            if (Mathf.Abs(rb.velocity.x) < conveyorLimit) {
                rb.AddForce(Vector2.right * conveyorForce, ForceMode2D.Impulse);
            }
        }
        else if (IsConveyorLeft())
        {
            if (Mathf.Abs(rb.velocity.x) < conveyorLimit) {
                rb.AddForce(Vector2.left * conveyorForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            canLandConveyor = true;
        }

        if (canLandConveyor && (IsConveyorRight() || IsConveyorLeft()))
        {
            canLandConveyor = false;
            rb.AddForce(Vector2.up * Mathf.Abs(rb.velocity.y) * landForce, ForceMode2D.Impulse);
        }

        //frozen bounce
        if (coll.IsTouchingLayers(freezeLayer) && !isFrozen && Mathf.Abs(rb.velocity.y) < .00001f)
        {
            if (!isFrozen && Mathf.Abs(rb.velocity.y) < .00001f)
            {
                rb.AddForce(Vector2.up * jumpForceY, ForceMode2D.Impulse);

                int randInt = Random.Range(0, 2);
                if (randInt == 0) {
                    rb.AddForce(Vector2.left * jumpForceX / 4.0f, ForceMode2D.Impulse);
                }
                else {
                    rb.AddForce(Vector2.right * jumpForceX / 4.0f, ForceMode2D.Impulse);
                }

                endGrounded();
                freezeInputs();
            }
        }

        InputFunction();

        RotationFunction();

        CollisionTypeFunction();
    }

    private void FixedUpdate()
    {

    }



    private void RotationFunction()
    {
        rotateAmount = (rotateMult * rb.velocity.x);
        if (rotateAmount > 60) {
            rotateAmount = 60;
        }
        if (rotateAmount < -60) {
            rotateAmount = -60;
        }

        transform.eulerAngles = new Vector3(0f, 0f, rotateAmount);
    }

    private void InputFunction()
    {
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && canGoRight && !isPaused)
        {
            if (isSinglePlayer)
            {
                if (!PlayerPrefs.HasKey("singleSavedRights"))
                {
                    PlayerPrefs.SetInt("singleSavedRights", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("singleSavedRights", PlayerPrefs.GetInt("singleSavedRights") + 1);
                }
            }

            else
            {
                if (!PlayerPrefs.HasKey("multiSavedRights"))
                {
                    PlayerPrefs.SetInt("multiSavedRights", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("multiSavedRights", PlayerPrefs.GetInt("multiSavedRights") + 1);
                }
            }

            flapSound.Play();
            
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            rb.AddForce(Vector2.up * jumpForceY, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * jumpForceX / 4.0f, ForceMode2D.Impulse);

            rightWing.GetComponent<Animator>().SetTrigger("rightflap");
            rightParticles.GetComponent<ParticleSystem>().Play();

            StartCoroutine(goRightTimer());
        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && canGoLeft && !isPaused)
        {
            if (isSinglePlayer)
            {
                if (!PlayerPrefs.HasKey("singleSavedLefts"))
                {
                    PlayerPrefs.SetInt("singleSavedLefts", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("singleSavedLefts", PlayerPrefs.GetInt("singleSavedLefts") + 1);
                }
            }

            else
            {
                if (!PlayerPrefs.HasKey("multiSavedLefts"))
                {
                    PlayerPrefs.SetInt("multiSavedLefts", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("multiSavedLefts", PlayerPrefs.GetInt("multiSavedLefts") + 1);
                }
            }

            flapSound.Play();

            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            rb.AddForce(Vector2.up * jumpForceY, ForceMode2D.Impulse);
            rb.AddForce(Vector2.left * jumpForceX / 4.0f, ForceMode2D.Impulse);

            leftWing.GetComponent<Animator>().SetTrigger("leftflap");
            leftParticles.GetComponent<ParticleSystem>().Play();

            StartCoroutine(goLeftTimer());
        }
    }

    private void CollisionTypeFunction()
    {
        // float totalVelocity = Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2)));
        // print(totalVelocity);

        // if (totalVelocity > 35)
        // {
        //     //rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        // }
        // else
        // {
        //     //rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        // }
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Lava")
        {
            lavaSound.Play();

            freezeInputs();
            
            if (Mathf.Sign(rb.velocity.x) == 1) {
                rb.velocity = new Vector2(0f, 0f);
                rb.AddForce(Vector2.right * 8.0f, ForceMode2D.Impulse);
            }
            else {
                rb.velocity = new Vector2(0f, 0f);
                rb.AddForce(Vector2.left * 8.0f, ForceMode2D.Impulse);
            }

            rb.AddForce(Vector2.up * 15.0f, ForceMode2D.Impulse);
        }
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Freeze")
        {
            endGrounded();
            freezeInputs();
        }

        if (other.gameObject.tag == "Water")
        {
            GameObject[] sounds = GameObject.FindGameObjectsWithTag("Sound");
            foreach (GameObject sound in sounds)
            {
                sound.GetComponent<AudioSource>().volume = sound.GetComponent<AudioSource>().volume / 2.0f;
            }

            GameObject[] musics = GameObject.FindGameObjectsWithTag("Music");
            foreach (GameObject music in musics)
            {
                music.GetComponent<AudioSource>().volume = music.GetComponent<AudioSource>().volume / 2.0f;
            }

            splashSound.Play();

            jumpForceY = jumpForceY / 1.5f;
            jumpForceX = jumpForceX / 1.25f;
            rb.gravityScale = rb.gravityScale / 2.0f;
            terminalVelocity = terminalVelocity / 3.0f;

            //entering water from below
            if (rb.velocity.y > 0f)
            {
                splashParticles.transform.position = transform.position + new Vector3(0f, .8f, 0f);
                splashParticles.transform.eulerAngles = new Vector3(0f, 0f, 195f);
            }
            //from above
            else
            {
                splashParticles.transform.position = transform.position - new Vector3(0f, .8f, 0f);
                splashParticles.transform.eulerAngles = new Vector3(0f, 0f, 15f);
            }
            var main = splashParticles.GetComponent<ParticleSystem>().main;
            main.startSpeed = Mathf.Abs(rb.velocity.y) * .1f;
            splashParticles.GetComponent<ParticleSystem>().Play();
        }

        if (other.gameObject.tag == "Heat")
        {
            inHeat = true;
        }

        if (other.gameObject.tag == "Gravity")
        {
            terminalVelocity /= 2.0f;
            rb.gravityScale = 1f;
        }

        if (other.gameObject.tag == "Ending")
        {
            endingObject.SetActive(true);
            this.GetComponent<PlayerMovement2>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Water")
        {
            GameObject[] sounds = GameObject.FindGameObjectsWithTag("Sound");
            foreach (GameObject sound in sounds)
            {
                sound.GetComponent<AudioSource>().volume = sound.GetComponent<AudioSource>().volume * 2.0f;
            }

            GameObject[] musics = GameObject.FindGameObjectsWithTag("Music");
            foreach (GameObject music in musics)
            {
                music.GetComponent<AudioSource>().volume = music.GetComponent<AudioSource>().volume * 2.0f;
            }

            jumpForceY = jumpForceY * 1.5f;
            jumpForceX = jumpForceX * 1.25f;
            rb.gravityScale = rb.gravityScale * 2.0f;
            terminalVelocity = terminalVelocity * 3.0f;
        }

        if (other.gameObject.tag == "Heat")
        {
            inHeat = false;
        }

        if (other.gameObject.tag == "Gravity")
        {
            terminalVelocity *= 2.0f;
            rb.gravityScale = 2f;
        }
    }

    private void beginGrounded()
    {
        if (timeGrounded == 0f)
        {
            timeGrounded += .1f;
        }
        else
        {
            timeGrounded += Time.deltaTime;
        }
    }

    private void endGrounded()
    {
        timeGrounded = 0f;
    }

    public void freezeInputs()
    {
        
        if (isSinglePlayer)
        {
            PlayerPrefs.SetInt("singleSavedFrozen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("multiSavedFrozen", 1);
        }

        if (!isFrozen)
        {
            stunSound.Play();
        }

        isFrozen = true;
        canGoLeft = false;
        canGoRight = false;
    }

    private void unfreezeInputs()
    {
        if (isSinglePlayer)
        {
            PlayerPrefs.SetInt("singleSavedFrozen", 0);
        }

        else
        {
            PlayerPrefs.SetInt("multiSavedFrozen", 0);
        }

        isFrozen = false;
        canGoLeft = true;
        canGoRight = true;
    }

    public bool IsGrounded()
    {
        //.5f = circle collider radius
        //return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        //return Physics2D.Raycast(transform.position - new Vector3(.1f, 0f, 0f), Vector2.down, .51f, jumpableGround) || Physics2D.Raycast(transform.position + new Vector3(.1f, 0f, 0f), Vector2.down, .51f, jumpableGround);
        
        return Physics2D.Raycast(transform.position, Vector2.down, .51f, jumpableGround);
    }

    private bool IsConveyorRight()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, .51f, conveyorRight);
    }

    private bool IsConveyorLeft()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, .51f, conveyorLeft);
    }

    private bool IsWalled()
    {
        return Physics2D.Raycast(transform.position, Vector2.right, .51f, jumpableGround) || Physics2D.Raycast(transform.position, Vector2.left, .51f, jumpableGround);
    }

    private bool IsCeiling()
    {    
        return Physics2D.Raycast(transform.position, Vector2.up, .51f, jumpableGround);
    }

    private bool IsLava()
    {    
        return Physics2D.Raycast(transform.position, Vector2.down, .55f, lavaLayer);
    }

    private IEnumerator bounceTimer()
    {
        canBounce = false;
        yield return new WaitForSeconds(0.02f);
        canBounce = true;
    }

    private IEnumerator goRightTimer()
    {
        canGoRight = false;
        yield return new WaitForSeconds(0.1f);
        if (!isFrozen)
        {
            canGoRight = true;
        }
    }

    private IEnumerator goLeftTimer()
    {
        canGoLeft = false;
        yield return new WaitForSeconds(0.1f);
        if (!isFrozen)
        {
            canGoLeft = true;
        }
    }
}
