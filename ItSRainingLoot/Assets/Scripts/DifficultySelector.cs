using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DifficultySelector : MonoBehaviour
{

    public bool begin;
    public Text countdown;

    public Text levelLabel;
    public Button nextLevelButton;
    public Button previousLevelButton;
    public Text peachCounter;
    public GameObject[] hideOnStart;
    public GameObject[] showOnStart;

    public GUISkin skin;
    public IndieQuiltCommunicator communicator;
    private int difficulty;
    private int maximumLevel;
    public GameObject[] Levels;
    public GameObject activeLevel;
    public VictoryChecker victoryChecker;
    private bool hide;
    private bool startPressed;

    // Use this for initialization
    void Start()
    {
        startPressed = false;
        difficulty = PlayerPrefs.GetInt("CTPDiff");
        maximumLevel = PlayerPrefs.GetInt("CTPMaxDiff");
        
        if (difficulty == 0)
        {
            difficulty = 1;
            activeLevel = Levels[difficulty - 1];
            activeLevel.SetActive(true);
        }
        else
        {
            activeLevel = Levels[difficulty - 1];
            activeLevel.SetActive(true);
        }

        if (maximumLevel == 0)
        {
            maximumLevel = difficulty;
            PlayerPrefs.SetInt("CTPMaxDiff", maximumLevel);
        }

        if (difficulty > maximumLevel)
        {
            maximumLevel = difficulty;
            PlayerPrefs.SetInt("CTPMaxDiff", maximumLevel);
        }

        countdown.enabled = false;
        
        if (!PlayerPrefs.HasKey("CTPInstGiv"))
            PlayerPrefs.SetInt("CTPInstGiv", 0);

        levelLabel.text = "Level " + difficulty;

        victoryChecker = GameObject.Find("CheckVictory").GetComponent<VictoryChecker>();
        victoryChecker.peachesToWin = activeLevel.GetComponent<LevelProperties>().requiredPeaches;
        peachCounter.text = "X"+victoryChecker.peachesToWin;
    }

    // Update is called once per frame
    void Update()
    {
        float startButton = Input.GetAxis("Start");

        if (startButton > 0 && !begin && !startPressed)
        {
            startPressed = true;
            PlayerPrefs.SetInt("CTPDiff", difficulty);
            Time.timeScale = 1.0f;
            hide = true;
            StartCoroutine(StartCountDown());
        }
    }

    IEnumerator StartCountDown()
    {
        countdown.enabled = true;
        countdown.text = "";
        if (PlayerPrefs.GetInt("CTPInstGiv") == 0)
        {
            countdown.text = "Take ALL the peaches";
            yield return new WaitForSeconds(2.0f);
            countdown.text = "in 30 seconds";
            yield return new WaitForSeconds(2.0f);
            countdown.text = "Do NOT get caught";
            yield return new WaitForSeconds(2.0f);
            countdown.text = "Run & Walljump";
            yield return new WaitForSeconds(2.0f);
            countdown.text = "Good luck";
            PlayerPrefs.SetInt("CTPInstGiv", 1);
            yield return new WaitForSeconds(1.0f);
        }
        for (int i = 0; i <= 3; i++)
        {
            yield return new WaitForSeconds(1.0f);
            countdown.text = "" + (3 - (i));
        }
        begin = true;
    }

    void OnGUI()
    {
        if (hide)
        {
            foreach (GameObject obj in hideOnStart)
            {
                obj.SetActive(false);
            }
            foreach(GameObject obj in showOnStart)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            if (difficulty == maximumLevel)
            {
                nextLevelButton.interactable = false;
            }
            else
            {
                nextLevelButton.interactable = true;
            }
            if (difficulty == 1)
            {
                previousLevelButton.interactable = false;
            }
            else
            {
                previousLevelButton.interactable = true;
            }
        }
    }

    public void nextLevel()
    {
        if (difficulty < maximumLevel && difficulty < maximumLevel && difficulty < 10)
        {
            if (!nextLevelButton.interactable)
            {
                nextLevelButton.interactable = true;
            }
            activeLevel.SetActive(false);
            difficulty++;
            activeLevel = Levels[difficulty - 1];
            activeLevel.SetActive(true);
            victoryChecker.peachesToWin = activeLevel.GetComponent<LevelProperties>().requiredPeaches;
            levelLabel.text = "Level " + difficulty;
        }

    }

    public void previousLevel()
    {
        if (difficulty > 1)
        {
            if (!previousLevelButton.interactable)
            {
                previousLevelButton.interactable = true;
            }
            activeLevel.SetActive(false);
            difficulty--;
            activeLevel = Levels[difficulty - 1];
            activeLevel.SetActive(true);
            victoryChecker.peachesToWin = activeLevel.GetComponent<LevelProperties>().requiredPeaches;
            levelLabel.text = "Level " + difficulty;
        }

    }

    public int getDifficulty()
    {
        return difficulty;
    }
}
