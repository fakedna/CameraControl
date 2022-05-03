using System;
using System.Threading;

namespace VideoSource
{
    /// <remarks>
    /// based on https://github.com/vignetteapp/SeeShark/blob/3818f7ac74d6000eacf952dd91a63508ad0cc537/SeeShark/Disposable.cs
    /// </remarks>
    public abstract class Disposable : IDisposable
    {
        private volatile int _disposeSignaled = 0;
        public bool IsDisposed { get; protected set; }
        protected bool IsOwner { get; private set; }

        protected Disposable(bool isOwner = true)
        {
            IsDisposed = false;
            IsOwner = isOwner;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Interlocked.Exchange(ref _disposeSignaled, 1) != 0) return;
            IsDisposed = true;
            if (disposing) DisposeManaged();
            DisposeUnmanaged();
        }

        ~Disposable() => Dispose(false);

        protected virtual void DisposeManaged() { }
        protected virtual void DisposeUnmanaged() { }

        public void TransferOwnership() => IsOwner = false;

        public void ThrowIfDisposed()
        {
            if (IsDisposed) throw new ObjectDisposedException(GetType().FullName);
        }
    }
}