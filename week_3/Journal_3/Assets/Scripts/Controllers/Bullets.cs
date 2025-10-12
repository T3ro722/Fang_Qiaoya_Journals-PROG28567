using UnityEngine;

public class Bullets : MonoBehaviour
{
    public GameObject bombPrefab;
    public float moveSpeed = 3f;//bullet speed
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawn4way()
    {
        Instantiate(bombPrefab, transform.position + new Vector3(0, 1), Quaternion.identity);
        Instantiate(bombPrefab, transform.position + new Vector3(0, -1), Quaternion.identity);
        Instantiate(bombPrefab, transform.position + new Vector3(1, 0), Quaternion.identity);
        Instantiate(bombPrefab, transform.position + new Vector3(-1, 0), Quaternion.identity);

        //four bullets shoot out in 4 directions

    }
}
