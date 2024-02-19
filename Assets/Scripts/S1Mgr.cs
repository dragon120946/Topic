using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S1Mgr : MonoBehaviour
{
    public List<Image> hpList;      //血量列表
    public List<Image> energyList;  //能量列表
    public Slider slidStamina;      //耐力條
    public Button btnReplay;        //重玩按鈕
    public Button btnExit;          //離開按鈕
    public Button btnMenu;          //主畫面按鈕
    public GameObject deadView;     //死亡視窗
    public GameObject settingView;  //設定視窗
    public PlayerCtrl playerctrl;

    private bool isSettingViewOpen;

    void Start()
    {
        slidStamina.value = 30;
        Time.timeScale = 1;
        GameDb.energy = 0;
        deadView.SetActive(false);
        settingView.SetActive(false);
        btnReplay.onClick.AddListener(OnBtnReplayClick);
        btnExit.onClick.AddListener(OnBtnExitClick);
        btnMenu.onClick.AddListener(OnBtnMenuClick);
    }
    
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i > GameDb.hp - 1)
            {
                hpList[i].gameObject.SetActive(false);
            }
            else
            {
                hpList[i].gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (i > GameDb.energy - 1)
            {
                energyList[i].gameObject.SetActive(false);
            }
            else
            {
                energyList[i].gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isSettingViewOpen = !isSettingViewOpen;
            if (isSettingViewOpen)
            {
               settingView.SetActive(true);
            }
            else
            {
                settingView.SetActive(false);
            }
        }
        if (slidStamina.value < 30)
        {
            slidStamina.value += 5 * Time.deltaTime;
        }
        if (GameDb.energy > 10)
        {
            GameDb.energy = 10;
        }
        if(GameDb.energy == 10)
        {

        }
        if (GameDb.hp == 0)
        {
            Dead();
        }
    }

    void OnBtnReplayClick()
    {
        SceneManager.LoadScene("S1");
    }
    void OnBtnExitClick()
    {
        Application.Quit();
    }
    void OnBtnMenuClick()
    {
        SceneManager.LoadScene("S0");
    }

    public void Dead()
    {
        Time.timeScale = 0;
        deadView.SetActive(true);
    }
}
