using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    bool isOn;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Move workss");
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
    }
}
