using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;
using FMOD;


public class Menu : MonoBehaviour {
    FMOD.Studio.EventInstance eventInstance;
    private void Start()
    {


        SoundManager.sm.getEvtinstance("event:/MenuMusic", out eventInstance);

        eventInstance.start();
        SoundManager.sm.UpdateSM();

    }
    public void Jugar()
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SoundManager.sm.UpdateSM();
        SceneManager.LoadScene("Game");
    }
}
