using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighScore;


public class TestScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HS.Init(this, "Natheid");
        HS.SubmitHighScore(this, "Nathan", int.Parse("42"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
