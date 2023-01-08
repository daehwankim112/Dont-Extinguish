using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CutWoodOnActivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(Chop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Chop(ActivateEventArgs arg)
    {

    }
}
