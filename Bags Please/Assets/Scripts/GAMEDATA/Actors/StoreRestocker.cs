using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreRestocker : Labourer
{
    public int MaxRandomAmountWantTake = 10;
    /**
       * Our only goal will ever be to fill stands
       * The StoreRestocker will be able to fulfill this goal.
       */
    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        goal.Add(new KeyValuePair<string, object>("fillStand", true));
        return goal;
    }

    public int RandomTakenAmount()
    {
        //Debug.Log("ssssisisi");
        return (int)UnityEngine.Random.Range(0.0f, MaxRandomAmountWantTake);
    }
}
