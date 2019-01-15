using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tocadiscos : MonoBehaviour {

    FMOD.Studio.EventInstance eventInstance;
    bool playing = false;
    Animation anim;
    public GameObject panelKey;
    // Use this for initialization
    void Start () {
        anim = GetComponentInChildren<Animation>();
        SoundManager.sm.getEvtinstance("event:/HipHop", out eventInstance);
        
        FMOD.ATTRIBUTES_3D pos = new FMOD.ATTRIBUTES_3D();

        SoundManager.sm.SetFMODVector(out pos.position, transform.position);
        SoundManager.sm.SetFMODVector(out pos.up, transform.up);
        SoundManager.sm.SetFMODVector(out pos.forward, new Vector3(-1,0,0));
        eventInstance.set3DAttributes(pos);
        panelKey.SetActive(false);
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        panelKey.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        panelKey.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
               
        if (Input.GetKeyUp(KeyCode.E))
        {
            playing = !playing;
            if (playing) {
                eventInstance.start();
                SoundManager.sm.UpdateSM();
                anim.Play();
            }
            else
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                SoundManager.sm.UpdateSM();
                anim.Stop();
            }
        }
    }
}
