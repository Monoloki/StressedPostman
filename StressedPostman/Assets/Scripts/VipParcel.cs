using System.Collections;
using UnityEngine;

public class VipParcel : MonoBehaviour {
    public float delay = 20f;

    public ISymbol symbol;
    [SerializeField] private new Renderer renderer;

    private void Start() {
        Debug.Log("kurwa");
        symbol = (ISymbol)Random.Range(0, 5);
        renderer.material = GameManager.instance.GetSymbolMaterial(symbol);
        StartCoroutine(DestroyObjectAfterTime());
    }

    IEnumerator DestroyObjectAfterTime() {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        GameManager.instance.VipParcelExpired();
    }
}
