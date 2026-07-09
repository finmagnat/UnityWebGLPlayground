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

        [DllImport("__Internal")]
        private static extern string WGL_GetDevicePixelRatio();

        [DllImport("__Internal")]
        private static extern string WGL_GetUserAgent();

        [DllImport("__Internal")]
        private static extern string WGL_GetWebGLVersion();

        [DllImport("__Internal")]
        private static extern string WGL_GetGpuInfo();

        [DllImport("__Internal")]
        private static extern int WGL_IsBrowserConnected();

        [DllImport("__Internal")]
        private static extern int WGL_IsClipboardApiAvailable();

        [DllImport("__Internal")]
        private static extern int WGL_IsFullscreenApiAvailable();
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

        public static string GetDevicePixelRatio()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return WGL_GetDevicePixelRatio();
#else
            return "Editor preview";
#endif
        }

        public static string GetUserAgent()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return WGL_GetUserAgent();
#else
            return "Unity Editor";
#endif
        }

        public static string GetWebGLVersion()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return WGL_GetWebGLVersion();
#else
            return "Editor preview";
#endif
        }

        public static string GetGpuInfo()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return WGL_GetGpuInfo();
#else
            return SystemInfo.graphicsDeviceName;
#endif
        }

        public static bool IsBridgeReady()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return true;
#else
            return false;
#endif
        }

        public static bool IsBrowserConnected()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return WGL_IsBrowserConnected() != 0;
#else
            return false;
#endif
        }

        public static bool IsClipboardApiAvailable()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return WGL_IsClipboardApiAvailable() != 0;
#else
            return false;
#endif
        }

        public static bool IsFullscreenApiAvailable()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return WGL_IsFullscreenApiAvailable() != 0;
#else
            return false;
#endif
        }
    }
}
