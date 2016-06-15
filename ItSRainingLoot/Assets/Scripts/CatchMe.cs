using UnityEngine;
using System.Collections;

public class CatchMe : MonoBehaviour
{

    private bool added;
    public GameObject instantiator;
    private CreatePeach createPeachScript;

    void Start()
    {
        instantiator = GameObject.Find("Instantiator");
        createPeachScript = instantiator.GetComponent<CreatePeach>();
    }

    public void DropMe(Vector3 position)
    {
        this.gameObject.transform.position = Vector3.zero;
        this.gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        HaveThePeach script = c.gameObject.GetComponent<HaveThePeach>();
        if (script != null)
        {
            script.haveItem = true;
            if (!added)
            {
                script.Peaches++;
                added = true;
            }
            createPeachScript.createSinglePeach();
            Destroy(this.gameObject);
        }
    }
}
