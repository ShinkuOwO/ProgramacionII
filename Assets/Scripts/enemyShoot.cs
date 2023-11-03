using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour
{
    [Header("Bala")]
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    private Vector2 direccion;

    [Header("Intervalo de Disparo")]
    public float intervaloDisparo;

    private RobotBehaviour RB;

    private void Start()
    {
        RB = FindObjectOfType<RobotBehaviour>();
    }

    public IEnumerator DispararConIntervalo(bool estaVolteado)
    {      
        Disparo(estaVolteado);
        yield return new WaitForSeconds(intervaloDisparo);      
    }

    private void Disparo(bool estaVolteado)
    {
        direccion = estaVolteado ? Vector2.left : Vector2.right;
        GameObject Bala = Instantiate(balaPrefab, puntoDisparo.position, Quaternion.identity);
        Bala.GetComponent<balaBehaviur>().darDireccion(direccion);
    }
}