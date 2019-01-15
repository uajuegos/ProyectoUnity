using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Geometry : MonoBehaviour {

    // Use this for initialization
    FMOD.Geometry geometry;
    BoxCollider bx;
    public float directOclusion = 1.0f, reverbOclusion= 1.0f;
	void Start () {
        bx = GetComponent<BoxCollider>();

        Vector3 pos = bx.transform.position;
        Vector3 f = bx.transform.forward;
        Vector3 r = bx.transform.right;
        Vector3 u = bx.transform.up;
        Vector3 min = bx.transform.TransformPoint(bx.center - bx.size * 0.5f) - pos;
        Vector3 max = bx.transform.TransformPoint(bx.center + bx.size * 0.5f) - pos;
        Vector3 P000 = pos + r*  min.x + u* min.y + f * min.z;
        Vector3 P001 = pos + r*  min.x + u*  min.y + f * max.z;
        Vector3 P010 = pos + r * min.x + u*  max.y + f * min.z;
        Vector3 P011 = pos + r  *min.x + u*  max.y + f * max.z;
        Vector3 P100 = pos + r  *max.x + u*  min.y + f * min.z;
        Vector3 P101 = pos + r  *max.x + u*  min.y + f * max.z;
        Vector3 P110 = pos + r  *max.x + u*  max.y + f * min.z;
        Vector3 P111 = pos + r  *max.x + u*  max.y + f * max.z;


        int polygonIndex;
        geometry = new FMOD.Geometry();
        SoundManager.sm.CreateGeometry(out geometry,1, 8);

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


        FMOD.VECTOR[] vertex = new FMOD.VECTOR[8];
        vertex[7] = ver1;
        vertex[6] = ver3;
        vertex[5] = ver7;
        vertex[4] = ver5;
        vertex[3] = ver2;
        vertex[2] = ver4;
        vertex[1] = ver8;
        vertex[0] = ver6;

        geometry.addPolygon(directOclusion, reverbOclusion, true,8, vertex, out polygonIndex);
        FMOD.VECTOR posPolygon = new FMOD.VECTOR();
        SoundManager.sm.SetFMODVector(out posPolygon, transform.position);
        geometry.setPosition(ref posPolygon);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
