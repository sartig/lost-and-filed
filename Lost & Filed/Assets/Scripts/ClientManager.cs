using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{

    public Animator clientAAnimator; // triggers = "GoToWindow" , "LeaveWindow"
    public Animator clientBAnimator;
    public Animator specialClientAnimator;
    public GameObject[] specialClientSprites;
    public SpriteRenderer speechBubbleSR;
    int currentActiveClient; //0 = A, 1 = B, 2 = special
    int specialClientNumber; // for the randomizable prefab later
    FoundObject requestedObjectType;
    FoundObject playerSelectedObjectType;
    int[] requestedObjectConfig;
    public RandomizeColours[] bubbleRandomizers;
    public GameObject[] regularClientRandomizableObjects; // the randomizable object prefab on the client's hand for when they collect
    public GameObject[] specialClientRandomizableObjects;
    List<int> specialCharsServed = new List<int>();
    List<bool> specialCharsServedCorrectly = new List<bool>();

    int requestCount;

    void Awake() {
        speechBubbleSR.enabled=false;
    }

    void Start() {
        clientAAnimator.SetTrigger("GoToWindow");
        Invoke("MakeNormalRequest",2f);
    }

    public void ClearRequestDisplay() {
        foreach(RandomizeColours rc in bubbleRandomizers) {
            rc.gameObject.SetActive(false);
        }
        speechBubbleSR.enabled=false;
    }

    public void MakeNextRequest() {
        if(requestCount%6==0) { // every 7th client is "special"
            currentActiveClient = 2;
            specialClientAnimator.SetTrigger("GoToWindow");

            foreach(GameObject go in specialClientSprites) {
                go.SetActive(false);
            }
            requestedObjectType = (FoundObject)Random.Range(13,22);
            if(requestedObjectType == FoundObject.Luggage) {
                requestedObjectType=FoundObject.Passport; // fix for Jorji
            }
            EnableCorrectSpecialSprite(requestedObjectType);

            Invoke("MakeSpecialRequest",2f);
        }
        else {
            currentActiveClient = requestCount%2;
            if(currentActiveClient==0) {
                clientAAnimator.SetTrigger("GoToWindow");
            }
            else {
                clientBAnimator.SetTrigger("GoToWindow");
            }
            Invoke("MakeNormalRequest",2f);
        }
    }

    void MakeNormalRequest() {
        Debug.Log("Making normal request");
        speechBubbleSR.enabled=true;
        requestedObjectType = (FoundObject)Random.Range(0,14);
        switch(requestedObjectType) {
            case FoundObject.StuffedBear: // stuffed bear
                requestedObjectConfig = new int[5]; // 5 variables
                requestedObjectConfig[0] = Random.Range(0,2); // 2 possible base colours
                requestedObjectConfig[1] = Random.Range(0,2); // 2 possible button colours
                requestedObjectConfig[2] = Random.Range(0,2); // 2 eye colours
                requestedObjectConfig[3] = Random.Range(0,3); // 3 eye types
                requestedObjectConfig[4] = Random.Range(0,3); // 3 nose colours
                bubbleRandomizers[0].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[0].gameObject.SetActive(true);
                break;
            case FoundObject.Child: // child
                requestedObjectConfig = new int[6]; // 6 variables
                requestedObjectConfig[0] = Random.Range(0,3); // 3 body colours
                requestedObjectConfig[1] = Random.Range(0,4); // 4 hair colours
                requestedObjectConfig[2] = Random.Range(0,4); // 4 hair shapes
                requestedObjectConfig[3] = Random.Range(0,4); // 4 pants colours
                requestedObjectConfig[4] = Random.Range(0,4); // 4 shirt colours
                requestedObjectConfig[5] = Random.Range(0,4); // 4 pants colours
                bubbleRandomizers[2].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[2].gameObject.SetActive(true);
                break;
            case FoundObject.Dog: // dog
                requestedObjectConfig = new int[2]; // 2 variables
                requestedObjectConfig[0] = Random.Range(0,4); // 4 collar colours
                requestedObjectConfig[1] = Random.Range(0,2); // 2 skin colours
                bubbleRandomizers[3].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[3].gameObject.SetActive(true);
                break;
            case FoundObject.GameConsole: // game console
                requestedObjectConfig = new int[2]; // 2 variables
                requestedObjectConfig[0] = Random.Range(0,4); // 4 base colours
                requestedObjectConfig[1] = Random.Range(0,3); // 3 types
                bubbleRandomizers[5].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[5].gameObject.SetActive(true);
                break;
            case FoundObject.Glasses: // glasses
                requestedObjectConfig = new int[2]; // 2 variables
                requestedObjectConfig[0] = Random.Range(0,4); // 4 frame colours
                requestedObjectConfig[1] = Random.Range(0,3); // 3 frame shapes
                bubbleRandomizers[6].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[6].gameObject.SetActive(true);
                break;
            case FoundObject.Headphones: // headphones
                requestedObjectConfig = new int[4]; // 4 variables
                requestedObjectConfig[0] = Random.Range(0,4); // 4 base colours
                requestedObjectConfig[1] = Random.Range(0,2); // 2 base shapes
                requestedObjectConfig[2] = Random.Range(0,2); // 2 cable colours
                requestedObjectConfig[3] = Random.Range(0,2); // 2 cable shapes
                bubbleRandomizers[8].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[8].gameObject.SetActive(true);
                break;
            case FoundObject.Key: // keyring
                requestedObjectConfig = new int[3]; // 3 variables
                requestedObjectConfig[0] = Random.Range(0,2); // 2 chain colours
                requestedObjectConfig[1] = Random.Range(0,3); // 3 key colours
                requestedObjectConfig[2] = Random.Range(0,3); // 3 key shapes
                bubbleRandomizers[9].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[9].gameObject.SetActive(true);
                break;
            case FoundObject.Laptop: // laptop
                requestedObjectConfig = new int[5]; // 5 variables
                requestedObjectConfig[0] = Random.Range(0,3); // 3 base colours
                requestedObjectConfig[1] = Random.Range(0,4); // 4 logo colours
                requestedObjectConfig[2] = Random.Range(0,2); // 2 logo shapes
                requestedObjectConfig[3] = 0;                 // 1 sticker colour
                requestedObjectConfig[4] = Random.Range(0,4); // 3 sticker shapes
                bubbleRandomizers[10].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[10].gameObject.SetActive(true);
                break;
            case FoundObject.Luggage: // luggage
                requestedObjectConfig = new int[2]; // 2 variables
                requestedObjectConfig[0] = Random.Range(0,4); // 4 base colours
                requestedObjectConfig[1] = Random.Range(0,3); // 3 base shapes
                bubbleRandomizers[11].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[11].gameObject.SetActive(true);
                break;
            case FoundObject.Passport: // passport
                requestedObjectConfig = new int[2]; // 2 variables
                requestedObjectConfig[0] = Random.Range(0,4); // 4 base colours
                requestedObjectConfig[1] = Random.Range(0,4); // 4 base shapes
                bubbleRandomizers[12].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[12].gameObject.SetActive(true);
                break;
            case FoundObject.PowerBank: // power bank
                requestedObjectConfig = new int[3]; // 3 variables
                requestedObjectConfig[0] = Random.Range(0,2); // 2 cable colours
                requestedObjectConfig[1] = Random.Range(0,4); // 4 cable types
                requestedObjectConfig[2] = Random.Range(0,4); // 4 base colours
                bubbleRandomizers[13].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[13].gameObject.SetActive(true);
                break;
            case FoundObject.Ring: // ring
                requestedObjectConfig = new int[3]; // 3 variables
                requestedObjectConfig[0] = Random.Range(0,2); // 2 band colours
                requestedObjectConfig[1] = Random.Range(0,4); // 4 gem colours
                requestedObjectConfig[2] = Random.Range(0,2); // 2 gem types
                bubbleRandomizers[15].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[15].gameObject.SetActive(true);
                break;
            case FoundObject.Umbrella: // umbrella
                requestedObjectConfig = new int[2]; // 2 variables
                requestedObjectConfig[0] = Random.Range(0,4); // 4 base colours
                requestedObjectConfig[1] = Random.Range(0,2); // 2 base shapes
                bubbleRandomizers[18].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[18].gameObject.SetActive(true);
                break;
            case FoundObject.Wallet: // wallet
                requestedObjectConfig = new int[2]; // 2 variables
                requestedObjectConfig[0] = Random.Range(0,3); // 3 base colours
                requestedObjectConfig[1] = Random.Range(0,2); // 2 base shapes
                bubbleRandomizers[19].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[19].gameObject.SetActive(true);
                break;
            default:
                Debug.Log("requested special item from \"MakeNormalRequest\" method");
                break;
        }   
        requestCount++;
    }

    void EnableCorrectSpecialSprite(FoundObject requestedObject) {
        switch (requestedObject)
        {
            case FoundObject.CardboardBox: // cardboard box = Snake
               specialClientNumber = 5;
                break;
            case FoundObject.Head: // head = dullahan
                specialClientNumber = 2;
                break;
            case FoundObject.Grandmother: // grandmother = child
                specialClientNumber = 7;
                break;
            case FoundObject.Passport: // passport = Jorji
                specialClientNumber = 0;
                break;
            case FoundObject.Revolver: // revolver = Ocelot
                specialClientNumber = 3;
                break;
            case FoundObject.Sword: // sword = Arthur
                specialClientNumber = 1;
                break;
            case FoundObject.VideoTape: // tape = ring girl
                specialClientNumber = 4;
                break;
            case FoundObject.Familiar: // familiar = witch
                specialClientNumber = 6;
                break;
            default:
                Debug.Log("requested normal item from \"MakeSpecialRequest\" method");
                break;
        }
        specialClientSprites[specialClientNumber].SetActive(true);
    }

    public void MakeSpecialRequest() {
        Debug.Log("Making special request");
        
        speechBubbleSR.enabled=true;
        switch (requestedObjectType)
        {
            case FoundObject.CardboardBox: // cardboard box = Snake
                requestedObjectConfig = new int[2]; // 2 variables
                requestedObjectConfig[0] = 0; // 1 box colour
                requestedObjectConfig[1] = Random.Range(0,5); // 5 box brands
                bubbleRandomizers[1].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[1].gameObject.SetActive(true);
                specialClientNumber = 5;
                break;
            case FoundObject.Head: // head = dullahan
                requestedObjectConfig = new int[4]; // 4 variables
                requestedObjectConfig[0] = Random.Range(0,3); // 3 base colours
                requestedObjectConfig[1] = Random.Range(0,4); // 4 base shapes
                requestedObjectConfig[2] = Random.Range(0,3); // 3 cable colours
                requestedObjectConfig[3] = Random.Range(0,3); // 3 cable shapes
                bubbleRandomizers[4].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[4].gameObject.SetActive(true);
                specialClientNumber = 2;
                break;
            case FoundObject.Grandmother: // grandmother = child
                requestedObjectConfig = new int[5]; // 5 variables
                requestedObjectConfig[0] = Random.Range(0,2); // 2 base colours
                requestedObjectConfig[1] = Random.Range(0,3); // 3 clothes colours
                requestedObjectConfig[2] = Random.Range(0,3); // 3 hair colours
                requestedObjectConfig[3] = 0;                 // 1 base pie colour
                requestedObjectConfig[4] = Random.Range(0,3); // 3 pie colours
                bubbleRandomizers[7].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[7].gameObject.SetActive(true);
                specialClientNumber = 7;
                break;
            case FoundObject.Passport: // passport = Jorji
                requestedObjectConfig = new int[2]; // 2 variables
                requestedObjectConfig[0] = Random.Range(0,4); // 4 base colours
                requestedObjectConfig[1] = Random.Range(0,4); // 4 base shapes
                bubbleRandomizers[12].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[12].gameObject.SetActive(true);
                specialClientNumber = 0;
                break;
            case FoundObject.Revolver: // revolver = Ocelot
                requestedObjectConfig = new int[2]; // 2 variables
                requestedObjectConfig[0] = Random.Range(0,2); // 2 gun colours
                requestedObjectConfig[1] = Random.Range(0,4); // 4 handle shapes
                bubbleRandomizers[14].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[14].gameObject.SetActive(true);
                specialClientNumber = 3;
                break;
            case FoundObject.Sword: // sword = Arthur
                requestedObjectConfig = new int[3]; // 3 variables
                requestedObjectConfig[0] = Random.Range(0,4); // 4 hilt colours
                requestedObjectConfig[1] = Random.Range(0,3); // 3 blade colours
                requestedObjectConfig[2] = Random.Range(0,2); // 2 blade types
                bubbleRandomizers[16].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[16].gameObject.SetActive(true);
                specialClientNumber = 1;
                break;
            case FoundObject.VideoTape: // tape = ring girl
                requestedObjectConfig = new int[2]; // 2 variables
                requestedObjectConfig[0] = 0;                 // 1 tape colour
                requestedObjectConfig[1] = Random.Range(0,6); // 6 tape types
                bubbleRandomizers[17].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[17].gameObject.SetActive(true);
                specialClientNumber = 4;
                break;
            case FoundObject.Familiar: // familiar = witch
                requestedObjectConfig = new int[2]; // 2 variables
                requestedObjectConfig[0] = 0;                 // 1 pet colour
                requestedObjectConfig[1] = Random.Range(0,6); // 6 pet types
                bubbleRandomizers[20].ApplyItemConfig(requestedObjectConfig);
                bubbleRandomizers[20].gameObject.SetActive(true);
                specialClientNumber = 6;
                break;
            default:
                Debug.Log("requested normal item from \"MakeSpecialRequest\" method");
                break;
        }
        requestCount++;
    }

    public void RecordPlayerSelectedObjectType(FoundObject foundObject) {
        playerSelectedObjectType = foundObject;
    }

    public FoundObject GetRequestedObjectType() {
        return requestedObjectType;
    }

    public int[] GetRequestedObjectConfig() {
        return requestedObjectConfig;
    }

    public void GetClientToLeave() {
        switch(currentActiveClient) {
            case 0:
                clientAAnimator.SetTrigger("LeaveWindow");
                break;
            case 1:
                clientBAnimator.SetTrigger("LeaveWindow");
                break;
            case 2:
                specialClientAnimator.SetTrigger("LeaveWindow");
                break;
            default:
                Debug.Log("current active client is not 0,1 or 2. HOW?!");
                break;
        }
    }

    public void GiveActiveClientAnItem(int[] itemConfig, int arrayEntry) {
        int[] copiedConfig = new int[itemConfig.Length];
        System.Array.Copy(itemConfig,copiedConfig,itemConfig.Length);
        StartCoroutine(GiveClientItemButDelayedProperly(copiedConfig,arrayEntry));
    }

    IEnumerator GiveClientItemButDelayedProperly(int[] itemConfig, int arrayEntry) {
        Debug.Log("Going to give client an item");
        int savedClient =  currentActiveClient;
        yield return new WaitForSeconds(3);
        Debug.Log("Giving client item now!");
        switch(savedClient) {
            case 0:
            Debug.Log("Giving it to client A(?)");
                regularClientRandomizableObjects[0].transform.GetChild(arrayEntry).gameObject.SetActive(true);
                regularClientRandomizableObjects[0].transform.GetChild(arrayEntry).GetComponent<RandomizeColours>().ApplyItemConfig(itemConfig);
                break;
            case 1:
            Debug.Log("Giving it to client B(?)");
                regularClientRandomizableObjects[1].transform.GetChild(arrayEntry).gameObject.SetActive(true);
                regularClientRandomizableObjects[1].transform.GetChild(arrayEntry).GetComponent<RandomizeColours>().ApplyItemConfig(itemConfig);
                break;
            case 2:
            Debug.Log("Giving it to special client");
                specialClientRandomizableObjects[specialClientNumber].transform.GetChild(arrayEntry).gameObject.SetActive(true);
                specialClientRandomizableObjects[specialClientNumber].transform.GetChild(arrayEntry).GetComponent<RandomizeColours>().ApplyItemConfig(itemConfig);
                break;
            default:
                Debug.Log("current active client is not 0,1 or 2. HOW?!");
                break;
        }
        yield return new WaitForSeconds(3);
        switch(savedClient) {
            case 0:
                regularClientRandomizableObjects[0].transform.GetChild(arrayEntry).gameObject.SetActive(false);
                break;
            case 1:
                regularClientRandomizableObjects[1].transform.GetChild(arrayEntry).gameObject.SetActive(false);
                break;
            case 2:
                specialClientRandomizableObjects[specialClientNumber].transform.GetChild(arrayEntry).gameObject.SetActive(false);
                break;
            default:
                Debug.Log("current active client is not 0,1 or 2. HOW?!");
                break;
        }
    }
    
}
