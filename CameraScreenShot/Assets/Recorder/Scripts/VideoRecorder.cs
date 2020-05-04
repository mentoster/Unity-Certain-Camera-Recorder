using UnityEngine;
using System.IO;
using UnityEngine.Serialization;

public enum QualityCam
{
	PNG,JPG
}
public class VideoRecorder : MonoBehaviour
{
	#region Settings
	public RenderTexture VideoTexture;
	public QualityCam QualityOfCam;
	public bool UseScreenSize=true;
	public bool MakeFreezingForCinema = true; 
    public bool RecordAlwaysOn = false;
	#endregion
	private string dataPath=$"{Application.dataPath}/Video/";
	Camera ScrnCam;
	float Width=Screen.width; 
	float Height=Screen.height;
	
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
		if (VideoTexture == null)
		{
			Debug.Log("reate a texture for the render!");
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
        if(!RecordAlwaysOn)
		ScrnCam.gameObject.SetActive(false);
		//if there is no file save directory
		//we will make it
		bool exists = System.IO.Directory.Exists(dataPath);
		if (!exists) 
			System.IO.Directory.CreateDirectory(dataPath);
	}
	void Start () {
		//  for rendering, after changing the scene
		//	DontDestroyOnLoad(this.gameObject);
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
			if(!RecordAlwaysOn)
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
			//transform Texture Into Picture
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
			imgIndex+=1;
			if (MakeFreezingForCinema)
			{
				//Time.timeScale = 1.0f/localDeltaTime/frameRate;
				//Time.fixedDeltaTime = fixedDeltaTimeCache / Time.timeScale;
			}
		}
	}
	//from RenderTexture to Texture2D
	Texture2D ToTexture2D(RenderTexture rTex)
	{
		Texture2D tex = new Texture2D((int) Width, (int) Height, TextureFormat.RGB24, false);
		RenderTexture.active = rTex;
		tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
		tex.Apply();
		return tex;
	}
}


