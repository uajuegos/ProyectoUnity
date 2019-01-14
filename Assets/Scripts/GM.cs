using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GM : MonoBehaviour {

    public enum PlayerState { WANDER, COMBAT, SWIM };
    PlayerState state = PlayerState.WANDER;
    public static GM gm;
    FMOD.Studio.EventInstance eventInstance;
   
    private void Awake()
    {
        if (gm == null)
        {
            //PlayerPrefs.DeleteAll();
            gm = this;
            DontDestroyOnLoad(gameObject);
           
        }
        else if (gm != this)
        {
            Destroy(gameObject);
        }


    }
    // Use this for initialization
    void Start () {
        SoundManager.sm.getEvtinstance("event:/GameMusic", out gm.eventInstance);

        gm.eventInstance.start();
        SoundManager.sm.UpdateSM();
    }
	
    public void setUnderwater(bool uw)
    {

        eventInstance.setParameterValue("Underwater", (float)Convert.ToInt32(uw));
        SoundManager.sm.UpdateSM();
    }

    public PlayerState State
    {
        get { return state; }
        set {
            state = value;
            /*switch (state)
            {
                state;
            }*/
            eventInstance.setParameterValue("War", (int)(state));
            SoundManager.sm.UpdateSM();
        }
    }
}
