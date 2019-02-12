using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCinematica : MonoBehaviour {

    // Use this for initialization
   public  GameObject[] mummies;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GM.gm.Cinematica();
            Destroy(gameObject);
            foreach (GameObject go in mummies) go.SetActive(true);
        }
    }
}
