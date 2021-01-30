using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LargeFolderScript : MonoBehaviour
{
    // Start is called before the first frame update
    RaiseFolderUp triggeringScript;
    Animator m_animator;
    public ClientManager clientManager;
    public TextMeshPro folderName;
    public RandomizeColours[] polaroidRandomizers;
    public GameObject[] randomObjectPrefabContainers;
    int arrayEntry; // basically used as a long-winded mapping of enum -> array position as an int
    public TextMeshPro[] codeTexts;
    string[] codes = new string[6];

    void Awake() {
        m_animator = GetComponent<Animator>();
    }
    public void LargeFolderAppear(RaiseFolderUp triggerScript, string categoryName) {
        triggeringScript = triggerScript;
        m_animator.SetBool("largeFolderActive",true);
        folderName.text = categoryName;
        foreach(RandomizeColours rc in polaroidRandomizers) { //randomize polaroid frames
            rc.Randomize(false);
        }
        foreach(GameObject go in randomObjectPrefabContainers) { //disable all prefabs
            for (int i = 0; i < go.transform.childCount; i++) {
                go.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        EnablePrefabsOfType(triggeringScript.myObject); //enable prefabs of triggering script type
        clientManager.RecordPlayerSelectedObjectType(triggeringScript.myObject); //pass type to client manager
        if(clientManager.GetRequestedObjectType()==triggeringScript.myObject) { // player picked correct folder
            // generate 1 wrong and 5 correct prefabs
            int correctPolaroid = Random.Range(0,randomObjectPrefabContainers.Length);
            Debug.Log("picked prefab number " + correctPolaroid + " as correct");
            for (int i = 0; i < randomObjectPrefabContainers.Length; i++)
            {
                if(i==correctPolaroid) { // correct prefab
                    int[] targetConfig = new int[clientManager.GetRequestedObjectConfig().Length];
                    System.Array.Copy(clientManager.GetRequestedObjectConfig(),targetConfig,clientManager.GetRequestedObjectConfig().Length);
                    randomObjectPrefabContainers[i].transform.GetChild(arrayEntry).GetComponent<RandomizeColours>().ApplyItemConfig(targetConfig);
                }
                else { // 5 guaranteed wrong prefabs
                    randomObjectPrefabContainers[i].transform.GetChild(arrayEntry).GetComponent<RandomizeColours>().Randomize(true);
                }
            }
        }
        else { // player picked wrong folder
            foreach(GameObject go in randomObjectPrefabContainers) { // generate 6 random prefabs, who cares if they look the same
                go.transform.GetChild(arrayEntry).GetComponent<RandomizeColours>().Randomize(false);
            }
            
        }
        // generate 6 unique codes, make note of all. format is 4 digits + A or B at end
        for(int i = 0; i < codes.Length; i++) {
            codes[i] =  Random.Range(0,10000).ToString("D4");
            codes[i] += Random.Range(0,2)==0?"A":"B";
            codeTexts[i].text = codes[i]; //actually, just forget about checking for uniqueness (for now). What are the odds?
        }
            /*
            If I was smart, I'd have the int arrays + object type somehow generate a unique code
            Then that way I wouldn't have to actually record it, just re-generate it when needed
            But I'm not, so recording it is the way~
            */
    }

    public void CheckForValidCode(string playerTypedCode) {
        for(int i = 0; i < codes.Length; i ++) {
            if(playerTypedCode==codes[i]) { // recreate the prefab in the correct client's hand
                Debug.Log("Player matched a code");
                clientManager.GiveActiveClientAnItem(randomObjectPrefabContainers[i].transform.GetChild(arrayEntry).GetComponent<RandomizeColours>().GetItemConfiguration(), arrayEntry);
            }
            // otherwise the poor person gets nothing :(
        }
    }

    public void LargeFolderDisappear(bool andShutCabinet) {
        if(m_animator.GetBool("largeFolderActive")) {
            m_animator.SetBool("largeFolderActive",false);
            if(andShutCabinet) {
                Invoke("ReturnSmallFolderAndShutCabinet",0.5f);
            }
            else {
                Invoke("ReturnSmallFolder",0.5f);
            }
        }
        else {
            JustShutCabinet();
        }
    }

    void ReturnSmallFolder() {
        triggeringScript.PutAway(false);
    }
    void ReturnSmallFolderAndShutCabinet() {
        triggeringScript.PutAway(true);
    }
    void JustShutCabinet() {
        GameObject.FindObjectOfType<RaiseFolderUp>().CabinetSlamOnly();
    }

    void EnablePrefabsOfType(FoundObject foundObject) { // is there a better way to do this? definitely. but this works.
        arrayEntry=0;
        switch(foundObject) {
            case FoundObject.StuffedBear:
                arrayEntry=0;
                break;
            case FoundObject.CardboardBox:
                arrayEntry=1;
                break;
            case FoundObject.Child:
                arrayEntry=2;
                break;
            case FoundObject.Dog:
                arrayEntry=3;
                break;
            case FoundObject.Head:
                arrayEntry=4;
                break;
            case FoundObject.GameConsole:
                arrayEntry=5;
                break;
            case FoundObject.Glasses:
                arrayEntry=6;
                break;
            case FoundObject.Grandmother:
                arrayEntry=7;
                break;
            case FoundObject.Headphones:
                arrayEntry=8;
                break;
            case FoundObject.Key:
                arrayEntry=9;
                break;
            case FoundObject.Laptop:
                arrayEntry=10;
                break;
            case FoundObject.Luggage:
                arrayEntry=11;
                break;
            case FoundObject.Passport:
                arrayEntry=12;
                break;
            case FoundObject.PowerBank:
                arrayEntry=13;
                break;
            case FoundObject.Revolver:
                arrayEntry=14;
                break;
            case FoundObject.Ring:
                arrayEntry=15;
                break;
            case FoundObject.Sword:
                arrayEntry=16;
                break;
            case FoundObject.VideoTape:
                arrayEntry=17;
                break;
            case FoundObject.Umbrella:
                arrayEntry=18;
                break;
            case FoundObject.Wallet:
                arrayEntry=19;
                break;
            case FoundObject.Familiar:
                arrayEntry=20;
                break;
            default:
                arrayEntry=0;
                Debug.Log("Got unknown object, defaulting to bear");
                break;
        }
        foreach(GameObject go in randomObjectPrefabContainers) {
            go.transform.GetChild(arrayEntry).gameObject.SetActive(true);
        }
    }    
}
