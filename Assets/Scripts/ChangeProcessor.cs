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
    public void Change ()
    {

        StartCoroutine(ChangeFocusDistance());
    }

    IEnumerator ChangeFocusDistance()
    {
        ppb.profile = new PostProcessingProfile();
        int i = 10;
        ppb.profile.depthOfField.enabled = true;
        var f = ppb.profile.depthOfField.settings;
       
        while (i >= 0)
        {
            f.focusDistance = i;
            i--;
            ppb.profile.depthOfField.settings = f;
           yield return new WaitForSeconds(0.1f);
        }
    }
}
