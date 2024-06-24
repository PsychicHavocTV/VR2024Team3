using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Tooltip("this is the guantlet in order")]
    public string[] guantletScenes;

    [Tooltip("scene scene is loaded after the last game")]
    public string resultsScene;

    int currentScene;

    private List<float> levelTimes;

    private float currentLevelTimer;

    public static GameManager singleton { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (singleton != null && singleton != this)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void StartGame()
    {
        currentLevelTimer = 0;
        SceneManager.LoadScene(guantletScenes[0]);
        currentScene = 0;

        levelTimes = new List<float>();
    }

    public void FinishCurrentGame()
    {
        float finishTime = currentLevelTimer;

        levelTimes.Add(finishTime);

        currentLevelTimer = 0;

        if(currentScene == guantletScenes.Length)
        {
            SceneManager.LoadScene(resultsScene);
            StartCoroutine(ShowResults());
        } else
        {
            currentScene++;
            SceneManager.LoadScene(guantletScenes[currentScene]);
        }
    }

    void Update()
    {
        currentLevelTimer += Time.deltaTime;
    }

    public IEnumerator ShowResults()
    {
        yield return new WaitForEndOfFrame();
        FindFirstObjectByType<Results>().ShowResults(levelTimes, guantletScenes);
    }
    
    public void CheckEnemies()
    {
        FindAnyObjectByType<GalleryWinChecker>().CheckPoppers();
    }

}
