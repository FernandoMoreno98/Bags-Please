using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeFoodFromStand : GoapAction
{

    private bool taked = false;
    private EstanteComponent targetEstanteComponent; // where we chop the firewood

    private float startTime = 0;
    public float workDuration = 2; // seconds
    public Alimento.enAlimentos DesiredFood;//Alimento deseado

    public TakeFoodFromStand()
    {
        //addPrecondition("hasFood", true); // we need have food 
        //addEffect("fillStand", true); // we filled the stand
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        EstanteComponent[] estantes = FindObjectsOfType(typeof(EstanteComponent)) as EstanteComponent[];
        EstanteComponent closest = null;
        float closestDist = 0;

        //Busca una estanteria no vacia , sin usar y con el alimento que desea
        foreach (EstanteComponent estante in estantes)
        {
            if (!estante.isEmpty() && !estante.isUsed && estante.alimento == DesiredFood)
            {
                if (closest == null)
                {
                    // first one, so choose it for now
                    closest = estante;
                    closestDist = (estante.gameObject.transform.position - agent.transform.position).magnitude;
                }
                else
                {
                    // is this one closer than the last?
                    float dist = (estante.gameObject.transform.position - agent.transform.position).magnitude;
                    if (dist < closestDist)
                    {
                        // we found a closer one, use it
                        closest = estante;
                        closestDist = dist;
                    }
                }
            }

        }
        targetEstanteComponent = closest;
        target = targetEstanteComponent.gameObject;
        return closest != null;
        
    }

    public override void reset()
    {
        taked = false;
        targetEstanteComponent = null;
        startTime = 0;
    }

    public override bool isDone()
    {
        return taked;
    }

    public override bool perform(GameObject agent)
    {
        throw new System.NotImplementedException();
    }

    public override bool requiresInRange()
    {
        return true;
    }
}
