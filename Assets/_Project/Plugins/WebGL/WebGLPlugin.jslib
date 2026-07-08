/*
 * WebGLPlugin.jslib
 *
 * NOTE:
 * Rider is configured to treat *.jslib as JavaScript.
 * This enables syntax highlighting, formatting and error inspection.
 */
mergeInto(LibraryManager.library, {
    WGL_ShowAlert: function () {
        alert("Hello from JavaScript!");
    },

    WGL_CopyToClipboard: function (textPtr) {
        const text = UTF8ToString(textPtr);

        navigator.clipboard.writeText(text)
            .then(function () {
                console.log("[WebGLPlugin] Copied to clipboard: " + text);
            })
            .catch(function (error) {
                console.error("[WebGLPlugin] Clipboard copy failed:", error);
            });
    },

    WGL_SendCallbackToUnity: function () {
        SendMessage(
            "WebGLPlaygroundController",
            "OnJavaScriptCallback",
            "Hello from JavaScript callback!"
        );
    },

    WGL_ToggleFullscreen: function () {
        if (document.fullscreenElement) {
            document.exitFullscreen();
            return;
        }

        const canvas = document.querySelector("#unity-canvas");

        if (!canvas) {
            console.error("[WebGLPlugin] Unity canvas not found.");
            return;
        }

        if (canvas.requestFullscreen) {
            canvas.requestFullscreen();
        } else if (canvas.webkitRequestFullscreen) {
            canvas.webkitRequestFullscreen();
        } else {
            console.error("[WebGLPlugin] Fullscreen API is not supported.");
        }
    },

    WGL_DownloadTextFile: function (fileNamePtr, contentPtr) {
        const fileName = UTF8ToString(fileNamePtr);
        const content = UTF8ToString(contentPtr);

        const blob = new Blob([content], { type: "text/plain;charset=utf-8" });
        const url = URL.createObjectURL(blob);

        const link = document.createElement("a");
        link.href = url;
        link.download = fileName;
        link.style.display = "none";

        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);

        URL.revokeObjectURL(url);

        console.log("[WebGLPlugin] Download requested: " + fileName);
    }
});