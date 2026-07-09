using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebGLPlayground.DemoModules;

namespace WebGLPlayground.UI
{
    public sealed class BrowserDiagnosticsWindow : MonoBehaviour
    {
        private const string WindowName = "RuntimeBrowserDiagnosticsWindow";

        private Transform _rowsRoot;

        public static void Show(BrowserDiagnosticsData data)
        {
            BrowserDiagnosticsWindow window = FindOrCreate();
            window.gameObject.SetActive(true);
            window.transform.SetAsLastSibling();
            window.Render(data);
        }

        private static BrowserDiagnosticsWindow FindOrCreate()
        {
            GameObject existing = GameObject.Find(WindowName);

            if (existing != null && existing.TryGetComponent(out BrowserDiagnosticsWindow window))
                return window;

            Canvas canvas = FindFirstObjectByType<Canvas>();
            GameObject root = CreateRect(WindowName, canvas.transform);
            root.AddComponent<CanvasRenderer>();
            Image backdrop = root.AddComponent<Image>();
            backdrop.color = new Color(0.01f, 0.015f, 0.025f, 0.72f);

            RectTransform rootRect = (RectTransform)root.transform;
            rootRect.anchorMin = Vector2.zero;
            rootRect.anchorMax = Vector2.one;
            rootRect.offsetMin = Vector2.zero;
            rootRect.offsetMax = Vector2.zero;

            BrowserDiagnosticsWindow component = root.AddComponent<BrowserDiagnosticsWindow>();
            component.BuildWindow();
            root.SetActive(false);

            return component;
        }

        private void BuildWindow()
        {
            GameObject panel = CreateRect("DiagnosticsPanel", transform);
            panel.AddComponent<CanvasRenderer>();
            Image panelImage = panel.AddComponent<Image>();
            panelImage.color = new Color(0.035f, 0.055f, 0.085f, 0.98f);

            RectTransform panelRect = (RectTransform)panel.transform;
            panelRect.anchorMin = new Vector2(0.12f, 0.08f);
            panelRect.anchorMax = new Vector2(0.88f, 0.92f);
            panelRect.offsetMin = Vector2.zero;
            panelRect.offsetMax = Vector2.zero;

            VerticalLayoutGroup panelLayout = panel.AddComponent<VerticalLayoutGroup>();
            panelLayout.padding = new RectOffset(28, 28, 24, 24);
            panelLayout.spacing = 18;
            panelLayout.childControlWidth = true;
            panelLayout.childControlHeight = true;
            panelLayout.childForceExpandWidth = true;
            panelLayout.childForceExpandHeight = false;

            GameObject header = CreateRect("Header", panel.transform);
            HorizontalLayoutGroup headerLayout = header.AddComponent<HorizontalLayoutGroup>();
            headerLayout.childControlWidth = true;
            headerLayout.childControlHeight = true;
            headerLayout.childForceExpandWidth = false;
            headerLayout.childForceExpandHeight = false;
            headerLayout.childAlignment = TextAnchor.MiddleCenter;

            TMP_Text title = CreateText("Title", header.transform, "Browser Diagnostics", 32, FontStyles.Bold, TextAlignmentOptions.Left);
            title.color = new Color(0.9f, 0.97f, 1f, 1f);
            LayoutElement titleLayout = title.gameObject.AddComponent<LayoutElement>();
            titleLayout.flexibleWidth = 1;
            titleLayout.preferredHeight = 46;

            Button close = CreateButton("CloseButton", header.transform, "Close");
            close.onClick.AddListener(() => gameObject.SetActive(false));
            LayoutElement closeLayout = close.gameObject.AddComponent<LayoutElement>();
            closeLayout.preferredWidth = 120;
            closeLayout.preferredHeight = 42;

            ScrollRect scrollRect = CreateScrollRect(panel.transform, out _rowsRoot);
            LayoutElement scrollLayout = scrollRect.gameObject.AddComponent<LayoutElement>();
            scrollLayout.flexibleHeight = 1;
            scrollLayout.minHeight = 260;
        }

        private void Render(BrowserDiagnosticsData data)
        {
            for (int i = _rowsRoot.childCount - 1; i >= 0; i--)
                Destroy(_rowsRoot.GetChild(i).gameObject);

            AddRow("Browser", data.Browser);
            AddRow("Platform", data.Platform);
            AddRow("Unity", data.UnityVersion);
            AddRow("Language", data.Language);
            AddRow("Resolution", data.Resolution);
            AddRow("Device Pixel Ratio", data.DevicePixelRatio);
            AddRow("WebGL", data.WebGlVersion);
            AddRow("GPU Vendor", data.GpuVendor);
            AddRow("GPU Renderer", data.GpuRenderer);
            AddRow("User Agent", data.UserAgent);

            Canvas.ForceUpdateCanvases();
        }

