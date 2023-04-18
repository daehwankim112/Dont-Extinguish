using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class FireManager : MonoBehaviour
{
    public GameObject logText;
    public float log = 100;
    public float speed;
    public Transform fire;
    public GameObject treeSpawner;
    public float burningRadius;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if ( GameController.isPlaying )
        {
            log -= (Time.deltaTime * speed);
            Debug.Log("log: " + log);
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
        foreach(Transform child in treeSpawner.transform)
        {
            if (Vector3.Distance(this.transform.position, child.transform.position) < burningRadius)
            {
                child.gameObject.GetComponent<TreeController>().burning = true;
            }
        }
        logText.GetComponent<TMP_Text>().text = "log: " + (int) log + "";
    }
    /*
    private bool LogDropped()
    {
        
    }
    */
}
