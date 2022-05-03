# CameraControl
Simple camera library with .NET5.0.

You can select camera device, capture video, change resolution and camera properties.\
The video output data is in raw byte array, so you can manipulate this data directly right after frame changed event.

The sample is provided as WPF application. WriteableBitmap is used to draw video data every frame.

Currently, DirectShow is used to capture video and retrieve properties, but you can add your own implementation.\
There is no external dependencies. Only necessary files from [DirectShowNet](https://sourceforge.net/projects/directshownet/files/) are included.

Tested with Logitech C100 web camera.


## Useful links
[SeeShark](https://github.com/vignetteapp/SeeShark) have the same idea of raw byte output, but use ffmpeg library for video decoding. It's also have many decoding options and cross-platform.

[Avalonia.WebCam](https://github.com/AvaloniaUI/Avalonia.WebCam) is a same DirectShow solution, but received frames are converted to Bitmaps.
