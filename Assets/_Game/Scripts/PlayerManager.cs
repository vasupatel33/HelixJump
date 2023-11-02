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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Move on triggerrr");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Move workss");
        if(!flag)
        {
            rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
            flag = true;
            Invoke("MovementWait",0.1f);
        }
    }
    private void MovementWait()
    {
        flag = false;
    }
    private void Update()
    {
        float cameraOffsetY = 4.0f; // Adjust this value to set the desired vertical offset

        if (transform.position.y < Camera.main.transform.position.y - cameraOffsetY)
        {
            Vector3 pos = new Vector3(Camera.main.transform.position.x, transform.position.y + cameraOffsetY, Camera.main.transform.position.z);
            Camera.main.transform.position = pos;
        }
    }
}
