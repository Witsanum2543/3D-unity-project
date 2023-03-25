using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLogUI : MonoBehaviour
{
    public TextMeshProUGUI level;
    public TextMeshProUGUI timeSpend;
    public TextMeshProUGUI moneyGain;

    public TextMeshProUGUI carrotAmount;
    public TextMeshProUGUI tomatoAmount;
    public TextMeshProUGUI potatoAmount;
    public TextMeshProUGUI eggAmount;
    public TextMeshProUGUI milkAmount;
    


    public void Render()
    {
        gameObject.SetActive(true);
        level.text = (LevelManager.Instance.currentLevel + 1).ToString();
        GameLog gameLog = GameState.Instance.gameLog;
        int totalSeconds = gameLog.totalTimeUsed;
        timeSpend.text = $"{totalSeconds / 60} m {totalSeconds % 60} s";
        moneyGain.text = gameLog.moneyGain.ToString();
        soldAmountRender();
    }

    public void soldAmountRender()
    {
        foreach(KeyValuePair<EObjectiveType, int> product in GameState.Instance.gameLog.productSold)
        {
            switch(product.Key)
            {
                case EObjectiveType.Carrot:
                    carrotAmount.text = product.Value.ToString();
                    break;
                case EObjectiveType.Tomato:
                    tomatoAmount.text = product.Value.ToString();
                    break;
                case EObjectiveType.Potato:
                    potatoAmount.text = product.Value.ToString();
                    break;
                case EObjectiveType.Egg:
                    eggAmount.text = product.Value.ToString();
                    break;
                case EObjectiveType.Milk:
                    milkAmount.text = product.Value.ToString();
                    break;
            }
        }
    }
}
