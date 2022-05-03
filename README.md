# CameraControl
Simple camera library with .NET5.0.\
You can select camera device, capture video, change resolution and camera properties.\
The video output data is in byte array, so you can manipulate raw data directly.\
Currently, DirectShow is used to capture video and retrieve properties, but you can add your own implementation.\
The sample is provided as WPF application. WriteableBitmap is used to draw video data every frame.
