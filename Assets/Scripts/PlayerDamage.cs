using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerDamage : MonoBehaviour {
    public Text lifeText;
    int life = 100;
    int contColision;
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
                                              // Use this for initialization
    void Start () {
        contColision = 50;
        life = 100;
        lifeText.text = "Life: " + life.ToString();
        m_Character = GetComponent<ThirdPersonCharacter>();
    }
    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.transform.gameObject.layer == 11) {
            if (contColision == 50)
            {
                life -= 5;
                lifeText.text = "Life: " + life.ToString();
                if(life <= 0)
                {
                    m_Character.Die();
                }
                contColision = 0;
            }
            contColision++;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.gameObject.layer == 11)
        {
            contColision = 50;
        }
    }

}
