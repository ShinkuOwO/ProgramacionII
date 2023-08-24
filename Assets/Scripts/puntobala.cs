using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puntobala : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float intervaloDisparo;
    public GameObject JhonController;

    private Vector2 direccion;
    private bool puedeDisparar = true;

    void Update()
    {
        JhonController johnController = JhonController.GetComponent<JhonController>();
        bool estaVolteado = johnController.volteado;

        if (puedeDisparar && Input.GetKeyDown(KeyCode.Z))
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
