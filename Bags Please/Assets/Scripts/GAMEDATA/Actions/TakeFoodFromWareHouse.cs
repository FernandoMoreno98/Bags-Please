using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TakeFoodFromWareHouse : GoapAction
{
    private bool filled = false;
    private WareHouseComponent targetWareHouseComponent; //where we take food

    private float startTime = 0;
    public float workDuration = 1; // seconds

    public TakeFoodFromWareHouse()
    {
        addPrecondition("hasFood", false); // we need to dont have food 
        addEffect("hasFood", true);
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        // find the nearest rock that we can mine
        WareHouseComponent[] wareHouses = FindObjectsOfType(typeof(WareHouseComponent)) as WareHouseComponent[];
        WareHouseComponent closest = wareHouses[0]; //A priori solo hay un warehouse en el supermercado

        targetWareHouseComponent = closest;
        target = targetWareHouseComponent.gameObject;
        return closest != null;
    }

    public override bool isDone()
    {
        return filled;
    }

    public override bool perform(GameObject agent)
    {
        
        if (startTime == 0)
            startTime = Time.time;
            //Debug.Log("startTime" + startTime);

        if (Time.time - startTime > workDuration)
        {
            //Debug.Log(Time.time-startTime);
            // finished filled
            BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
            WareHouseComponent wareHouse = targetWareHouseComponent;
            BackpackComponent backpackWareHouse = (BackpackComponent)wareHouse.GetComponent(typeof(BackpackComponent));

            while (!backpack.isFull() && backpackWareHouse.HasFood())
            {
                //Seleccionamos de los alimentos disponibles en el almacen uno aleatorio.
                List<Alimento.enAlimentos> ListofAlimentosAvailable = backpackWareHouse.KindsOfFoodAvailable();
                Alimento.enAlimentos alimentoRandom = ListofAlimentosAvailable[(int)UnityEngine.Random.Range(0.0f, ListofAlimentosAvailable.Count-1)];

                int preTakeAmount = 0;
                if(agent.GetComponent<Labourer>() is StoreRestocker)
                {
                   
                    StoreRestocker aux = (StoreRestocker) agent.GetComponent<Labourer>();
                    preTakeAmount = aux.RandomTakenAmount();//ESTA CANTIDAD NO ES LA CANTIDAD QUE SE COGE FINALMENTE , SIMPLEMENTE ES LA CANTIDAD QUE DESEA COGER
                }
                int canFillAmount = backpack.canFill(alimentoRandom, preTakeAmount);//Cantidad de ese alimento que puede meter en su inventario a pesar de que su idea fuera meter mas.

                //TakedAmount es la cantidad que si ha podido coger en base a la disponibilidad de ese producto en el almacen y teniendo en cuenta que ya se valoro la disponibilidad del propio inventario
                int takedAmount = backpackWareHouse.Take(alimentoRandom, canFillAmount);
                backpack.Fill(alimentoRandom,takedAmount);
                //Debug.Log("Creo que estoy embuclado " + canFillAmount + " " + alimentoRandom);
                filled = true;
            }
            //Debug.Log("He salido de un bucle");
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
        targetWareHouseComponent = null;
        startTime = 0;
    }

}
