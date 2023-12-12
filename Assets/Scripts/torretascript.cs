using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torretascript : MonoBehaviour
{
    private Transform player; 
    private Animator animator;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float tiempoEntreDisparos = 1.5f;
    private bool puedeDisparar = true;
    float velocidadProyectil = 10f;
    int vidaTorreta;
    private Vector2 direccion;
    int vidamaxima = 100;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        animator = GetComponent<Animator>();
        vidaTorreta = vidamaxima;
    }

    void Update()
    {
        float distancia = Mathf.Abs(player.transform.position.x - transform.position.x);
        if (player != null)
        {
            if(distancia < 10.0f)
            {
                Vector3 dir = player.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                if (angle < 0)
                {
                    angle += 360;
                }
                int estadoActual = ObtenerEstadoPorGrados(angle);
                animator.SetInteger("estado", estadoActual);
                Debug.Log("Ángulo entre la torreta y el jugador: " + angle);
                if (puedeDisparar)
                {
                    StartCoroutine(Disparar(angle));
                }
            }            
        }
    }
    private IEnumerator Disparar(float angle)
    {
        
        puedeDisparar = false;
        GameObject Balatorreta = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb2dB = Balatorreta.GetComponent<Rigidbody2D>();
        Vector2 direccion = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        rb2dB.velocity = direccion * velocidadProyectil;
        yield return new WaitForSeconds(tiempoEntreDisparos);      
        puedeDisparar = true;
    }

    private int ObtenerEstadoPorGrados(float grados)
    {
        
        grados = Mathf.Round(grados);

        if ((grados >= 337.5 && grados <= 360) || (grados >= 0 && grados < 22.5))
        {
            return 4; 
        }
        else if (grados >= 22.5 && grados < 67.5)
        {
            return 3; 
        }
        else if (grados >= 67.5 && grados < 112.5)
        {
            return 0; 
        }
        else if (grados >= 112.5 && grados < 157.5)
        {
            return 2; 
        }
        else if (grados >= 157.5 && grados < 202.5)
        {
            return 1; 
        }
        else if (grados >= 202.5 && grados < 247.5)
        {
            return 7; 
        }
        else if (grados >= 247.5 && grados < 292.5)
        {
            return 6; 
        }
        else if (grados >= 292.5 && grados < 337.5)
        {
            return 5; 
        }

        
        return -1;
    }
}