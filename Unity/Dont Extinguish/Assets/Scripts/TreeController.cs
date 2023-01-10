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
    public int damage = 20;
    public GameObject treeSample;
    public Transform treeSpawner;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Tree";
    }

    // Update is called once per frame
    void Update()
    {
        if ( health > 0 )
        {
            if (growing && !watered)
            {
                gameObject.transform.localScale += new Vector3(0.01f * speed, 0.01f * speed, 0.01f * speed);
            }
            else if (growing && watered)
            {
                gameObject.transform.localScale += new Vector3(0.01f * speed * (1 + wateredSpeed), 0.01f * speed * (1 + wateredSpeed), 0.01f * speed * (1 + wateredSpeed));
            }
            if (burning)
            {
                Debug.Log("Tree " + this.gameObject.name + " is burning");
            }
        }
        else // destroy tree
        {
            drop( 1 );
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 1f);
        }
    }

    public void lastSelectExited()
    {
        gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, 0);
    }

    public void cutting()
    {
        if ( health > 0 )
        {
            health -= damage;
        }
    }

    public void drop( int stage )
    {
        Instantiate(treeSample, this.gameObject.transform.position - new Vector3(0f, 1f, 0f), treeSample.transform.rotation, treeSpawner);
    }
}
