using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColours : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.childCount);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Randomize();
        }
    }

    void Randomize() {
        for(int i = 0; i < transform.childCount; i++) {
            if(transform.GetChild(i).childCount>0) {
                int enabled = Random.Range(0,transform.GetChild(i).childCount);
                foreach(Transform tr in transform.GetChild(i)) {
                    tr.gameObject.SetActive(false);
                }
                transform.GetChild(i).GetChild(enabled).gameObject.SetActive(true);
                transform.GetChild(i).GetChild(enabled).GetComponent<SpriteRenderer>().color=transform.GetChild(i).GetComponent<Palette>().palette[Random.Range(0,transform.GetChild(i).GetComponent<Palette>().palette.Length)];
            }
            else {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = transform.GetChild(i).GetComponent<Palette>().palette[Random.Range(0,transform.GetChild(i).GetComponent<Palette>().palette.Length)];
            }
        }
    }
}
