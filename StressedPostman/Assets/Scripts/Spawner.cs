using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject parcel;

    public Transform spawnPoint;

    public void StartRegularSpawner() {
        StartCoroutine(SpawnObjectAtRandomIntervals());
    }

    IEnumerator SpawnObjectAtRandomIntervals() {
        while (GameManager.instance.gameActive) {
            float randomInterval = Random.Range(1f, 4f);
            yield return new WaitForSeconds(randomInterval);
            var spawneParcel = Instantiate(parcel, spawnPoint.position, Quaternion.identity);
            MusicController.instance.SpawnSound(transform, ISound.throwing);
            float randomY = Random.Range(1f, 7f);
            float randomX = Random.Range(-1f, -10f);
            spawneParcel.GetComponent<Rigidbody>().AddForce(new Vector3(randomX, randomY, 0),ForceMode.Impulse);
        }
    }

}
