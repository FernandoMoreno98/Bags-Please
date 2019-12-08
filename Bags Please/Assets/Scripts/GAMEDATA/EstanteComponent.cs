using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstanteComponent : MonoBehaviour
{

    public Alimento.enAlimentos alimento;
    public float maxAmount;
    public int currentAmount;
    public bool isUsed = false;//Se supone que es atomico como especifica C# en su web

    //Visual
    public Alimento.enAlimentos VisualAlimento; //Se establece para que el update solo se realice si cambia el alimento y la cantidad que guarda
    public int VisualAmount = 0;


    // Start is called before the first frame update
    void Awake()
    {
        Alimento.setAlimentos_MaxAmountEstante();
        maxAmount = Alimento.Alimentos_MaxAmountEstante[alimento];
    }

    // Update is called once per frame
    void Update()
    {
        if(VisualAlimento!=alimento || VisualAmount != currentAmount)
        {
            DrawnFoodContained();
        }
    }

    public bool isFull()
    {
        return (currentAmount == maxAmount);

    }

    public bool isEmpty()
    {
        return (currentAmount == 0);
    }
    //Devuelve la maxima cantidad que se puede rellenar
    //Devuelve la cantidad que es posible rellenar de cierto producto.
    public int canFill(Alimento.enAlimentos a,int amount)
    {
        if (currentAmount == 0)//Cambiamos el tipo de producto que guarda
        {
            alimento = a;
            maxAmount = Alimento.Alimentos_MaxAmountEstante[a];//CAMBIAMOS EL MAXIMO DE CANTIDAD PERMITIDA EN FUNCION DEL NUEVO PRODUCTO
        }

        int aux = amount;
        int aux2 = currentAmount;
        int c = 0;
        while (maxAmount > aux2 && aux > 0)
        {
            //alimentos.Add(a);
            aux--;
            aux2++;
            c++;
        }
        //ToDictionary();
        return c;
    }

    //Rellena la estanteria con la cantidad maxima posible de cierto producto.
    //Devuelve la cantidad que se ha rellenado
    public int Fill(Alimento.enAlimentos a, int amount)
    {

        int cFill = canFill(a,amount);//Se vuelve a comprobar si la cantidad introducida es apropiada.

        for (int i = 0; i < cFill; i++)
        {
            currentAmount++;
        }
        return cFill;
    }
    //Devuelve la maxima cantidad que se puede tomar 
    //Devuelve la cantidad de producto que es posible retirar
    public int canTake(int amount)
    {
        int c = currentAmount;
        int aux = 0;
        while (c > 0 && aux < amount)
        {
            aux++;
            c--;
        }
        return aux;
    }

    //Coje la estanteria con la cantidad maxima disponible de cierto producto.
    //Devuelve la cantidad de producto retirada
    public int Take(int amount)
    {
        int cTake = canTake(amount);
        for (int i = 0; i < cTake; i++)
        {
            currentAmount--;
        }
        return cTake;
    }


    //VISUAL EXCLUSIVAMENTE ESTE METODO//
    public void DrawnFoodContained()
    {
       //Para que solo ejecute una vez
       VisualAlimento = alimento;
       VisualAmount = currentAmount;

       Bounds EstanteriaBounds = this.GetComponent<MeshRenderer>().bounds;//Se usa meshrenderer para que las posiciones sean globales.
       //Hallamos el longitud en z de la estanteria
       float zLenghtEstanteria = EstanteriaBounds.size.z;
       float minPosBoundsZ = EstanteriaBounds.min.z;
       //Hallamos cuantos alimentos van en la primera linea y en la segunda en funcion de la cantidad que tenemos 
       int numberOfCurrentFood1 = (currentAmount / 2);
       int numberOfCurrentFood2 = currentAmount - numberOfCurrentFood1;

       Vector3 line1 = transform.Find("line1").transform.position;
       Vector3 line2 = transform.Find("line2").transform.position;


       List<Vector3> ListPosFood1 = getPositionOnLine(line1, zLenghtEstanteria, numberOfCurrentFood1, minPosBoundsZ);
       List<Vector3> ListPosFood2 = getPositionOnLine(line2, zLenghtEstanteria, numberOfCurrentFood2, minPosBoundsZ);
       ListPosFood1.AddRange(ListPosFood2);

       GameObject food = Alimento.GetAlimentoPrefab(alimento);
        
        //Destroy Previus Intances
       foreach (FoodComponent c in GetComponentsInChildren<FoodComponent>())
       {
            Destroy(c.gameObject);
       }

       //Instance new ones
       foreach(Vector3 p in ListPosFood1)
       {
            Vector3 originalScale = food.transform.lossyScale;
            GameObject aux = Instantiate(food, p,food.transform.rotation);
            aux.transform.parent = this.transform;
            ExtensionMethods.MyExtensions.SetGlobalScale(aux.transform, originalScale);
            
        }
       
       
       
    }

    private List<Vector3> getPositionOnLine(Vector3 line,float totalLenghtZ,int numberOfCurrentFood,float minPos)
    {
        List<Vector3> listPointsLine = new List<Vector3>();
        for (int i = 0; i < numberOfCurrentFood; i++)
        {
            listPointsLine.Add(new Vector3(line.x, line.y, minPos + (i+1) * (totalLenghtZ / (numberOfCurrentFood+1))));
        }
        return listPointsLine;
    }

  
}
