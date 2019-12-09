using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public GameObject[] waypoints;
    public string[] products;

    // Start is called before the first frame update
    void Start()
    {
        setPositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPositions()
    {
        waypoints[0].transform.position = new Vector3(0.3f, 0.054f, 0.837f);
        waypoints[1].transform.position = new Vector3(0.3f, 0.054f, -0.172f);
        waypoints[2].transform.position = new Vector3(0.3f, 0.054f, -1.166f);
        waypoints[3].transform.position = new Vector3(0.886f, 0.054f, 0.837f);
        waypoints[4].transform.position = new Vector3(0.886f, 0.054f, -0.172f);
        waypoints[5].transform.position = new Vector3(0.886f, 0.054f, -1.166f);
        waypoints[6].transform.position = new Vector3(1.243f, 0.054f, 0.837f);
        waypoints[7].transform.position = new Vector3(1.243f, 0.054f, -0.172f);
        waypoints[8].transform.position = new Vector3(1.243f, 0.054f, -1.166f);
        waypoints[9].transform.position = new Vector3(1.822f, 0.054f, 0.837f);
        waypoints[10].transform.position = new Vector3(1.822f, 0.054f, -0.172f);
        waypoints[11].transform.position = new Vector3(1.822f, 0.054f, -1.166f);
        waypoints[12].transform.position = new Vector3(2.124f, 0.054f, 1.335f);
        waypoints[13].transform.position = new Vector3(2.124f, 0.054f, 0.331f);
        waypoints[14].transform.position = new Vector3(2.124f, 0.054f, -0.663f);
        waypoints[15].transform.position = new Vector3(2.124f, 0.054f, -1.674f);

        products[0] = "tomates";
        products[1] = "calabazas";
        products[2] = "platanos";
        products[3] = "uvas";
        products[4] = "melocotones";
        products[5] = "pizza";
        products[6] = "hamburguesa";
        products[7] = "carne";
        products[8] = "sandia";
        products[9] = "huevos";
        products[10] = "tarta";
        products[11] = "patatas";
        products[12] = "vino";
        products[13] = "helados";
        products[14] = "yogures";
        products[15] = "kiwis";
    }

    public Vector3 getTarget(string target)
    {
        Vector3 pos;
        int i = 0;
        while(products[i] != target && i <= 15)
        {
            i++;
        }
        pos = waypoints[i].transform.position;
        return pos;
    }
}
