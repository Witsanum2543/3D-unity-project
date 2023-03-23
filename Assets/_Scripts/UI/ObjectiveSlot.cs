using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveSlot : MonoBehaviour
{
    public Image objectiveDisplayImage;
    public TextMeshProUGUI amount;
    public GameObject checkMark;

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
        if (objective.isComplete)
        {
            checkMark.SetActive(true);
            amount.gameObject.SetActive(false);
        }
        if (amount.gameObject.activeSelf)
        {
        amount.text = objective.currentAmount + "/" + objective.requireAmount;
        }
        

    }
}
