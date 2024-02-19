using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{

    public AudioSource[] 音效;

    public AudioSource 背景音樂;
    [Range(0,1)]
    public float 音效音量;
    public Slider slidSound;
    public Slider slidMusic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var VARIABLE in 音效)
        {
            VARIABLE.volume = slidSound.value / 10f;
        }
        背景音樂.volume = slidMusic.value;
    }
}
