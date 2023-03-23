using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance { get; private set; }

    public ObjectiveSlot[] objectiveSlotList;

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RenderObjective()
    {
        List<ObjectiveData> objectiveList = GameState.Instance.getObjectives();
        for (int i=0; i<objectiveList.Count; i++)
        {
            objectiveSlotList[i].Display(objectiveList[i]);
        }

        for(int i=objectiveList.Count; i<5; i++)
        {
            objectiveSlotList[i].gameObject.SetActive(false); 
        }
    }
}
