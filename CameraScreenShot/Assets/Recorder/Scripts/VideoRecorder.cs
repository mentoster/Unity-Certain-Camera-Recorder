using UnityEngine;
using System.IO;

public enum QualityCam
{
	PNG,JPG
}
[RequireComponent(typeof(Camera))]
public class VideoRecorder : MonoBehaviour
{
	public QualityCam QualityOfCam;
	public RenderTexture VideoTexture;
	Camera ScrnCam;
	float Width=Screen.width; 
	float Height=Screen.height;
	private string dataPath;
	public bool UseScreenSize=true;
	public bool MakeFreezingForCinema = true;
	public bool AlwaysOn = false;
	#region VideoCapt
	public int frameRate = 30;
	bool isCapturing = false;
	string path;
	int folderIndex = 0;
	int imgIndex = 0;
	float localDeltaTime,prevTime,fixedDeltaTimeCache;
	#endregion

	private void Awake()
	{
		ScrnCam = GetComponent<Camera>();
		dataPath = $"{Application.dataPath}/Video/";
		ScrnCam = GetComponent<Camera>();
		if (VideoTexture == null)
		{
			Debug.Log("Создайте текстуру для рендера! \n Create a texture for the render!");
		}
		if (!UseScreenSize)
		{
			Width = VideoTexture.width;
			Height = VideoTexture.height;
		}
		else
		{
			VideoTexture.width = (int) Width;
			VideoTexture.height = (int) Height;
		}
       if(!AlwaysOn)
		ScrnCam.gameObject.SetActive(false);
		//если нет директории сохранения файла
		bool exists = System.IO.Directory.Exists(dataPath);
		if (!exists) 
			System.IO.Directory.CreateDirectory(dataPath);
	}
	void Start () {
		DontDestroyOnLoad(this.gameObject);
		fixedDeltaTimeCache = Time.fixedDeltaTime;
	}
	
	public void MakeVideo()
	{
		isCapturing = !isCapturing;
		if (isCapturing) {
			ScrnCam.gameObject.SetActive(true);
			folderIndex+=1;
			imgIndex = 0;
			path = dataPath+"IMG Sequence "+folderIndex;
			if (Directory.Exists(path)==false) 
				Directory.CreateDirectory(path);
			path = path + "/";
		} else {
			if(!AlwaysOn)
			ScrnCam.gameObject.SetActive(false);
			if (MakeFreezingForCinema)
			{
				Time.timeScale = 1f;
				//Time.fixedDeltaTime = fixedDeltaTimeCache;
			}
		}
	}
	void LateUpdate () {
		
		if (ScrnCam.gameObject.activeInHierarchy){
			localDeltaTime = Time.realtimeSinceStartup - prevTime;
			prevTime = Time.realtimeSinceStartup;	
			Texture2D Shot = ToTexture2D(VideoTexture);
			//перевод текстуры в картинку
			if ( QualityOfCam == QualityCam.PNG)
			{
				byte[] bytes = Shot.EncodeToPNG();
				string filename = path+imgIndex.ToString("D8")+".png";
				File.WriteAllBytes(filename, bytes);
			}
			else
			{
				byte[] bytes = Shot.EncodeToJPG();
				string filename = path+imgIndex.ToString("D8")+".jpg";
				File.WriteAllBytes(filename, bytes);
			}
			//декодируем в png, для наивышего качества
		
			imgIndex+=1;
			if (MakeFreezingForCinema)
			{
				//Time.timeScale = 1.0f/localDeltaTime/frameRate;
				//Time.fixedDeltaTime = fixedDeltaTimeCache / Time.timeScale;
			}
		}
	}
	//переводим из RenderTexture в Texture2D
	Texture2D ToTexture2D(RenderTexture rTex)
	{
		Texture2D tex = new Texture2D((int) Width, (int) Height, TextureFormat.RGB24, false);
		RenderTexture.active = rTex;
		tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
		tex.Apply();
		return tex;
	}
}


