using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    Vector3 StartPosition, PositionDifferent;

    [SerializeField] List<GameObject> AllRings;
 
    public int RingCount;
    bool isRing;
    public static GameManager Instance;
    private void Start()
    {
        Instance = this;
        GameObject g;
       
        //PlayerPrefs.SetInt("RingCount",RingCount);
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
