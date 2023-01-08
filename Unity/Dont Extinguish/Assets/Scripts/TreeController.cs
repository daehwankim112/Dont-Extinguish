using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public GameObject Axe;
    public bool watered;
    public int health;
    public float speed;
    public bool growing;


    // Start is called before the first frame update
    void Start()
    {
        watered= false;
        health = 100;
        growing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ( growing )
        {

        }
    }
}
