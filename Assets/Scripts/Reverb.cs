using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reverb : MonoBehaviour {

	// Use this for initialization
        FMOD.Reverb3D myReverb;
	void Start () {
        SoundManager.sm.createReverb(out myReverb, transform.position, 0.0f, 100.0f, FMOD.PRESET.CAVE());

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
