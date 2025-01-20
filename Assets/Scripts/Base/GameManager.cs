using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public int currentLevel;
    //public Slider slidStamina;      //耐力條
    public Slider slidMusic;
    public Slider slidSound;
    public Slider slidTimer;
    public Button btnReplay;        //重玩按鈕
    public Button btnExit;          //離開按鈕
    public Button btnMenu;          //主畫面按鈕
    public Button btnIce;           //冰型態按鈕
    public Button btnWater;         //水型態按鈕
    public Button btnSteam;         //蒸氣型態按鈕
    public RectTransform timerTrans;//計時器位置
    public Text txtDialogue;        //劇情文字
    public Text txtTutor;           //任務文字
    public GameObject deadView;     //死亡視窗
    public GameObject settingView;  //設定視窗
    public GameObject typeChange;   //型態轉換畫面
    public Transform rebirthPoint;  //初始重生點
    public AudioSource music;       //音樂大小
    public AudioSource sound;       //音效大小
    public PlayerCtrl playerctrl;
    public float nextTime;

    [NonSerialized]
    public float timer;
    [NonSerialized]
    public bool isSettingViewOpen;
    [NonSerialized]
    public bool skip;
    public List<Slider> hpList;     //血量列表
    public List<Slider> energyList; //能量列表

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region 繼承用

    public void Base_Start()
    {
        slidMusic.value = music.volume = GameDb.musicVolum;
        slidSound.value = GameDb.soundVolum * 10;
        slidTimer.value = playerctrl.typeTimer;
        //slidStamina.value = GameDb.stamina = 30f;
        GameDb.index = 0;
        GameDb.isWater = true;
        GameDb.isIce = false;
        GameDb.isSteam = false;
        GameDb.level = currentLevel;
        Time.timeScale = 1;
        deadView.SetActive(false);
        settingView.SetActive(false);
        typeChange.SetActive(false);
        slidTimer.gameObject.SetActive(false);
        btnReplay.onClick.AddListener(OnBtnReplayClick);
        btnExit.onClick.AddListener(OnBtnMenuClick);
        btnMenu.onClick.AddListener(OnBtnMenuClick);
        btnIce.onClick.AddListener(OnBtnIceClick);
        btnWater.onClick.AddListener(OnBtnWaterClick);
        btnSteam.onClick.AddListener(OnBtnSteamClick);
        slidSound.onValueChanged.AddListener(OnslidSoundValueChange);
        

        if (GameDb.isSave)
        {
            GameDb.hp = PlayerPrefs.GetInt("HP");
            GameDb.energy = PlayerPrefs.GetInt("Energy");
            float posX = PlayerPrefs.GetFloat("rebirthPointX");
            float posY = PlayerPrefs.GetFloat("rebirthPointY");
            playerctrl.gameObject.transform.position = new Vector3(posX, posY, 0);
        }
        else
        {
            playerctrl.gameObject.transform.position = new Vector3(rebirthPoint.position.x, rebirthPoint.position.y, 0);
        }
    }
    public void Base_FixedUpdate()
    {
        Vector2 playerPos = Camera.main.WorldToScreenPoint(playerctrl.gameObject.transform.position);
        

        if (GameDb.hp > 80 && GameDb.hp <= 100)
        {
            timerTrans.position = playerPos + new Vector2(0, 160);
            timerTrans.localScale = new Vector3(3.0f, 3.0f, 1.0f);
        }
        if (GameDb.hp > 60 && GameDb.hp <= 80)
        {
            timerTrans.position = playerPos + new Vector2(0, 140);
            timerTrans.localScale = new Vector3(2.5f, 2.5f, 1.0f);
        }
        if (GameDb.hp > 40 && GameDb.hp <= 60)
        {
            timerTrans.position = playerPos + new Vector2(0, 120);
            timerTrans.localScale = new Vector3(2.0f, 2.0f, 1.0f);
        }
        if (GameDb.hp > 20 && GameDb.hp <= 40)
        {
            timerTrans.position = playerPos + new Vector2(0, 100);
            timerTrans.localScale = new Vector3(1.5f, 1.5f, 1.0f);
        }
        if (GameDb.hp <= 20)
        {
            timerTrans.position = playerPos + new Vector2(0, 80);
            timerTrans.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    public void Base_Update()
    {
        PlayerPrefs.SetInt("Level", currentLevel);
        slidTimer.value = playerctrl.typeTimer;
        //GameDb.stamina = slidStamina.value;
        GameDb.musicVolum = music.volume = slidMusic.value;
        GameDb.soundVolum = sound.volume = slidSound.value / 10f; 
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
            //slidStamina.gameObject.SetActive(true);
            slidTimer.gameObject.SetActive(true);
            slidTimer.maxValue = playerctrl.iceTime;
        }
        else
        {
            btnIce.interactable = true;
            //slidStamina.gameObject.SetActive(false);
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
        /*
        //如果耐力不是全滿，則每秒恢復5耐力
        if (slidStamina.value < 30)
        {
            slidStamina.value += 5 * Time.deltaTime;
        }
        */
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
        SceneManager.LoadScene(GameDb.level + 3);
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
        playerctrl.typeTimer = playerctrl.iceTime;
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
        playerctrl.typeTimer = playerctrl.steamTime;
        playerctrl.audiosource.clip = playerctrl.steamAudio[0];
        playerctrl.audiosource.Play();
    }

    #endregion

    public void Dead()
    {
        deadView.SetActive(true); 
        EventSystem.current.SetSelectedGameObject(btnReplay.gameObject);
        Time.timeScale = 0;
    }

    public void OnslidSoundValueChange(float value)
    {
        if (Input.GetMouseButton(0))
        {
            sound.Play();
        }
    }

    public void DialogeSkip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            skip = true;
        }
        else
        {
            skip = false;
        }
    }
    public void Setting(InputAction.CallbackContext context)
    {
        isSettingViewOpen = !isSettingViewOpen;
        if (isSettingViewOpen)
        {
            Time.timeScale = 0;
            settingView.SetActive(true);
            EventSystem.current.SetSelectedGameObject(slidMusic.gameObject);
        }
        else
        {
            Time.timeScale = 1;
            settingView.SetActive(false);
        }
    }

    public void TypeChange(InputAction.CallbackContext context)
    {
        if (GameDb.energy >= 10 && context.performed)
        {
            Time.timeScale = 0;
            typeChange.SetActive(true);
            EventSystem.current.SetSelectedGameObject(btnWater.gameObject);
        }
        else
        {
            Time.timeScale = 1;
            typeChange.SetActive(false);
        }

        //如果對應型態按鈕能互動，且被選中的情況下放手，則被視為點擊按鈕
        if (context.canceled && btnIce.interactable && 
            EventSystem.current.currentSelectedGameObject.name == "IceType")
        {
            OnBtnIceClick();
        }
        else if (context.canceled && btnWater.interactable && 
            EventSystem.current.currentSelectedGameObject.name == "WaterType")
        {
            OnBtnWaterClick();
        }
        else if (context.canceled && btnSteam.interactable && 
            EventSystem.current.currentSelectedGameObject.name == "SteamType")
        {
            OnBtnSteamClick();
        }
    }
}
