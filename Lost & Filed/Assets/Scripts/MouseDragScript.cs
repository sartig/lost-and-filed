using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDragScript : MonoBehaviour
{
    public Transform drawerParent;
    Vector3 mousePosition;
    Vector3 delta;
    AudioSource cabinetDrawerSFX;

    void Awake() {
        cabinetDrawerSFX = GameObject.Find("CabinetDrawerMoving").GetComponent<AudioSource>();
    }
    
    private void OnMouseDown() {
        mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        delta = mousePosition-drawerParent.position;
    }

    private void OnMouseDrag() {
        mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 drawerRawPosition = mousePosition-delta;
        drawerRawPosition.x = Mathf.Clamp(drawerRawPosition.x,-5.42f,83.79066f);
        drawerRawPosition.y = Mathf.Clamp(drawerRawPosition.y,-55.30952f,-5.87f);
        Vector3 drawerSnappedPosition = NearestPointOnLine(drawerParent.position,new Vector3(1,-0.5543f,0),drawerRawPosition);
        drawerSnappedPosition.z=0;
        //drawerParent.position = mousePosition-delta;
        drawerParent.position = drawerSnappedPosition;
        cabinetDrawerSFX.volume=Mathf.Clamp(cabinetDrawerSFX.volume+Time.deltaTime*5,0,1);
    }

    private void OnMouseUp() {
        StartCoroutine(ReduceCabinetVolume());
    }
    IEnumerator ReduceCabinetVolume() {
        while(cabinetDrawerSFX.volume>0) {
            cabinetDrawerSFX.volume = Mathf.Clamp(cabinetDrawerSFX.volume-Time.deltaTime*5,0,1);
            yield return new WaitForEndOfFrame();
        }
    }

    public static Vector3 NearestPointOnLine(Vector3 linePnt, Vector3 lineDir, Vector3 pnt)
{
    lineDir.Normalize();//this needs to be a unit vector
    var v = pnt - linePnt;
    var d = Vector3.Dot(v, lineDir);
    return linePnt + lineDir * d;
}

    // y = -0.5543x - 8.886629 (?)
    // xMin = -5.42
    // xMax = 83.79066
    // yMin = -55.31952
    // yMax = -5.87

    // diff between drawers: x: -6.5-.6.17 = -12.6285405015
    // diff between drawers: y: 7
}
