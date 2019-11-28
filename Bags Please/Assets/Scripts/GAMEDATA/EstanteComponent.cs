using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstanteComponent : MonoBehaviour
{


    public bool isEmpty = false;
    public Alimento.enAlimentos alimento;
    public float MaxAmountHoldable;
    public float currentAmount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isFull()
    {
        if (currentAmount == MaxAmountHoldable)
            return true;
        return false;
    }

    public float Take(float amount)
    {
        float obtainedAmount = 0;
        if (currentAmount - amount >= 0)
        {
            obtainedAmount = amount;
            currentAmount -= amount;
            if (currentAmount == 0)
            {
                this.isEmpty = true;
            }
        }
        else
        {
            obtainedAmount = currentAmount;
            this.currentAmount = 0;
            this.isEmpty = true;
        }
        return obtainedAmount;
    }

    public float Fill(float amount, Alimento.enAlimentos alimento)
    {
        float fillAmount = 0;
        if(this.alimento.Equals(alimento) && currentAmount>0)
        {
            if (amount >= (MaxAmountHoldable - currentAmount))
            {

                fillAmount = MaxAmountHoldable - currentAmount;
                this.currentAmount = MaxAmountHoldable;
            }
            else
            {
                fillAmount = amount;
                this.currentAmount += amount;
            }
        }
        else if(currentAmount <= 0)
        {
            this.alimento = alimento;
            this.MaxAmountHoldable = Alimento.Alimentos_MaxAmountEstante[this.alimento];
            if (MaxAmountHoldable >= amount)
            {
                this.currentAmount = amount;
                fillAmount = amount;
            }
            else
            {
                this.currentAmount = MaxAmountHoldable;
                fillAmount = MaxAmountHoldable;
            }

        }

        return fillAmount;
    }
}
