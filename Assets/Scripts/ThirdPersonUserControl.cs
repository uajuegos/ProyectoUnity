using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    { 
        enum State { ATACK, HUNTING}
        State state;
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        int ataque = 0;
        bool blockSpell = false;
        public Collider colHammer;
        public Collider colFoot;
        MagicBall mgBall;
        FMOD.Studio.EventInstance spellEvent;
        FMOD.Studio.EventInstance atackEvent;
        float velH, velV;
        public GameObject hammerAndShield;
        FMOD.ATTRIBUTES_3D pos;

        public bool start = false;

        private void Start()
        {
            SoundManager.sm.getEvtinstance("event:/Spell", out spellEvent);
            SoundManager.sm.getEvtinstance("event:/Hammer", out atackEvent);

            pos = new FMOD.ATTRIBUTES_3D();

            SoundManager.sm.SetFMODVector(out pos.position, transform.position);
            SoundManager.sm.SetFMODVector(out pos.up, transform.up);
            SoundManager.sm.SetFMODVector(out pos.forward, transform.forward);
            spellEvent.set3DAttributes(pos);
   
            SoundManager.sm.UpdateSM();

            SoundManager.sm.UpdateSM();
            state = State.HUNTING;
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
            mgBall = GetComponent<MagicBall>();
        }


        private void Update()
        {
            if (!m_Jump&& start)
            {
                m_Jump = Input.GetButtonDown("Jump");
            }
        }
        public void Started()
        {
            start = true;
        }
        public void Stop()
        {
            

        }
        IEnumerator trackPosition() {
            if (this != null)
            {
                TrackerObject.tr.tracker.AddEvent(Tracker.EventCreator.Position(Tracker.ActorSubjectType.Player, "Level " + SceneManager.GetActiveScene().name, (int)transform.position.x, (int)transform.position.y, (int)transform.position.z));
                yield return new WaitForSeconds(3.0f);
            }
            else
                yield return null;
        }
        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if (start)
            {
                // read inputs
                velH = Input.GetAxis("Horizontal");
                velV = Input.GetAxis("Vertical");

                bool crouch = Input.GetKey(KeyCode.C);
                bool atack = Input.GetKeyDown(KeyCode.V);
                bool spell = Input.GetKeyDown(KeyCode.B);
                bool dance = Input.GetKeyDown(KeyCode.F);
                if (dance)
                {
                    hammerAndShield.SetActive(false);
                }
                else
                {
                    if (Mathf.Abs(velH) >= 0.1f || Mathf.Abs(VelV) >= 0.1f || atack || spell) // URGENTE :mirar en git si no funciona
                        hammerAndShield.SetActive(true);
                }


                int spellAux = 0;
                if (spell && !blockSpell)
                {
                    blockSpell = true;
                    StartCoroutine(StartSpell());
                    spellAux = 1;
                }
                //Bonus de golpes
                int atacar = 0;
                if (atack)
                {
                    
                    StartCoroutine(StartAttack());
                    ataque = (ataque % 3) + 1;
                    atacar = ataque;
                }


                // calculate move direction to pass to character
                if (m_Cam != null)
                {
                    // calculate camera relative direction to move:
                    m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                    m_Move = velV * m_CamForward + velH * m_Cam.right;
                }
                else
                {
                    // we use world-relative directions in the case of no main camera
                    if (!atack)
                        m_Move = velV * Vector3.forward + velH * Vector3.right;
                }
#if !MOBILE_INPUT
                // walk speed multiplier
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    m_Move *= 0.5f;
                    velV *= 0.5f;
                    velH *= 0.5f;
                }
#endif

                // pass all parameters to the character control script
                m_Character.Move(m_Move, crouch, atacar, spellAux, dance, m_Jump);
                m_Jump = false;

                if (state == State.ATACK)
                {
                    colFoot.enabled = true;
                    colHammer.enabled = true;
                }
                else if (state == State.HUNTING)
                {
                    colFoot.enabled = false;
                    colHammer.enabled = false;

                }

                if (m_Character.IsGrouding())
                {
                    state = State.HUNTING;
                }
            }
        }

        IEnumerator StartAttack()
        {
            yield return new WaitForSeconds(.5f);
            atackEvent.start();
            state = State.ATACK;
        }
        IEnumerator StartSpell()
        {
            SoundManager.sm.SetFMODVector(out pos.position, transform.position);
            SoundManager.sm.SetFMODVector(out pos.up, transform.up);
            SoundManager.sm.SetFMODVector(out pos.forward, transform.forward);
            spellEvent.set3DAttributes(pos);
            SoundManager.sm.UpdateSM();
            spellEvent.start();
            yield return new WaitForSeconds(1.25f);
            mgBall.SpawnBall(transform.forward);
            blockSpell = false;

        }

        public float VelH
        {
            get { return velH; }
        }
        public float VelV
        {
            get { return velV; }
        }
        public bool IsGrounded
        {
            get { return m_Character.M_IsGrounded; }
        }
        public bool StartFlag
        {
            set { start = value; }
        }
    }
    
}
