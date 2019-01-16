using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerDamage : MonoBehaviour {
    public Text lifeText;
    int life = 100;
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    bool block = false;
    Camera c;
    void Start () {
        c = Camera.main;
        life = 100;
        lifeText.text = "Life: " + life.ToString();
        m_Character = GetComponent<ThirdPersonCharacter>();
    }
    private void OnCollisionStay(Collision collision)
    {
        m_Character.Hit(false);
        if (collision.transform.gameObject.layer == 11) {
            if (!block)
            {
                life -= 5;
                lifeText.text = "Life: " + life.ToString();
                block = true;
               StartCoroutine(React());

                if (life <= 0)
                {
                    m_Character.Die();
                    collision.gameObject.GetComponent<Mummy>().ChangeTarget(c.gameObject);
                    c.GetComponent<Rigidbody>().isKinematic = false;
                    c.GetComponent<SmoothCamera>().enabled = false;
                    c.GetComponentInChildren<ChangeProcessor>().Change();
                    Invoke("dead", 1.5f);
                }
               
            }
           
        }
    }
  void dead()
    {
        GetComponent<CapsuleCollider>().height = 1.0f;

    }

    IEnumerator React()
    {
        m_Character.Hit(true);
        yield return new WaitForSeconds(1);
        m_Character.Hit(false);
        block = false;
    }
}
