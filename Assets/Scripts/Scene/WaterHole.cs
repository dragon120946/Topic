using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterHole : MonoBehaviour
{
    public List<AudioSource> waterHoleAudio;
    public Slider slidSound;
    public GameObject fxHeal;
    public GameObject save;
    public GameObject rebirthPoint;
    private AudioSource enterWaterVoice;
    private AudioSource swimVoice;

    void Start()
    {
        enterWaterVoice = waterHoleAudio[0];
        swimVoice = waterHoleAudio[1];
        fxHeal.SetActive(false);
        fxHeal.GetComponent<ParticleSystem>().Stop();
        save.SetActive(false);
    }

    void Update()
    {
        enterWaterVoice.volume = swimVoice.volume = slidSound.value / 10f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            save.SetActive(true);
            //截圖
            string path = Application.dataPath;
            ScreenCapture.CaptureScreenshot(path + "/Resources/ScreenShots/Screenshot.png");

            enterWaterVoice.Play();
            GameDb.isGround = true;
            fxHeal.SetActive(true);
            fxHeal.GetComponent<ParticleSystem>().Play();
        } 
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameDb.hp += 1;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("HP", GameDb.hp);
            PlayerPrefs.SetInt("Energy", GameDb.energy);
            PlayerPrefs.SetFloat("rebirthPointX", rebirthPoint.transform.position.x);
            PlayerPrefs.SetFloat("rebirthPointY", rebirthPoint.transform.position.y);

            GameDb.isSave = true;

            save.SetActive(false);
            fxHeal.SetActive(false);
            fxHeal.GetComponent<ParticleSystem>().Stop();
        }
    }
}
