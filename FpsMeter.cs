using System;
using System.Linq;

namespace CameraControl
{
    public sealed class FpsMeter
    {
        private readonly int _frameCount;
        private double[] _frameTimes;
        private int _currentFrame;
        private double _lastTime;

        public FpsMeter(int frameCount = 30)
        {
            _frameCount = frameCount;
            Stop();
        }

        public void Update(double lastFrameTime)
        {
            if (_currentFrame >= _frameCount) _currentFrame = 0;
            _frameTimes[_currentFrame] = lastFrameTime - _lastTime;
            _lastTime = lastFrameTime;
        }

        public void Stop()
        {
            _lastTime = 0;
            _currentFrame = 0;
            _frameTimes = new double[_frameCount];
        }

        public double GetFrameRate() => _lastTime != 0 ? Math.Round(1 / _frameTimes.Sum()) : 0;
    }
}