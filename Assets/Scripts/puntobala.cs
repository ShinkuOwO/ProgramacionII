using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puntobala : MonoBehaviour
{
    public GameObject balaPrefab;
    private Vector3 flipbala;
    void Start()
    {
        
    }  
    void Update()
    {
        
    }
    private void Disparo(float johnflip)
    {
        if (johnflip == 1.0f)
        {
            flipbala = Vector2.right;
        }
        else
        {
            flipbala = Vector2.left;
        }
        GameObject Bala = Instantiate(balaPrefab, transform.position + flipbala * 0.1f, Quaternion.identity);
        Bala.GetComponent<balaBehaviur>().setDirection(flipbala);
    }
}
