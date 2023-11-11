using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Common : MonoBehaviour
{
    public static Common Instance;
    public bool isMusicPlaying, isSoundPlaying;

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}