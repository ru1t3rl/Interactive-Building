using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] PostProcessVolume darkVolume;

    bool switched = false;
    public bool Switched => switched;

    public System.Action onSwitchScene;

    static GameManager instance;
    public static GameManager Instance { get => instance; set => instance = value; }

    [Header("Darkmode Scene")]
    [SerializeField] Material skyboxLight;
    [SerializeField] Material skyboxDark;
    [SerializeField] GameObject darkObjects;
    [SerializeField] Light sun;
    [SerializeField] float darkIntensitiy;
    float baseIntensity;

    [Header("Help Text")]
    [SerializeField] TextMeshPro textObject;
    [SerializeField] float distanceFromPlayer = 20;
    [SerializeField] string victoryMessage;
    Coroutine helpMessageCoroutine;

    [Header("Ghost Pickups")]
    [SerializeField] List<GameObject> ghosts;
    [SerializeField] int ghostToActivate;
    [SerializeField] Portal[] portals;
    int ghostsEntered = 0;
    public bool firstPickup = true;

    private void Awake()
    {
        Instance = this;
        baseIntensity = sun.intensity;

        SetupGhosts();

        for(int iPortal = 0; iPortal < portals.Length; iPortal++)
        {
            portals[iPortal].onGhostEnter += OnGhostEnter;
        }
    }

    void OnGhostEnter()
    {
        ghostsEntered++;
        
        if(ghostsEntered == ghostToActivate)
        {
            ShowHelp(victoryMessage, 7f);
            darkObjects.SetActive(false);
            darkVolume.gameObject.SetActive(false);
        }
    }

    void SetupGhosts()
    {
        for (int i = 0; i < ghosts.Count; i++)
        {
            ghosts[i].SetActive(false);
        }
        
        ghostToActivate = ghostToActivate > ghosts.Count ? ghosts.Count : ghostToActivate;

        int rIndex = 0;
        for (int i = 0; i < ghostToActivate; i++)
        {
            rIndex = Random.Range(0, ghosts.Count);
            ghosts[rIndex].SetActive(true);
            ghosts.RemoveAt(rIndex);
        }
    }

    public void SwitchScene()
    {
        switched = !switched;

        onSwitchScene?.Invoke();

        darkObjects?.SetActive(switched);
        sun.intensity = switched ? darkIntensitiy : baseIntensity;
        UnityEngine.RenderSettings.skybox = switched ? skyboxDark : skyboxLight;

        darkVolume.gameObject?.SetActive(switched);
        darkVolume.isGlobal = switched;
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName).completed += (AsyncOperation operation) => onSwitchScene?.Invoke();
    }

    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex).completed += (AsyncOperation operation) => onSwitchScene?.Invoke();
    }

    public void ShowHelp(string helpMessage, float time = 5f)
    {
        if (helpMessageCoroutine != null)
            StopCoroutine(helpMessageCoroutine);

        helpMessageCoroutine = StartCoroutine(helpRoutine(helpMessage, time));
    }

    IEnumerator helpRoutine(string message, float duration)
    {
        textObject.text = message;
        textObject.gameObject.SetActive(true);

        // Set the y position to the players camera height (use localPosition since camera is a child just like the text)
        textObject.transform.localPosition = new Vector3(
            distanceFromPlayer * Camera.main.transform.forward.x,
            Camera.main.transform.localPosition.y,
            distanceFromPlayer * Camera.main.transform.forward.z);

        textObject.transform.LookAt(Camera.main.transform);
        textObject.transform.Rotate(0, 180, 0);

        yield return new WaitForSeconds(duration);

        textObject.gameObject.SetActive(false);
    }
}
