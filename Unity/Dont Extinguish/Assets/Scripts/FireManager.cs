using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FireManager : MonoBehaviour
{
    public TMP_Text logText;
    public int log= 100;
    public float speed = 0.01f;
    public Transform fire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( GameController.isPlaying )
        {
            log -= (int)(Time.deltaTime * speed);
            fire.localScale = new Vector3(log/100, log/100, log/100);
        }
        if ( GameController.reset )
        {
            log = 100;
        }
        if ( log < 1 )
        {
            GameController.isPlaying = false;
        }
    }
    /*
    private bool LogDropped()
    {
        
    }
    */
}
