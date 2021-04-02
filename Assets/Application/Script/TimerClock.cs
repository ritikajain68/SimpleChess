using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerClock : MonoBehaviour
{
    public GameObject gameOver;
    public float remainingTime;
    public TextMeshProUGUI timer;
    public bool isTimerOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        // Check if timer is running or not
        if(isTimerOn){
        
            // Check if timerCount is remaining
            if(remainingTime > 0){
                remainingTime -= Time.deltaTime;
                setTimerCount(remainingTime);
            }
            else{
                isTimerOn =false;
                remainingTime = 0;

                gameOver.SetActive(true);            
            }
        }
   }

   public void setTimerCount(float timerText){
       timerText +=1;

       float min = Mathf.FloorToInt(timerText / 60);
       float sec = Mathf.FloorToInt(timerText % 60);

       timer.text = "Remaining Time : " +string.Format("{0:00}:{1:00}", min, sec);

   }
}
