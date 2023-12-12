using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGrunt : MonoBehaviour
{
    public GameObject Grunts;
    public Transform puntoSpawn;
    public float IntervaloSpawn = 5f;

    private float tiempoUltimoSpawn;

    private int limiteGrunts = 5;
    private int GruntsExistentes = 0;   



    private void Update()
    {
        tiempoUltimoSpawn += Time.deltaTime;
        if(tiempoUltimoSpawn >= IntervaloSpawn && GruntsExistentes< limiteGrunts)
        {
            SpawnGrunts();
            tiempoUltimoSpawn = 0f;
        }
    }
    void SpawnGrunts()
    {
        GameObject NuevoGrunt = Instantiate(Grunts, puntoSpawn.position, Quaternion.identity);
        Rigidbody2D rb2DNuevoGrunt = NuevoGrunt.GetComponent<Rigidbody2D>();
        GruntsExistentes++;
    }
    public void ReduceGruntCount()
    {
        GruntsExistentes = Mathf.Max(0, GruntsExistentes - 1);
    }
    public int GetGruntsExistentes()
    {
        return GruntsExistentes;
    }

    public void SetGruntsExistentes(int value)
    {
        GruntsExistentes = value;
    }
    public int GetlimiteGrunts()
    {
        return limiteGrunts;
    }
    public void SetlimiteGrunts(int value)
    {
        limiteGrunts = value;
    }
}
