using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public static TimeSystem Instance { get; private set; }

    [SerializeField] GameTimeStamp timeStamp;

    // List of Objects to inform of changes to the time
    List<ITimeTracker> listerners = new List<ITimeTracker>();

    private void Awake() {
        // If more than one instance, destroy the extra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // Set the static instance to this instance
            Instance = this;
        }
    }

    private void Start() {
        timeStamp = new GameTimeStamp(0);
        StartCoroutine(TimeUpdate());
    }

    public void TimeTick()
    {
        timeStamp.UpdateTime();
        // Inform the listeners of the new time state
        foreach(ITimeTracker listener in listerners)
        {
            listener.ClockUpdate(timeStamp);
        }
    }

    IEnumerator TimeUpdate() {
        while(true) {
            yield return new WaitForSeconds(1);
            TimeTick();
        }
    }

    // Handle listerners

    // Add the object to the list of listeners
    public void RegisterTracker(ITimeTracker listener)
    {
        listerners.Add(listener);
    }

    // Remove the object from the list of listeners
    public void UnregisterTracker(ITimeTracker listener)
    {
        listerners.Remove(listener);
    }

    public GameTimeStamp GetGameTimeStamp()
    {
        // return clone of timestamp
        return new GameTimeStamp(timeStamp);
    }
}
