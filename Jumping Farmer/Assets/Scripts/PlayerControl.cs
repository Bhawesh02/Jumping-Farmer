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
    private bool doubleJump = false;

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
        transform.position = new Vector3(0,transform.position.y,0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            dirtParticle.Stop();
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound);
            doubleJump = true;   
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !gameOver && doubleJump)
        {
            playerRb.AddForce(Vector3.up * (jumpForce/1.3f),ForceMode.Impulse);
            Debug.Log("Double Jump");
            playerAnim.SetTrigger("doubleJump");
            doubleJump = false;
            playerAudio.PlayOneShot(jumpSound);
        }
        if (gameOver)
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
            Debug.Log("On Ground");
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
