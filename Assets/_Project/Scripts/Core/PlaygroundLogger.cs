using TMPro;
using UnityEngine;

namespace WebGLPlayground.Core
{
    public sealed class PlaygroundLogger : MonoBehaviour
    {
        [SerializeField] private TMP_Text output;
        [SerializeField, Min(5)] private int maxLines = 12;

        private readonly System.Collections.Generic.Queue<string> _lines = new();

        public void Log(string message)
        {
            string line = $"[{System.DateTime.Now:HH:mm:ss}] {message}";

            _lines.Enqueue(line);

            while (_lines.Count > maxLines)
                _lines.Dequeue();

            if (output != null)
                output.text = string.Join("\n", _lines);

            Debug.Log(line);
        }

        public void Clear()
        {
            _lines.Clear();

            if (output != null)
                output.text = string.Empty;
        }
    }
}