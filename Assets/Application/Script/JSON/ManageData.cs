using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageData : MonoBehaviour
{
    public TimerClock timerClock;
    public string jsonUrl;
    public GameData save_data;
    
    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        StartCoroutine(getData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getJsonData(string url){
    //       GameData save_data = JsonUtility.FromJson<GameData>(url);

        save_data = JsonUtility.FromJson<GameData>(url);
        Debug.Log(save_data.gameTimer);

        timerClock.remainingTime = save_data.gameTimer;       

        // Start the tmer by default
        timerClock.isTimerOn = true;

    }

    [System.Obsolete]
    IEnumerator getData(){
        Debug.Log("Processing");

        WWW data= new WWW(jsonUrl);
        yield return data;

        if(data.error == null)
        {
            getJsonData(data.text);
        }
        else{
            Debug.Log("OOPS..!!");
        }
    }
}