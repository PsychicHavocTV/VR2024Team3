using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Tooltip("this is the guantlet in order")]
    public string[] guantletScenes;

    [Tooltip("scene scene is loaded after the last game")]
    public string resultsScene;

    int currentScene;

    private List<float> levelTimes;

    private float currentLevelTimer;

    public TMP_Text timerText;

    public TMP_Text backBoardText;

    public Animator rotator;

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
        //called only when the start button in the start screen is pressed
        currentLevelTimer = 0;
        SceneManager.LoadScene(guantletScenes[0]);
        currentScene = 0;

        levelTimes = new List<float>();

        timerText.gameObject.SetActive(true);
    }

    public void Restart()
    {
        //called only when the restart button is shot
        SceneManager.LoadScene("Start");
        currentLevelTimer = 0;
        currentScene = 0;

        levelTimes = new List<float>();
        timerText.gameObject.SetActive(false);
    }

    public void FinishCurrentGame()
    {
        //when a game is over they will call this wich will trigger an animation to call load next level
        float finishTime = currentLevelTimer;

        levelTimes.Add(finishTime);

        if (backBoardText)
        {
            string results = "";

            for (int i = 0; i < guantletScenes.Length; i++)
            {
                results += guantletScenes[i] + " time: " + levelTimes[i] + "\n";
            }

            backBoardText.text = results;
        }

        currentLevelTimer = 0;

        rotator.SetTrigger("LevelOver");
    }

    public void LoadNextLevel()
    {
        //loads either the next level is gauntletScenes or the results scene
        currentLevelTimer = 0;

        if (currentScene == guantletScenes.Length)
        {
            SceneManager.LoadScene(resultsScene);
            StartCoroutine(ShowResults());
        }
        else
        {
            currentScene++;
            SceneManager.LoadScene(guantletScenes[currentScene]);
        }
    }

    void Update()
    {
        //update timer
        currentLevelTimer += Time.deltaTime;


        timerText.text = currentLevelTimer.ToString("0.000");
    }

    public IEnumerator ShowResults()
    {
        //this is called when the scene is loaded so we need to wait for the scene to be active 
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame(); 
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        FindFirstObjectByType<Results>().ShowResults(levelTimes, guantletScenes);
    }
    
    public void CheckEnemies()
    {
        FindAnyObjectByType<GalleryWinChecker>().CheckPoppers();
    }

    public void TakeDamage(float damage)
    {
        currentLevelTimer += damage;
    }

}
