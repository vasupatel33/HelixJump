using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject SettingPanel;
    public void OnPlayBtnClicked()
    {
        SceneManager.LoadScene(1);
    }
    public void OnSettingPanelOpen()
    {
        SettingPanel.SetActive(true);
    }
    public void OnSettingPanelClose()
    {
        SettingPanel.SetActive(false);
    }

}
