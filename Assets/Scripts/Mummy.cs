using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mummy : MonoBehaviour {

    enum State { WANDERING, ATACK}
    State state;
    Rigidbody rb;
    public GameObject spawner;
    public GameObject target;
    public GameObject particleSystem;
    public GameObject particleSystemStart;

    public bool start;
    public float vel = 1;
    public int life = 5;
    float timeToRefresh = 0.01f;
    float tempY;
    Animator m_Animator;
    bool blockAttack = false;
    FMOD.Studio.EventInstance deathInstance;
    FMOD.Studio.EventInstance spawnInstance;
    FMOD.Studio.EventInstance hammerHitInstance;
    FMOD.ATTRIBUTES_3D pos;
    SkinnedMeshRenderer[] list;
    // Use this for initialization
    void Start () {
        list = GetComponentsInChildren<SkinnedMeshRenderer>();
        transform.position = spawner.transform.GetChild(Random.Range(0, 3)).transform.position;
        Destroy(Instantiate(particleSystemStart, transform.position, transform.rotation), 1);
        start = false;
        state = State.WANDERING;
        rb =gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,0,1);
        m_Animator = GetComponent<Animator>();
        SoundManager.sm.getEvtinstance("event:/MuerteMomia", out deathInstance);
        SoundManager.sm.getEvtinstance("event:/SpawnMomia", out spawnInstance);
        SoundManager.sm.getEvtinstance("event:/HammerHit", out hammerHitInstance);

        pos = new FMOD.ATTRIBUTES_3D();

        SoundManager.sm.SetFMODVector(out pos.position, transform.position);
        SoundManager.sm.SetFMODVector(out pos.up, transform.up);
        SoundManager.sm.SetFMODVector(out pos.forward, transform.forward);
        deathInstance.set3DAttributes(pos);
        spawnInstance.set3DAttributes(pos);
        hammerHitInstance.set3DAttributes(pos);
        spawnInstance.start();

        SoundManager.sm.UpdateSM();
    }

    public void Started()
    {
        start = true;
    }
    // Update is called once per frame
    void FixedUpdate () {
        m_Animator.SetBool("Attack", false);
        if (start)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < 1.5f && !blockAttack)
            {
                StartCoroutine(Attack());
            }
            else if (distance < 7.5f)
                state = State.ATACK;
            else state = State.WANDERING;
        }

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
        SoundManager.sm.SetFMODVector(out pos.position, transform.position);
        SoundManager.sm.SetFMODVector(out pos.up, transform.up);
        SoundManager.sm.SetFMODVector(out pos.forward, transform.forward);
        deathInstance.set3DAttributes(pos);
        SoundManager.sm.UpdateSM();
    }
    public void ChangeTarget(GameObject tg)
    {
        target = tg;
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
        life -= 5;
        actualizaPos();
        hammerHitInstance.set3DAttributes(pos);
        SoundManager.sm.UpdateSM();
        hammerHitInstance.start();
        m_Animator.SetBool("Hit", true);
        yield return new WaitForSeconds(0.5f);
        m_Animator.SetBool("Hit", false);
        if (life <= 0)
        {
            deathInstance.start();
            SoundManager.sm.UpdateSM();
            Destroy(Instantiate(particleSystem, transform.position, transform.rotation), 1.0f);
           
            foreach (SkinnedMeshRenderer sknd in list ) sknd.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            yield return new WaitForSeconds(1.5f);
            transform.position = spawner.transform.GetChild(Random.Range(0, 2)).transform.position;
            Destroy(Instantiate(particleSystemStart, transform.position,transform.rotation), 1);
            foreach (SkinnedMeshRenderer sknd in list) sknd.enabled = true;
            GetComponent<CapsuleCollider>().enabled = true;
            actualizaPos();
            spawnInstance.set3DAttributes(pos);
            SoundManager.sm.UpdateSM();
            spawnInstance.start();
            life = 5;
        }
    }
    IEnumerator Attack()
    {
        blockAttack = true;
        m_Animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.5f);
        m_Animator.SetBool("Attack", false);
        blockAttack = false;
    }
    void actualizaPos()
    {
        SoundManager.sm.SetFMODVector(out pos.position, transform.position);
        SoundManager.sm.SetFMODVector(out pos.up, transform.up);
        SoundManager.sm.SetFMODVector(out pos.forward, transform.forward);
    }
}
