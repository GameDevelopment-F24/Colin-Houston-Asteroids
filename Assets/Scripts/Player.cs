using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float speed = 5f;

    public float rotationSpeed = 200f;
    public float thrustForce = 10f;
    public float drag = 0.95f;
    public Vector2 spawnPos = new Vector2(0,0);
    public InputAction playerControls;

    public GameManager gameManager;
    public GameObject bulletPrf;
    public Transform shootingPoint;
    //public int lives;


    Vector2 moveDirection = Vector2.zero;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //if (Settings.livesSet != 0) { lives = Settings.livesSet; } else { lives = 3; }
        //lives = gameManager.lives;
        gameManager.SetLivesText();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();

    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    

    // Update is called once per frame
    void Update()
    {
      // RotatePlayer();
      HandleMovement();
      HandleShooting();
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Asteroid")){
            Debug.Log("asteroid hit");
            Death();
        }
        else if (other.CompareTag("Border")){
            rb.velocity = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        // RotatePlayer();
        // MovePlayer();
       // HandleMovement();
    }
    void HandleMovement()
    {
         // Rotate
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

        // Thrust
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            Vector2 thrustDirection = transform.up * thrustForce;
            rb.AddForce(thrustDirection);
        }

        // Apply drag for drifting effect
        //rb.velocity *= dragAmount; // This reduces the velocity gradually
        ApplyDrag();
    }

    void ApplyDrag()
    {
        // Apply drag to reduce velocity gradually
        rb.velocity *= drag;

        // Optionally limit the minimum speed to prevent complete stop
        if (rb.velocity.magnitude < 0.1f)
        {
            rb.velocity = Vector2.zero;
        }
    }

    // void RotatePlayer(){
    //     float rotationInput = Input.GetAxis("Horizontal");
    //     float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;
    //     transform.Rotate(0,0,-rotationAmount);
    // }

    // void MovePlayer()
    // {
    //     if (Input.GetKey(KeyCode.W))
    //     {
    //         Vector3 direction = transform.up;
    //         transform.position += direction * movementSpeed * Time.deltaTime;
    //     }
    // }

    void HandleShooting()
    {
        // Shoot when Space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrf, shootingPoint.position, shootingPoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = transform.up * bullet.GetComponent<Bullet>().speed;
    }

    public void Death()
    {
        if (gameManager.lives == 0)
        {
            gameManager.GameOver();
        } else {
            gameManager.lives --;
            gameManager.ResetGame();
        }
    }
}
