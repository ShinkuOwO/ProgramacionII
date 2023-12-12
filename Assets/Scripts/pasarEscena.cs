using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pasarEscena : MonoBehaviour
{
    public string SiguienteEscena;
    public AudioSource audioSource;
    public AudioClip select;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(ReproducirSonidoYCambiarEscena());
        }
    }

    IEnumerator ReproducirSonidoYCambiarEscena()
    {
        if (audioSource != null && select != null)
        {
            audioSource.PlayOneShot(select);
            yield return new WaitForSeconds(select.length); // Espera la duración del sonido

            if (!string.IsNullOrEmpty(SiguienteEscena))
            {
                SceneManager.LoadScene(SiguienteEscena);
            }
            else
            {
                Debug.LogWarning("El nombre de la siguiente escena no está definido.");
            }
        }
        else
        {
            Debug.LogWarning("AudioSource o AudioClip no asignado.");
        }
    }
}