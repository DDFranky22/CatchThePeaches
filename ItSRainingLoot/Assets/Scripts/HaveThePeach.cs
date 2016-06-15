using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HaveThePeach : MonoBehaviour
{

    public bool haveItem;
    public GameObject Item;

    public int Peaches;
    public Text peachesIndicator;
    public VictoryChecker victoryChecker;

    // Use this for initialization
    void Start()
    {
        victoryChecker = GameObject.Find("CheckVictory").GetComponent<VictoryChecker>();
    }

    public void LoseItem()
    {
        CatchMe scriptCatch = Item.GetComponent<CatchMe>();
        scriptCatch.DropMe(this.transform.position);
        haveItem = false;
    }

    void OnGUI()
    {
        peachesIndicator.text = "X" + (victoryChecker.peachesToWin - Peaches);
    }
}
