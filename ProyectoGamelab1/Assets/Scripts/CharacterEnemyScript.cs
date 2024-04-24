using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnemyScript : MonoBehaviour
{
    private Animator animator;

    public Rigidbody2D rb;

    public float timer;

    public float timerRate;

    public float moveSpeed;

    public float moveLeftRight;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        timer = 0;
        moveLeftRight = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag == "Enemy") { changeMovementDirectionE1(); }
        
    }

    public void changeMovementDirectionE1()
    {
        if (timer < timerRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (moveLeftRight == 1) { moveLeftRight = -1; }
            else { moveLeftRight = 1; }

            if (moveLeftRight == -1) { animator.SetFloat("moveX", 0); }
            else { animator.SetFloat("moveX", 1); }

            timer = 0;
        }

        rb.velocity = moveSpeed *
                        (Vector2.left * Convert.ToInt32(moveLeftRight == -1) +
                        Vector2.right * Convert.ToInt32(moveLeftRight == 1)) +
                        new Vector2(0, rb.velocity.y);
    }
}
