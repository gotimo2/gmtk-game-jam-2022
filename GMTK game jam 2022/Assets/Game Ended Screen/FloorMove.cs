using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    public GameObject grid;
    public GameObject gridPrefab;
    public int x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        grid.transform.position = grid.transform.position - new Vector3(x, 0, 0) * Time.deltaTime;
        if (grid.transform.position.x <= -22.74998f)
        {
            Instantiate(gridPrefab, new Vector3(25.00002f, 0,0), transform.rotation);
            Destroy(grid);
        }
        
    }
}
