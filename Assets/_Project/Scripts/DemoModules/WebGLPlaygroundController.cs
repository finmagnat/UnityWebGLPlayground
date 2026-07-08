using WebGLPlayground.Core;
using UnityEngine;
using WebGLPlayground.WebGL;

namespace WebGLPlayground.DemoModules
{
    public sealed class WebGLPlaygroundController : MonoBehaviour
    {
        [SerializeField] private PlaygroundLogger logger;

        public void OpenUrl()
        {
            Application.OpenURL("https://unity.com/");
            logger.Log("OpenURL called.");
        }

        public void SavePrefs()
        {
            PlayerPrefs.SetString("webgl_demo_value", System.DateTime.Now.ToString("O"));
            PlayerPrefs.Save();

            logger.Log("PlayerPrefs saved.");
        }

        public void LoadPrefs()
        {
            string value = PlayerPrefs.GetString("webgl_demo_value", "<empty>");
            logger.Log($"PlayerPrefs loaded: {value}");
        }

        public void ShowBrowserInfo()
        {
            logger.Log($"Platform: {Application.platform}");
            logger.Log($"Unity version: {Application.unityVersion}");
            logger.Log($"System language: {Application.systemLanguage}");
        }
        
        public void JSCall()
        {
            WebGLBridge.ShowAlert();
            logger.Log("JavaScript alert requested.");
        }
        
        public void CopyClipboard()
        {
            const string text = "Hello from Unity WebGL!";
            WebGLBridge.CopyToClipboard(text);
            logger.Log($"Copy to clipboard requested: {text}");
        }
        
        public void RequestJavaScriptCallback()
        {
            logger.Log("JavaScript callback requested.");
            WebGLBridge.SendCallbackToUnity();
        }

        public void OnJavaScriptCallback(string message)
        {
            logger.Log($"JavaScript callback received: {message}");
        }
        
        public void Fullscreen()
        {
            WebGLBridge.ToggleFullscreen();
            logger.Log("Fullscreen toggled.");
        }
        
        public void DownloadFile()
        {
            string fileName = "webgl-playground-log.txt";
            string content =
                "Unity WebGL Playground\n" +
                $"Generated at: {System.DateTime.Now:O}\n" +
                "This file was created in Unity and downloaded through Browser API.";

            WebGLBridge.DownloadTextFile(fileName, content);
            logger.Log($"Download requested: {fileName}");
        }
    }
}