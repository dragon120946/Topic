using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train_EnergyFlower : MonoBehaviour
{
    public TrainMgr train;
    public List<AudioClip> energyFlowerAudio;
    public int randomVoice;
    private AudioSource audioSources;
    void Start()
    {
        audioSources = GetComponent<AudioSource>();
        randomVoice = Random.Range(0, energyFlowerAudio.Count);
        gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //�I���q��N�[��q
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSources.Play();
            train.touchFlower = true;
            GameDb.energy ++;
        }
    }
}
