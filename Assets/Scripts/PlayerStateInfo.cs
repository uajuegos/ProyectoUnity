using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateInfo : MonoBehaviour {

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
