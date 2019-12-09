using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreRestocker : Labourer
{
    public float MaxRandomAmountWantTake = 10.0f;
    public float MinRandomAmountWantTake = 2.0f;
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
        return (int)UnityEngine.Random.Range(MinRandomAmountWantTake, MaxRandomAmountWantTake);
    }
}
