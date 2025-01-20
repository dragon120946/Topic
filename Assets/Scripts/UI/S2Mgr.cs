using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class S2Mgr : GameManager
{
    public AudioSource normalMusic;
    public AudioSource bossMusic;
    public AudioClip bossClip;
    public CinemachineVirtualCamera playerCine;
    public CinemachineVirtualCamera bossCine;
    public CinemachineVirtualCamera multiCine;
    public CinemachineImpulseSource impulseSource;

    private float musicTimer;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameDb.isSave)
        {
            GameDb.isSave = true;
            PlayerPrefs.SetInt("HP", GameDb.hp);
            PlayerPrefs.SetInt("Energy", GameDb.energy);
            PlayerPrefs.SetFloat("rebirthPointX", rebirthPoint.transform.position.x);
            PlayerPrefs.SetFloat("rebirthPointY", rebirthPoint.transform.position.y);
        }
        GameDb.key = 1;
        Base_Start();
        txtDialogue.transform.parent.parent.gameObject.SetActive(true);
        txtTutor.text = "����BOSS�è��o�_�]�H��";
        slidMusic.value = normalMusic.volume = GameDb.musicVolum;
        bossMusic.volume = 0;
        normalMusic.Play();
        bossMusic.enabled = false;
        playerCine.enabled = true;
        bossCine.enabled = false;
        multiCine.enabled = false;
    }

    void FixedUpdate()
    {
        Base_FixedUpdate();

        if (GameDb.isBossWar)
        {
            StartCoroutine(FadeOutMusic());

            musicTimer += Time.deltaTime;
            if (musicTimer < 3)
            {
                playerCine.enabled = false;
                bossCine.enabled = true;
                multiCine.enabled = false;
            }
            else
            {
                playerCine.enabled = false;
                bossCine.enabled = false;
                multiCine.enabled = true;
            }
            impulseSource.GenerateImpulse();
        }
        else
        {
            playerCine.enabled = true;
            bossCine.enabled = false;
            multiCine.enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Base_Update();
        timer += Time.deltaTime;
        if (timer >= nextTime || skip)
        {
            timer = 0;

            if (GameDb.index == 1)
            {
                txtDialogue.text = "���K�}�]���Ѥj���ӴN��b�}�]���`�B�A����B�����ݬݧa�C";
                GameDb.index++;
                return;
            }
            if (GameDb.index == 2)
            {
                txtDialogue.transform.parent.parent.gameObject.SetActive(false);
            }
        }
        if (GameDb.index == 0)
        {
            txtDialogue.text = "�z�㨪�K�}�]�u���n����......�o�̨�B���O���šA�]�\�i�H�����]�𫬺A������ɶ���I";
            GameDb.index++;
        }
        if(GameDb.key == 2)
        {
            txtDialogue.transform.parent.parent.gameObject.SetActive(true);
            txtDialogue.text = "(���C�����ժ��즹�����A�P�±z���C��~)";
        }
        if (GameDb.hp <= 0)
        {
            Dead();
        }
    }

    public IEnumerator FadeOutMusic()
    {
        while(normalMusic.volume > 0)
        {
            normalMusic.volume -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
       
        if(normalMusic.volume == 0)
        {
            normalMusic.enabled = false;
            bossMusic.enabled = true;
            StartCoroutine(FadeInMusic());
        }
        
    }
    public IEnumerator FadeInMusic()
    {
        while (bossMusic.volume < slidMusic.value)
        {
            bossMusic.volume += 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
        bossMusic.PlayOneShot(bossClip, 0.2f);
    }
}
