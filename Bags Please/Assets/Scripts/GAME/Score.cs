using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text pstars;
    public Text nstars;
    public Text money;

    void Start()
    {
        nstars.text = "0";
        pstars.text = "0";
        money.text = "100";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
