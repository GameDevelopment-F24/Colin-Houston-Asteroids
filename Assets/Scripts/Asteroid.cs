using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

//using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;

//using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : MonoBehaviour
{
    public GameManager gm;
    private float leftXMin = -15f;
    private float leftXMax = -12f;
    private float rightXMin = 12f;
    private float rightXMax = 15f; 
    private float horXMin = -10f;
    private float horXMax = 10f;
    private float bottomYMin = -6f;
    private float bottomYMax = -4f;
    private float topYMin = 4f;
    private float topYMax = 6f;
    public float rotationSpeed;
    public float movementSpeed = 5f;
    public char direction;
    public Rigidbody2D rb;
    public Vector2 velocity;
    public Vector2 initPos;
    public Asteroid ast;
    public bool tiny;
    public int xMin;
    public int xMax;
    public int yMin;
    public int yMax;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetVelocity();
        gm = FindObjectOfType<GameManager>();
        
    }

    void Update()
    {
        if (transform.position.x > xMax || transform.position.x < xMin) { Destroy(gameObject); }
        if (transform.position.y > yMax || transform.position.y < yMin) { Destroy(gameObject); }
    }

    public void Spawn()
    {
        Instantiate(ast, initPos, Quaternion.identity);
    }

    public void SetVelocity(){
        if (direction == 'l'){ velocity = new Vector2(-movementSpeed, 0); }
        else if (direction == 'r'){ velocity = new Vector2(movementSpeed, 0); }
        else if (direction == 'd'){ velocity = new Vector2(0, -movementSpeed); }
        else{ velocity = new Vector2(0, movementSpeed); }
        rb.velocity = velocity;
        Debug.Log("velocity set to " + rb.velocity);
        if (rb == null) {Debug.Log("uh oh");}
        Debug.Log($"Asteroid velocity set to {rb.velocity} in direction {direction}");
    }

    public void OnTriggerEnter2D(Collider2D col){
        Debug.Log("Collision detected with asteroid");
        if (col.CompareTag("Bullet")){
            Vector2 pos = transform.position;
            Destroy(gameObject);
            Destroy(col.gameObject);
            if (!tiny) {gm.SpawnAsteroids(true, new Vector2(pos.x, pos.y));}
        }
    }

    public Vector3 RandomPositionLeft() 
    {
        float randomX = Random.Range(leftXMin, leftXMax);
        float randomY = Random.Range(bottomYMin, topYMax);
        return new Vector2(randomX, randomY);
    }

    public Vector2 RandomPositionRight() 
    {
        float randomX = Random.Range(rightXMin, rightXMax);
        float randomY = Random.Range(bottomYMin, topYMax);
        return new Vector2(randomX, randomY);
    }

    public Vector2 RandomPositionTop() 
    {
        float randomX = Random.Range(horXMin, horXMax);
        float randomY = Random.Range(topYMin, topYMax);
        return new Vector2(randomX, randomY);
    }

    public Vector2 RandomPositionBottom() 
    {
        float randomX = Random.Range(horXMin, horXMax);
        float randomY = Random.Range(bottomYMin, bottomYMax);
        return new Vector2(randomX, randomY);
    }

    public Vector2 RandomPosition()
    {
        int randomX = Random.Range(1,4);
        if (randomX == 1){direction = 'l'; return RandomPositionRight();}
        else if (randomX == 2){direction = 'r'; return RandomPositionLeft();}
        else if (randomX == 3){direction = 'd'; return RandomPositionTop();}
        else {direction = 'u'; return RandomPositionBottom();}
        
    }



}
