using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject CompletePanel;
    [SerializeField] Ease easeType;

    float YDifference;
    [SerializeField] List<GameObject> AllSplash;
    [SerializeField] ParticleSystem particle;
    [SerializeField] TextMeshProUGUI levelText, scoreGame, scoreGameOver;
    [SerializeField] int LevelValue, scoreValue;
    private void Start()
    {
        LevelValue = PlayerPrefs.GetInt("LevelPref", 1);
        Debug.Log("Current val = "+LevelValue);
    }
    void Move()
    {
        transform.DOScaleY(0.8f, 0.1f);
        transform.DOScaleX(0.8f, 0.1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        particle.Play();
        int RandomSplash = Random.Range(0, AllSplash.Count);
        GameObject splashObj = Instantiate(AllSplash[RandomSplash], new Vector3(0.3f, transform.position.y-0.2f, -2), Quaternion.Euler(90, 0, 0), collision.transform);
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
            SceneManager.LoadScene(1);
        }
        if (collision.gameObject.CompareTag("Complete"))
        {
            if(!isScore)
            {
                levelText.text = LevelValue.ToString();
                LevelValue++;
                PlayerPrefs.SetInt("LevelPref", LevelValue);
                Debug.Log("After val = " + LevelValue);
                isScore = true;
                Invoke("ScoreBool",1);
            }
            int value = PlayerPrefs.GetInt("ScorePref", scoreValue);
            scoreGameOver.text = value.ToString();
            Debug.Log("Get val = "+value);
            CompletePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    bool isScore;
    private void ScoreBool()
    {
        isScore = false;
    }
    public void OnClickNextLevelUnlock()
    {
        Time.timeScale = 1;
        int RingLength = PlayerPrefs.GetInt("RingCount", 10);
        
        PlayerPrefs.SetInt("RingCount", RingLength + 2);
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
        scoreValue += 25;
        PlayerPrefs.SetInt("ScorePref",scoreValue);
        scoreGame.text = scoreValue.ToString();
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
