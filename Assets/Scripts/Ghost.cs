using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] Limb[] limbs;

    private void Awake()
    {
        for(int iLimb = 0; iLimb < limbs.Length; iLimb++)
        {
            limbs[iLimb].onHitPortal += onHitPortal;
        }
    }

    void onHitPortal()
    {
        gameObject.SetActive(false);
        for (int iLimb = 0; iLimb < limbs.Length; iLimb++)
        {
            limbs[iLimb].gameObject.SetActive(false);
        }
    }
}
