using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictoryChecker : MonoBehaviour
{

    public Timer timerscript;
    public bool Timeout;
    public bool Busted;
    public HaveThePeach catched;
    public CreatePeach created;
    public Text textOnScreen;
    public int peachesToWin;
    public bool endGame;

    private bool oneShot;
    //one opporinity

    public IndieQuiltCommunicator Communicator;

    public AudioSource[] musics;


    private int Level;
    // Use this for initialization
    void Start()
    {
        timerscript = GameObject.Find("Timer").GetComponent<Timer>();
        Level = Camera.main.GetComponent<DifficultySelector>().getDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        checkVictory();
    }

    void checkVictory()
    {
        if (Timeout && catched.Peaches != peachesToWin)
        {
            textOnScreen.text = "Game Over";
            timerscript.stop = true;
            Communicator.success = false;
            endGame = true;
            musics[2].Stop();
            if (!musics[0].isPlaying && !oneShot)
            {
                musics[0].Play();
                oneShot = true;
                Level = Camera.main.GetComponent<DifficultySelector>().getDifficulty();
            }
            StartCoroutine(GameOver());
            return;
        }
        if (catched.Peaches == peachesToWin && !Timeout)
        {
            textOnScreen.text = "You Win";
            timerscript.stop = true;
            Communicator.success = true;
            endGame = true;
            musics[2].Stop();
            if (!musics[1].isPlaying && !oneShot)
            {
                musics[1].Play();
                oneShot = true;
                Level = Camera.main.GetComponent<DifficultySelector>().getDifficulty();

                if (Level < 10)
                    Level += 1;
            }
            StartCoroutine(GameOver());
            return;
        }
        if (Busted)
        {
            textOnScreen.text = "Game Over";
            timerscript.stop = true;
            Communicator.success = false;
            endGame = true;
            musics[2].Stop();
            if (!musics[0].isPlaying && !oneShot)
            {
                musics[0].Play();
                Level = Camera.main.GetComponent<DifficultySelector>().getDifficulty();
                oneShot = true;
            }
            StartCoroutine(GameOver());
            return;
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2.0f);
        PlayerPrefs.SetInt("CTPDiff", Level);
        Communicator.finished = true;
        Application.LoadLevel(Application.loadedLevelName);
    }

}
