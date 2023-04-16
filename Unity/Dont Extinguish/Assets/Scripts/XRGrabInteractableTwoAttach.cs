using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    public Transform leftAttachTransform;
    public Transform rightAttachTransform;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Left Hand"))
        {
            attachTransform = leftAttachTransform;
        }
        else if (args.interactorObject.transform.CompareTag("Right Hand"))
        {
            attachTransform = rightAttachTransform;
        }

        SetParentToXRRig();
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(XRBaseInteractor args)
    {
        SetParentToWorld();
        base.OnSelectExited(args);
    }

    public void SetParentToXRRig()
    {
        transform.SetParent(interactorsSelecting[0].transform);
    }

    public void SetParentToWorld()
    {

    }

}
