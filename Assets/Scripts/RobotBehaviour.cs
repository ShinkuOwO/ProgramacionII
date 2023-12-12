using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Video;

public class RobotBehaviour : MonoBehaviour
{
    public GameObject Player;
    public GameObject Manzanita;
    private float ultimoDisparo;
    public float delayDisparo;
    int vida = 100;
    private enemyShoot eS;
    private SpawnGrunt spawnGrunt;
    int gruntsExistentes;
    int limitegrunts;
    [Header("Volteado")]
    private bool volteado;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        eS = FindObjectOfType<enemyShoot>();
        spawnGrunt = FindObjectOfType<SpawnGrunt>();
    }
    void Update()
    {
        if (Player != null) {

            gruntsExistentes = spawnGrunt.GetGruntsExistentes();
            limitegrunts = spawnGrunt.GetlimiteGrunts();
            MirarAlPlayer();
            DisparoEnemigo();
        }
        
    }
    private void DisparoEnemigo()
    {
        float distancia = Mathf.Abs(Player.transform.position.x - transform.position.x);
                   
            if (distancia < 8f && Time.time > ultimoDisparo + delayDisparo)
            {
                if (spawnGrunt != null && gruntsExistentes < limitegrunts)
                {
                    StartCoroutine(eS.DispararConIntervalo(volteado));
                    ultimoDisparo = Time.time;
                }
            }
        if (distancia > 6f && distancia < 15.0f)
        {
            Vector2 direccionNueva = (Player.transform.position - transform.position).normalized;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(direccionNueva.x * 5f,rb.velocity.y);
        }
        else
        {
            
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero; 
        }


    }
    private void MirarAlPlayer()
    {
        if(Player != null) 
        {
            Vector3 direccion = Player.transform.position - transform.position;
            if (direccion.x >= 0.0f) { transform.localScale = new Vector3(6.0f, 6.0f, 1.0f); volteado = false; }
            else { transform.localScale = new Vector3(-6.0f, 6.0f, 1.0f); volteado = true; }
        }

        }
    public bool GetVolteado()
    {
        return volteado;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bala"))
        {
            vida -= 10;
            if (vida <= 0)
            {
                Destroy(gameObject);
                Instantiate(Manzanita, transform.position, quaternion.identity);
                if (spawnGrunt != null)
                {
                    spawnGrunt.ReduceGruntCount();
                }
            }
        }
    }
}