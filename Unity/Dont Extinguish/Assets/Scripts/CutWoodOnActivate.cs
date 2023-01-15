using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CutWoodOnActivate : MonoBehaviour
{
    public Transform blade;
    public GameObject spawner;

    private GameObject closestTree;
    private float closestDistance;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(Chop);
        closestDistance = Vector3.Distance(blade.transform.position, spawner.GetComponent<TreeSpawner>().trees[0].transform.position);
        closestTree = spawner.GetComponent<TreeSpawner>().trees[0];
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject tree in spawner.GetComponent<TreeSpawner>().trees)
        {
            if ( Vector3.Distance( blade.transform.position, tree.transform.position) < closestDistance ) // Find the closest tree
            {
                closestTree = tree;
            }
        }

    }

    public void Chop(ActivateEventArgs arg)
    {

    }
}
