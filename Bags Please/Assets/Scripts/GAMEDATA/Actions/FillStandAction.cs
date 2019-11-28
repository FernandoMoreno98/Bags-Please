using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillStandAction : GoapAction
{
    private bool filled = false;
    private EstanteComponent targetEstanteComponent; // where we chop the firewood

    private float startTime = 0;
    public float workDuration = 2; // seconds

    public FillStandAction()
    {

    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        // find the nearest rock that we can mine
        EstanteComponent[] estantes = FindObjectsOfType(typeof(EstanteComponent)) as EstanteComponent[];
        EstanteComponent closest = null;
        float closestDist = 0;

        //PRIMERO SE PRIORIZA RELLENAR LAS ESTANTERIAS VACIAS
        foreach (EstanteComponent estante in estantes)
        {
            if (estante.isEmpty)
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

        //SI HEMOS ENCONTRADO UNA VACIA SE SELECCIONA ESA Y SE DICE QUE SE PUEDE RELLENAR
        if (closest != null)
        {
            targetEstanteComponent = closest;
            target = targetEstanteComponent.gameObject;
            return closest != null;
        }

        //SEGUNDO SE PRIORIZA RELLENAR AQUELLAS CON EL MISMO PRODUCTO QUE SE TIENE Y QUE NO ESTEN LLENAS
        foreach (EstanteComponent estante in estantes)
        {
            //if (estante.alimento == agent.GetComponent<>().alimento && estante.isFull)//DEBUG
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

        //SI HEMOS ENCONTRADO UNA QUE CUMPLA LA SEGUNDA POSIBLE SE SELECCIONA ESA Y SE DICE QUE SE PUEDE RELLENAR
        targetEstanteComponent = closest;
        target = targetEstanteComponent.gameObject;
        return closest != null;
    }

    public override bool isDone()
    {
        throw new System.NotImplementedException();
    }

    public override bool perform(GameObject agent)
    {
        throw new System.NotImplementedException();
    }

    public override bool requiresInRange()
    {
        return true; // yes we need to be near a chopping block
    }

    public override void reset()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
