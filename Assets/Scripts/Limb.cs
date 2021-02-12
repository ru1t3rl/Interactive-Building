using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    public System.Action onHitPortal;
    [SerializeField] LayerMask portalLayer;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == portalLayer.ToInteger())
        {
            onHitPortal.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == portalLayer.ToInteger())
        {
            onHitPortal.Invoke();
        }
    }
}
