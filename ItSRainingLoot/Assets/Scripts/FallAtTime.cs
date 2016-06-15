using UnityEngine;
using System.Collections;

public class FallAtTime : MonoBehaviour
{

    private Rigidbody2D body;
    public float secondFromStar;
    private VictoryChecker victoryChecker;
    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        victoryChecker = GameObject.Find("CheckVictory").GetComponent<VictoryChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (victoryChecker.endGame)
        {
            body.gravityScale = 0.0f;
            body.velocity = new Vector2(0.0f, 0.0f);
        }
    }

}
