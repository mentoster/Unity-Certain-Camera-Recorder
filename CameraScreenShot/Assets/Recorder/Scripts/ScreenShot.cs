using UnityEngine;
public class ScreenShot : MonoBehaviour
{

    Camera ScrnCam;
     int Width=Screen.width;
     int Height=Screen.height;
     int scrNumber=0;
     private string dataPath;
   private void Awake()
   {
       dataPath = $"{Application.dataPath}/Screenshots";
       ScrnCam = GetComponent<Camera>();
       if (ScrnCam.targetTexture = null)
       {
           
           ScrnCam.targetTexture=new RenderTexture(Width,Height,(int) ScrnCam.depth);
       }
       ScrnCam.gameObject.SetActive(false);
       //if there is no file save directory
       bool exists = System.IO.Directory.Exists(dataPath);
       if (!exists)
           System.IO.Directory.CreateDirectory(dataPath);
   }

   public void MakeScrn()
   {
       ScrnCam.gameObject.SetActive(true);
   }
   private void  LateUpdate()
   {
       if (ScrnCam.gameObject.activeInHierarchy)
       {
           //if enabled, create a 2D texture png
           
           Texture2D Shot = new Texture2D(Width,Height,TextureFormat.RGB24,false);
           //tell the camera to generate a texture
           //because while it is not really rendered yet
           ScrnCam.Render();
           RenderTexture.active = ScrnCam.targetTexture;
           
           Shot.ReadPixels(new Rect(0, 0, Width, Height), 0, 0);
           //form the resulting array of bytes into a PNG image
           byte[] bytes = Shot.EncodeToPNG();
           
           //name of the screenshot
           string filename = ScreenShotName();
           scrNumber++;
           System.IO.File.WriteAllBytes(filename, bytes);
           //Done!
           Debug.Log($"Sreenshot taken with Width - {Width} and Height - {Height}, {scrNumber} ");
           ScrnCam.gameObject.SetActive(false);
       }
   }

   private string ScreenShotName()
   {
       
       //Path
       return string.Format($"{dataPath}/" +
                            $"Number_{scrNumber}_data_" +
                            $"{System.DateTime.Now.ToString("yy-MM-dd_HH-mm-ss")}" +
                            $"_screen_{Width}x{Height}.png");

   }
}
