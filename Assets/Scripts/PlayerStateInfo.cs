using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


[RequireComponent(typeof(ThirdPersonUserControl))]
public class PlayerStateInfo : MonoBehaviour {

    ThirdPersonUserControl thUserControl;
    FMOD.Studio.EventInstance stepsEvent;
    private void Start()
    {
        thUserControl = GetComponent<ThirdPersonUserControl>();
        SoundManager.sm.SetListener(transform.position,Vector3.zero, new Vector3(0,0,1), transform.up);

        SoundManager.sm.getEvtinstance("event:/Steps2", out stepsEvent);
        stepsEvent.start();
       
    }
    private void Update()
    {
        SoundManager.sm.SetListener(transform.position, Vector3.zero, new Vector3(0, 0, 1), transform.up);
        float suma = Mathf.Max(Mathf.Abs(thUserControl.VelH), Mathf.Abs(thUserControl.VelV));        
        stepsEvent.setParameterValue("Velocidad", suma);
        SoundManager.sm.UpdateSM();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Arena")
        {
            GM.gm.State = GM.PlayerState.COMBAT;
        }
        else if (collision.gameObject.tag == "Terreno")
        {
            GM.gm.State = GM.PlayerState.WANDER;
        }
    }
}
