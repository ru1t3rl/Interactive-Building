using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RisingSpirit : MonoBehaviour
{
    [Header("Spirit Stuff")]
    [SerializeField] GameObject teleportObject;
    [SerializeField] GameObject objectToMove;
    [SerializeField] Transform target;
    [SerializeField] float duration;
    [SerializeField] LayerMask playerLayer;

    [Header("Player Help")]

    [SerializeField] string message;
    [SerializeField] float messageDuration;
    Coroutine helpRoutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayer.ToInteger())
        {
            teleportObject.SetActive(false);
            objectToMove.SetActive(true);

            // Tween/Lerp the spirit towards the target
            objectToMove.transform.DOMove(target.position, duration).onComplete += () =>
            {
                teleportObject.SetActive(true);
                objectToMove.SetActive(false);
                GameManager.Instance.ShowHelp(message, messageDuration);
                GameManager.Instance.SwitchScene();
            };
        }
    }
}
