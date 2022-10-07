using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject player;
    public GameObject menu;
    public GameObject[] bloquePrefab;

    int score;

    public int enemiesToWin;          
    public int countEnemies;          

    public Camera camaraJuego;
    
    public float punteroJuego;
    public float posicionGenerar;

    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI butcherCountText;

    public bool hasPerdido;
    public float delayTimer;
    public float delayTime;

    void Start()
    {
        punteroJuego = -7;
        posicionGenerar = 50;

        hasPerdido = false;

        score = 0;

        enemiesToWin = 10;
        countEnemies = 0;

        menu = GameObject.FindGameObjectWithTag("MenuController");

        delayTime = 1.3f;
        delayTimer = delayTime;
    }

    void Update()
    {

        PlayerController controller = gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            camaraJuego.transform.position = new Vector3(player.transform.position.x + 8,
                                                         camaraJuego.transform.position.y,
                                                         camaraJuego.transform.position.z);

            score = (int)Mathf.Floor(player.transform.position.x);
            scoreText.text = "Puntaje: " + score;
            butcherCountText.text = "Carniceros: " + countEnemies;
        }
        else
        {
            if (!hasPerdido)
                hasPerdido = true;
                
            if (hasPerdido)
            {
                delayTimer -= Time.deltaTime;
                if (delayTimer <= 0.0f)
                    menu.GetComponent<MenusController>().ShowLose();
            }
        }

        while (player != null && punteroJuego < player.transform.position.x + posicionGenerar)
        {
            int indexBloque = Random.Range(0, bloquePrefab.Length - 1);

            if (punteroJuego < 0)
                indexBloque = 2;

            GameObject objetoBloque = Instantiate(bloquePrefab[indexBloque]);
            objetoBloque.transform.SetParent(this.transform);
            BlockController bloque = objetoBloque.GetComponent<BlockController>();
            objetoBloque.transform.position = new Vector2(punteroJuego + bloque.tamano / 2, 0);

            punteroJuego += bloque.tamano;
        }

        if (countEnemies >= enemiesToWin)
            menu.GetComponent<MenusController>().ShowWin();
    }

    public void EnemiesCount(int amount)
    {
        countEnemies += amount;
    }
}
