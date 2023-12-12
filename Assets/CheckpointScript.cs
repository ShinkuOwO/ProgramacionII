using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointScript : MonoBehaviour
{
    Animator an;
    private void Start()
    {
        an = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(AnimacionBandera());
        }
    }
    IEnumerator AnimacionBandera()
    {
        an.SetInteger("estado", 1);
        yield return new WaitForSeconds(2.25f);
        an.SetInteger("estado", 2);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Escena 2");
    }
}
