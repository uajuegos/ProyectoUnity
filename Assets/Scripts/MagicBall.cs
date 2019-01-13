using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour {

    // Use this for initialization
    public Fireball ball;
	void Start () {
       
	}
	
	public void SpawnBall(Vector3 dir)
    {
        Fireball aux = Instantiate(ball, new Vector3(transform.position.x+dir.x, transform.position.y + 1, transform.position.z+dir.z), transform.rotation);
        aux.GetComponent<Rigidbody>().velocity = dir*5;
        aux.name = ball.name;

    }
}
