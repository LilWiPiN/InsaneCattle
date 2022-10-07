using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    bool onFloor;
    bool crossPickUp;

    public float insanityStatusTime; 
    float insanityStatusTimer;       
    public bool isInsane;            

    public int jumpForce;
    public float initialSpeed;        
    public float insaneSpeed;         
    public float currentSpeed;        

    public AudioSource deathSound;    

    void Start()
    {
        QualitySettings.vSyncCount = 0;

        initialSpeed = 7.0f;
        insaneSpeed = 10.0f;
        currentSpeed = initialSpeed;
        jumpForce = 500;

        isInsane = false;
        insanityStatusTime = 10.0f;
    }

    void Update()
    {
        Time.timeScale = 1.0f;

        if (Input.GetKeyDown("space") && onFloor)
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(currentSpeed + 0.5f,
                                                    this.GetComponent<Rigidbody2D>().velocity.y);

        if (isInsane)
        {
            insanityStatusTimer -= Time.deltaTime;
            if (insanityStatusTimer <= 0.0f)
            {
                ChangeOriginalSpeed();
                isInsane = false;
                insanityStatusTimer = insanityStatusTime;
            }
        }
    }

    public void ChangeSpeed()
    {
        insanityStatusTimer += insanityStatusTime;
        currentSpeed = insaneSpeed;
        isInsane = true;
    }

    public void ChangeOriginalSpeed()
    {
        currentSpeed = initialSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        onFloor = true;

        if (collision.tag == "Enemy" && isInsane)
        {
            enemy.EnableDeath();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onFloor = false;
        if (collision.tag == "Enemy" && isInsane)
        {
            onFloor = true;
        }
    }

    public void EnableDeath()
    {
        GameObject.Destroy(this.gameObject);
        Instantiate(deathSound);
    }
}