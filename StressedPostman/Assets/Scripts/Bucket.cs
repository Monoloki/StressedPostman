using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour {
    public ISymbol symbol;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "parcel") {
            Parcel parcel = other.GetComponent<Parcel>();

            if (symbol == parcel.symbol) {
                GameManager.instance.ParcledPlacedCorrectly();
                Destroy(other.gameObject);
            }
            else {
                GameManager.instance.ParcelPlacedWrongly();
            }

            
        }
        else if (other.tag == "vipparcel") {
            VipParcel parcel = other.GetComponent<VipParcel>();

            if (symbol == parcel.symbol) {
                GameManager.instance.VipParcledPlacedCorrectly();
                Destroy(other.gameObject);
            }
            else {
                GameManager.instance.VipParcledPlacedWrongly();
            }
        } 
    }
}
