using UnityEngine.XR.Interaction.Toolkit;

// https://forum.unity.com/threads/xrgrabinteractable-velocitytracking-massive-lag-stutter-when-moving-player.993144/

public class XRGrabVelocityTracked : XRGrabInteractable
{
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        SetParentToXRRig();
        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        SetParentToWorld();
        base.OnSelectExited(interactor);
    }

    public void SetParentToXRRig()
    {
        transform.SetParent(selectingInteractor.transform);
    }

    public void SetParentToWorld()
    {
        transform.SetParent(null);
    }
}