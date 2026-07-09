using UnityEngine;
using WebGLPlayground.WebGL;

namespace WebGLPlayground.DemoModules
{
    public readonly struct BrowserDiagnosticsData
    {
        public BrowserDiagnosticsData(
            string browser,
            string platform,
            string unityVersion,
            string language,
            string resolution,
            string devicePixelRatio,
            string webGlVersion,
            string gpuVendor,
            string gpuRenderer,
            string userAgent)
        {
            Browser = browser;
            Platform = platform;
            UnityVersion = unityVersion;
            Language = language;
            Resolution = resolution;
            DevicePixelRatio = devicePixelRatio;
            WebGlVersion = webGlVersion;
            GpuVendor = gpuVendor;
            GpuRenderer = gpuRenderer;
            UserAgent = userAgent;
        }

        public string Browser { get; }
        public string Platform { get; }
        public string UnityVersion { get; }
        public string Language { get; }
        public string Resolution { get; }
        public string DevicePixelRatio { get; }
        public string WebGlVersion { get; }
        public string GpuVendor { get; }
        public string GpuRenderer { get; }
        public string UserAgent { get; }
    }

    public static class BrowserDiagnosticsModule
    {
        public static BrowserDiagnosticsData BuildData()
        {
            string userAgent = WebGLBridge.GetUserAgent();
            string gpuInfo = WebGLBridge.GetGpuInfo();
            SplitGpuInfo(gpuInfo, out string gpuVendor, out string gpuRenderer);

            return new BrowserDiagnosticsData(
                DetectBrowser(userAgent),
                GetPlatformLabel(),
                Application.unityVersion,
                Application.systemLanguage.ToString(),
                $"{Screen.width} x {Screen.height}",
                WebGLBridge.GetDevicePixelRatio(),
                WebGLBridge.GetWebGLVersion(),
                gpuVendor,
                gpuRenderer,
                userAgent);
        }

        private static void SplitGpuInfo(string gpuInfo, out string vendor, out string renderer)
        {
            if (string.IsNullOrWhiteSpace(gpuInfo))
            {
                vendor = "Unavailable";
                renderer = "Unavailable";
                return;
            }

            string[] parts = gpuInfo.Split(new[] { " / " }, System.StringSplitOptions.None);

            if (parts.Length >= 2)
            {
                vendor = parts[0];
                renderer = parts[1];
                return;
            }

            vendor = "Unavailable";
            renderer = gpuInfo;
        }

        private static string GetPlatformLabel()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                return "WebGL";

            if (Application.isEditor)
                return "Unity Editor";

            return Application.platform.ToString();
        }

        private static string DetectBrowser(string userAgent)
        {
            if (string.IsNullOrWhiteSpace(userAgent))
                return "Unavailable";

            if (userAgent.Contains("Firefox/"))
                return ExtractBrowserVersion(userAgent, "Firefox/");

            if (userAgent.Contains("Edg/"))
                return ExtractBrowserVersion(userAgent, "Edg/", "Edge ");

            if (userAgent.Contains("Chrome/"))
                return ExtractBrowserVersion(userAgent, "Chrome/");

            if (userAgent.Contains("Safari/") && userAgent.Contains("Version/"))
                return ExtractBrowserVersion(userAgent, "Version/", "Safari ");

            if (userAgent == "Unity Editor")
                return "Unity Editor";

            return "Unknown";
        }

        private static string ExtractBrowserVersion(string userAgent, string token, string displayName = null)
        {
            int start = userAgent.IndexOf(token, System.StringComparison.Ordinal);

            if (start < 0)
                return "Unknown";

            start += token.Length;
            int end = userAgent.IndexOf(' ', start);

            if (end < 0)
                end = userAgent.Length;

            string version = userAgent.Substring(start, end - start);
            int dotIndex = version.IndexOf('.');

            if (dotIndex > 0)
                version = version.Substring(0, dotIndex);

            return $"{displayName ?? token.TrimEnd('/')} {version}";
        }
    }
}
