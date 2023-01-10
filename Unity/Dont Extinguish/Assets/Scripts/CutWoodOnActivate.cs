using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CutWoodOnActivate : MonoBehaviour
{
    public Transform blade;
    public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(Chop);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject tree in spawner.GetComponent<TreeSpawner>().trees)
        {
            if ( Vector3.Distance( blade.transform.position, tree.transform.position) < 0.1f ) // Blade is colliding with tree
            {
                tree.GetComponent<TreeController>().cutting();
            }
        }
        
    }

    public void Chop(ActivateEventArgs arg)
    {

    }
}
