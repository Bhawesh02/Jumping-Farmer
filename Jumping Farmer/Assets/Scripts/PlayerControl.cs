using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody playerRb ;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private MoveLeft moveLeft;
    public ParticleSystem explosionParticles;
    public ParticleSystem dirtParticles;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private float playerScore = 0;
    private float walkSpeed = 1.5f;
    public float jumpForce = 13;
    public float gravityModifier = 2.0f;
    public bool isOnGround = true;
    public bool gameOver = false;
    private bool doubleJump = false;
    public bool reaStratPos = false;
    private bool initDirtAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = GameObject.Find("Background").GetComponent<MoveLeft>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAnim.speed = 0.49f;
        playerAudio = GetComponent<AudioSource>();
        playerAnim.SetBool("Static_b", false);
        Physics.gravity *= gravityModifier;
        Debug.Log("Score= " + playerScore);
        InvokeRepeating("ShowScore",1,1f);
    }

    // Update is called once per frame
    void Update()
    {   if (transform.position.x < 0)
        {
            transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);

        }
        else
        {
            reaStratPos = true;
        }

        if (reaStratPos)
        {
            playerAnim.SetBool("Static_b", true);
            transform.position = new Vector3(0, transform.position.y, 0);
            transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            playerAnim.speed = MoveLeft.speedMultiplier;
            if (!initDirtAnim)
            {
                dirtParticles.Play();
                initDirtAnim = true;
            }
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                dirtParticles.Stop();
                isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
                playerAudio.PlayOneShot(jumpSound);
                doubleJump = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !gameOver && doubleJump)
            {
                playerRb.AddForce(Vector3.up * (jumpForce / 1.3f), ForceMode.Impulse);
                playerAnim.SetTrigger("doubleJump");
                doubleJump = false;
                playerAudio.PlayOneShot(jumpSound);
            }
            if (gameOver)
            {
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
            }
        }
    }
    private void ShowScore()
    {
        if (!gameOver && reaStratPos)
        {
            playerScore += 10 * MoveLeft.speedMultiplier;
            Debug.Log("Score= " + playerScore);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if (initDirtAnim && !gameOver)
            {
                dirtParticles.Play();
            }
            isOnGround = true;
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            dirtParticles.Stop();
            explosionParticles.Play();
            playerAudio.PlayOneShot(crashSound);
        }
    }
}
