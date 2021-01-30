﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDragScript : MonoBehaviour
{
    public Transform drawerParent;
    Vector3 mousePosition;
    Vector3 delta;
    
    private void OnMouseDown() {
        Debug.Log("Mouse down");
        mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        delta = mousePosition-drawerParent.position;
    }

    private void OnMouseDrag() {
        Debug.Log("Mouse dragging");
        mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 drawerRawPosition = mousePosition-delta;
        Vector3 drawerSnappedPosition = NearestPointOnLine(drawerParent.position,new Vector3(1,-0.5543f,0),drawerRawPosition);
        drawerSnappedPosition.z=0;
        //drawerParent.position = mousePosition-delta;
        drawerParent.position = drawerSnappedPosition;
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
    // xMax = ??
    // yMin = ??
    // yMax = -5.87

    // diff between drawers: x: -6.5-.6.17 = -12.6285405015
    // diff between drawers: y: 7
}