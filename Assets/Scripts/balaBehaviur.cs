using UnityEngine;

public class balaBehaviur : MonoBehaviour
{
    [Header("Configuraci�n de la Bala")]
    public float velocidad;

    private Rigidbody2D rb2d;
    private Vector2 direccion;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb2d.velocity = direccion * velocidad;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("La Bala a Da�ado a John!");
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Suelo"))
        {
            Debug.Log("soy la bala, Colisione con el piso");
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Debug.Log("La Bala a Da�ado al Enemigo");
            Destroy(gameObject);
        }
    }    
    public void darDireccion(Vector2 nuevaDireccion)
    {
        direccion = nuevaDireccion;
    }
}