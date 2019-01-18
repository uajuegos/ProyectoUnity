using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour {


    FMOD.Studio.EventInstance onButton, click;

    // Use this for initialization
    void Start () {
        SoundManager.sm.getEvtinstance("event:/OnButton", out onButton);
        SoundManager.sm.getEvtinstance("event:/ClickButton", out click);
        SoundManager.sm.UpdateSM();


        
    }


    public void onClickButton() {
        Debug.Log("clic button");
        click.start();
        SoundManager.sm.UpdateSM();
        
    }

    public void OnMouseOver()
    {
        Debug.Log("mouseOver");
        onButton.start();
        SoundManager.sm.UpdateSM();
    }
}
