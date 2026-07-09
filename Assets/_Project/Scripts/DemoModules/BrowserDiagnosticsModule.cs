using System.Text;
using UnityEngine;
using WebGLPlayground.WebGL;

namespace WebGLPlayground.DemoModules
{
    public static class BrowserDiagnosticsModule
    {
        public static string BuildReport()
        {
            var builder = new StringBuilder();

            builder.AppendLine("Browser Diagnostics");
            builder.AppendLine($"Unity version: {Application.unityVersion}");
            builder.AppendLine($"Application platform: {Application.platform}");
            builder.AppendLine($"System language: {Application.systemLanguage}");
            builder.AppendLine($"Screen: {Screen.width} x {Screen.height}");
            builder.AppendLine($"Device pixel ratio: {WebGLBridge.GetDevicePixelRatio()}");
            builder.AppendLine($"User agent: {WebGLBridge.GetUserAgent()}");
            builder.AppendLine($"WebGL version: {WebGLBridge.GetWebGLVersion()}");
            builder.AppendLine($"GPU: {WebGLBridge.GetGpuInfo()}");

            return builder.ToString().TrimEnd();
        }
    }
}
