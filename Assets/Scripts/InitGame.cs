using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighScore;

public class InitGame : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        HS.Init(this, "TEAM 5");
    }

    
}
