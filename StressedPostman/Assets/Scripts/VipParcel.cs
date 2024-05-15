using System.Collections;
using UnityEngine;

public class VipParcel : MonoBehaviour {
    public float delay = 20f;

    public ISymbol symbol;
    [SerializeField] private new Renderer renderer;

    private void Start() {
        symbol = (ISymbol)Random.Range(0, 5);
        renderer.material = GameManager.instance.GetSymbolMaterial(symbol);
        StartCoroutine(DestroyObjectAfterTime());
    }

    IEnumerator DestroyObjectAfterTime() {
        yield return new WaitForSeconds(delay);
        MusicController.instance.SpawnSound(transform, ISound.wrong);
        Destroy(gameObject);
        GameManager.instance.VipParcelExpired();
    }
}
