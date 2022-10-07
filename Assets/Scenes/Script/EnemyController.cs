using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyController : MonoBehaviour
{
    public GameObject scene;

    public AudioSource deathSound;

    void Start()
    {
        scene = GameObject.FindGameObjectWithTag("SceneController");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null && !player.isInsane)
            player.EnableDeath();
    }

    public void EnableDeath()
    {
        Instantiate(deathSound);
        Destroy(gameObject);
        scene.GetComponent<SceneController>().EnemiesCount(1);
    }
}