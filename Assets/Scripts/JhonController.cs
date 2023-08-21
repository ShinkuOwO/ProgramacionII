using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JhonController : MonoBehaviour
{
    public GameObject balaPrefab;
    [Range(1, 10)] public float velocidad;
    [Range(-10, 10)] public float raycast;
    public float FuerzaSalto;
    private float Horizontal;
    private bool Suelo;
    private Vector3 flipbala;

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

        johnVolteado(Horizontal);

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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Disparo(Horizontal);
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
    private void Disparo(float johnflip)

    {
        if (johnflip > 0) 
        {
            flipbala = Vector2.right;
        }
        else if (johnflip < 0)
        {
            flipbala = Vector2.left;
        }

        GameObject Bala = Instantiate(balaPrefab,transform.position + flipbala * 0.1f ,Quaternion.identity);
        Bala.GetComponent<balaBehaviur>().setDirection(flipbala);
    }
    
    public void johnVolteado(float horizontal) 
    {
        if (horizontal > 0)
        {
            sr.flipX = false;
        }
        else if (horizontal < 0)
        {
            sr.flipX = true;
        }
    }

}

