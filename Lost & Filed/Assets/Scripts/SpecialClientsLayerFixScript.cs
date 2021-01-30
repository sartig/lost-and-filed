using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialClientsLayerFixScript : MonoBehaviour
{
    // Start is called before the first frame update
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
