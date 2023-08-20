using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JhonController : MonoBehaviour
{
    public GameObject Bala;
    [Range(1, 10)] public float velocidad;
    [Range(-10, 10)] public float raycast;
    public float FuerzaSalto;
    private float Horizontal;
    private bool Suelo;

    
    Rigidbody2D rb2d;
    Animator animacion;
    SpriteRenderer sr;

    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animacion = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

   
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal"); // -1,0,1 

        animacion.SetBool("corriendo", Horizontal != 0.0f);

        if (Horizontal > 0)
        {
            sr.flipX = false;
        }
        else if (Horizontal < 0)
        {
            sr.flipX = true;
        }

        
        Debug.DrawRay(transform.position, Vector2.down * raycast, Color.green); 
        
        if (Physics2D.Raycast(transform.position, Vector2.down, raycast))
        {
            Suelo = true;
        }
        else
        {
            Suelo = false;
        }

        if (Input.GetKeyDown(KeyCode.X) && Suelo)
        {
            Salto();
        }
    }
    
    private void Salto()
    {
        rb2d.AddForce(Vector2.up * FuerzaSalto);
    }
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(Horizontal * velocidad, rb2d.velocity.y);

    }
    private void Disparo()

    {
        GameObject instanciarBala = Instantiate(Bala,transform.position,Quaternion.identity);
    }
}

