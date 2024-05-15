using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
    public static T instance { get; private set; }

    private void Awake() {
        if (instance != null & instance != (T)this) {
            Destroy(this);
        }
        else {
            instance = (T)this;
        }
    }
}