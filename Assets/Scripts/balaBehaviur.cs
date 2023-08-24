using UnityEngine;

public class balaBehaviur : MonoBehaviour
{
    [Header("Configuración de la Bala")]
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

    public void darDireccion(Vector2 nuevaDireccion)
    {
        direccion = nuevaDireccion;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}