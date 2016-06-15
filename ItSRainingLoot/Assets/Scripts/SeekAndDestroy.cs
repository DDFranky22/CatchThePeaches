using UnityEngine;
using System.Collections;

public class SeekAndDestroy : MonoBehaviour
{

    public DifficultySelector start;
    public GameObject Player;
    private Vector3 startingPosition;
    public float speed;
    public float movementSpeed;
    public bool chase;
    public bool Left;
    public bool Right;

    private VictoryChecker victoryScript;

    // Use this for initialization
    void Start()
    {
        start = Camera.main.GetComponent<DifficultySelector>();
        Player = GameObject.FindGameObjectWithTag("Player") as GameObject;
        startingPosition = this.transform.position;
        victoryScript = GameObject.Find("CheckVictory").GetComponent<VictoryChecker>();
        chase = true;
    }

    private bool DoINeedToChase()
    {
        if (Right)
        {
            if (Player.transform.position.x < 0.0f)
            {
                return false;
            }
        }
        else if (Left)
        {
            if (Player.transform.position.x > 0.0f)
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start.begin)
        {
            if (Player == null)
                Player = this.gameObject;
            if (victoryScript.endGame)
            {
                this.transform.position = this.transform.position;
            }
            else if (chase)
                this.transform.position = Vector3.MoveTowards(this.transform.position, Player.transform.position, movementSpeed * Time.deltaTime);
            chase = DoINeedToChase();
            if (!chase)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, startingPosition, movementSpeed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Player = this.gameObject;
            victoryScript.Busted = true;
        }
    }
}
