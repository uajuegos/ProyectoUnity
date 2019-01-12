using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GM : MonoBehaviour {

    public static GM gm;
    
   
    private void Awake()
    {
        if (gm == null)
        {
            //PlayerPrefs.DeleteAll();
            gm = this;
            DontDestroyOnLoad(gameObject);
           
        }
        else if (gm != this)
        {
            Destroy(gameObject);
        }


    }
    // Use this for initialization
    void Start () {
       
	}
	

}
