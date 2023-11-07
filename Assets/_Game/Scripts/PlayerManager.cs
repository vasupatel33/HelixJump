using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject CompletePanel;
    [SerializeField] Ease easeType;

    float YDifference;
    [SerializeField] List<GameObject> AllSplash;

    private void Start()
    {
        YDifference = 0;
    }
    void Move()
    {
        transform.DOScaleY(0.8f, 0.1f);
        transform.DOScaleX(0.8f, 0.1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
            transform.DOMoveY(transform.position.y + 2.5f, 0.35f);
            
            transform.DOScaleY(0.7f, 0.5f).SetEase(easeType).OnComplete(Move);
            transform.DOScaleX(0.7f, 0.2f).SetEase(easeType).OnComplete(Move);

        //Sequence mySequence = DOTween.Sequence();
        //mySequence.Append(transform.DOScaleX(0.6f, 0.1f));
        //mySequence.Append(transform.DOScaleY(0.6f, 0.1f));
        //mySequence.PrependInterval(0.1f);
        //mySequence.Append(transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.1f));
        Debug.Log("collision detect");
        int RandomSplash = Random.Range(0, AllSplash.Count);
        GameObject splashObj = Instantiate(AllSplash[RandomSplash],  new Vector3(0.3f,-0.14f - YDifference, -2), Quaternion.Euler(90,0,0), collision.transform);
        Destroy(splashObj, 1.5f); 

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy detect"); 
        }
        if (collision.gameObject.CompareTag("Complete"))
        {
            CompletePanel.SetActive(true);
        }
    }
    float ExtraDiffCount;
    [SerializeField] float Exploforce, power, radius;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detect");
        YDifference += 4.42f + ExtraDiffCount;
        ExtraDiffCount = 0.1f;
        foreach(int child in other.gameObject.transform)
        {
            other.AddComponent<Rigidbody>();
            other.gameObject.AddComponent<Rigidbody>();
            //other.gameObject.AddComponent<Rigidbody>().AddExplosionForce(Exploforce, other.gameObject.GetComponentsInChildren<Transform>.pos,radius, power);

        }
        other.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    private void Update()
    {
        float cameraOffsetY = 4.0f;

        if (transform.position.y < Camera.main.transform.position.y - cameraOffsetY)
        {
            Vector3 pos = new Vector3(Camera.main.transform.position.x, transform.position.y + cameraOffsetY, Camera.main.transform.position.z);
            Camera.main.transform.position = pos;
        }
    }
}
