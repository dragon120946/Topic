using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Train_WaterHole : MonoBehaviour
{
    public List<AudioSource> waterHoleAudio;
    public Slider slidSound;
    public TrainMgr train;
    public GameObject fxHeal;

    private AudioSource enterWaterVoice;
    private AudioSource swimVoice;
    private bool isTouchwater;

    void Start()
    {
        enterWaterVoice = waterHoleAudio[0];
        swimVoice = waterHoleAudio[1];
        fxHeal.SetActive(false);
        fxHeal.GetComponent<ParticleSystem>().Stop();
    }

    void Update()
    {
        enterWaterVoice.volume = swimVoice.volume = slidSound.value / 10f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            train.touchWaterhole = true;
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
            fxHeal.SetActive(false);
            fxHeal.GetComponent<ParticleSystem>().Stop();
        }
    }
}
