using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parcel : MonoBehaviour
{
    public ISymbol symbol;
    [SerializeField] private new Renderer renderer;

    private void Start() {
        symbol = (ISymbol)Random.Range(0, 6);
        renderer.material = GameManager.instance.GetSymbolMaterial(symbol);
    }
}
