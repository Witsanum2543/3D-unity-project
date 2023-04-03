using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emily : MonoBehaviour
{
    public bool milk;
    public bool egg;
    public bool tomato;

    public void checkCondition()
    {
        if (milk && egg && tomato)
        {
            gameObject.SetActive(true);
        }
    }

}
