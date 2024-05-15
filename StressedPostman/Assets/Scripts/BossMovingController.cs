using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovingController : MonoBehaviour
{
    public Transform startPoint; 
    public Transform endPoint;   
    public float speed = 2f;     

    private float startTime;     
    private float journeyLength; 

    void Start() {

        journeyLength = Vector3.Distance(startPoint.position, endPoint.position);

        startTime = Time.time;
    }

    void Update() {

        float distCovered = (Time.time - startTime) * speed;

        float fractionOfJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fractionOfJourney);

        if (fractionOfJourney >= 1f) {
            Transform temp = startPoint;
            startPoint = endPoint;
            endPoint = temp;
            startTime = Time.time;
        }
    }
}
