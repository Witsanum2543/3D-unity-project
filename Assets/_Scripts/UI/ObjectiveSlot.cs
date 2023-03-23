using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveSlot : MonoBehaviour
{
    public Image objectiveDisplayImage;
    public TextMeshProUGUI amount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display(ObjectiveData objective)
    {  
        objectiveDisplayImage.sprite = objective.objectiveIcon;
        amount.text = objective.currentAmount + "/" + objective.requireAmount;
    }
}
