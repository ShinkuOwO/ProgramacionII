using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;

public class ControladorJuan : MonoBehaviour
{
    private int vida ;
    private int vidamaxima = 200;
    public Slider Barravidajuan;
    [Header("Movimiento")]
    [Range(1, 10)] public float velocidad;
    [Range(-10, 10)] public float distanciaRayo;

    [Header("Salto")]
    public float FuerzaSalto;
    private bool EnSuelo;

    [Header("Volteado")]
    public bool volteado;

    [Header("Componentes")]
    private Rigidbody2D rb2d;
    private Animator animacion;
    private AudioSource fuenteAudioCaminar;

    [Header("Sonidos")]
    public AudioClip sonidoSalto;
    public AudioClip[] sonidosPasos;
    private float tiempoUltimoPaso;
    private float intervaloEntrePasos = 0.3f;
    private float longitudPaso = 0.5f;
    private Vector3 ultimaPosicionPaso;
    private int longitudSonidosPasos;

    [Header("C�mara Jugador")]
    private Camera camaraPrincipal;
    public float EjeZCamara;

    private float Horizontal;
    private int estado;
    private Vector3 puntoInicio;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animacion = GetComponent<Animator>();
        fuenteAudioCaminar = GetComponents<AudioSource>()[1];
        fuenteAudioCaminar.loop = false;
        fuenteAudioCaminar.playOnAwake = false;
        ultimaPosicionPaso = transform.position;
        vida = vidamaxima;
        Barravidajuan.maxValue = vida;
        camaraPrincipal = Camera.main;

        longitudSonidosPasos = sonidosPasos.Length;

        puntoInicio = transform.position;
    }

    private void Update()
    {
        Barravidajuan.value = vida;      
        ManejarEntrada();
        ManejarSalto();             
        ManejarSonidos();
    }

    private void FixedUpdate()
    {
        if(vida > 0)
        {
            ManejarMovimiento();
        }        
        ManejarCamara();
    }

    private void ManejarEntrada()
    {
        if (vida <= 0)
        {
            estado = 4;
            Destroy(gameObject,5f);
        }
        animacion.SetInteger("Estado", estado);
        Horizontal = Input.GetAxis("Horizontal");
        if (Horizontal != 0) {
            estado = 1;
        }
        else if (Horizontal == 0) {  estado = 0; }
        EnSuelo = Physics2D.Raycast(transform.position, Vector2.down, distanciaRayo);
    }

    private void ManejarMovimiento()
    {
        rb2d.velocity = new Vector2(Horizontal * velocidad, rb2d.velocity.y);
        VoltearPersonaje();
    }

    private void ManejarSalto()
    {
        if (Input.GetKeyDown(KeyCode.X) && EnSuelo)
        {
            estado = 2;
            rb2d.AddForce(Vector2.up * FuerzaSalto);
            fuenteAudioCaminar.PlayOneShot(sonidoSalto);
        }
    }


    private void ManejarSonidos()
    {
        if (Mathf.Abs(rb2d.velocity.x) > 0.1f)            
        {
            if(EsSuficientementeLargoParaSonidoPaso())
            {
                if(Time.time > tiempoUltimoPaso)
                {
                    ReproducirSonidoDePaso();
                    ultimaPosicionPaso = transform.position;
                    tiempoUltimoPaso = Time.time + intervaloEntrePasos;
                }                
            }          
        }
    }

    private bool EsSuficientementeLargoParaSonidoPaso()
    {
        return Vector3.Distance(transform.position, ultimaPosicionPaso) >= longitudPaso;
    }

    private void ReproducirSonidoDePaso()
    {
        if (SeMueveEnSuelo())
        {
            fuenteAudioCaminar.clip = sonidosPasos[Random.Range(0, longitudSonidosPasos)];
            fuenteAudioCaminar.Play();
        }

    }

    private bool SeMueveEnSuelo()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, distanciaRayo))
        {
            if (Mathf.Abs(rb2d.velocity.x) > 0.1f)
            {
                return true;
            }
        }
        return false;
    }

    private void VoltearPersonaje()
    {
        if ((Horizontal > 0 && volteado) || (Horizontal < 0 && !volteado))
        {
            volteado = !volteado;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void ManejarCamara()
    {
        if (!EstaDentroDeLaCamara())
        {
            VolverAlPuntoDeInicio();
        }
   
        Vector3 dondeEstoy = camaraPrincipal.transform.position;
        Vector3 dondeQuieroIr = transform.position - new Vector3(0, 0, EjeZCamara);
        camaraPrincipal.transform.position = Vector3.Lerp(dondeEstoy,dondeQuieroIr, .05f);
    }

    private bool EstaDentroDeLaCamara()
    {
        Vector3 posicionEnPantalla = camaraPrincipal.WorldToViewportPoint(transform.position);

        return (posicionEnPantalla.x >= 0 && posicionEnPantalla.x <= 1 &&
                posicionEnPantalla.y >= 0 && posicionEnPantalla.y <= 1);
    }

    private void VolverAlPuntoDeInicio()
    {
        transform.position = puntoInicio;
    }

    public bool GetVolteado()
    {
        return volteado;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Manzana"))
        {
            if (vida < 200)
            {
                vida += 20;
                Destroy(collision.gameObject);

            }
            else if(vida >= 200)
            {
                vida = 200;
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BalaE"))
        {
            vida -= 10;
            
        }
    }
}