# Build fix guide (Spaces + Unity Android)

This repo needed a few targeted fixes to get a successful Android Gradle build.

## 1) C# compile fix: ARSubsystems camera registration API mismatch

- **Symptom**
  - `CS1503: cannot convert from 'XRCameraSubsystemDescriptor.Cinfo' to 'XRCameraSubsystemCinfo'`
- **Cause**
  - The Spaces package used `XRCameraSubsystemDescriptor.Cinfo` but called the obsolete `XRCameraSubsystem.Register(...)` overload (older ARSubsystems type).
- **Fix**
  - **File**: `Packages/com.qualcomm.snapdragon.spaces/Runtime/Camera Access/CameraSubsystem.cs`
  - **Change**: register using the descriptor API:
    - `XRCameraSubsystemDescriptor.Register(cinfo);`

## 2) Android native build fix: OpenXR loader Prefab STL mismatch

- **Symptom**
  - Gradle task fails at CMake configuration:
    - `NoMatchingLibraryException: No compatible library found for //loader/openxr_loader`
    - `[CXX1211] Library is a shared library with a statically linked STL ... [//loader/openxr_loader]`
- **Cause**
  - Unity/AGP config uses `-DANDROID_STL=c++_shared`, but the Prefab metadata in `libopenxr_loader.aar` declared **`c++_static`**.
- **Fix**
  - **File**: `Packages/com.qualcomm.snapdragon.spaces/Runtime/Core/Android/libopenxr_loader.aar`
  - **Inside the AAR**: `prefab/modules/openxr_loader/libs/android.arm64-v8a/abi.json`
  - **Change**:
    - `"stl": "c++_static"` → `"stl": "c++_shared"`

Notes:
- This is an edit to a packaged AAR. If you update/replace the Spaces package, you may need to re-apply this patch.

## 3) Android resources fix: missing Material + Navigation attributes

- **Symptoms**
  - AAPT resource linking errors like:
    - `resource attr/colorOnPrimary not found`
    - `attribute navGraph/defaultNavHost/startDestination not found`
- **Cause**
  - Some AAR resources pulled in by Spaces expect Material + AndroidX Navigation dependencies, but they were only being added to the *launcher* module, not `unityLibrary` (which is built as an Android library and verified).
- **Fix**
  - **File**: `Packages/com.qualcomm.snapdragon.spaces/Editor/AndroidGradleProjectPostProcessor.cs`
  - **Change**: inject required dependencies into the exported `unityLibrary/build.gradle` when Spaces OpenXR runtime feature is enabled:
    - `com.google.android.material:material:1.3.0`
    - `androidx.navigation:navigation-fragment-ktx:2.4.2`
    - `androidx.navigation:navigation-ui-ktx:2.4.2`

## Verification

- After applying these changes, the exported Gradle project build (`assembleRelease`) succeeded.

## If it breaks again / Unity keeps using stale Gradle output

Unity regenerates `Library/Bee/.../Gradle` during builds. If you still see old errors after changing package sources:

- Delete `Library/Bee/` (or the full `Library/`), then rebuild so Unity regenerates and copies fresh artifacts.

