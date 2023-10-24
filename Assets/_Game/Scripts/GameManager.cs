using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    Vector3 StartPosition, PositionDifferent;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartPosition = Input.mousePosition;
            Debug.Log("Mouse pos = "+StartPosition);
        }
        if(Input.GetMouseButton(0))
        {
            PositionDifferent = StartPosition - Input.mousePosition;
            Player.transform.Rotate(new Vector3(0,PositionDifferent.x * 0.2f,0));
            StartPosition = Input.mousePosition;
        }
    }
}
