# QWR Snapdragon Spaces Demo (Unity)

Unity project set up to build and run on **Android** devices supported by **Snapdragon Spaces** (OpenXR), with optional **QCHT hand-tracking interactions**.

## What’s in this repo

- **Unity project**: open the project root in Unity.
- **Local SDK packages (checked in)**:
  - `Packages/com.qualcomm.snapdragon.spaces` (**Snapdragon Spaces** package, version **0.23.0**)
  - `Packages/com.qualcomm.qcht.unity.interactions` (**QCHT Unity Interactions**, version **4.1.7**)
- **SDK drop folder**: `SDK_for_Unity/`  
  This folder currently only contains `SDK_for_Unity/README.txt` (no tarballs). Official guidance is on Qualcomm’s docs site.

## Requirements

- **Unity**: `6000.0.54f1` (see `ProjectSettings/ProjectVersion.txt`)
- **Unity modules** (via Unity Hub):
  - Android Build Support
  - Android SDK & NDK Tools
  - OpenJDK
- **Device**: a Snapdragon Spaces–supported Android XR device with the required Spaces Services/runtime installed.

Project Android settings (current):
- **Min SDK**: 29 (see `ProjectSettings/ProjectSettings.asset`)

## Open the project

1. Launch Unity Hub.
2. Add this folder as a project: `QWR Snapdragon Spaces Demo/`
3. Open in Unity.

## How the SDK is wired

This project references the Qualcomm packages via **local file dependencies** in `Packages/manifest.json`:

- `com.qualcomm.snapdragon.spaces`: `file:com.qualcomm.snapdragon.spaces` → `Packages/com.qualcomm.snapdragon.spaces/`
- `com.qualcomm.qcht.unity.interactions`: `file:com.qualcomm.qcht.unity.interactions` → `Packages/com.qualcomm.qcht.unity.interactions/`

If you want to switch to the **official UPM tarball install flow**, follow `SDK_for_Unity/README.txt` and Qualcomm docs, then update `Packages/manifest.json` accordingly.

## Running samples (recommended starting point)

Both Qualcomm packages include Unity samples. In Unity:

1. Open **Window → Package Manager**
2. Select **Snapdragon Spaces** (`com.qualcomm.snapdragon.spaces`)
3. In **Samples**, import:
   - **Core Samples** (includes a main menu scene)
   - (Optional) **Fusion Samples**
4. (Optional) Select **QCHT Unity Interactions** (`com.qualcomm.qcht.unity.interactions`) and import **QCHT Samples**

You can also explore the project scenes under `Assets/Scenes/`.

## Build & deploy (Android)

1. Switch platform: **File → Build Settings… → Android → Switch Platform**
2. Ensure **OpenXR** is the active XR runtime for Android (via XR Plug-in Management / OpenXR settings).
3. Connect the device (USB) and verify it appears under **Run Device**.
4. **Build And Run**

Notes:
- Snapdragon Spaces builds are typically **ARM64** and **IL2CPP** for device deployment.
- If you change SDK/package files and Unity seems to use stale Gradle output, clearing `Library/` often helps (see troubleshooting below).

## Troubleshooting / known build fixes

See `Common Errors.md` for fixes that were applied/needed for Android builds, including:

- ARSubsystems camera registration API mismatch
- OpenXR loader prefab STL mismatch inside `libopenxr_loader.aar`
- Missing Android dependencies (Material / AndroidX Navigation) injected into `unityLibrary/build.gradle`

If you update/replace the Spaces package, you may need to re-apply those patches.

## Links

- Snapdragon Spaces docs: `https://docs.spaces.qualcomm.com/`

