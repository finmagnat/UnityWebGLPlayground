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

        // Clipboard access in browsers generally requires a direct user gesture,
        // such as a button click, especially in WebGL builds.
        if (!navigator.clipboard || !navigator.clipboard.writeText) {
            console.error("[WebGLPlugin] Clipboard API is not available in this browser context.");
            return;
        }

        navigator.clipboard.writeText(text)
            .then(function () {
                console.log("[WebGLPlugin] Copied to clipboard: " + text);
            })
            .catch(function (error) {
                console.error("[WebGLPlugin] Clipboard copy failed:", error);
            });
    },

    WGL_SendCallbackToUnity: function () {
        // SendMessage routes a JavaScript event back to a named Unity GameObject
        // and calls a public method on one of its components.
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

        // Fullscreen API calls must also be triggered by a user gesture.
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

        // Browser downloads are created with a Blob and a temporary <a download>
        // link because WebGL builds cannot write arbitrary files to disk.
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
    },

    WGL_GetDevicePixelRatio: function () {
        const ratio = window.devicePixelRatio || 1;
        const value = ratio.toString();
        const byteCount = lengthBytesUTF8(value) + 1;
        const pointer = _malloc(byteCount);
        stringToUTF8(value, pointer, byteCount);
        return pointer;
    },

    WGL_GetUserAgent: function () {
        const value = navigator.userAgent || "Unavailable";
        const byteCount = lengthBytesUTF8(value) + 1;
        const pointer = _malloc(byteCount);
        stringToUTF8(value, pointer, byteCount);
        return pointer;
    },

    WGL_GetWebGLVersion: function () {
        const canvas = document.createElement("canvas");
        const gl = canvas.getContext("webgl2") || canvas.getContext("webgl") || canvas.getContext("experimental-webgl");

        if (!gl) {
            const unavailable = "Unavailable";
            const byteCount = lengthBytesUTF8(unavailable) + 1;
            const pointer = _malloc(byteCount);
            stringToUTF8(unavailable, pointer, byteCount);
            return pointer;
        }

        const value = gl.getParameter(gl.VERSION) || "Unknown";
        const byteCount = lengthBytesUTF8(value) + 1;
        const pointer = _malloc(byteCount);
        stringToUTF8(value, pointer, byteCount);
        return pointer;
    },

    WGL_GetGpuInfo: function () {
        const canvas = document.createElement("canvas");
        const gl = canvas.getContext("webgl2") || canvas.getContext("webgl") || canvas.getContext("experimental-webgl");

        if (!gl) {
            const unavailable = "Unavailable";
            const byteCount = lengthBytesUTF8(unavailable) + 1;
            const pointer = _malloc(byteCount);
            stringToUTF8(unavailable, pointer, byteCount);
            return pointer;
        }

        const debugInfo = gl.getExtension("WEBGL_debug_renderer_info");

        if (!debugInfo) {
            const unavailable = "Unavailable: WEBGL_debug_renderer_info is blocked or unsupported";
            const byteCount = lengthBytesUTF8(unavailable) + 1;
            const pointer = _malloc(byteCount);
            stringToUTF8(unavailable, pointer, byteCount);
            return pointer;
        }

        const vendor = gl.getParameter(debugInfo.UNMASKED_VENDOR_WEBGL) || "Unknown vendor";
        const renderer = gl.getParameter(debugInfo.UNMASKED_RENDERER_WEBGL) || "Unknown renderer";
        const value = vendor + " / " + renderer;
        const byteCount = lengthBytesUTF8(value) + 1;
        const pointer = _malloc(byteCount);
        stringToUTF8(value, pointer, byteCount);

        return pointer;
    },

    WGL_IsBrowserConnected: function () {
        return typeof window !== "undefined" && typeof document !== "undefined" ? 1 : 0;
    },

    WGL_IsClipboardApiAvailable: function () {
        return navigator.clipboard && navigator.clipboard.writeText ? 1 : 0;
    },

    WGL_IsFullscreenApiAvailable: function () {
        const canvas = document.querySelector("#unity-canvas");

        if (!canvas) {
            return 0;
        }

        return canvas.requestFullscreen || canvas.webkitRequestFullscreen ? 1 : 0;
    }
});
