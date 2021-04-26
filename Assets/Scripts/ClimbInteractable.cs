using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRBaseInteractable
{
    protected override void OnSelectEntering(XRBaseInteractor interactor)
    {
        base.OnSelectEntering(interactor);

        // setup new climbing hand
        if(interactor is XRDirectInteractor) // stops climbing with rays
            Climber.climbingHand = interactor.GetComponent<XRController>();
    }

    protected override void OnSelectExiting(XRBaseInteractor interactor)
    {
        base.OnSelectExiting(interactor);

        if(interactor is XRDirectInteractor && Climber.climbingHand.name == interactor.name)
        {
            Climber.climbingHand = null;
        }

    }
}
