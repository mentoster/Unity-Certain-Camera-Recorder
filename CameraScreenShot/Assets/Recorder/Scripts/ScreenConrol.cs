using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenConrol : MonoBehaviour
{
    
    public KeyCode MakeScreenShotButton = KeyCode.C;
    public  ScreenShot _screenShot;


    private void Update()
    {
        //Кнопка для нажатия
        if (Input.GetKeyDown(MakeScreenShotButton))
        {
            //делаем скриншот
            _screenShot.MakeScrn();
        }
    }
}
