using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    public GameObject particleSystem;
    FMOD.Studio.EventInstance destroyEvent;
    void Start () {
        StartCoroutine(Destroy());
        SoundManager.sm.getEvtinstance("event:/ExplodeSpell", out destroyEvent);
        
        SoundManager.sm.UpdateSM();

    }
    private void OnTriggerEnter(Collider other)
    {
        PlayDestroySound();
        Destroy(Instantiate(particleSystem, transform.position, transform.rotation), 1.5f);
        Destroy(gameObject);

    }
    private void OnCollisionEnter(Collision collision)
    {

        PlayDestroySound();
        Destroy(Instantiate(particleSystem, transform.position, transform.rotation), 1.5f);
        Destroy(gameObject);
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        PlayDestroySound();
        Destroy(Instantiate(particleSystem, transform.position, transform.rotation),1.5f);
        Destroy(gameObject);
    }
    void PlayDestroySound()
    {

        FMOD.ATTRIBUTES_3D pos = new FMOD.ATTRIBUTES_3D();
        SoundManager.sm.SetFMODVector(out pos.position, transform.position);
        SoundManager.sm.SetFMODVector(out pos.up, transform.up);
        SoundManager.sm.SetFMODVector(out pos.forward, transform.forward);
        destroyEvent.set3DAttributes(pos);

        destroyEvent.start();
        SoundManager.sm.UpdateSM();
    }
}
