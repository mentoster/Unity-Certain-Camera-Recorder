using UnityEngine;
//разрешаем использовать скрипт только на камере 
[RequireComponent(typeof(Camera))]
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
       //если нет директории сохранения файла
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
       //проверяем, включена ли камера
       if (ScrnCam.gameObject.activeInHierarchy)
       {
           //если включена, создаем 2д текстуру png
           Texture2D Shot = new Texture2D(Width,Height,TextureFormat.RGB24,false);
           //говорим камере сгенерировать текстуру,
           //потому что пока она на самом деле еще не срендерена
           ScrnCam.Render();
           RenderTexture.active = ScrnCam.targetTexture;
           
           Shot.ReadPixels(new Rect(0, 0, Width, Height), 0, 0);
           //формируем получченный массив байтов в картинку формата PNG
           byte[] bytes = Shot.EncodeToPNG();
           
           //формируем имя скриншота
          
           string filename = ScreenShotName();
           scrNumber++;
           System.IO.File.WriteAllBytes(filename, bytes);
           //Скриншот Успешно Сделан!
           Debug.Log($"Sreenshot taken with Width - {Width} and Height - {Height}, {scrNumber} ");
           ScrnCam.gameObject.SetActive(false);
       }
   }

   private string ScreenShotName()
   {
       
       //расположение скриншота
       return string.Format($"{dataPath}/" +
                            $"Number_{scrNumber}_data_" +
                            $"{System.DateTime.Now.ToString("yy-MM-dd_HH-mm-ss")}" +
                            $"_screen_{Width}x{Height}.png");

   }
}
