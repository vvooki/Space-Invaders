using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMS : MonoBehaviour
{
    public GameObject ship;
    public float spawnTime = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnShip", spawnTime, spawnTime); 
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void SpawnShip()
    {
        var newShip = GameObject.Instantiate(ship);
        
    }
}
