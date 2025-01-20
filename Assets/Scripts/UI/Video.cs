using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.InputSystem;

public class Video : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.isPaused)
        {
            SceneManager.LoadScene("Loading");
        }
    }

    public void VideoSkip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SceneManager.LoadScene("Loading");
        }
    }
}
