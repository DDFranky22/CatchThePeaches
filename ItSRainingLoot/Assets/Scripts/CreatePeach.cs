using UnityEngine;
using System.Collections;

public class CreatePeach : MonoBehaviour
{
    public float NumberOfPeaches;
    public GameObject peachPrefab;
    private float seconds;
    private DifficultySelector start;
    private bool started;

    void Start()
    {
        start = Camera.main.GetComponent<DifficultySelector>();
    }

    // Use this for initialization

    public void createSinglePeach()
    {
        Collider2D collider = GetComponent<Collider2D>();
        GameObject peach = Instantiate(peachPrefab, new Vector3(Random.Range(this.transform.position.x - collider.bounds.size.x / 2, this.transform.position.x + collider.bounds.size.x / 2), this.transform.position.y - collider.bounds.size.y), Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started && start.begin)
        {
            started = true;
            createSinglePeach();
        }
    }
}
