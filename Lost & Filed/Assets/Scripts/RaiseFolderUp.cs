using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaiseFolderUp : MonoBehaviour
{
    public Transform folderRaiseHeight;
    public float raiseTime;
    public Material textMaskedMat;
    public Material textUnmaskedMat;
    Vector3 startPosition;
    Vector3 endPosition;
    bool folderInCabinet = true;

    // Start is called before the first frame update
    public void RaiseUp() {
        folderInCabinet=false;
        startPosition = transform.position;
        //enable click blocker collider
        endPosition = new Vector3(startPosition.x, folderRaiseHeight.position.y,startPosition.z);
        StartCoroutine(RaiseCoroutine(startPosition,endPosition,raiseTime));
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        GetComponentInChildren<TextMeshPro>().fontMaterial = textUnmaskedMat;
    }

    public void PutAway() {
        StartCoroutine(RaiseCoroutine(endPosition,startPosition,raiseTime));
        folderInCabinet=true;

    }

    IEnumerator RaiseCoroutine(Vector3 startPos, Vector3 endPos, float duration) {
        float timer = 0;
        while (timer<duration) {
            transform.position = Vector3.Lerp(startPos,endPos,timer/duration);
            timer+=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        // raise big folder
        // pass script to folder anim to make returning the folder correct
            
        if(folderInCabinet) {
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        GetComponentInChildren<TextMeshPro>().fontMaterial = textMaskedMat;
        //disable click blocker collider
        }
    }
}
