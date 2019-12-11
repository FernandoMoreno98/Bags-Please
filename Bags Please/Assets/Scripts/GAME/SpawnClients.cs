using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnClients : MonoBehaviour
{
    public float minTimeSpawn = 1;
    public float maxTimeSpawn = 1;
    private double currentTime;
    private double tAleatorio;
    [SerializeField] private List<GameObject> Clientes;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = Time.time;
        tAleatorio = Random.Range(minTimeSpawn,maxTimeSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime + tAleatorio < Time.time)
        {
            Instantiate(Clientes[Random.Range(0, Clientes.Count)],this.transform.position,Quaternion.identity);
            tAleatorio = Random.Range(minTimeSpawn, maxTimeSpawn);
            currentTime = Time.time;
        }
    }
}
