using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaBehaviur : MonoBehaviour
{
    public float velocidad;

    private Rigidbody2D rb2d;
    private Vector2 Direccion;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       
    }
    
    private void FixedUpdate()
    {
        rb2d.velocity = Direccion * velocidad;
    }

    public void setDirection(Vector2 direction)
    {
        Direccion = direction;
    }
}
