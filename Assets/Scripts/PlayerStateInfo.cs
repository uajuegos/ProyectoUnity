using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateInfo : MonoBehaviour {

    private void Start()
    {
        SoundManager.sm.SetListener(transform.position,Vector3.zero, new Vector3(0,0,1), transform.up);
    }
    private void Update()
    {
        SoundManager.sm.SetListener(transform.position, Vector3.zero, new Vector3(0, 0, 1), transform.up);
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
