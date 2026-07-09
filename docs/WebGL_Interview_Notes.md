# WebGL Interview Notes

## WebGL Sandbox

Unity WebGL runs inside the browser sandbox. It cannot freely access the local file system, native OS APIs, raw sockets, or arbitrary background processes. Browser permissions and user gestures matter.

## WebAssembly

Unity compiles game code to WebAssembly for WebGL builds. WebAssembly gives near-native execution speed in the browser, but it still runs under browser security and platform limits.

## Browser APIs

Unity WebGL can call JavaScript through `.jslib` plugins and `DllImport("__Internal")`. This is the usual path for clipboard, fullscreen, downloads, browser info, analytics integrations, and page-level interactions.

## File System Limitations

WebGL builds cannot write directly to arbitrary user folders. Downloads usually use a `Blob` plus a temporary `<a download>` link. Persistent app data should use browser-backed storage.

## Threads Limitations

Thread support in WebGL depends on browser support, SharedArrayBuffer, and the required cross-origin isolation headers. Many WebGL projects avoid threads unless they control deployment headers.

## TCP/UDP vs WebSocket

Browsers do not expose raw TCP or UDP sockets to WebGL content. Real-time networking usually uses WebSocket, WebRTC, or HTTP-based APIs instead of native socket APIs.

## PlayerPrefs / IndexedDB

Unity WebGL stores `PlayerPrefs` in browser storage, commonly backed by IndexedDB. Data is origin-specific and can be cleared by the user or browser privacy settings.

## Build Size Optimization

Common approaches include managed code stripping, removing unused assets, compressing textures/audio, avoiding unnecessary packages, splitting large content, and checking build reports for heavy assets.

## Compression Headers

Brotli and Gzip builds require correct server headers. `.br` needs `Content-Encoding: br`, `.gz` needs `Content-Encoding: gzip`, and `.wasm` should be served as `application/wasm`.
