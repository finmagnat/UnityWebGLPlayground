# Unity WebGL Browser Integration Lab

A compact Unity 6 WebGL portfolio project that demonstrates practical browser integration from a Unity scene. The lab focuses on simple, reliable examples that are easy to explain, test, and extend.

## Demo Preview

_Screenshot/GIF will be added after the UI polish pass._

## Why This Project Exists

This repository demonstrates practical browser integration techniques for Unity WebGL: C# to JavaScript calls, JavaScript callbacks into Unity, browser capability checks, WebGL diagnostics, persistent browser-backed storage, fullscreen, clipboard, and file download workflows.

## Features

- Open an external URL from Unity.
- Copy text to the browser clipboard through a WebGL `.jslib` bridge.
- Toggle fullscreen through the browser Fullscreen API.
- Save and load data with `PlayerPrefs`.
- Call JavaScript alerts from C#.
- Send JavaScript callbacks back into Unity with `SendMessage`.
- Display Unity, browser, screen, WebGL, and GPU diagnostics.
- Download a generated text file through the Browser API.

## Technical Highlights

- Unity C# to JavaScript interop through `DllImport("__Internal")`.
- WebGL plugin located at `Assets/Plugins/WebGL/WebGLPlugin.jslib`.
- JavaScript strings returned to C# with the `lengthBytesUTF8`, `stringToUTF8`, and `_malloc` pattern.
- Small demo modules under `Assets/_Project/Scripts`.
- No external packages or asynchronous infrastructure beyond browser APIs already required by the platform.

## WebGL Concepts Demonstrated

- Browser sandbox constraints for Unity WebGL builds.
- Calling browser APIs from Unity.
- User gesture requirements for clipboard and fullscreen actions.
- Blob-based file downloads with a temporary `<a download>` element.
- JavaScript-to-Unity callbacks through `SendMessage`.
- `PlayerPrefs` persistence in WebGL through browser storage.
- Browser diagnostics through JavaScript and WebGL context queries.

## How To Build And Run

1. Open the project in Unity 6.
2. Open `Assets/_Project/Scenes/WebGLPlayground.unity`.
3. Switch platform to WebGL in Build Settings.
4. Build the project.
5. Run the build from a local web server, not directly from `file://`.

Unity's Build And Run option is usually the fastest way to test locally. For an existing build folder, use any simple static server from the build output directory.

## Compression Notes

Unity WebGL builds often use Brotli (`.br`) or Gzip (`.gz`) compression. A local or production server must send the correct `Content-Encoding` headers for compressed files:

- `.br` files need `Content-Encoding: br`.
- `.gz` files need `Content-Encoding: gzip`.
- WebAssembly files should use `Content-Type: application/wasm`.

If those headers are missing, the browser may fail to load the build even when the files are present.

## Current Status And Roadmap

Current status:

- Browser integration demo is functional.
- WebGL bridge covers alerts, clipboard, fullscreen, callback, download, and diagnostics.
- Documentation is ready for portfolio review.

Roadmap:

- Add a more polished UI layout for each demo module.
- Add visible success/failure states for browser API calls.
- Add a deployed demo page with correct compression headers.
- Add screenshots or a short video walkthrough.
