using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] GameObject Target;
    Vector3 offset;
    [SerializeField] float smoothSpeed;
    void Start()
    {
        offset = transform.position - Target.transform.position;
    }

    void Update()
    {
        Vector3 desirePos = Target.transform.position + offset;
        Vector3 currentPos = Vector3.Lerp(transform.position, desirePos, smoothSpeed * Time.deltaTime);
        transform.position = currentPos;
    }
}
