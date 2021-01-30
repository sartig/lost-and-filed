using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CodeTypingScript : MonoBehaviour
{

    ClientManager clientManager;
    LargeFolderScript largeFolderScript;
    public TextMeshPro lcdText;
    Animator m_anim;
    public BoxCollider2D keypadActivationCollider;

    void Awake() {
        ClearText();
        m_anim = GetComponent<Animator>();
        clientManager = GameObject.FindObjectOfType<ClientManager>();
        largeFolderScript = GameObject.FindObjectOfType<LargeFolderScript>();
    }

    public void MakeKeypadAppear() {
        m_anim.SetBool("KeypadExtended",true);
        keypadActivationCollider.enabled=false;
    }
    public void ClearText() {
        lcdText.text = "";
    }

    public void AddChar(string s) {
        lcdText.text+=s;
    }

    public void PrintReceipt() {
        //make ticket appear + print
        largeFolderScript.CheckForValidCode(lcdText.text); // pass code to see if it matches anything
        ClearText(); //then clear display
        clientManager.ClearRequestDisplay(); // clear speech bubble and that prefab display
        clientManager.GetClientToLeave(); //trigger customer to move
        clientManager.MakeNextRequest(); //call next customer
        largeFolderScript.LargeFolderDisappear(true); //put away folder if folder is out - and also shut cabinet ;)
        m_anim.SetBool("KeypadExtended",false); //make keypad retract
        keypadActivationCollider.enabled=true;
    }
}
