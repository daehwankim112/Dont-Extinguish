using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using System.ComponentModel.Design;

public class TreeController : MonoBehaviour
{
    public GameObject Axe;
    public bool watered = false;
    public int health = 99;
    public float speed = 50;
    public float wateredSpeed = 10;
    public bool growing = true;
    public bool burning = false;
    public float burningSpeed = 0.2f;
    public int damage = 20;
    public GameObject smallTreeSample;
    public GameObject logSample;
    public Transform treeSpawner;
    public Transform logSpawner;
    public bool planted = false;

    private GameObject healthText;
    private float tempZeroToCalculateBurning = 0f;
    private float tempZeroToCalculateHealth = 0f;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity= Vector3.zero;
        gameObject.tag = "Tree";
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.firstSelectEntered.AddListener(holding);
        grabbable.lastSelectExited.AddListener(released);
    }

    // Update is called once per frame
    void Update()
    {
        if (planted)
        {
            transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
        transform.rotation = Quaternion.Euler(0f, transform.rotation.y, 0f);
        healthText = this.gameObject.transform.GetChild(3).GetChild(0).gameObject;
        if ( health > 0 )
        {
            if (growing && !watered && planted) // tree is growing, not watered, and planted
            {
                gameObject.transform.localScale = new Vector3(health / 100f, health / 100f, health / 100f);
                tempZeroToCalculateHealth += 0.01f * speed;
                if (tempZeroToCalculateHealth > 1)
                {
                    tempZeroToCalculateHealth = 0;
                    health += 1;
                }
            }
            else if (growing && watered && planted) // tree is growing, watered, and planted
            {
                gameObject.transform.localScale = new Vector3(health / 100f, health / 100f, health / 100f);
                tempZeroToCalculateHealth += 0.01f * speed * (1f + wateredSpeed/100f); // tree grows faster by waterSpeed%
                if (tempZeroToCalculateHealth > 1)
                {
                    tempZeroToCalculateHealth = 0;
                    health += 1;
                }
            }
            if (burning && planted) // tree is burning and planted regardless tree is growing or not
            {
                Debug.Log("Tree " + this.gameObject.name + " is burning");
                isburning();
            }
            if (health==100 && planted) // tree is fully grown
            {
                growing = false;
            }
            healthText.GetComponent<TMP_Text>().text = "" + health;
        }
        else // health = 0. destroy tree
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 1f);
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
        }
    }

    public void cutting() // player is chopping this tree
    {
        Debug.Log("cutting");
        if ( health > 0 )
        {
            health -= damage;
        }
    }

    public void drop( int stage )
    {
        if (stage==2) // tree was fully grown
        {
            GameObject instantiated = Instantiate(smallTreeSample, this.gameObject.transform.position, smallTreeSample.transform.rotation, treeSpawner);
            // Instantiate(logSample, this.gameObject.transform.position - new Vector3(0f, 1f, 0f), logSample.transform.rotation, logSpawner);
            // Instantiate(logSample, this.gameObject.transform.position - new Vector3(0f, 1f, 0f), logSample.transform.rotation, logSpawner);
            // Instantiate(logSample, this.gameObject.transform.position - new Vector3(0f, 1f, 0f), logSample.transform.rotation, logSpawner);
        }
        else if (stage==1) // tree was growing
        {
            GameObject instantiated = Instantiate(smallTreeSample, this.gameObject.transform.position - new Vector3(0f, 1f, 0f), smallTreeSample.transform.rotation, treeSpawner);
            instantiated.GetComponent<TreeController>().health = health;
        }
        else if (stage==0) // tree was burning
        {

        }
    }

    public void isburning() // this tree is burning
    {
        tempZeroToCalculateBurning += burningSpeed;
        if(tempZeroToCalculateBurning > 1) 
        {
            tempZeroToCalculateBurning = 0f;
            health -= 1;
        }
    }

    private void OnCollisionEnter(Collision collision) // detects if Blade of Axe is hitting this tree
    {
        if(collision.gameObject.name == "Blade")
        {
            cutting();
        }
    }

    public void holding(SelectEnterEventArgs args) // player is holding this tree
    {
        planted = false;
        Debug.Log("holding");
    }

    public void released(SelectExitEventArgs args) // plater released this tree
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
        planted = true;
        Debug.Log("released");
    }
}
