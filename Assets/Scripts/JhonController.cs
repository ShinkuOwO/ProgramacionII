using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JhonController : MonoBehaviour
{
    [Range(1, 10)] public float velocidad;
    [Range(-10, 10)] public float raycast;
    public float FuerzaSalto;
    private float Horizontal;
    private bool Suelo;

    Rigidbody2D rb2d;
    Animator animacion;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animacion = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
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

        //Esta línea de código está dibujando un raycast desde la posición del personaje hacia abajo para verificar si el personaje está en el suelo. 
        Debug.DrawRay(transform.position, Vector2.down * raycast, Color.green); 
        //Esta sección de código está verificando si el personaje está en el suelo. Esto se hace mediante un raycast que se dibuja desde la posición del personaje hacia abajo. Si el raycast colisiona con algo, entonces el personaje está en el suelo. Esto se guarda en la variable booleana "Suelo".
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
    
    //El método Salto() se encarga de aplicar una fuerza hacia arriba al Rigidbody2D del objeto, para que este realice un salto. 
    //Recibe como parámetro una fuerza de salto, la cual se asigna en el Inspector de Unity.
    private void Salto()
    {
        rb2d.AddForce(Vector2.up * FuerzaSalto);
    }
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(Horizontal * velocidad, rb2d.velocity.y);

    }
}

