using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject parcel;

    public Transform spawnPoint;

    void Start() {
        StartCoroutine(SpawnObjectAtRandomIntervals());
    }

    IEnumerator SpawnObjectAtRandomIntervals() {
        while (true) {
            float randomInterval = Random.Range(1f, 4f);

            yield return new WaitForSeconds(randomInterval);

            var spawneParcel = Instantiate(parcel, spawnPoint.position, Quaternion.identity);


            float randomY = Random.Range(1f, 7f);
            float randomX = Random.Range(-1f, -10f);

            spawneParcel.GetComponent<Rigidbody>().AddForce(new Vector3(randomX, randomY, 0),ForceMode.Impulse);
        }
    }

}
