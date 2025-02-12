using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class S0Mgr : MonoBehaviour 
{
    public Button btnStart;         //開始遊戲按鈕
    public Button btnLoad;          //載入遊戲按鈕
    public Button btnSave;          //存檔按鈕
    public Button btnSetting;       //設定按鈕
    public Button btnExit;          //離開遊戲按鈕
    public Button btnMember;        //工作人員按紐
    public Button btnCloseLoad;     //關閉載入視窗按鈕
    public Button btnCloseSetting;  //關閉設定視窗按鈕
    public Button btnCloseMember;  //關閉工作人員視窗按鈕
    public Image imgScreen;         //截圖圖片

    public Slider slidMusic;        //音樂大小滑桿
    public Slider slidSound;        //音效大小滑桿
    public GameObject memberView;   //工作人員視窗
    public GameObject laodView;     //載入視窗
    public GameObject settingView;  //設定視窗
    public AudioSource music;       //音樂大小
    public AudioSource sound;       //音效大小
    public AudioSource btnClickVoice;
    
    void Start()
    {
        GameDb.level = 0;
        laodView.SetActive(false);
        settingView.SetActive(false);
        memberView.SetActive(false);
        slidMusic.value = music.volume = GameDb.musicVolum;
        slidSound.value = GameDb.soundVolum *10;
        btnStart.onClick.AddListener(OnBtnStartClick);
        btnLoad.onClick.AddListener(OnBtnLoadClick);
        btnSave.onClick.AddListener(OnBtnSaveClick);
        btnSetting.onClick.AddListener(OnBtnSettingClick);
        btnExit.onClick.AddListener(OnBtnExitClick);
        btnCloseLoad.onClick.AddListener(OnBtnCloseClick);
        btnCloseSetting.onClick.AddListener(OnBtnCloseClick);
        btnCloseMember.onClick.AddListener(OnBtnCloseClick);
        btnMember.onClick.AddListener(OnBtnMemberClick);
        slidSound.onValueChanged.AddListener(OnslidSoundValueChange);
        EventSystem.current.SetSelectedGameObject(btnStart.gameObject);
    }
    void Update()
    {
        Time.timeScale = 1;
        GameDb.musicVolum = music.volume = slidMusic.value;
        GameDb.soundVolum = sound.volume = btnClickVoice.volume = slidSound.value / 10f;
        imgScreen.sprite = Resources.Load<Sprite>("ScreenShots/Screenshot");
        if(GameDb.isSave)
        {
            imgScreen.gameObject.SetActive(true);
        }
        else
        {
            imgScreen.gameObject.SetActive(false);
        }
    }
    void OnslidSoundValueChange(float value)
    {
        if (Input.GetMouseButton(0))
        {
            sound.Play();
        }
    }

    void OnBtnStartClick()      //點擊開始按鈕
    {
        btnClickVoice.Play();
        if(GameDb.isSave)
        {
            GameDb.isSave = false;
        }
        SceneManager.LoadScene("Video");
    }
    void OnBtnLoadClick()       //點擊載入按鈕
    {
        btnClickVoice.Play();
        laodView.SetActive(true);
        EventSystem.current.SetSelectedGameObject(btnCloseLoad.gameObject);
    }
    void OnBtnSaveClick()     //點擊存檔按鈕
    {
        if(GameDb.isSave)
        {
            GameDb.level =  PlayerPrefs.GetInt("Level");
        }
        else
        {
            return;
        }
        SceneManager.LoadScene("Loading");
    }
    void OnBtnSettingClick()    //點擊設定按鈕
    {
        btnClickVoice.Play();
        settingView.SetActive(true);
        EventSystem.current.SetSelectedGameObject(btnCloseSetting.gameObject);
    }
    void OnBtnExitClick()       //點擊離開按鈕
    {
        btnClickVoice.Play();
        Application.Quit();
    }
    void OnBtnMemberClick()     //點擊工作人員按鈕
    {
        btnClickVoice.Play();
        memberView.SetActive(true);
        EventSystem.current.SetSelectedGameObject(btnCloseMember.gameObject);
    }
    void OnBtnCloseClick()      //點擊關閉按鈕
    {
        btnClickVoice.Play();
        laodView.SetActive(false);
        settingView.SetActive(false);
        memberView.SetActive(false);
        EventSystem.current.SetSelectedGameObject(btnStart.gameObject);
    }
}
