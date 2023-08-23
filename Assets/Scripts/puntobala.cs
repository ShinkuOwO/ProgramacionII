using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puntobala : MonoBehaviour
{
    public GameObject balaPrefab;
    public GameObject JhonController;
    private Vector2 direccion;
    void Start()
    {

    }
    void Update()
    {
        JhonController johnController = JhonController.GetComponent<JhonController>();
        bool estaVolteado = johnController.sr.flipX;
        if (Input.GetKey(KeyCode.Z))
        {
            //Disparar();
            Disparo(estaVolteado);
        }

    }
    private void Disparo(bool estaVolteado)
    {
        if (estaVolteado == false) { direccion = Vector2.right; }
        else if (estaVolteado == true) { direccion = Vector2.left; }
        GameObject Bala = Instantiate(balaPrefab, transform.position, Quaternion.identity);
        Bala.GetComponent<balaBehaviur>().darDireccion(direccion);
    }
    
    //public void Disparar()
    //{
    //    //Aqui creamos la bala
    //    Vector2 direccion = new Vector2(0, 1);
    //    GameObject Bala =Instantiate(balaPrefab, transform.position, Quaternion.identity);
    //    Bala.GetComponent<balaBehaviur>().darDireccion(direccion);
    //}
}
