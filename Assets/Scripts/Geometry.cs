using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Geometry : MonoBehaviour {

    // Use this for initialization
    FMOD.VECTOR[] vertex1;
    FMOD.VECTOR[] vertex2;
    BoxCollider bx;
    public float directOclusion = 1.0f, reverbOclusion= 1.0f;
    
    void Start () {
        bx = GetComponent<BoxCollider>();
        vertex1 = new FMOD.VECTOR[4];
        vertex2 = new FMOD.VECTOR[4];
        
        Vector3 pos = bx.transform.position;
        Vector3 f = bx.transform.forward;
        Vector3 r = bx.transform.right;
        Vector3 u = bx.transform.up;                                                                                
        Vector3 min = bx.transform.TransformPoint(bx.center - bx.size * 0.5f) - pos;
        Vector3 max = bx.transform.TransformPoint(bx.center + bx.size * 0.5f) - pos;
        Vector3 P000 = pos + r * min.x + u * min.y + f * min.z;
        P000.x += (max.x - min.x) / 2.0f; //PLANO1
        Vector3 P001 = pos + r * min.x + u * min.y + f * max.z;
        P001.x += (max.x - min.x) / 2.0f;//PLANO1
        Vector3 P010 = pos + r * min.x + u * max.y + f * min.z;
        P010.x += (max.x - min.x) / 2.0f;//PLANO1
        Vector3 P011 = pos + r * min.x + u * max.y + f * max.z;
        P011.x += (max.x - min.x) / 2.0f;//PLANO1
        Vector3 P100 = pos + r * max.x + u * min.y + f * min.z;
        P100.x -= (max.x - min.x); P100.z += (max.z - min.z) / 2.0f; //PLANO2
        Vector3 P101 = pos + r * max.x + u * min.y + f * max.z;
        P101.z -= (max.z - min.z) / 2.0f;//PLANO2
        Vector3 P110 = pos + r * max.x + u * max.y + f * min.z;
        P110.x -= (max.x - min.x); P110.z += (max.z - min.z) / 2.0f;//PLANO2
        Vector3 P111 = pos + r * max.x + u * max.y + f * max.z;
        P111.z -= (max.z - min.z) / 2.0f;//PLANO2

        int polygonIndex;
        
        

        FMOD.VECTOR ver1 = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out ver1, P000);
        FMOD.VECTOR ver2 = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out ver2, P001);
        FMOD.VECTOR ver3 = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out ver3, P010);
        FMOD.VECTOR ver4 = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out ver4, P011);

        FMOD.VECTOR ver5 = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out ver5, P100);
        FMOD.VECTOR ver6 = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out ver6, P101);
        FMOD.VECTOR ver7 = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out ver7, P110);
        FMOD.VECTOR ver8 = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out ver8, P111);


        vertex2[0] = ver1;
        vertex2[1] = ver2;
        vertex2[3] = ver3;
        vertex2[2] = ver4;
        //YRGB
        vertex1[1] = ver8;
        vertex1[2] = ver7;
        vertex1[0] = ver6;
        vertex1[3] = ver5;

        Debug.Log(gameObject.name);
      

        //SoundManager.sm.CreateGeometry();
        SoundManager.sm.AddPolygon(directOclusion, reverbOclusion, true, 4, vertex1, out polygonIndex);
        SoundManager.sm.AddPolygon(directOclusion, reverbOclusion, true, 4, vertex2, out polygonIndex);
        
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log( bx.transform.forward.x + "   " + bx.transform.forward.y + "   " + bx.transform.forward.z);
        Debug.Log(bx.transform.right.x + "   " + bx.transform.right.y + "   " + bx.transform.right.z);
        Debug.Log(bx.transform.up.x + "   " + bx.transform.up.y + "   " + bx.transform.up.z);

	}
    //private void OnDrawGizmosSelected()
    //{
    //    Color[] colores = new Color[4];
    //    colores[2] = Color.yellow;
    //    colores[3] = Color.red;
    //    colores[0] = Color.green;
    //    colores[1] = Color.blue;
        
        
    //    for (int i = 0; i < 4; i++)
    //    {
    //        Gizmos.color = colores[i];
    //        Gizmos.DrawSphere(new Vector3(vertex2[i].x, vertex2[i].y, vertex2[i].z), 0.5f);
    //        Gizmos.DrawSphere(new Vector3(vertex1[i].x, vertex1[i].y, vertex1[i].z), 0.5f);
    //    }
    //}

}
