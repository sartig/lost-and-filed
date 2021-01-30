using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColours : MonoBehaviour
{
    public bool randomizeOnSpawn = false;
    public bool ignoreLast = false;
    int arrayCount;
    int[] itemConfiguration;
    ClientManager clientManager;

    void Awake() {
        if(GameObject.FindObjectOfType<ClientManager>()) {
            clientManager = GameObject.FindObjectOfType<ClientManager>();
        }
        for (int i = 0; i < transform.childCount; i++)
        {
           if(!(i==transform.childCount-1 && ignoreLast)) {
                if(transform.GetChild(i).GetComponent<Palette>()) {
                    arrayCount++;
                }
                if(transform.GetChild(i).childCount>0) {
                    arrayCount++;
                }
            }
        }
        //Debug.Log("Object " + transform.name + " has count of " + arrayCount);
        itemConfiguration = new int[arrayCount];
        if(randomizeOnSpawn) {
            Randomize(false);
        }
        else {
            gameObject.SetActive(false);
        }
    }

    public void Randomize(bool checkIfAccidentallyCorrect) {
        itemConfiguration = new int[arrayCount];
        arrayCount=0;
        for(int i = 0; i < transform.childCount; i++) {
            if(!(i==transform.childCount-1 && ignoreLast)) {
                if(transform.GetChild(i).childCount>0) {
                    int enabled = Random.Range(0,transform.GetChild(i).childCount);
                    //Debug.Log("something something indexoutofrange in " + transform.GetChild(i).name + ", child of " + transform.name);
                    itemConfiguration[arrayCount+1] = enabled;
                    foreach(Transform tr in transform.GetChild(i)) {
                        tr.gameObject.SetActive(false);
                    }
                    transform.GetChild(i).GetChild(enabled).gameObject.SetActive(true);
                    int randCol = Random.Range(0,transform.GetChild(i).GetComponent<Palette>().palette.Length);
                    itemConfiguration[arrayCount] = randCol;
                    arrayCount+=2;
                    transform.GetChild(i).GetChild(enabled).GetComponent<SpriteRenderer>().color=transform.GetChild(i).GetComponent<Palette>().palette[randCol];
                }
                else {
                    //Debug.Log("trying to randomize " + transform.GetChild(i).name + ", child of " + transform.name);
                    int randCol = Random.Range(0,transform.GetChild(i).GetComponent<Palette>().palette.Length);
                    itemConfiguration[arrayCount]=randCol;
                    arrayCount++;
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color = transform.GetChild(i).GetComponent<Palette>().palette[randCol];
                }
            }
        }
        if(checkIfAccidentallyCorrect) {
            if(AreArraysEqual(itemConfiguration,clientManager.GetRequestedObjectConfig())) {
                //Debug.Log("accidentally made another correct item, oops, let's try again");
                Randomize(true);
            }
        }
    }

    bool AreArraysEqual(int[] array1, int[] array2) {
        if(array1.Length!=array2.Length) {
            return false;
        }
        else {
            for (int i = 0; i < array1.Length; i++) {
                if(array1[i]!=array2[i]) {
                    return false;
                }
            }
            return true;
        }
    }

    public void ApplyItemConfig(int[] config) {
        //Debug.Log("Forcing child of " + transform.name + " with item config");
        arrayCount=0;
        for(int i = 0; i < transform.childCount; i++) {
            if(transform.GetChild(i).childCount>0) {
                int enabled = config[arrayCount+1];
                foreach(Transform tr in transform.GetChild(i)) {
                    tr.gameObject.SetActive(false);
                }
                transform.GetChild(i).GetChild(enabled).gameObject.SetActive(true);
                int randCol = config[arrayCount];
                arrayCount+=2;
                transform.GetChild(i).GetChild(enabled).GetComponent<SpriteRenderer>().color=transform.GetChild(i).GetComponent<Palette>().palette[randCol];
            }
            else {
                int randCol = config[arrayCount];
                arrayCount++;
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = transform.GetChild(i).GetComponent<Palette>().palette[randCol];
            }
        }
        itemConfiguration=config;
    }
    public int[] GetItemConfiguration() {
        return itemConfiguration;
    }

    void OnEnable() {
        if(randomizeOnSpawn) {
            Randomize(false);
        }
    }

    public void SetToBackCustomerLayer() {
        SpriteRenderer[] spriteRends = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer sr in spriteRends) {
            sr.sortingLayerName = "CustomerBackLayer";
        }
    }

    public void SetToFrontCustomerLayer() {
        SpriteRenderer[] spriteRends = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer sr in spriteRends) {
            sr.sortingLayerName = "CustomerFrontLayer";
        }
    }


}
