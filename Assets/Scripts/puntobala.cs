using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puntobala : MonoBehaviour
{
    [Header("Bala")]
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    private Vector2 direccion;

    [Header("Intervalo de Disparo")]
    public float intervaloDisparo;
    private bool puedeDisparar = true;

    [Header("Controlador de Personaje")]
    public GameObject JhonController;

    void Update()
    {
        JhonController johnController = JhonController.GetComponent<JhonController>();
        bool estaVolteado = johnController.volteado;

        if (puedeDisparar && Input.GetKeyDown(KeyCode.C))
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
        if (estaVolteado == false)
        {
            direccion = Vector2.right;
        }
        else if (estaVolteado == true)
        {
            direccion = Vector2.left;
        }

        GameObject Bala = Instantiate(balaPrefab, transform.position, Quaternion.identity);
        Bala.GetComponent<balaBehaviur>().darDireccion(direccion);
    }
}
