using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour
{
    public GameObject Player;
    private float ultimoDisparo;
    public float delayDisparo;

    [Header("Volteado")]
    public bool volteado;

    // Update is called once per frame
    void Update()
    {
        MirarAlPlayer();
        DisparoEnemigo();
    }
    private void DisparoEnemigo()
    {
        float distancia = Mathf.Abs(Player.transform.position.x - transform.position.x);
        if(distancia > 1.0f && Time.time > ultimoDisparo + delayDisparo) 
        {
            Debug.Log("Enemigo Disparando"); 

            ultimoDisparo = Time.time;
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
}