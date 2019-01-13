using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour {

    // Use this for initialization
    public GameObject ball;
	void Start () {
       
	}
	
	public void SpawnBall(Vector3 dir)
    {
        GameObject aux = Instantiate(ball, new Vector3(transform.position.x+dir.x, transform.position.y + 1, transform.position.z+dir.z), transform.rotation);
        aux.GetComponent<Rigidbody>().velocity = dir*5;
        Destroy(aux, 2);

    }
}
