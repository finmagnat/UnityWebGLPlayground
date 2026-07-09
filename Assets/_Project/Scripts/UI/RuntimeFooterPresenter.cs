using TMPro;
using UnityEngine;

namespace WebGLPlayground.UI
{
    public static class RuntimeFooterPresenter
    {
        private const string FooterObjectName = "RuntimeFooter";

        public static void Ensure()
        {
            if (GameObject.Find(FooterObjectName) != null)
                return;

            Canvas canvas = Object.FindFirstObjectByType<Canvas>();

            if (canvas == null)
                return;

            GameObject footer = new GameObject(FooterObjectName, typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
            footer.transform.SetParent(canvas.transform, false);
            footer.transform.SetAsLastSibling();

            RectTransform rectTransform = (RectTransform)footer.transform;
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 0);
            rectTransform.pivot = new Vector2(0.5f, 0);
            rectTransform.anchoredPosition = new Vector2(0, 10);
            rectTransform.sizeDelta = new Vector2(-48, 26);

            TextMeshProUGUI text = footer.GetComponent<TextMeshProUGUI>();
            text.text = $"Unity {Application.unityVersion}  |  WebGL 2.0  |  Browser Integration Lab  |  v0.2";
            text.fontSize = 16;
            text.enableAutoSizing = false;
            text.textWrappingMode = TextWrappingModes.NoWrap;
            text.alignment = TextAlignmentOptions.Right;
            text.color = new Color(0.62f, 0.74f, 0.86f, 0.82f);
            text.raycastTarget = false;
        }
    }
}
