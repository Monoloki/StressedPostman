using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject parcel;

    public Transform spawnPoint;

    public void StartBossSpawner() {
        StartCoroutine(SpawnObjectAtRandomIntervals());
    }

    IEnumerator SpawnObjectAtRandomIntervals() {
        while (GameManager.instance.gameActive) {
            float randomInterval = Random.Range(10f, 30f);
            yield return new WaitForSeconds(randomInterval);
            var spawneParcel = Instantiate(parcel, spawnPoint.position, Quaternion.identity);
            spawneParcel.GetComponent<Rigidbody>().AddForce(new Vector3(0, 2f, -2f), ForceMode.Impulse);
            MusicController.instance.SpawnSound(transform, ISound.throwing);
        }
    }
}
