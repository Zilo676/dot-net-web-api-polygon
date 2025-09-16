using System.Runtime.InteropServices;

namespace BasicsFetures;

public class FileProcessor: IDisposable
{
    private FileStream _fileStream;
    private IntPtr _unmanagedHandle; // например, HBITMAP
    private bool _disposed = false;

    public FileProcessor(string filePath)
    {
        _fileStream = new FileStream(filePath, FileMode.Open);
        _unmanagedHandle = AllocateUnmanagedMemory();
    }
    // From Idisposable
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed) return;

        if (disposing)
        {
            // освобождаем управляемые ресурсы
            _fileStream.Dispose();
        }
        // освобождаем неуправляемые ресурсы
        if (_unmanagedHandle != IntPtr.Zero)
        {
            FreeUnmanagedMemory(_unmanagedHandle);
            _unmanagedHandle = IntPtr.Zero;
        }
        
        _disposed = true;
    }

    ~FileProcessor()
    {
        Dispose(false);
    }
    
    // Методы для неуправляемой памяти (пример)
    private IntPtr AllocateUnmanagedMemory() => Marshal.AllocHGlobal(1024);
    private void FreeUnmanagedMemory(IntPtr ptr) => Marshal.FreeHGlobal(ptr);
}