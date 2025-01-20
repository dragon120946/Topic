using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{

    public AudioSource[] SE;

    public AudioSource[] BGM;

    public Slider slidSound;
    public Slider slidMusic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var VARIABLE in SE)
        {
            VARIABLE.volume = slidSound.value / 10f;
        }

        if (GameDb.level == 2)
        {
            return;
        }
        foreach (var VARIABLE in BGM)
        {
            VARIABLE.volume = slidMusic.value;
        }
    }
}
