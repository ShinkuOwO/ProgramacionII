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
    private bool puedeDisparar = true;

    private RobotBehaviour RB;

    private void Start()
    {
        RB = FindObjectOfType<RobotBehaviour>();
    }

    private void Update()
    {
        bool estaVolteado = RB.GetVolteado();

        if (puedeDisparar)
        {
            StartCoroutine(DispararConIntervalo(estaVolteado));
        }
    }

    private IEnumerator DispararConIntervalo(bool estaVolteado)
    {
        puedeDisparar = false;

        Disparo(estaVolteado);

        yield return new WaitForSeconds(intervaloDisparo);

        puedeDisparar = true;
    }

    private void Disparo(bool estaVolteado)
    {
        direccion = estaVolteado ? Vector2.left : Vector2.right;

        GameObject Bala = Instantiate(balaPrefab, puntoDisparo.position, Quaternion.identity);
        Bala.GetComponent<balaBehaviur>().darDireccion(direccion);
    }
}
