using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DisableRb : MonoBehaviour
{
    [SerializeField] Rigidbody[] rigidbodies;
    [SerializeField] VelocityEstimator velEstimator;
    [SerializeField] float velMultiplier = 1;
    [SerializeField] string helpMessage;

    public void ToggleRBGravity()
    {
        if (GameManager.Instance.firstPickup)
        {
            GameManager.Instance.ShowHelp(helpMessage, 10);
            GameManager.Instance.firstPickup = false;
        }

        for(int iRb = 0; iRb < rigidbodies.Length; iRb++)
        {
            rigidbodies[iRb].useGravity = !rigidbodies[iRb].useGravity;
            rigidbodies[iRb].AddForce(velEstimator.GetVelocityEstimate() * velMultiplier);
        }
    }
}
