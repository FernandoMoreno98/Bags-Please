using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepotDemand : MonoBehaviour
{
    public Text[] depot = new Text[16];
    public Text[] demand = new Text[16];
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i <= 15; i++)
        {
            depot[i].text = "3";
            demand[i].text = "0";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
