using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JhonController : MonoBehaviour
{
    [Header("Movimiento")]
    [Range(1, 10)] public float velocidad;
    [Range(-10, 10)] public float raycast;

    [Header("Salto")]
    public float FuerzaSalto;
    private bool Suelo;

    [Header("Volteado")]
    public bool volteado;

    [Header("Componentes")]
    private Rigidbody2D rb2d;
    private Animator animacion;
    private SpriteRenderer sr;
    private AudioSource caminarAudioSource;

    [Header("Sonidos")]
    public AudioClip jumpSound;
    public AudioClip[] PasosSound;
    private float tiempoUltimoPaso, intervaloEntrePasos = 0.3f, longitudPaso = 0.5f;
    private Vector3 ultimoPaso;

    private float Horizontal;

    private Vector3 puntoInicio;
    private Camera mainCamera;

    private int pasosSoundLength;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animacion = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        caminarAudioSource = GetComponents<AudioSource>()[1];
        caminarAudioSource.loop = false;
        caminarAudioSource.playOnAwake = false;
        ultimoPaso = transform.position;

        mainCamera = Camera.main;

        pasosSoundLength = PasosSound.Length;
    }

    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        animacion.SetBool("corriendo", Mathf.Abs(Horizontal) > 0.0f);

        if (Horizontal > 0 && volteado) { invertirJohn(1.0f, 1.0f, 1.0f); volteado = false; }
        else if (Horizontal < 0 && !volteado) { invertirJohn(-1.0f, 1.0f, 1.0f); volteado = true; }

        Debug.DrawRay(transform.position, Vector2.down * raycast, Color.green);

        if (Physics2D.Raycast(transform.position, Vector2.down, raycast)) { Suelo = true; }
        else { Suelo = false; }

        if (Input.GetKeyDown(KeyCode.X) && Suelo) { Salto(); }

        bool saltando = animacion.GetBool("saltando");

        if (Suelo == false && saltando == false) { animacion.SetBool("saltando", true); }
        else { animacion.SetBool("saltando", false); }

        if (Mathf.Abs(rb2d.velocity.x) > 0.1f)
        {
            if (Vector3.Distance(transform.position, ultimoPaso) >= longitudPaso)
            {
                if (Time.time > tiempoUltimoPaso)
                {
                    ReproducirSonidoDePaso();
                    ultimoPaso = transform.position;
                    tiempoUltimoPaso = Time.time + intervaloEntrePasos;
                }
            }
        }
        if (!EstaDentroDeLaCamara())
        {
            VolverAlPuntoDeInicio();
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
            caminarAudioSource.clip = PasosSound[Random.Range(0, pasosSoundLength)];
            caminarAudioSource.Play();
        }
    }

    private bool seMueveEnSuelo()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, raycast))
        {
            if (Mathf.Abs(rb2d.velocity.x) > 0.1f)
            {
                return true;
            }
        }
        return false;
    }

    public void invertirJohn(float a, float b, float c)
    {
        transform.localScale = new Vector3(a, b, c);
    }
    bool EstaDentroDeLaCamara()
    {
        Vector3 posicionEnpantalla = mainCamera.WorldToViewportPoint(transform.position);

        return (posicionEnpantalla.x >= 0 && posicionEnpantalla.x <= 1 &&
                posicionEnpantalla.y >= 0 && posicionEnpantalla.y <= 1);
    }

    void VolverAlPuntoDeInicio()
    {
        transform.position = puntoInicio;
    }
}
