using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    
    public ScreenShot ScrnCamera;
    private void Update()
    {
        //Кнопка для нажатия
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //делаем скриншот
            ScrnCamera.MakeScrn();
        }
    }
}
