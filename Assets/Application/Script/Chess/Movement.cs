using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public string v1;
    public int v2;
    public List<GameObject> coins;
    public List<GameObject> captured;

    // Check Movement
    public Movement(string v1, bool v2)
    {
        this.v1 = v1;
        coins = new List<GameObject>();
        captured = new List<GameObject>();
        
        // Check if movements is true or false
        if(v2 == true){
            // Movement in Positive direction
            this.v2 = 1;
        }
        else{

            this.v2 = -1;
        }
    }

}