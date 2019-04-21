using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;
using TrackerP3;

public class PlayerDamage : MonoBehaviour
{
    public Slider lifeSlider;
    private int maxLife = 100;
    int life;
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    public GameObject particlesPuff;
    FMOD.Studio.EventInstance beerFx;
    FMOD.Studio.EventInstance hit;
    bool block = false;
    Camera c;
    public GameObject gameOverPanel;
    void Start()
    {
        gameOverPanel.SetActive(false);
        c = Camera.main;
        life = 100;
        lifeSlider.value = (float)life / maxLife;
        m_Character = GetComponent<ThirdPersonCharacter>();
        SoundManager.sm.getEvtinstance("event:/Beer", out beerFx);
        SoundManager.sm.getEvtinstance("event:/HitPlayer", out hit);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("beer"))
        {
            beerFx.start();
            BeerSpawner.instance.drinkedBeer();
            Instantiate(particlesPuff, other.transform.position, other.transform.rotation);
            life += 15;
            if (life > maxLife) life = maxLife;
            lifeSlider.value = (float)life / maxLife;
            Tracker.Instance.AddEvent(EventCreator.Damage(ActorSubjectType.Item, ActorSubjectType.Player, "HP: +15"));


            Destroy(other.gameObject);
        }

        else if (other.gameObject.CompareTag("DeathZone"))
        {
            Tracker.Instance.AddEvent(EventCreator.Dead(ActorSubjectType.DeathZone, ActorSubjectType.Player, "Position" + transform.position.ToString()));
            Death();
            
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        m_Character.Hit(false);
        if (collision.transform.gameObject.layer == 11)
        {
            if (!block)
            {
                life -= 5;
                if (life < 0) life = 0;
                lifeSlider.value = (float)life / maxLife;
                block = true;
                Tracker.Instance.AddEvent(EventCreator.Damage(ActorSubjectType.Enemy, ActorSubjectType.Player, "HP: -5"));
                StartCoroutine(React());

                if (life <= 0)
                {
                    collision.gameObject.GetComponent<Mummy>().ChangeTarget(c.gameObject);
                    Tracker.Instance.AddEvent(EventCreator.Dead(ActorSubjectType.Enemy, ActorSubjectType.Player, "Position" + transform.position.ToString()));
                    Death();
                }

            }

        }
    }

    private void Death()
    {
        gameOverPanel.SetActive(true);
        GetComponent<ThirdPersonUserControl>().StartFlag = false;
        m_Character.Die();
        c.GetComponent<Rigidbody>().isKinematic = false;
        c.GetComponent<SmoothCamera>().enabled = false;
        c.GetComponentInChildren<ChangeProcessor>().Change();
        Invoke("dead", 1.5f);
    }

    void dead()
    {
        GetComponent<CapsuleCollider>().height = 1.0f;

    }

    IEnumerator React()
    {
        hit.start();
        m_Character.Hit(true);
        yield return new WaitForSeconds(1);
        m_Character.Hit(false);
        block = false;
    }
}
