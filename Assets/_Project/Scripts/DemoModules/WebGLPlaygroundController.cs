using WebGLPlayground.Core;
using UnityEngine;

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
    }
}