using UnityEngine;
using System.IO;

[RequireComponent(typeof(Camera))]
public class VideoRecorder : MonoBehaviour {
	

	public int frameRate = 30;
	public RenderTexture VideoTexture;
	Camera ScrnCam;
	float Width=Screen.width; 
	float Height=Screen.height;
	private string dataPath;
	#region VideoCapt
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
			Debug.Log("Создайте текстуру для рендера!");
		}
		Width = VideoTexture.width;
		Height = VideoTexture.height;
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
			ScrnCam.gameObject.SetActive(false);
			Time.timeScale = 1f;
			//Time.fixedDeltaTime = fixedDeltaTimeCache;
		}
	}
	void LateUpdate () {
		
		if (ScrnCam.gameObject.activeInHierarchy){
			localDeltaTime = Time.realtimeSinceStartup - prevTime;
			prevTime = Time.realtimeSinceStartup;
			//перевод текстуры в картинку
			Texture2D Shot =ToTexture2D(VideoTexture);
			byte[] bytes = Shot.EncodeToPNG();
			string filename = path+imgIndex.ToString("D8")+".png";
			File.WriteAllBytes(filename, bytes);
			imgIndex+=1;
			Time.timeScale = 1.0f/localDeltaTime/frameRate;
			//Time.fixedDeltaTime = fixedDeltaTimeCache / Time.timeScale;
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


