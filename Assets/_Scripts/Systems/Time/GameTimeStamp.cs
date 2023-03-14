using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameTimeStamp
{
    public int second;

    public GameTimeStamp(int second) {
        this.second = second;
    }

    public GameTimeStamp(GameTimeStamp timestamp)
    {
        this.second = timestamp.second;
    }

    public void UpdateTime() {
        second++;
    }

    public static int CompareTimeStamps(GameTimeStamp timeStamp1, GameTimeStamp timeStamp2) {
        return Mathf.Abs(timeStamp1.second - timeStamp2.second);
    }
}

