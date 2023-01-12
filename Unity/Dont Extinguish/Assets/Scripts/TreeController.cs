using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreeController : MonoBehaviour
{
    public GameObject Axe;
    public bool watered = false;
    public int health = 100;
    public float speed = 1;
    public float wateredSpeed = 10;
    public bool growing = true;
    public bool burning = false;
    public float burningSpeed = 0.2f;
    public int damage = 20;
    public GameObject treeSample;
    public GameObject logSample;
    public Transform treeSpawner;
    public Transform logSpawner;
    public TMP_Text healthText;

    private float tempZeroToCalculateBurning = 0f;


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
                isburning();
            }
            if (health==100)
            {
                growing = false;
            }
            healthText.text = "" + health;
        }
        else // destroy tree
        {
            if (growing && !burning) // tree is down but was still growing
            {
                drop(1);
            }
            else if (!growing && !burning) // tree is down and was fully grown
            {
                drop(2);
            }
            else if (burning) // tree was burning
            {
                drop(0);
            }
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
        if (stage==2) // tree was fully grown
        {
            Instantiate(treeSample, this.gameObject.transform.position - new Vector3(0f, 1f, 0f), treeSample.transform.rotation, treeSpawner);
            Instantiate(logSample, this.gameObject.transform.position - new Vector3(0f, 1f, 0f), logSample.transform.rotation, logSpawner);
            Instantiate(logSample, this.gameObject.transform.position - new Vector3(0f, 1f, 0f), logSample.transform.rotation, logSpawner);
            Instantiate(logSample, this.gameObject.transform.position - new Vector3(0f, 1f, 0f), logSample.transform.rotation, logSpawner);
        }
        else if (stage==1) // tree was growing
        {
            Instantiate(treeSample, this.gameObject.transform.position - new Vector3(0f, 1f, 0f), treeSample.transform.rotation, treeSpawner);
        }
        else if (stage==0) // tree was burning
        {

        }
    }

    public void isburning()
    {
        tempZeroToCalculateBurning += burningSpeed;
        if(tempZeroToCalculateBurning > 1) 
        {
            tempZeroToCalculateBurning = 0f;
            health -= 1;
        }
    }
}
