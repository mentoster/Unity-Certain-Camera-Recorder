using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoControl : MonoBehaviour
{
    public KeyCode MakeVideoButton = KeyCode.V;
    public VideoRecorder _videoRecorder;
    private void Update()
    {
        if (Input.GetKeyDown(MakeVideoButton))
        {
            //начинаем запись видео
            _videoRecorder.MakeVideo();
        }
    }
}
