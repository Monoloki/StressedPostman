using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private  GameObject dictionary;
    [SerializeField] private GameObject howToPlay;

    public void ToggleDictionary() {
        dictionary.SetActive(!dictionary.activeSelf);
    }

    public void ToggleHowToPlay() {
        howToPlay.SetActive(!howToPlay.activeSelf);
    }
}
