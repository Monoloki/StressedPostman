using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.1f;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Transform holdingSlot;
    [SerializeField] private Transform closestObject;
    [SerializeField] private Transform holdingParcel;
    [SerializeField] private List<Transform> objectsInRange = new List<Transform>();
    [SerializeField] private Animator animatior;
    
    void Update() {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ);
        MovePlayer(move);

        if (move.magnitude > 0.1) {
            animatior.Play("Run",0);
        }
        else {
            animatior.Play("Idle",0);
        }

        if (moveX > 0) {
            animatior.gameObject.transform.localScale = new Vector3(1.101079f, 1.101079f, 2.202158f);
        }
        else {
            animatior.gameObject.transform.localScale = new Vector3(-1.101079f, 1.101079f, 2.202158f);
        }

        UpdateClosestObject();

        if (Input.GetKeyDown("e")) {
            PickUp();
        }
    }

    private void PickUp() {
        if (holdingParcel != null) {
            Drop();
        }

        if (!closestObject) return;
        holdingParcel = closestObject;
        holdingParcel.transform.parent = transform;
        closestObject.GetComponent<Rigidbody>().isKinematic = true;
        holdingParcel.rotation = Quaternion.identity;
        holdingParcel.position = holdingSlot.position;
        MusicController.instance.SpawnSound(transform, ISound.throwing);
    }

    private void Drop() {
        Rigidbody rb = holdingParcel.GetComponent<Rigidbody>();
        holdingParcel.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(new Vector3(0, 3, 2), ForceMode.Impulse);
        if (holdingParcel == closestObject) {
            closestObject = null;
        }
        holdingParcel = null;
    }

    private void MovePlayer(Vector3 direction) {
        Vector3 velocity = direction * moveSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other) {
        if ((other.tag == "parcel" || other.tag == "vipparcel") && !objectsInRange.Contains(other.transform)) {
            objectsInRange.Add(other.transform);
        }
    }

    void OnTriggerExit(Collider other) {
        if (objectsInRange.Contains(other.transform)) {
            objectsInRange.Remove(other.transform);
        }
    }

    void UpdateClosestObject() {
        float closestDistance = Mathf.Infinity;
        closestObject = null;

        objectsInRange.RemoveAll(item => item == null);

        foreach (Transform obj in objectsInRange) {
            float distance = Vector3.Distance(transform.position, obj.position);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestObject = obj;
            }
        }
    }
}
