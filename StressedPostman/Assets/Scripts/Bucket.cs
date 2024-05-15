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
                MusicController.instance.SpawnSound(other.transform, ISound.correct);
                Destroy(other.gameObject);         
            }
            else {
                GameManager.instance.ParcelPlacedWrongly();
                MusicController.instance.SpawnSound(other.transform, ISound.wrong);
            }

            
        }
        else if (other.tag == "vipparcel") {
            VipParcel parcel = other.GetComponent<VipParcel>();

            if (symbol == parcel.symbol) {
                GameManager.instance.VipParcledPlacedCorrectly();
                MusicController.instance.SpawnSound(other.transform, ISound.correct);
                Destroy(other.gameObject);
            }
            else {
                GameManager.instance.VipParcledPlacedWrongly();
                MusicController.instance.SpawnSound(other.transform, ISound.wrong);
            }
        } 
    }
}
