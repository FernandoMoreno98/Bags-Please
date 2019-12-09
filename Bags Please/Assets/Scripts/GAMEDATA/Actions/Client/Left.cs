using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : GoapAction
{

    private bool left = false;
    private Salida salida; // where we chop the firewood

    private float startTime = 0;
    public float workDuration = 2; // seconds

    public Left()
    {
        addEffect("Irse", true); // we filled the stand
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    { 
      
        Salida[] salida = FindObjectsOfType(typeof(Salida)) as Salida[];
        Salida targetSalida = salida[0];
        target = targetSalida.gameObject;
        
        return targetSalida != null;
    }

    public override bool isDone()
    {
        return left;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;
        //Debug.Log("startTime" + startTime);

        if (Time.time - startTime > workDuration)
        {
            left = true;
            Destroy(this.gameObject);
        }
        return true;
    }

    public override bool requiresInRange()
    {
        return true; 
    }

    public override void reset()
    {
        left = false;
        salida = null;
        startTime = 0;
    }


}
