using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Geometry : MonoBehaviour {

    // Use this for initialization
    FMOD.VECTOR[] vertex1;
    FMOD.VECTOR[] vertex2;
    FMOD.VECTOR[] vertex3;
    int[] polInx = new int[3];

    BoxCollider bx;
    public float directOclusion = 1.0f, reverbOclusion= 1.0f;
    
    void Start () {
        bx = GetComponent<BoxCollider>();

        vertex1 = new FMOD.VECTOR[4];
        vertex2 = new FMOD.VECTOR[4];
        vertex3 = new FMOD.VECTOR[4];



        FMOD.VECTOR ver1 = new FMOD.VECTOR();
        FMOD.VECTOR ver2 = new FMOD.VECTOR();
        FMOD.VECTOR ver3 = new FMOD.VECTOR();
        FMOD.VECTOR ver4 = new FMOD.VECTOR();

        FMOD.VECTOR ver5 = new FMOD.VECTOR();
        FMOD.VECTOR ver6 = new FMOD.VECTOR();
        FMOD.VECTOR ver7 = new FMOD.VECTOR();
        FMOD.VECTOR ver8 = new FMOD.VECTOR();

        FMOD.VECTOR ver9 = new FMOD.VECTOR();
        FMOD.VECTOR ver10 = new FMOD.VECTOR();
        FMOD.VECTOR ver11 = new FMOD.VECTOR();
        FMOD.VECTOR ver12 = new FMOD.VECTOR();
    

        BoxCollider b = GetComponent<BoxCollider>();

        SoundManager.sm.SetFMODVector(out ver1, transform.TransformPoint(b.center + new Vector3(b.size.x, 0, b.size.z) * 0.5f));
        SoundManager.sm.SetFMODVector(out ver2, transform.TransformPoint(b.center + new Vector3(-b.size.x, 0, b.size.z) * 0.5f));
        SoundManager.sm.SetFMODVector(out ver3, transform.TransformPoint(b.center + new Vector3(-b.size.x, 0, -b.size.z) * 0.5f));
        SoundManager.sm.SetFMODVector(out ver4, transform.TransformPoint(b.center + new Vector3(b.size.x, 0, -b.size.z) * 0.5f));

        SoundManager.sm.SetFMODVector(out ver5, transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, 0) * 0.5f));
        SoundManager.sm.SetFMODVector(out ver6, transform.TransformPoint(b.center + new Vector3(-b.size.x, b.size.y, 0) * 0.5f));
        SoundManager.sm.SetFMODVector(out ver7, transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, 0) * 0.5f));
        SoundManager.sm.SetFMODVector(out ver8, transform.TransformPoint(b.center + new Vector3(b.size.x, b.size.y, 0) * 0.5f));

        SoundManager.sm.SetFMODVector(out ver9, transform.TransformPoint(b.center + new Vector3(0, b.size.y, b.size.z) * 0.5f));
        SoundManager.sm.SetFMODVector(out ver10, transform.TransformPoint(b.center + new Vector3(0, b.size.y, -b.size.z) * 0.5f));
        SoundManager.sm.SetFMODVector(out ver11, transform.TransformPoint(b.center + new Vector3(0, -b.size.y, -b.size.z) * 0.5f));
        SoundManager.sm.SetFMODVector(out ver12, transform.TransformPoint(b.center + new Vector3(0, -b.size.y, b.size.z) * 0.5f));

        vertex3[0] = ver9;
        vertex3[1] = ver10;
        vertex3[2] = ver11;
        vertex3[3] = ver12;

        vertex2[0] = ver1;
        vertex2[1] = ver2;
        vertex2[2] = ver3;
        vertex2[3] = ver4;
        //YRGB
        vertex1[0] = ver5;
        vertex1[1] = ver8;
        vertex1[2] = ver6;
        vertex1[3] = ver7;



        //SoundManager.sm.CreateGeometry();
        SoundManager.sm.AddPolygon(directOclusion, reverbOclusion, true, 4, vertex1, out polInx[0]);
        SoundManager.sm.AddPolygon(directOclusion, reverbOclusion, true, 4, vertex2, out polInx[1]);
        SoundManager.sm.AddPolygon(directOclusion, reverbOclusion, true, 4, vertex3, out polInx[2]);
        
    }
	
    //private void OnDrawGizmosSelected()
    //{
    //    Color[] colores = new Color[4];
    //    colores[0] = Color.green;
    //    colores[1] = Color.blue;
    //    colores[2] = Color.yellow;
    //    colores[3] = Color.red;


    //    for (int i = 0; i < 4; i++)
    //    {
    //        Gizmos.color = colores[i];
    //        Gizmos.DrawSphere(new Vector3(vertex1[i].x, vertex1[i].y, vertex1[i].z), 0.3f);
    //       // Gizmos.color = colores[1];
    //       // Gizmos.DrawSphere(new Vector3(vertex3[i].x, vertex3[i].y, vertex3[i].z), 0.3f);
    //       //Gizmos.color = colores[2];
    //       // Gizmos.DrawSphere(new Vector3(vertex2[i].x, vertex2[i].y, vertex2[i].z), 0.3f);
    //    }
    //}


}
