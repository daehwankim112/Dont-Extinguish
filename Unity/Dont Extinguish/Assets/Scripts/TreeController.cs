using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public GameObject Axe;
    public bool watered = false;
    public int health = 100;
    public float speed = 1;
    public float wateredSpeed = 10;
    public bool growing = true;
    public bool burning = false;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Tree";
    }

    // Update is called once per frame
    void Update()
    {
        if ( growing && ! watered )
        {
            gameObject.transform.localScale += new Vector3(0.01f * speed, 0.01f * speed, 0.01f * speed);
        }
        else if ( growing && watered )
        {
            gameObject.transform.localScale += new Vector3(0.01f * speed * (1 + wateredSpeed), 0.01f * speed * (1 + wateredSpeed), 0.01f * speed * (1 + wateredSpeed));
        }
        if ( burning )
        {

        }
    }

    public void lastSelectExited()
    {
        gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, 0);
    }
}
