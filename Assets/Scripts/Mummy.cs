using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mummy : MonoBehaviour {

    enum State { WANDERING, ATACK}
    State state;
    Rigidbody rb;
    public GameObject target;
    public float vel = 1;
    public int life = 5;
	// Use this for initialization
	void Start () {
        state = State.WANDERING;
        rb =gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,0,1);
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(target.transform.position, transform.position) < 7.5f)
            state = State.ATACK;
        else state = State.WANDERING;

        switch (state)
        {
            case State.ATACK:
              
                transform.LookAt(target.transform);
                rb.velocity = transform.forward*vel;

                break;
            case State.WANDERING:
                if (Random.Range(0, 200) == 1)
                {
                    transform.Rotate(new Vector3(0, 1, 0), 90);
                    rb.velocity = transform.forward;
                }
                break;
        }
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Hammer" || other.name == "RigRFoot") Destroy(gameObject,.5F);
    }
}
