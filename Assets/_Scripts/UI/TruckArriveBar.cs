using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckArriveBar : MonoBehaviour
{
    public Image truck;
    public RectTransform truckArriveTimeText;

    private float distance;
    private float speed;
    private float targetPositon;
    private bool isDriving = false;
    private bool isFlip = false;

    public RectTransform homeWayPoint;
    public RectTransform shopWayPoint;

    // Start is called before the first frame update
    void Start()
    {
        truck.transform.position = homeWayPoint.position;
        distance = Mathf.Abs(homeWayPoint.anchoredPosition.x - shopWayPoint.anchoredPosition.x);
    }

    public void startDrive(int totalTime)
    {
        speed = (distance * 2) / (totalTime - 0.5f);
        targetPositon = shopWayPoint.anchoredPosition.x;
        isDriving = true;
    }

    private void flipTruckDirection()
    {
        isFlip = !isFlip;
        truck.transform.localScale = new Vector3(-truck.transform.localScale.x, truck.transform.localScale.y, truck.transform.localScale.z);

        truckArriveTimeText.localScale = new Vector3(-truckArriveTimeText.localScale.x, truckArriveTimeText.localScale.y, truckArriveTimeText.localScale.z);
        if (isFlip == true)
        {
            truckArriveTimeText.anchoredPosition = new Vector2(truckArriveTimeText.anchoredPosition.x + 123, truckArriveTimeText.anchoredPosition.y);
        } 
        else
        {
            truckArriveTimeText.anchoredPosition = new Vector2(truckArriveTimeText.anchoredPosition.x - 123, truckArriveTimeText.anchoredPosition.y);
        }
    }

    private void Update() {
        if (isDriving)
        {
            Vector2 currentTruckPosition = truck.rectTransform.anchoredPosition;
            float step = speed * Time.deltaTime;

            truck.rectTransform.anchoredPosition = new Vector2(
                Mathf.MoveTowards(currentTruckPosition.x, targetPositon, step), 
                currentTruckPosition.y
            ); 

            if (Mathf.Approximately(currentTruckPosition.x, targetPositon))
            {
                if (!isFlip)
                {
                    flipTruckDirection();
                    targetPositon = homeWayPoint.anchoredPosition.x;
                }
                else if (isFlip)
                {
                    flipTruckDirection();
                    isDriving = false;
                }    
            }
        }
    } 

}
