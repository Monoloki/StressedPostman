using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private  GameObject dictionary;

    public void ToggleDictionary() {
        dictionary.SetActive(!dictionary.activeSelf);
    }
}
