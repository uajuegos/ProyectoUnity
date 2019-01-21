using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


[RequireComponent(typeof(ThirdPersonUserControl))]
public class PlayerStateInfo : MonoBehaviour {

    public enum PlayerState { WANDER, COMBAT, SWIM, JUMPING };
    PlayerState state = PlayerState.WANDER;
    ThirdPersonUserControl thUserControl;
    FMOD.Studio.EventInstance stepsEvent;
    FMOD.Studio.EventInstance jumpEvent;
    public GameObject camera;
    bool isGrounded = true;

    private void Start()
    {

        thUserControl = GetComponent<ThirdPersonUserControl>();
        SoundManager.sm.SetListener(transform.position,Vector3.zero, camera.transform.forward.normalized, camera.transform.up.normalized);

        SoundManager.sm.getEvtinstance("event:/Steps2", out stepsEvent);
        stepsEvent.start();
        SoundManager.sm.getEvtinstance("event:/Jump", out jumpEvent);

    }
    private void Update()
    {
        if (isGrounded) isGrounded = !Input.GetKeyDown(KeyCode.Space);
		SoundManager.sm.SetListener(transform.position, Vector3.zero, camera.transform.forward.normalized, camera.transform.up.normalized);
        float suma = 0;
       
        if (state != PlayerState.SWIM && state != PlayerState.JUMPING)
        {
            suma = Mathf.Max(Mathf.Abs(thUserControl.VelH), Mathf.Abs(thUserControl.VelV));
        }      
        stepsEvent.setParameterValue("Velocidad", suma);
        SoundManager.sm.UpdateSM();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (state != PlayerState.SWIM)
        {
            if (collision.gameObject.tag == "Arena")
            {
                state = PlayerState.COMBAT;
                GM.gm.ChangeMusic((int)state);
                stepsEvent.setParameterValue("War", 1);

                if (!isGrounded)
                {
                    jumpEvent.setParameterValue("Tipo", 1);
                    jumpEvent.start();
                }

                
            }
            else if (collision.gameObject.tag == "Terreno")
            {
                state = PlayerState.WANDER;
                GM.gm.ChangeMusic((int)state);
                stepsEvent.setParameterValue("War", 0);
                if (!isGrounded)
                {
                    jumpEvent.setParameterValue("Tipo", 1);
                    jumpEvent.start();
                }
                
            }
            isGrounded = thUserControl.IsGrounded;
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
       
        if (state != PlayerState.SWIM && (collision.gameObject.tag == "Arena"|| collision.gameObject.tag == "Terreno"))
        {

            if (!isGrounded)
            {
                state = PlayerState.JUMPING;
                jumpEvent.setParameterValue("Tipo", 0);
                jumpEvent.start();

            }

        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Agua")
        {
            if(state == PlayerState.JUMPING && !isGrounded)
            {
                jumpEvent.setParameterValue("Tipo", 2);
                jumpEvent.start();
            }
            state = PlayerState.SWIM;
            Time.timeScale *= 0.5f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Agua")
        {
            state = PlayerState.WANDER;
            Time.timeScale = 1f;
        }
    }
    public bool IsGrounded
    {
        get { return isGrounded; }
        set { isGrounded = value; }
    }
}
