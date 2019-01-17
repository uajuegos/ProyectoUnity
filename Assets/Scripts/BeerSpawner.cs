using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerSpawner : MonoBehaviour {
    public GameObject beer;
    public GameObject[] spawners;
	// Use this for initialization
	void Start () {
        Invoke("instantiateBeer", 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void instantiateBeer()
    {
        System.Random rnd = new System.Random();
        
        Instantiate(beer,spawners[rnd.Next(0,spawners.Length)].transform.position + new Vector3(rnd.Next(0,5),0, rnd.Next(0, 5)), Quaternion.identity).transform.Rotate(new Vector3(-90,0,0));
        Invoke("instantiateBeer", 10.0f);
    }
}
