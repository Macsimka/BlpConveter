using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Reflection;
using System.Runtime.InteropServices;
using static BlpConverter.BLP.RustBlpConverter;

namespace BlpConverter.BLP;

public static class RustBlpConverter
{
    private const string DLL_NAME = "rust_blp_converter.dll";
    private static readonly Lazy<IntPtr> NativeHandle = new(EnsureNativeLibraryLoaded);

    public const int SUCCESS = 0;
    public const int ERROR_NULL_POINTER = -1;
    public const int ERROR_INVALID_PATH = -2;
    public const int ERROR_IO_ERROR = -3;
    public const int ERROR_IMAGE_ERROR = -4;
    public const int ERROR_UNKNOWN = -99;

    public enum ImageFormat
    {
        PNG = 0,
        JPEG = 1
    }

    public enum BlpVersion : uint
    {
        BLP0,
        BLP1,
        BLP2
    }

    static RustBlpConverter()
    {
        NativeLibrary.SetDllImportResolver(typeof(RustBlpConverter).Assembly, ResolveLibrary);
        _ = NativeHandle.Value; // force extraction early
    }

    private static IntPtr ResolveLibrary(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (!libraryName.Contains("rust_blp_converter", StringComparison.OrdinalIgnoreCase))
            return IntPtr.Zero;

        try
        {
            return NativeHandle.Value;
        }
        catch
        {
            return IntPtr.Zero;
        }
    }

    private static IntPtr EnsureNativeLibraryLoaded()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            throw new PlatformNotSupportedException("Embedded rust_blp_converter is only available for Windows.");

        string resourceName = "rust_blp_converter.dll";
        var assembly = typeof(RustBlpConverter).Assembly;
        using var stream = assembly.GetManifestResourceStream(resourceName) ??
                           assembly.GetManifestResourceStream($"BlpConverter.{resourceName}") ??
                           throw new DllNotFoundException("Embedded rust_blp_converter.dll resource not found.");

        string extractDir = Path.Combine(Path.GetTempPath(), "BlpConverter");
        Directory.CreateDirectory(extractDir);
        string extractPath = Path.Combine(extractDir, resourceName);

        // Always overwrite to ensure consistent version
        using (var fs = File.Open(extractPath, FileMode.Create, FileAccess.Write, FileShare.Read))
            stream.CopyTo(fs);

        return NativeLibrary.Load(extractPath);
    }

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern int blp_to_image(
        [MarshalAs(UnmanagedType.LPStr)] string blpPath,
        [MarshalAs(UnmanagedType.LPStr)] string outputPath,
        int format
    );

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern int image_to_blp(
        [MarshalAs(UnmanagedType.LPStr)] string imagePath,
        [MarshalAs(UnmanagedType.LPStr)] string blpPath,
        int compression,
        int mipmapCount
    );

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern int blp_get_info(
        [MarshalAs(UnmanagedType.LPStr)] string blpPath,
        out uint width,
        out uint height,
        out uint mipmapCount
    );

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    public static extern int blp_get_info_extended(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string blpPath,
        ref BlpInfo info
    );

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr get_error_message(int errorCode);

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void free_string(IntPtr ptr);

    public static void BlpToImage(string blpPath, string outputPath, ImageFormat format = ImageFormat.PNG)
    {
        int result = blp_to_image(blpPath, outputPath, (int)format);
        if (result != SUCCESS)
            throw new Exception($"Failed to convert BLP to image: {GetErrorMessage(result)} (code: {result})");
    }

    public static void ImageToBlp(string imagePath, string blpPath,
        BlpCompression compression = BlpCompression.DXT1, int mipmapCount = 0)
    {
        int result = image_to_blp(imagePath, blpPath, (int)compression, mipmapCount);
        if (result != SUCCESS)
            throw new Exception($"Failed to convert image to BLP: {GetErrorMessage(result)} (code: {result})");
    }

    public static (uint width, uint height, uint mipmaps) GetBlpInfo(string blpPath)
    {
        int result = blp_get_info(blpPath, out uint width, out uint height, out uint mipmaps);
        if (result != SUCCESS)
            throw new Exception($"Failed to get BLP info: {GetErrorMessage(result)} (code: {result})");

        return (width, height, mipmaps);
    }

    public static string GetErrorMessage(int errorCode)
    {
        IntPtr ptr = get_error_message(errorCode);
        if (ptr == IntPtr.Zero)
        {
            return "Unknown error";
        }

        try
        {
            return Marshal.PtrToStringAnsi(ptr) ?? "Unknown error";
        }
        finally
        {
            free_string(ptr);
        }
    }

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr blp_get_pixels(
        [MarshalAs(UnmanagedType.LPStr)] string blpPath,
        int mipmapLevel,
        out uint width,
        out uint height,
        out UIntPtr dataLen
    );

    [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
    private static extern void free_pixel_data(IntPtr ptr, UIntPtr len);

    public static Image<Rgba32> LoadBlpImage(string blpPath, int mipmapLevel = 0)
    {
        IntPtr pixelPtr = blp_get_pixels(blpPath, mipmapLevel, out uint width, out uint height, out UIntPtr dataLen);

        if (pixelPtr == IntPtr.Zero)
            throw new Exception($"Failed to load BLP file: {blpPath}");

        try
        {
            int len = (int)dataLen;

            byte[] pixelData = new byte[len];
            Marshal.Copy(pixelPtr, pixelData, 0, len);

            var image = Image.LoadPixelData<Rgba32>(pixelData, (int)width, (int)height);
            return image;
        }
        finally
        {
            free_pixel_data(pixelPtr, dataLen);
        }
    }

    public static Image LoadBlp(string blpPath, int mipmapLevel = 0)
    {
        return LoadBlpImage(blpPath, mipmapLevel);
    }
}

public enum BlpCompression
{
    Uncompressed = 0,
    DXT1 = 1,
    DXT3 = 2,
    DXT5 = 3
}

public enum Compression
{
    Jpeg,
    Raw1,
    Raw3,
    Dxtc,
}

[StructLayout(LayoutKind.Sequential)]
public struct BlpInfo
{
    public uint Width;
    public uint Height;
    public uint MipmapCount;
    public BlpVersion Version;
    public uint Content;
    public Compression Compression;
    public uint AlphaBits;
    public uint AlphaType;
    public uint HasMipmaps;
}
