using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController2 : MonoBehaviour
{
    float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    //Variables del movimiento del personaje
    public float jumpForce = 6f;
    public float runningSpeed = 2f;
    private float keyStrokes;
    private int frames = 0;

    Rigidbody2D rigidBody;
    Animator animator;

    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    public LayerMask ground;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
    }

    // Update is called once per frame
    void Update()
    {
        frames++;
        if (frames % 30 == 0)
        {
            frames = 0;
            keyStrokes = 0.0f;
            runningSpeed = 0.0f;
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        else if(Input.GetKeyDown("d"))
        {

            keyStrokes++;
        }
        else if (Input.GetKeyDown("a"))
        {

            keyStrokes++;
        }
        runningSpeed = Map(keyStrokes, 1.0f, 10.0f, 2.0f, 12.0f);
        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());

        Debug.DrawRay(this.transform.position, Vector2.down * 1.5f, Color.red);
    }
    void FixedUpdate()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)//cambiarloDesdelaEscena1
        {
             if (rigidBody.velocity.x < runningSpeed)
             {
             rigidBody.velocity = new Vector2(runningSpeed,rigidBody.velocity.y);
             }
        }else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }

    void Jump()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)// solo puede saltar si esta en modo juego
        {
            if (IsTouchingTheGround())
            {
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    //Nos indica si el personaje está o no tocando el suelo
    bool IsTouchingTheGround()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 1.5f, ground))
        {
            animator.enabled = true;
            return true;
        }
        else
        {
            //TODO: programar lógica de no contacto
            animator.enabled = false;
            return false;
        }
    }

}
