using UnityEngine;

public class RowGeneration : MonoBehaviour
{
    public float squares;
    public GameObject squarePrefab;
    public float spacing = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRow()
    {
        float startX = - ((squares - 1) * spacing) / 2;
        for (int i = 0; i < squares; i++)
        {
            Vector3 position = new Vector3(startX + i * spacing, 0, 0);
            Instantiate(squarePrefab, position, Quaternion.identity);
        }
    }

    public void changesqnumber()
    {
        //squares = 
    }
}
