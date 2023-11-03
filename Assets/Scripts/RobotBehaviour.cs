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

    [Header("Volteado")]
    private bool volteado;
    private void Start()
    {
        eS = FindObjectOfType<enemyShoot>();
    }
    void Update()
    {
        
        MirarAlPlayer();
        DisparoEnemigo();
    }
    private void DisparoEnemigo()
    {
        float distancia = Mathf.Abs(Player.transform.position.x - transform.position.x);
        if (distancia < 10.0f && Time.time > ultimoDisparo + delayDisparo)
        {
            if (eS != null) // Verifica que la referencia sea válida
            {
                StartCoroutine(eS.DispararConIntervalo(volteado));
                ultimoDisparo = Time.time;
            }
        }
    }
    private void MirarAlPlayer()
    {
        Vector3 direccion = Player.transform.position - transform.position;
        if (direccion.x >= 0.0f) { transform.localScale = new Vector3(6.0f, 6.0f, 1.0f); volteado = false; }//mirando a la derecha osea default.
        else { transform.localScale = new Vector3(-6.0f, 6.0f, 1.0f); volteado = true; }// de lo contrario mirando a la izquierda.
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
            }
        }
    }
}