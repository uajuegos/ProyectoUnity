using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mummy : MonoBehaviour {

    enum State { WANDERING, ATACK}
    State state;
    Rigidbody rb;
    public GameObject target;
    public GameObject particleSystem;
    public float vel = 1;
    public int life = 5;
    float timeToRefresh = 0.01f;
    float tempY;
    Animator m_Animator;
    bool blockAttack = false;
	// Use this for initialization
	void Start () {
        state = State.WANDERING;
        rb =gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,0,1);
        m_Animator = GetComponent<Animator>();
    }


    // Update is called once per frame
	void FixedUpdate () {
        m_Animator.SetBool("Attack", false);
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if(distance < 1.5f && !blockAttack)
        {
            StartCoroutine(Attack());
        }
        else if ( distance < 7.5f)
            state = State.ATACK;
        else state = State.WANDERING;

        tempY = rb.velocity.y;
        switch (state)
        {
            case State.ATACK:
               
                transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                rb.velocity = transform.forward * vel;
                rb.velocity = new Vector3(rb.velocity.x, tempY, rb.velocity.z);

                break;
            case State.WANDERING:
                if (Random.Range(0, 200) == 1)
                {
                    transform.Rotate(new Vector3(0, 1, 0), 90);
                    //rb.velocity = transform.forward*vel;
                   // rb.velocity = new Vector3(rb.velocity.x, tempY, rb.velocity.z);
                }
                rb.velocity = transform.forward * vel;
                rb.velocity = new Vector3(rb.velocity.x, tempY, rb.velocity.z);
                break;
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer != 12)
            transform.Rotate(new Vector3(0, 1, 0), 180);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Hammer" || other.name == "RigRFoot" || other.name == "Firebola") StartCoroutine(Hit());
    }
    IEnumerator Hit()
    {
        m_Animator.SetBool("Hit",true);
        yield return new WaitForSeconds(0.5f);
        m_Animator.SetBool("Hit", false);
        Destroy(Instantiate(particleSystem, transform.position, transform.rotation),1.5f);
        Destroy(gameObject);
    }
    IEnumerator Attack()
    {
        blockAttack = true;
        m_Animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.5f);
        m_Animator.SetBool("Attack", false);
        blockAttack = false;
    }
}
