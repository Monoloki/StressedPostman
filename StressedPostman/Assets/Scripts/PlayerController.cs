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
    [SerializeField]  private List<Transform> objectsInRange = new List<Transform>();
    
    void Update() {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ);
        MovePlayer(move);
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
        holdingParcel.position = holdingSlot.position;
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
