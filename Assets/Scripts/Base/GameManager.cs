using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int currentLevel;
    public List<Slider> hpList;     //血量列表
    public List<Slider> energyList; //能量列表
    public Slider slidStamina;      //耐力條
    public Slider slidMusic;
    public Slider slidSound;
    public Slider slidTimer;
    public Button btnReplay;        //重玩按鈕
    public Button btnExit;          //離開按鈕
    public Button btnMenu;          //主畫面按鈕
    public Button btnIce;           //冰型態按鈕
    public Button btnWater;         //水型態按鈕
    public Button btnSteam;         //蒸氣型態按鈕
    public GameObject autoSave;
    public GameObject deadView;     //死亡視窗
    public GameObject settingView;  //設定視窗
    public GameObject typeChange;   //型態轉換畫面
    public AudioSource music;       //音樂大小
    public AudioSource sound;       //音效大小
    public PlayerCtrl playerctrl;
    public float autoSaveMaxTimer;
    [NonSerialized]
    public bool isSettingViewOpen;
    public float autoSaveTimer;             

    void Start()
    {
        Base_Start();
    }

    void Update()
    {
        Base_Update();
    }

    #region 繼承用

    public void Base_Start()
    {
        slidMusic.value = music.volume = GameDb.musicVolum;
        slidSound.value = GameDb.soundVolum * 10;
        slidTimer.value = playerctrl.timer;
        slidStamina.value = GameDb.stamina = 30f;
        GameDb.isWater = true;
        GameDb.isIce = false;
        GameDb.isSteam = false;
        GameDb.level = currentLevel;
        Time.timeScale = 1;
        autoSave.SetActive(false);
        deadView.SetActive(false);
        settingView.SetActive(false);
        typeChange.SetActive(false);
        slidTimer.gameObject.SetActive(false);
        btnReplay.onClick.AddListener(OnBtnReplayClick);
        btnExit.onClick.AddListener(OnBtnExitClick);
        btnMenu.onClick.AddListener(OnBtnMenuClick);
        btnIce.onClick.AddListener(OnBtnIceClick);
        btnWater.onClick.AddListener(OnBtnWaterClick);
        btnSteam.onClick.AddListener(OnBtnSteamClick);
        slidSound.onValueChanged.AddListener(OnslidSoundValueChange);
    }

    public void Base_Update()
    {
        slidTimer.value = playerctrl.timer;
        GameDb.stamina = slidStamina.value;
        GameDb.musicVolum = music.volume = slidMusic.value;
        GameDb.soundVolum = sound.volume = slidSound.value / 10f;
        autoSaveTimer += Time.deltaTime;
        if (autoSaveTimer >= autoSaveMaxTimer)
        {
            autoSaveTimer = 0;
            autoSave.SetActive(true);
            PlayerPrefs.SetInt("HP", GameDb.hp);
            PlayerPrefs.SetInt("Energy", GameDb.energy);
            PlayerPrefs.SetFloat("PlayerPosX", playerctrl.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosY", playerctrl.gameObject.transform.position.y);
            PlayerPrefs.SetInt("Level", currentLevel);
        }
        else
        {
            autoSave.SetActive(false);
        }
        
        //水形態下，水型態按鈕無法按
        if (GameDb.isWater)
        {
            btnWater.interactable = false;
            slidTimer.gameObject.SetActive(false);
        }
        else
        {
            btnWater.interactable = true;
        }
        //冰形態下，冰型態按鈕無法按，出現計時器
        if (GameDb.isIce)
        {
            btnIce.interactable = false;
            slidStamina.gameObject.SetActive(true);
            slidTimer.gameObject.SetActive(true);
            slidTimer.maxValue = playerctrl.iceTime;
        }
        else
        {
            btnIce.interactable = true;
            slidStamina.gameObject.SetActive(false);
        }
        //蒸氣形態下，蒸氣型態按鈕無法按，出現計時器
        if (GameDb.isSteam)
        {
            btnSteam.interactable = false;
            slidTimer.gameObject.SetActive(true);
            slidTimer.maxValue = playerctrl.steamTime;
        }
        else
        {
            btnSteam.interactable = true;
        }

        //血量增減
        for (int i = 0; i < 5; i++)
        {
            hpList[i].value = GameDb.hp;
        }
        //能量增減
        for (int i = 0; i < 10; i++)
        {
            energyList[i].value = GameDb.energy;
        }
        //如果耐力不是全滿，則每秒恢復5耐力
        if (slidStamina.value < 30)
        {
            slidStamina.value += 5 * Time.deltaTime;
        }
        //能量不會超過最大值
        if (GameDb.energy > 10)
        {
            GameDb.energy = 10;
        }
    }

    #endregion

    #region  按鈕事件

    public void OnBtnReplayClick()
    {
        SceneManager.LoadScene(GameDb.level + 2);
        GameDb.hp = 20;
        GameDb.energy = 0;
    }
    public void OnBtnExitClick()
    {
        Application.Quit();
    }
    public void OnBtnMenuClick()
    {
        SceneManager.LoadScene("S0");
    }
    public void OnBtnIceClick()
    {
        GameDb.energy = 0;
        GameDb.isIce = true;
        GameDb.isWater = false;
        GameDb.isSteam = false;
        GameDb.tab = true;
        playerctrl.timer = playerctrl.iceTime;
        playerctrl.audiosource.clip = playerctrl.iceAudio[0];
        playerctrl.audiosource.Play();
    }
    public void OnBtnWaterClick()
    {
        GameDb.energy = 0;
        GameDb.isWater = true;
        GameDb.isIce = false;
        GameDb.isSteam = false;
    }
    public void OnBtnSteamClick()
    {
        GameDb.energy = 0;
        GameDb.isSteam = true;
        GameDb.isIce = false;
        GameDb.isWater = false;
        GameDb.tab = true;
        playerctrl.timer = playerctrl.steamTime;
        playerctrl.audiosource.clip = playerctrl.steamAudio[0];
        playerctrl.audiosource.Play();
    }

    #endregion

    public void Dead()
    {
        deadView.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnslidSoundValueChange(float value)
    {
        if (Input.GetMouseButton(0))
        {
            sound.Play();
        }
    }
    public void Setting(InputAction.CallbackContext context)
    {
        isSettingViewOpen = !isSettingViewOpen;
        if (isSettingViewOpen)
        {
            Time.timeScale = 0;
            settingView.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            settingView.SetActive(false);
        }
    }

    public void TypeChange(InputAction.CallbackContext context)
    {
        if (GameDb.energy == 10 && context.performed)
        {
            Time.timeScale = 0;
            typeChange.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            typeChange.SetActive(false);
        }
    }
}
