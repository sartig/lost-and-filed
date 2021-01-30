using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LargeFolderScript : MonoBehaviour
{
    // Start is called before the first frame update
    RaiseFolderUp triggeringScript;
    Animator m_animator;
    public TextMeshPro folderName;

    void Awake() {
        m_animator = GetComponent<Animator>();
    }
    public void LargeFolderAppear(RaiseFolderUp triggerScript, string categoryName) {
        triggeringScript = triggerScript;
        m_animator.SetBool("largeFolderActive",true);
        folderName.text = categoryName;
        //randomize polaroid frames
    }

    void GeneratePolaroidImages() {
        
    }

    public void LargeFolderDisappear() {
        Debug.Log("Tried to put away large folder");
        m_animator.SetBool("largeFolderActive",false);
        Invoke("ReturnSmallFolder",0.5f);
    }

    void ReturnSmallFolder() {
        triggeringScript.PutAway();
    }

    public void WriteInDebug(string text) {
        Debug.Log(text);
    }
    
}
