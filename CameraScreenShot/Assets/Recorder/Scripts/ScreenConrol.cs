using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenConrol : MonoBehaviour
{
    
    public KeyCode MakeScreenShotButton = KeyCode.C;
    public  ScreenShot _screenShot;


    private void Update()
    {
        //Button to press
        if (Input.GetKeyDown(MakeScreenShotButton))
        {
            //take screenshot
            _screenShot.MakeScrn();
        }
    }
}
