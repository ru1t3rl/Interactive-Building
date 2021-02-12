using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public System.Action onGhostEnter;
    [SerializeField] LayerMask ghostLayer;
    
    private void OnTriggerEnter(Collider other)
    {        
        if(ghostLayer.ToInteger() == other.gameObject.layer)
        {
            other.gameObject.SetActive(false);
            onGhostEnter?.Invoke();
        }
    }
}
