using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayerMovementScript : MonoBehaviour
{
    public GameManagerScript GMS;

    private Animator animator;

    public float moveLeftRight;

    public int verticalMovement;

    public bool isMoving;

    public bool canJump;

    public bool isOnTheGround;

    public bool canStomp;

    public Rigidbody2D rb;

    public float moveSpeed;

    private float justSpeed;

    public float jumpSpeed;

    public float stompSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isMoving = false; canJump = false; canStomp = false;
        verticalMovement = 0; isOnTheGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        processMoveInputs();
        processJuStInputs();
    }

    // Update is called less than once per frame, but is more for physics
    private void FixedUpdate()
    {

    }

    public void processMoveInputs()
    {
        moveLeftRight = UnityEngine.Input.GetAxisRaw("Horizontal");
        
        if(moveLeftRight == -1) { animator.SetFloat("moveX", 0); }
        else if(moveLeftRight == 1) { animator.SetFloat("moveX", 1); }

        isMoving = (moveLeftRight == -1 || moveLeftRight == 1);

        animator.SetBool("isMoving", isMoving);

        rb.velocity =  moveSpeed * 
                        (Vector2.left * Convert.ToInt32(moveLeftRight == -1) + 
                        Vector2.right * Convert.ToInt32(moveLeftRight == 1)) +
                        new Vector2(0, rb.velocity.y);
        
        GMS.moveCameraHorizontal(transform.position.x);
    }
    
    public void processJuStInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (canJump) 
            { 
                canJump = true; isOnTheGround = false;
                verticalMovement = 1; justSpeed = jumpSpeed;
                rb.velocity = Vector2.up * justSpeed;
                verticalMovement = 0;
            }
            if (!isOnTheGround)
            {
                // when player jumps off the ground, the next jumps will generate a little ellipse in the air
                // that will dissappear in 1 second.
                Instantiate(gameObject, new Vector2(0,0), Quaternion.identity);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (canStomp) 
            { 
                canStomp = true; verticalMovement = -1; justSpeed = stompSpeed;
                rb.velocity = Vector2.down * justSpeed;
                verticalMovement = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ForegroundFloor") || collision.gameObject.CompareTag("Platform"))
        {
            //Debug.Log("Colision con el piso");
            if (!canJump) { canJump = true; }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (this.transform.position.y > collision.gameObject.transform.position.y)
            {
                Debug.Log("Enemigo derrotado");
                Destroy(collision.gameObject);
                rb.velocity = rb.velocity * Convert.ToInt32(rb.velocity.y > 0) + Vector2.up * 4.5f;
            }
            else
            {
                Debug.Log("Has sido derrotado");
            }
        }
        else if (collision.relativeVelocity.y > 0)
        {
            isOnTheGround = true;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ForegroundFloor") || collision.gameObject.CompareTag("Platform"))
        {
            //Debug.Log("Fuera de el piso");
            if (!canStomp) { canStomp = true; }
        }
    }
}
