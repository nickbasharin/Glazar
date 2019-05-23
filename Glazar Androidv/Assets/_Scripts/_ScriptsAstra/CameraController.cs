using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

    private bool torch;
    public GameObject Bar;
    private RecordUIManager rUIm;
    public int videoH=1280;
    public int videoW=720;
    public int fpsValue = 30;
    private Rect area;
    public Capture cap;
	public string folderName;
    private int BarValue;
    private float rat;
    public AudioClip ac;
    private bool capturing=false;

    void Start()
    {        
		VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
		VuforiaARController.Instance.RegisterOnPauseCallback(OnPaused);
        ScreenshotManager.OnScreenshotTaken += ScreenshotTaken;
        ScreenshotManager.OnScreenshotSaved += ScreenshotSaved;
        BarValue = (int) Mathf.RoundToInt(Bar.GetComponent<RectTransform>().rect.height * Bar.GetComponent<RectTransform>().lossyScale.y);
        area.height = Screen.height - BarValue;
        area.width = Screen.width;
        area.x = 0;
        area.y = Bar.GetComponent<RectTransform>().rect.height*Bar.GetComponent<RectTransform>().lossyScale.y;

        videoW = Screen.width;
        videoH = Screen.height;
        if (videoW > 720 || videoH > 1280)
            {
            rat = (float)Screen.height / (float)Screen.width;
            videoW = 720;
            videoH = (int)(videoW * rat);
            }
        rUIm = GetComponent<RecordUIManager>();
    }

    public void ToMenu() {
        SceneManager.LoadScene(0);
    }

    private void OnVuforiaStarted()
    {
        CameraDevice.Instance.SetFocusMode(
            CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    private void OnPaused(bool paused)
    {
        if (!paused) // resumed
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }

    private void OnApplicationFocus(bool onFocus) {
        if (onFocus)
        {
            Debug.Log("APP_TAKE_FOCUS");
        }
        else {
            Debug.Log("APP_LOSE_FOCUS");
            RecordStop();
        }    
    }

    public void WInput(Text w) { videoW = int.Parse(w.text); Debug.Log("VideoW ="+w.text); }
    public void HInput(Text h) { videoH = int.Parse(h.text); Debug.Log("VideoH =" + h.text); }

    private void Update() {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
            ToMenu();
            }
         
        }

    public void SwapCamera() {
        CameraDevice.CameraDirection currentDir = CameraDevice.Instance.GetCameraDirection();
        if (currentDir == CameraDevice.CameraDirection.CAMERA_BACK || currentDir == CameraDevice.CameraDirection.CAMERA_DEFAULT)
            RestartCamera(CameraDevice.CameraDirection.CAMERA_FRONT, true);
        else
            RestartCamera(CameraDevice.CameraDirection.CAMERA_BACK, false);
    }

    private void RestartCamera(CameraDevice.CameraDirection newDir, bool mirror)
    {
        CameraDevice.Instance.Stop();
        CameraDevice.Instance.Deinit();

        CameraDevice.Instance.Init(newDir);

        // Set mirroring 
        var config = VuforiaRenderer.Instance.GetVideoBackgroundConfig();
        config.reflection = mirror ? VuforiaRenderer.VideoBackgroundReflection.ON : VuforiaRenderer.VideoBackgroundReflection.OFF;
        VuforiaRenderer.Instance.SetVideoBackgroundConfig(config);

        CameraDevice.Instance.Start();
    }

    public void TorchTrigger()

    {
        
        if (torch)
        {
            CameraDevice.Instance.SetFlashTorchMode(false);
            torch = false;
        }
        else
        {
            CameraDevice.Instance.SetFlashTorchMode(true);
            torch = true;
        }
    }
   
    private void ScreenshotSaved(string takedtext)
    {
        Debug.Log("saved");
    }
    private void ScreenshotTaken(Texture2D temp)
    {
        Debug.Log("taken");
    }

    public void TakeScreenshot()

    {
        Debug.Log("take start");
		ScreenshotManager.SaveScreenshot("AR", folderName,"jpg",area);
    }

    void OnDestroy()
    {
        ScreenshotManager.OnScreenshotTaken -= ScreenshotTaken;
        ScreenshotManager.OnScreenshotSaved -= ScreenshotSaved;
        RecordStop();
    }

    public void RecordDelayedStart() {        
        cap.StartCapturing();
   }

    public void RecordStart()
    {
        if (capturing == false) {
            SetVideoParams(videoW, videoH, fpsValue);
            rUIm.Change(true);
            capturing = true;
            StartCoroutine("ClipTimeWait");
        }
        
    }

    IEnumerator ClipTimeWait()
    {
        yield return new WaitForSeconds(ac.length);
        RecordDelayedStart();
    }

    public void RecordStop() {
        if (capturing == true) {
            rUIm.Change(false);
            cap.StopCapturing();
            capturing = false;
        }        
 }

    private void SetVideoParams(int w,int h, int fps, int btrt=3000) {
        if ((Screen.orientation == ScreenOrientation.Portrait) || (Screen.orientation == ScreenOrientation.PortraitUpsideDown))
        {
            cap.videoWidth = w;
            cap.videoHeight = h;
        }
        else {
            cap.videoWidth = h;
            cap.videoHeight = w;

        }
        cap.videoFrameRate = fps;
        cap.videoBitRate = btrt;
    }
    

}
