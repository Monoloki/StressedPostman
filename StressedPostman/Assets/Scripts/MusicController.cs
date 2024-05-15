using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicController : Singleton<MusicController>
{

    public AudioClip[] sounds;

    public void SpawnSound(Transform transform, ISound sound) {
        AudioSource.PlayClipAtPoint(sounds[(int)sound], transform.position);
    }

    public void ButtonDown() {
        SpawnSound(transform, ISound.button);
    }
}
