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
        addEffect("completingList", true); // we filled the stand
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        EstanteComponent[] estantes = FindObjectsOfType(typeof(EstanteComponent)) as EstanteComponent[];
        EstanteComponent closest = null;
        float closestDist = 0;
        if (agent.GetComponent<Client>().listaDelaCompra.Count!=0)
        DesiredFood = agent.GetComponent<Client>().listaDelaCompra[Random.Range(0, agent.GetComponent<Client>().listaDelaCompra.Count)];
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
                Debug.Log(closest.name);
            }

        }
       
            targetEstanteComponent = closest;
        if (targetEstanteComponent != null)
        {
            target = targetEstanteComponent.gameObject;
        }
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
        if (startTime == 0)
            startTime = Time.time;
            targetEstanteComponent.isUsed = true;
        //Debug.Log("startTime" + startTime);

        if (Time.time - startTime > workDuration)
        {
            //Debug.Log(Time.time-startTime);
            // finished filled
            BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
            EstanteComponent estanteComponent = targetEstanteComponent;

            Debug.LogError("desiredFood " + DesiredFood);
            if (estanteComponent.alimento == DesiredFood)
            {
                int preTakeAmount = 0;
                Client aux = null;
                if (agent.GetComponent<Labourer>() is Client)
                {

                    aux = (Client)agent.GetComponent<Client>();
                    preTakeAmount = aux.listaDelaCompraDictionary[DesiredFood];//ESTA CANTIDAD NO ES LA CANTIDAD QUE SE COGE FINALMENTE , SIMPLEMENTE ES LA CANTIDAD QUE DESEA COGER
                }
                int canFillAmount = backpack.canFill(DesiredFood, preTakeAmount);//Cantidad de ese alimento que puede meter en su inventario a pesar de que su idea fuera meter mas.
                Debug.LogError(canFillAmount + " canfill");
                //TakedAmount es la cantidad que si ha podido coger en base a la disponibilidad de ese producto en el almacen y teniendo en cuenta que ya se valoro la disponibilidad del propio inventario
                int takedAmount = estanteComponent.Take(canFillAmount);
                Debug.LogError(takedAmount + " taked");
                int r = backpack.Fill(DesiredFood, takedAmount);
                Debug.LogError(r + " fill");
                //Debug.Log("Creo que estoy embuclado " + canFillAmount + " " + alimentoRandom);

                //HEMOS COGIDO LA COMIDA , AHORA HAY QUE ACTUALIZAR LA LISTA DE LA COMPRA
                aux.updateBuyList(DesiredFood, r);
                targetEstanteComponent.isUsed = false;
                if (aux.listaDelaCompra.Count==0)
                {
                    addEffect("isListComplete", true);
                }
                taked = true;
            }
            else
            {
                return false; //Se ha fallado al intentar conseguir el alimento del estante que queriamos.
            }
            //Debug.Log("He salido de un bucle");
        }
        return true;
    }

    public override bool requiresInRange()
    {
        return true;
    }
}
