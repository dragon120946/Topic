using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Slider slidLoading;
    public Text txtProcess;
    public Text txtTips;
    public TextAsset textFile;
    public float maxTime;


    private float timer;
    private List<string> tipsList = new List<string>();
    private int loadProgress;
    
    void Start()
    {
        StartCoroutine("LoadScene");
        txtTips.text = "收集滿10個能量花，按Tab可以變身喔！";
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > maxTime)
        {
            timer = 0;
        }
    }
    
    public IEnumerator LoadScene()
    {
        AsyncOperation async;
        if (GameDb.level == 0)
        {
            async = SceneManager.LoadSceneAsync("Train");
        }
        else if (GameDb.level == 1)
        {
            async = SceneManager.LoadSceneAsync("S1");
        }
        else
        {
            async = SceneManager.LoadSceneAsync("S2");
        }
        loadProgress = (int)async.progress * 100;
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            slidLoading.value = async.progress;
            txtProcess.text = Mathf.Floor(slidLoading.value) * 100 + " %";
            //Debug.Log("000");
            if (async.progress >=0.88f)
            {
                break;
            }
            yield return new WaitForEndOfFrame(); 
        }
        //Debug.Log("強制100");
        loadProgress = 100;
        slidLoading.value = 100;
        txtProcess.text = Mathf.Floor(slidLoading.value) * 100 + " %";
        async.allowSceneActivation = true;  
    }
}
