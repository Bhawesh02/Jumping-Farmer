using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody playerRb ;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticles;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 13;
    public float gravityModifier = 2.0f;
    private bool isOnGround = true;
    public bool gameOver = false;
    private bool doubleJump = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();  
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            dirtParticle.Stop();
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound);
            if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !gameOver && doubleJump)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                doubleJump = false;
                playerAnim.SetTrigger("Jump_trig");
                playerAudio.PlayOneShot(jumpSound);
            }
        }
        if(gameOver)
        {
            dirtParticle.Stop();
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            doubleJump = true;
            dirtParticle.Play();
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            explosionParticles.Play();
            playerAudio.PlayOneShot(crashSound);
        }
    }
}
