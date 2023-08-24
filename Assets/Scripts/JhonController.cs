using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JhonController : MonoBehaviour
{
    [Range(1, 10)] public float velocidad;
    [Range(-10, 10)] public float raycast;
    public float FuerzaSalto,Horizontal;
    public bool volteado;
    Rigidbody2D rb2d;
    Animator animacion;
    SpriteRenderer sr;
    public AudioClip jumpSound;
    public AudioClip[] PasosSound;

    private bool Suelo,estaCaminando = false;
    private float tiempoUltimoPaso,intervaloEntrePasos = 0.3f,longitudPaso = 0.5f;
    private Vector3 ultimoPaso;
    private AudioSource caminarAudioSource;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animacion = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        caminarAudioSource = GetComponents<AudioSource>()[1];
        caminarAudioSource.loop = false;
        caminarAudioSource.playOnAwake = false;
        ultimoPaso = transform.position;
    }
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal"); // -1,0,1 
        animacion.SetBool("corriendo", Horizontal != 0.0f);
        if (Horizontal > 0){invertirJohn(1.0f, 1.0f, 1.0f); volteado = false;}
        else if (Horizontal < 0){invertirJohn(-1.0f,1.0f,1.0f); volteado = true;}
        Debug.DrawRay(transform.position, Vector2.down * raycast, Color.green);       
        if (Physics2D.Raycast(transform.position, Vector2.down, raycast)){Suelo = true;}
        else{Suelo = false;}
        if (Input.GetKeyDown(KeyCode.X) && Suelo){Salto();}
        if (!Input.anyKey){animacion.SetBool("corriendo", false);}
        bool saltando = animacion.GetBool("saltando");       
        if (Suelo == false && saltando == false) { animacion.SetBool("saltando", true);}
        else { animacion.SetBool("saltando", false);}
        if (Vector3.Distance(transform.position, ultimoPaso) >= longitudPaso)
        {
            if (Time.time > tiempoUltimoPaso) {
                ReproducirSonidoDePaso();
                ultimoPaso = transform.position;
                tiempoUltimoPaso = Time.time + intervaloEntrePasos;
            }
        }

    }  
    private void Salto()
    {
        rb2d.AddForce(Vector2.up * FuerzaSalto);
        GetComponent<AudioSource>().PlayOneShot(jumpSound);
    }
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(Horizontal * velocidad, rb2d.velocity.y);
    }
    private void ReproducirSonidoDePaso()
    {
        if (seMueveEnSuelo())
        {
            caminarAudioSource.clip = PasosSound[Random.Range(0, PasosSound.Length)];
            caminarAudioSource.Play();
        }
            
    }

    private bool seMueveEnSuelo()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, raycast)) {
            if (Mathf.Abs(Horizontal) > 0.0f)
            {
                return true;
            }
        }
        return false;
    }
    public void invertirJohn(float a,float b, float c)
    {
        transform.localScale = new Vector3(a, b, c);       
    }
}

