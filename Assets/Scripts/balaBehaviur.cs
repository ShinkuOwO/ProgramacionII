using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaBehaviur : MonoBehaviour
{
    [Header("Configuraci�n de la Bala")]
    public float velocidad;

    private Rigidbody2D rb2d;
    private Vector2 Direccion;
    private SpriteRenderer sr;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = Direccion * velocidad;
    }

    public void darDireccion(Vector2 direccion)
    {
        Direccion = direccion;
    }

    private void OnBecameInvisible()
    {     
        Destroy(gameObject);
    }
}

