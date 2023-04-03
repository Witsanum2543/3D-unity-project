using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour, ITimeTracker
{
    public Image timeBar;
    public TextMeshProUGUI timeText;

    private int totalTime;
    private int remainingTime;
    private int warningThreshold;
    private int alertThreshold;

    // Start is called before the first frame update
    void Start()
    {
        TimeSystem.Instance.RegisterTracker(this);

        totalTime = GameState.Instance.timeLeft;
        remainingTime = GameState.Instance.timeLeft;

        calculateWarningThreshold();
        updateTimeVisual(remainingTime);
    }

    private void updateTimeVisual(int totalSeconds)
    {
        timeText.text = $"{totalSeconds / 60}:{totalSeconds % 60:00}";
        timeBar.fillAmount = Mathf.InverseLerp(0, totalTime, remainingTime);

        if (remainingTime <= alertThreshold)
        {
            timeBar.color = Color.red;
        }
        else if (remainingTime <= warningThreshold)
        {
            timeBar.color = new Color(1f, 0.5f, 0f);
        }
    }

    private void calculateWarningThreshold()
    {
        warningThreshold = totalTime / 2;
        alertThreshold = totalTime / 4;
    }

    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        remainingTime = GameState.Instance.timeLeft;
        updateTimeVisual(remainingTime);
    }
}
