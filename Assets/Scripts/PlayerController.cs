using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;
    public float jumpForce = 700;
    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D playerRigidboody;
    private Animator animator;
    private AudioSource playerAudio;
        void Start()
    {
        playerRigidboody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            return;
        }
        if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;
            playerRigidboody.velocity = Vector2.zero;
            playerRigidboody.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play();
        }
        else if(Input.GetMouseButtonUp(0) && playerRigidboody.velocity.y > 0)
        {
            playerRigidboody.velocity = playerRigidboody.velocity * 0.5f;
        }
        animator.SetBool("Grounded", isGrounded);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Dead" && isDead)
        {
            Die();
        }
        
    }
    private void Die()
    {
        animator.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerRigidboody.velocity = Vector2.zero;
        isDead = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts[0].normal.y >= 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
