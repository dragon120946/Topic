using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S0Mgr : MonoBehaviour
{
    public Button btnStart;
    public Button btnLoad;
    public Button btnSetting;
    public Button btnExit;
    public Button btnCloseLoad;
    public Button btnCloseSetting;
    public GameObject laodView;
    public GameObject settingView;

    void Start()
    {
        laodView.SetActive(false);
        settingView.SetActive(false);

        btnStart.onClick.AddListener(OnBtnStartClick);
        btnLoad.onClick.AddListener(OnBtnLoadClick);
        btnSetting.onClick.AddListener(OnBtnSettingClick);
        btnExit.onClick.AddListener(OnBtnExitClick);
        btnCloseLoad.onClick.AddListener(OnBtnCloseClick);
        btnCloseSetting.onClick.AddListener(OnBtnCloseClick);
    }
    void Update()
    {
        
    }

    void OnBtnStartClick()
    {
        SceneManager.LoadScene("S1");
    }
    void OnBtnLoadClick()
    {
        laodView.SetActive(true);
    }
    void OnBtnSettingClick()
    {
        settingView.SetActive(true);
    }
    void OnBtnExitClick()
    {
        Application.Quit();
    }
    void OnBtnCloseClick()
    {
        laodView.SetActive(false);
        settingView.SetActive(false);
    }
}
