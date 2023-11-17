using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player, pausePanel;
    Vector3 StartPosition, PositionDifferent;

    [SerializeField] List<GameObject> AllRings;
    [SerializeField] TextMeshProUGUI LevelTitle;
    
    public int RingCount;
    bool isRing;
    public static GameManager Instance;
    private void Start()
    {
        Instance = this;
        GameObject g;
        
        int RingLength = PlayerPrefs.GetInt("RingCount",10);
        Debug.Log("RingCount =" + RingLength);
        for(int i = 0; i < RingLength; i++)
        {
            if(i==0)
            {
                g = Instantiate(AllRings[0],Player.transform);
            }    
            else if(i == RingLength - 1)
            {
                g = Instantiate(AllRings[AllRings.Count - 1], Player.transform);
            }
            else
            {
                g = Instantiate(AllRings[Random.Range(1, AllRings.Count-1)], Player.transform);
            }
            g.transform.Rotate(0, Random.Range(0, 360),0);
            g.transform.Translate(0, -i * 4.5f,0);
        }
    }
    public void OnPauseButton()
    { 
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnResumeButton()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnRestartButton()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void OnHomeButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartPosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            PositionDifferent = StartPosition - Input.mousePosition;
            Player.transform.Rotate(new Vector3(0,PositionDifferent.x * 0.4f,0));
            StartPosition = Input.mousePosition;
        }
    }
}