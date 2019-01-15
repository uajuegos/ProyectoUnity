using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GM : MonoBehaviour {

    
    public static GM gm;
    FMOD.Studio.EventInstance musicInstance;
    FMOD.Studio.EventInstance ambientInstance;
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
        SoundManager.sm.getEvtinstance("event:/GameMusic", out gm.musicInstance);
        gm.musicInstance.start();
        SoundManager.sm.getEvtinstance("event:/Ambiente", out gm.ambientInstance);
        gm.ambientInstance.start();
        SoundManager.sm.UpdateSM();
    }
	
    public void setUnderwater(bool uw)
    {
        ambientInstance.setParameterValue("Underwater", (float)Convert.ToInt32(uw));
        musicInstance.setParameterValue("Underwater", (float)Convert.ToInt32(uw));
        SoundManager.sm.UpdateSM();
    }

    public void ChangeMusic(int state)
    {
        musicInstance.setParameterValue("War", (int)(state));
        SoundManager.sm.UpdateSM();
    }
   
    public void muteMusic()
    {
        mute = !mute;
        if (mute)
        {
            buttonMute.GetComponent<Image>().sprite = muteSprites[1];
            musicInstance.setVolume(0);
        }else
        {
            buttonMute.GetComponent<Image>().sprite = muteSprites[0];
            musicInstance.setVolume(1);
        }
    }
}
