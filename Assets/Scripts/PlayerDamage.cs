using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerDamage : MonoBehaviour {
    public Slider lifeSlider;
    private int maxLife = 100;
    int life;
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    public GameObject particlesPuff;
    bool block = false;
    Camera c;
    void Start () {
        c = Camera.main;
        life = 100;
        lifeSlider.value = (float)life / maxLife;
        m_Character = GetComponent<ThirdPersonCharacter>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("beer"))
        {
            Instantiate(particlesPuff, other.transform.position, other.transform.rotation);
            life += 30;
            if (life > maxLife) life = maxLife;
            lifeSlider.value = (float)life / maxLife;

            Destroy(other.gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        m_Character.Hit(false);
        if (collision.transform.gameObject.layer == 11) {
            if (!block)
            {
                life -= 20;
                if (life < 0) life = 0;
                lifeSlider.value = (float)life / maxLife;
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
