using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject CompletePanel;
    [SerializeField] Ease easeType;

    void Move()
    {
        transform.DOScaleX(0.8f, 0.1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
            transform.DOMoveY(3, 0.4f);
            
            transform.DOScaleX(0.7f, 0.3f).SetEase(easeType).OnComplete(Move);
            
            //Sequence mySequence = DOTween.Sequence();
            //mySequence.Append(transform.DOScaleX(0.6f, 0.1f));
            //mySequence.Append(transform.DOScaleY(0.6f, 0.1f));
            //mySequence.PrependInterval(0.1f);
            //mySequence.Append(transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.1f));

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy detect");
        }
        if (collision.gameObject.CompareTag("Complete"))
        {
            CompletePanel.SetActive(true);
        }
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
