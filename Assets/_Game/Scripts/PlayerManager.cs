using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject CompletePanel;
    [SerializeField] Ease easeType;

    float YDifference;
    [SerializeField] List<GameObject> AllSplash;

    private void Start()
    {
    }
    void Move()
    {
        transform.DOScaleY(0.8f, 0.1f);
        transform.DOScaleX(0.8f, 0.1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        int RandomSplash = Random.Range(0, AllSplash.Count);
        GameObject splashObj = Instantiate(AllSplash[RandomSplash], new Vector3(0.3f, transform.position.y-0.2f/*-0.14f - YDifference*/, -2), Quaternion.Euler(90, 0, 0), collision.transform);
        transform.DOMoveY(transform.position.y + 2.5f, 0.35f);
            
            transform.DOScaleY(0.7f, 0.5f).SetEase(easeType).OnComplete(Move);
            transform.DOScaleX(0.7f, 0.2f).SetEase(easeType).OnComplete(Move);

        //Sequence mySequence = DOTween.Sequence();
        //mySequence.Append(transform.DOScaleX(0.6f, 0.1f));
        //mySequence.Append(transform.DOScaleY(0.6f, 0.1f));
        //mySequence.PrependInterval(0.1f);
        //mySequence.Append(transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.1f));
        //Debug.Log("collision detect");
        
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
    public void OnClickNextLevelUnlock()
    {
        int RingLength = PlayerPrefs.GetInt("RingCount", 10);
        
        PlayerPrefs.SetInt("RingCount", RingLength + 1);
        Debug.Log("After set Ring Count = " + RingLength);

        SceneManager.LoadScene(1);
    }
    public void OnClickHomeBtn()
    {
        SceneManager.LoadScene(0);
    }
    [SerializeField] float Exploforce, radius;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger detect");
        foreach(Transform child in other.transform)
        {
            child.transform.gameObject.GetComponent<MeshCollider>().convex = true;
            child.transform.gameObject.AddComponent<Rigidbody>();
            child.transform.gameObject.GetComponent<Rigidbody>().AddExplosionForce(Exploforce, child.transform.position, radius,200);
            child.GetComponent<MeshCollider>().enabled = false;
            Destroy(child.transform.gameObject, 1.5f);
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
