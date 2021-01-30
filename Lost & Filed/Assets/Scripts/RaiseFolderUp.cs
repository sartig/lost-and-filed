using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum FoundObject
{
    Key,PowerBank,Child,Laptop,Umbrella,Glasses,Wallet,Passport,Headphones,GameConsole,Ring,
    StuffedBear,Dog,Luggage,Revolver,CardboardBox,Sword,Grandmother,VideoTape,Familiar,Head,NSFW
}

public class RaiseFolderUp : MonoBehaviour
{
    Transform drawerCabinet;
    public FoundObject myObject;
    public Transform folderRaiseHeight;
    public float raiseTime;
    public Material textMaskedMat;
    public Material textUnmaskedMat;
    public LargeFolderScript largeFolderScript;
    Vector3 startPosition;
    Vector3 endPosition;
    bool folderInCabinet = true;
    ClientManager clientManager;

    void Awake() {
        clientManager = GameObject.FindObjectOfType<ClientManager>();
        drawerCabinet = GameObject.Find("DrawerFront").transform;
    }

    // Start is called before the first frame update
    public void RaiseUp() {
        folderInCabinet=false;
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x, folderRaiseHeight.position.y,startPosition.z);
        StartCoroutine(RaiseCoroutine(startPosition,endPosition,raiseTime));
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        GetComponentInChildren<TextMeshPro>().fontMaterial = textUnmaskedMat;
    }

    public void PutAway(bool andShutCabinet) {
        if(!andShutCabinet) {
            StartCoroutine(RaiseCoroutine(endPosition,startPosition,raiseTime));
            folderInCabinet=true;
        }
        else {
            StartCoroutine(SlamCabinet(endPosition,startPosition,raiseTime));
            folderInCabinet=true;
        }

    }

    public void CabinetSlamOnly() {
        StartCoroutine(JustSlamCabinet());
    }

    IEnumerator JustSlamCabinet() {
        float timer = 0;
        Vector3 startPos = drawerCabinet.position;
        Vector3 endPos = new Vector3(-5.42f,-5.87f,0f);
        while (timer<0.2f) {
            drawerCabinet.position = Vector3.Lerp(startPos,endPos,timer/0.2f);
            timer+=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        drawerCabinet.position = endPos;
    }

    IEnumerator SlamCabinet(Vector3 startPos, Vector3 endPos, float duration) {
        float timer = 0;
        while (timer<duration) {
            transform.position = Vector3.Lerp(startPos,endPos,timer/duration);
            timer+=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if(folderInCabinet) {
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        GetComponentInChildren<TextMeshPro>().fontMaterial = textMaskedMat;
        }
        StartCoroutine(JustSlamCabinet());
    }

    IEnumerator RaiseCoroutine(Vector3 startPos, Vector3 endPos, float duration) {
        float timer = 0;
        while (timer<duration) {
            transform.position = Vector3.Lerp(startPos,endPos,timer/duration);
            timer+=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if(!folderInCabinet) { // raise big folder
            largeFolderScript.LargeFolderAppear(this,GetComponentInChildren<TextMeshPro>().text); // pass this script to folder so it can call PutAway()
            
        }

        if(folderInCabinet) {
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        GetComponentInChildren<TextMeshPro>().fontMaterial = textMaskedMat;
        }
    }
}
