using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaBehaviur : MonoBehaviour
{
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
        // El objeto se ha vuelto invisible para la cámara, así que destrúyelo
        Destroy(gameObject);
    }
}
