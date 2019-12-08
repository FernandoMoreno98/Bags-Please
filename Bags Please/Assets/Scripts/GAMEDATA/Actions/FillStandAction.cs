using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class FillStandAction : GoapAction
{
    private bool filled = false;
    private EstanteComponent targetEstanteComponent; // where we chop the firewood

    private float startTime = 0;
    public float workDuration = 2; // seconds
    public Alimento.enAlimentos FillerFood;//Alimento usado para rellenar

    public FillStandAction()
    {
        addPrecondition("hasFood", true); // we need have food 
        addEffect("fillStand", true); // we filled the stand
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        Debug.Log("Check Procedural");
        // find the nearest stand we can fill
        EstanteComponent[] estantes = FindObjectsOfType(typeof(EstanteComponent)) as EstanteComponent[];
        EstanteComponent closest = null;
        float closestDist = 0;

        //PRIMERO SE PRIORIZA RELLENAR LAS ESTANTERIAS VACIAS && QUE NO ESTEN EN USO
        foreach (EstanteComponent estante in estantes)
        {
            if (estante.isEmpty() && !estante.isUsed)
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

        //SI HEMOS ENCONTRADO UNA VACIA SE SELECCIONA ESA Y SE DICE QUE SE PUEDE RELLENAR && QUE NO ESTEN EN USO
        if (closest != null)
        {
            //Toma un alimento al azar pero tienen mayor probabilidad aquellos de los que almacene mas cantidad
            List<Alimento.enAlimentos> auxList = agent.GetComponent<BackpackComponent>().alimentos;
            if(auxList.Count>0)
            FillerFood = auxList[(int) (UnityEngine.Random.Range(0,auxList.Count-1.0f))] ;
            Debug.Log(FillerFood);
            targetEstanteComponent = closest;
            target = targetEstanteComponent.gameObject;
            return closest != null;
        }

        //SEGUNDO SE PRIORIZA RELLENAR AQUELLAS CON EL MISMO PRODUCTO QUE SE TIENE Y QUE NO ESTEN LLENAS Y QUE NO ESTE EN USO
        foreach (EstanteComponent estante in estantes)
        {
            if (agent.GetComponent<BackpackComponent>().HasFood(estante.alimento) && !estante.isFull() && !estante.isUsed)//DEBUG
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
        FillerFood = closest.alimento;
        targetEstanteComponent = closest;
        target = targetEstanteComponent.gameObject;
        return closest != null;
    }

    public override bool isDone()
    {
        return filled;
    }

    public override bool perform(GameObject agent)
    {
        BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
        if (backpack.KindsOfFoodAvailable().Contains(FillerFood))
        {
            if (startTime == 0)
                startTime = Time.time;
                targetEstanteComponent.isUsed = true;//Se ocupa la estanteria por tanto nadie mas puede acceder.
                                                 //(Asi tambien resolvemos temas de concurrencia , aunque se podria hacer que pudieran "modificar" varios)

            if (Time.time - startTime > workDuration)
            {
                // finished filled
                EstanteComponent estante = targetEstanteComponent;

                int filledAmount = estante.canFill(FillerFood, backpack.dictionary[FillerFood]);
                Debug.LogError(filledAmount + " Amount i can fill");
                int takedAmount = backpack.Take(FillerFood, filledAmount);
                Debug.LogError(takedAmount + " Amount i take considering the one i can fill");
                int a = estante.Fill(FillerFood, takedAmount);
                Debug.LogError(a + " Amount i fill considering the one i take");
                filled = true;

                //SOLO si nos quedamos sin comida en la mochila añadimos el efecto 
                //(Se deberia hacer en el constructor, yo creo q no porq no es algo q se sabe que se cumple a priori)
                if (!backpack.HasFood())
                {
                    addEffect("hasFood", false);
                }
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    public override bool requiresInRange()
    {
        return true; // yes we need to be near a chopping block
    }

    public override void reset()
    {
        filled = false;
        targetEstanteComponent = null;
        startTime = 0;
    }

}
