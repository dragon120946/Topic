using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Train_WaterHole : MonoBehaviour
{
    public List<AudioSource> waterHoleAudio;
    public Slider slidSound;
    public TrainMgr train;
    private AudioSource enterWaterVoice;
    private AudioSource swimVoice;
    private bool isTouchwater;

    void Start()
    {
        enterWaterVoice = waterHoleAudio[0];
        swimVoice = waterHoleAudio[1];
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

            GameDb.hp += 20;
            collision.gameObject.transform.localScale += new Vector3(0.5f, 0.5f, 0);
            if (GameDb.hp >= 100)
            {
                GameDb.hp = 100;
            }
            if (collision.gameObject.transform.localScale.x >= 3f && collision.gameObject.transform.localScale.y >= 3f)
            {
                collision.gameObject.transform.localScale = new Vector3(3f, 3f, 0f);
            }
        }
    }
}
