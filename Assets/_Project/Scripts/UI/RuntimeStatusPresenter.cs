using TMPro;
using UnityEngine;
using WebGLPlayground.WebGL;

namespace WebGLPlayground.UI
{
    public static class RuntimeStatusPresenter
    {
        private const string StatusTextObjectName = "StatusText";

        public static void Refresh()
        {
            GameObject statusObject = GameObject.Find(StatusTextObjectName);

            if (statusObject == null || !statusObject.TryGetComponent(out TMP_Text statusText))
                return;

            statusText.richText = true;
            statusText.fontSize = 18;
            statusText.enableAutoSizing = false;
            statusText.textWrappingMode = TextWrappingModes.Normal;
            statusText.text =
                FormatStatus("JS Bridge Ready", WebGLBridge.IsBridgeReady()) + "    " +
                FormatStatus("Browser Connected", WebGLBridge.IsBrowserConnected()) + "    " +
                FormatStatus("Clipboard API Available", WebGLBridge.IsClipboardApiAvailable()) + "    " +
                FormatStatus("Fullscreen API Available", WebGLBridge.IsFullscreenApiAvailable());
        }

        private static string FormatStatus(string label, bool isReady)
        {
            string color = isReady ? "#7CFF9A" : "#FF6B6B";
            return $"<color={color}>\u25CF</color> {label}";
        }
    }
}
