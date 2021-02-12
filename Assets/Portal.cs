using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public System.Action onGhostEnter;
    [SerializeField] LayerMask ghostLayer;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"A gameObject with the layer {other.gameObject.layer} hit me ");
        if(ghostLayer.ToInteger() == other.gameObject.layer)
        {
            other.gameObject.SetActive(false);
            onGhostEnter?.Invoke();
        }
    }
}
