using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    bool flag;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Move workss");
        if(!flag)
        {
            rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
            flag = true;
            Invoke("MovementWait",0.2f);
        }
    }
    private void MovementWait()
    {
        flag = false;
    }
}
