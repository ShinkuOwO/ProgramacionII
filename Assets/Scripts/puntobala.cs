using System.Collections;
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

    private ControladorJuan cj;

    private void Start()
    {
        cj = FindObjectOfType<ControladorJuan>();
    }

    private void Update()
    {
        bool estaVolteado = cj.GetVolteado();

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
        direccion = estaVolteado ? Vector2.left : Vector2.right;

        GameObject Bala = Instantiate(balaPrefab, puntoDisparo.position, Quaternion.identity);
        Bala.GetComponent<balaBehaviur>().darDireccion(direccion);
    }
}