using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceLowLevel : MonoBehaviour {

    // Use this for initialization
    FMOD.Sound sonido;
    public string soundName;
    FMOD.Channel source; 
	void Start () {
        sonido = new FMOD.Sound();
        SoundManager.sm.loadSound(soundName, out sonido);
        source = new FMOD.Channel();
        SoundManager.sm.setSource(out source, sonido);

        FMOD.VECTOR pos = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out pos, transform.position);
        FMOD.VECTOR vel = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out vel, new Vector3(0,0,0));
        FMOD.VECTOR alt_pan = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out alt_pan, Vector3.zero);
       
        if(source.set3DAttributes(ref pos, ref vel,ref alt_pan) != FMOD.RESULT.OK) Debug.Log("Nosepue");
        SoundManager.sm.UpdateSM();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
