using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public GameObject button;
    public GameObject panel1;
    public GameObject panel2;
    // Start is called before the first frame update
    void Start()
    {
        /*
        foreach(Alimento.enAlimentos a in (Alimento.enAlimentos[])Alimento.enAlimentos.GetValues(typeof(Alimento.enAlimentos))){
            if(panel2.transform.childCount % 4 == 0)
            {
                GameObject Aux = Instantiate(panel2);// Clone
                Aux.GetComponent<RectTransform>().position += new Vector3(panel2.GetComponent<RectTransform>().sizeDelta.x,0,0);
                panel2 = Aux;

            }
            GameObject auxBut = Instantiate(button);
            auxBut.transform.parent = panel2.transform;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
