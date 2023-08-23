using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JhonController : MonoBehaviour
{   
    [Range(1, 10)] public float velocidad;
    [Range(-10, 10)] public float raycast;
    public float FuerzaSalto;
    private float Horizontal;
    private float ultimoCambioAudio;
    private bool Suelo;
    Rigidbody2D rb2d;
    Animator animacion;
    public SpriteRenderer sr;
    public AudioClip jumpSound;
    public AudioClip caminar1;
    public AudioClip caminar2;

    private AudioSource caminarAudioSource;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animacion = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        caminarAudioSource = GetComponents<AudioSource>()[1];
    }
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal"); // -1,0,1 
        animacion.SetBool("corriendo", Horizontal != 0.0f);
        if (Horizontal > 0){sr.flipX = false;}
        else if (Horizontal < 0){sr.flipX = true;}
        Debug.DrawRay(transform.position, Vector2.down * raycast, Color.green);       
        if (Physics2D.Raycast(transform.position, Vector2.down, raycast)){Suelo = true;}
        else{Suelo = false;}
        if (Input.GetKeyDown(KeyCode.X) && Suelo){Salto();}
        if (!Input.anyKey){animacion.SetBool("corriendo", false);}
        bool saltando = animacion.GetBool("saltando");       
        if (Suelo == false && saltando == false) { animacion.SetBool("saltando", true);}
        else { animacion.SetBool("saltando", false);}
        bool caminando = animacion.GetBool("corriendo");
        if (caminando)
        {
            if (Time.time - ultimoCambioAudio >= 0.5f)
            {
                caminarAudioSource.clip = Random.Range(0, 2 + 1) == 0 ? caminar1 : caminar2;
                caminarAudioSource.Play();
                ultimoCambioAudio = Time.time;}
            }
        else{caminarAudioSource.Stop();}
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
}

