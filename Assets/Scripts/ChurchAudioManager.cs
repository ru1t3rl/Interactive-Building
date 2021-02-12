using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using DG.Tweening;

public class ChurchAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource noise, music;
    [SerializeField] float minDistToPlayer = 50f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float fadeDuration = 1f;
    RaycastHit hit;
    float baseVolume;
    bool fading = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        baseVolume = noise.volume;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayer.ToInteger())
            PlayMusic();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayer.ToInteger())
            PlayNoise();
    }

    private void PlayMusic()
    {
        if (noise.isPlaying)
            noise.DOFade(0.4f, fadeDuration);

        if(!music.isPlaying)
        {
            music.volume = 0;
            music.DOFade(1, fadeDuration);
            music.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == playerLayer.ToInteger())
            PlayMusic();
    }

    private void PlayNoise()
    {
        if (noise.volume != baseVolume && !fading)
        {
            noise.DOFade(baseVolume, fadeDuration).onComplete = () => { fading = false; };
            fading = true;
        }

        if (music.isPlaying && !GameManager.Instance.Switched)
        {
            music.DOFade(0, fadeDuration).onComplete = music.Pause;
        }
    }
}
