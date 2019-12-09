using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayOnCash : GoapAction
{

    private bool payed = false;
    private CajaComponent targetCajaComponent; // where we chop the firewood

    private float startTime = 0;
    public float workDuration = 2; // seconds

    public PayOnCash()
    {
        addPrecondition("hasFood", true); // have things on the list
        addEffect("Irse", true); // we filled the stand
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        CajaComponent[] cajas = FindObjectsOfType(typeof(CajaComponent)) as CajaComponent[];
        CajaComponent closest = null;
        float closestDist = 0;
        //Busca una caja que no este en uso
        foreach (CajaComponent caja in cajas)
        {
            if (!caja.isUsed)
            {
                if (closest == null)
                {
                    // first one, so choose it for now
                    closest = caja;
                    closestDist = (caja.gameObject.transform.position - agent.transform.position).magnitude;
                }
                else
                {
                    // is this one closer than the last?
                    float dist = (caja.gameObject.transform.position - agent.transform.position).magnitude;
                    if (dist < closestDist)
                    {
                        // we found a closer one, use it
                        closest = caja;
                        closestDist = dist;
                    }
                }
                Debug.Log(closest.name);
            }
        }

        targetCajaComponent = closest;
        if (targetCajaComponent != null)
        {
            target = targetCajaComponent.gameObject;
        }
        return closest != null;
    }

    public override bool isDone()
    {
        return payed;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;
            targetCajaComponent.isUsed = true;
        //Debug.Log("startTime" + startTime);

        if (Time.time - startTime > workDuration)
        {
            //Debug.Log(Time.time-startTime);
            // finished filled
            //BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
            //CajaComponent estanteComponent = targetCajaComponent;

            //////////////////////////////////////////////////
            //AQUI SE SUMARIA EL DINERO OBTENIDO CON LAS VENTAS 
            ///////////////////////////////////////////////////

            //Debug.Log("He salido de un bucle");
            payed = true;
        }
        return true;
    }

    public override bool requiresInRange()
    {
        return true; 
    }

    public override void reset()
    {
        payed = false;
        targetCajaComponent = null;
        startTime = 0;
    }


}
