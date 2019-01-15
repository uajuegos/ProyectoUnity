using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;


public class ChangeProcessor : MonoBehaviour {

    bool underwater = false;
    public PostProcessingProfile normal, underwaterProfile;
    public PostProcessingBehaviour ppb;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        GM.gm.setUnderwater(true);
        ppb.profile = underwaterProfile;
       
        
    }
    private void OnTriggerExit(Collider other)
    {
        GM.gm.setUnderwater(false);

        ppb.profile = normal;
        
    }
}
