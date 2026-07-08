using System.Runtime.InteropServices;
using UnityEngine;

namespace WebGLPlayground.WebGL
{
    public static class WebGLBridge
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void WGL_ShowAlert();

        [DllImport("__Internal")]
        private static extern void WGL_CopyToClipboard(string text);
        
        [DllImport("__Internal")]
        private static extern void WGL_SendCallbackToUnity();
        
        [DllImport("__Internal")]
        private static extern void WGL_ToggleFullscreen();
        
        [DllImport("__Internal")]
        private static extern void WGL_DownloadTextFile(string fileName, string content);
#endif

        public static void ShowAlert()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            WGL_ShowAlert();
#else
            Debug.Log("[WebGLBridge] ShowAlert works only in WebGL build.");
#endif
        }

        public static void CopyToClipboard(string text)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            WGL_CopyToClipboard(text);
#else
            GUIUtility.systemCopyBuffer = text;
            Debug.Log($"[WebGLBridge] Copied in Editor: {text}");
#endif
        }
        
        public static void SendCallbackToUnity()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            WGL_SendCallbackToUnity();
#else
            Debug.Log("[WebGLBridge] JS callback works only in WebGL build.");
#endif
        }
        
        public static void ToggleFullscreen()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            WGL_ToggleFullscreen();
#else
            Debug.Log("[WebGLBridge] Fullscreen toggle works only in WebGL build.");
#endif
        }
        
        public static void DownloadTextFile(string fileName, string content)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            WGL_DownloadTextFile(fileName, content);
#else
            Debug.Log($"[WebGLBridge] Download simulated in Editor: {fileName}\n{content}");
#endif
        }
    }
}