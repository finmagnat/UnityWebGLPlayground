using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WebGLPlayground.Core
{
    public sealed class PlaygroundLogger : MonoBehaviour
    {
        [SerializeField] private TMP_Text output;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField, Min(5)] private int maxLines = 12;

        private readonly System.Collections.Generic.Queue<string> _lines = new();
        private Coroutine _scrollCoroutine;

        public void Log(string message)
        {
            string line = $"[{System.DateTime.Now:HH:mm:ss}] {message}";

            _lines.Enqueue(line);

            while (_lines.Count > maxLines)
                _lines.Dequeue();

            if (output != null)
            {
                output.text = string.Join("\n", _lines);

                RebuildLogLayout();
            }

            Debug.Log(line);
        }

        public void Clear()
        {
            _lines.Clear();

            if (output != null)
            {
                output.text = string.Empty;
                RebuildLogLayout();
            }
        }

        private void RebuildLogLayout()
        {
            if (scrollRect == null)
                return;

            Canvas.ForceUpdateCanvases();

            if (output != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)output.transform);

            if (scrollRect.content != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);

            if (_scrollCoroutine != null)
                StopCoroutine(_scrollCoroutine);

            _scrollCoroutine = StartCoroutine(ScrollToBottomNextFrame());
        }

        private IEnumerator ScrollToBottomNextFrame()
        {
            yield return null;

            Canvas.ForceUpdateCanvases();

            if (output != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)output.transform);

            if (scrollRect != null && scrollRect.content != null)
                LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);

            if (scrollRect != null)
                scrollRect.verticalNormalizedPosition = 0f;

            _scrollCoroutine = null;
        }
    }
}
