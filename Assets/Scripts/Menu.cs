using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;
using FMOD;


public class Menu : MonoBehaviour {
    FMOD.Studio.EventInstance eventInstance;
    AsyncOperation loading;

    private void Start()
    {


        SoundManager.sm.getEvtinstance("event:/MenuMusic", out eventInstance);

        eventInstance.start();
        SoundManager.sm.UpdateSM();

    }
    public void ChangeScene(string scene)
    {
        if (scene == "Game")
        {
            loading = SceneManager.LoadSceneAsync(scene);
            loading.allowSceneActivation = false;
        }

        StartCoroutine(clearSM(scene));
    }
    IEnumerator clearSM(string scene) {
        yield return new WaitForSeconds(0.5f);
        loading.allowSceneActivation = true;
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SoundManager.sm.UpdateSM();
        SoundManager.sm.Clear();
        if (scene == "Exit")
            Application.Quit();
    }
}