        private void AddRow(string label, string value)
        {
            GameObject row = CreateRect(label, _rowsRoot);
            HorizontalLayoutGroup rowLayout = row.AddComponent<HorizontalLayoutGroup>();
            rowLayout.spacing = 18;
            rowLayout.childAlignment = TextAnchor.UpperLeft;
            rowLayout.childControlWidth = true;
            rowLayout.childControlHeight = true;
            rowLayout.childForceExpandWidth = false;
            rowLayout.childForceExpandHeight = false;

            TMP_Text labelText = CreateText("Label", row.transform, label + ":", 20, FontStyles.Bold, TextAlignmentOptions.Left);
            labelText.color = new Color(0.64f, 0.78f, 0.92f, 1f);
            LayoutElement labelLayout = labelText.gameObject.AddComponent<LayoutElement>();
            labelLayout.preferredWidth = 220;

            TMP_Text valueText = CreateText("Value", row.transform, string.IsNullOrWhiteSpace(value) ? "Unavailable" : value, 20, FontStyles.Normal, TextAlignmentOptions.Left);
            valueText.color = new Color(0.94f, 0.97f, 1f, 1f);
            valueText.textWrappingMode = TextWrappingModes.Normal;
            LayoutElement valueLayout = valueText.gameObject.AddComponent<LayoutElement>();
            valueLayout.flexibleWidth = 1;
        }

        private static ScrollRect CreateScrollRect(Transform parent, out Transform content)
        {
            GameObject scroll = CreateRect("ScrollView", parent);
            ScrollRect scrollRect = scroll.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.movementType = ScrollRect.MovementType.Clamped;
            scrollRect.scrollSensitivity = 24;

            GameObject viewport = CreateRect("Viewport", scroll.transform);
            viewport.AddComponent<CanvasRenderer>();
            Image viewportImage = viewport.AddComponent<Image>();
            viewportImage.color = new Color(0.015f, 0.025f, 0.04f, 0.9f);
            viewport.AddComponent<RectMask2D>();

            RectTransform viewportRect = (RectTransform)viewport.transform;
            viewportRect.anchorMin = Vector2.zero;
            viewportRect.anchorMax = Vector2.one;
            viewportRect.offsetMin = Vector2.zero;
            viewportRect.offsetMax = Vector2.zero;

            GameObject contentObject = CreateRect("Content", viewport.transform);
            VerticalLayoutGroup contentLayout = contentObject.AddComponent<VerticalLayoutGroup>();
            contentLayout.padding = new RectOffset(18, 18, 18, 18);
            contentLayout.spacing = 14;
            contentLayout.childControlWidth = true;
            contentLayout.childControlHeight = true;
            contentLayout.childForceExpandWidth = true;
            contentLayout.childForceExpandHeight = false;
            ContentSizeFitter fitter = contentObject.AddComponent<ContentSizeFitter>();
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            RectTransform contentRect = (RectTransform)contentObject.transform;
            contentRect.anchorMin = new Vector2(0, 1);
            contentRect.anchorMax = new Vector2(1, 1);
            contentRect.pivot = new Vector2(0.5f, 1);
            contentRect.anchoredPosition = Vector2.zero;
            contentRect.sizeDelta = Vector2.zero;

            scrollRect.viewport = viewportRect;
            scrollRect.content = contentRect;
            content = contentObject.transform;

            return scrollRect;
        }

        private static Button CreateButton(string name, Transform parent, string label)
        {
            GameObject buttonObject = CreateRect(name, parent);
            buttonObject.AddComponent<CanvasRenderer>();
            Image image = buttonObject.AddComponent<Image>();
            image.color = new Color(0.11f, 0.17f, 0.25f, 1f);

            Button button = buttonObject.AddComponent<Button>();
            button.targetGraphic = image;
            ColorBlock colors = button.colors;
            colors.normalColor = new Color(0.11f, 0.17f, 0.25f, 1f);
            colors.highlightedColor = new Color(0.16f, 0.25f, 0.36f, 1f);
            colors.pressedColor = new Color(0.07f, 0.11f, 0.17f, 1f);
            colors.selectedColor = colors.highlightedColor;
            button.colors = colors;

            TMP_Text text = CreateText("Label", buttonObject.transform, label, 19, FontStyles.Bold, TextAlignmentOptions.Center);
            text.color = new Color(0.92f, 0.97f, 1f, 1f);
            RectTransform textRect = (RectTransform)text.transform;
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            return button;
        }

        private static TMP_Text CreateText(string name, Transform parent, string text, float size, FontStyles style, TextAlignmentOptions alignment)
        {
            GameObject textObject = CreateRect(name, parent);
            textObject.AddComponent<CanvasRenderer>();
            TextMeshProUGUI tmp = textObject.AddComponent<TextMeshProUGUI>();
            tmp.text = text;
            tmp.fontSize = size;
            tmp.fontStyle = style;
            tmp.alignment = alignment;
            tmp.enableAutoSizing = false;
            tmp.textWrappingMode = TextWrappingModes.Normal;
            tmp.overflowMode = TextOverflowModes.Overflow;
            tmp.raycastTarget = false;
            return tmp;
        }

        private static GameObject CreateRect(string name, Transform parent)
        {
            GameObject gameObject = new GameObject(name, typeof(RectTransform));
            gameObject.transform.SetParent(parent, false);
            return gameObject;
        }
    }
}
