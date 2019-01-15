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
    bool mute = false;
    public Sprite[] muteSprites;
    public GameObject buttonMute;
   
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
        //gm.eventInstance.set3DAttributes
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
            
            eventInstance.setParameterValue("War", (int)(state));
            SoundManager.sm.UpdateSM();
        }
    }
    public void muteMusic()
    {
        mute = !mute;
        if (mute)
        {
            buttonMute.GetComponent<Image>().sprite = muteSprites[1];
            eventInstance.setVolume(0);
        }else
        {
            buttonMute.GetComponent<Image>().sprite = muteSprites[0];
            eventInstance.setVolume(1);
        }
    }
}
