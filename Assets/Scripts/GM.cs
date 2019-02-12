using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GM : MonoBehaviour {

    
    public static GM gm;
    FMOD.Studio.EventInstance musicInstance;
    FMOD.Studio.EventInstance ambientInstance;
    bool mute = false;
    public Sprite[] muteSprites;
    public GameObject[] componentes;
    public GameObject buttonMute;
    public GameObject camera;
    public GameObject canvas;
    public GameObject canvasIntro;

    AsyncOperation loading;
    bool start = false;
   
    private void Awake()
    {
       
            gm = this;
     


    }
    // Use this for initialization
    void Start () {
       
        //canvasIntro.gameObject.SetActive(true);
        canvas.SetActive(true);
        SoundManager.sm.getEvtinstance("event:/GameMusic", out gm.musicInstance);
       
       
        SoundManager.sm.getEvtinstance("event:/Ambiente", out gm.ambientInstance);
        gm.ambientInstance.start();
        SoundManager.sm.UpdateSM();
        camera.GetComponent<SmoothCamera>().enabled = true;
        //gm.musicInstance.start();
        start = true;

        foreach (GameObject go in componentes)
        {
            go.SendMessage("Started");
        }

    }
    public void Cinematica()
    {
        start = false;
        camera.GetComponent<SmoothCamera>().enabled = false;
        camera.GetComponent<Animation>().Play("CameraInicio");
        canvasIntro.gameObject.SetActive(true);
        canvas.SetActive(false);
        gm.musicInstance.start();

        foreach (GameObject go in componentes)
        {
            go.SendMessage("Stop");
        }
    }
    private void Update()
    {
        if (!start)
        {
            if (!camera.GetComponent<Animation>().IsPlaying("CameraInicio"))
            {
                start = true;
               
                foreach (GameObject go in componentes)
                {
                    go.SendMessage("Started");
                }
                
                canvas.SetActive(true);
                canvasIntro.gameObject.SetActive(false);
                camera.GetComponent<SmoothCamera>().enabled = true;

            }
        }
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
    public void ChangeScene(string scene)
    {
            loading = SceneManager.LoadSceneAsync(scene);
            loading.allowSceneActivation = false;

        StartCoroutine(clearSM(scene));
    }
    public bool Started
    {
        get { return start; }
    }

    IEnumerator clearSM(string scene)
    {
        yield return new WaitForSeconds(0.5f);
        loading.allowSceneActivation = true;
        SoundManager.sm.UpdateSM();
        SoundManager.sm.Clear();

    }
}
