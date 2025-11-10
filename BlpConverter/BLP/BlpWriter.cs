using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using BlpConverter.Config;

namespace BlpConverter.BLP;

public class BlpWriter
{
    public static void SaveBlp(string sourcePath, string targetPath, ConversionSettings settings)
    {
        using var image = Image.Load<Rgba32>(sourcePath);
        SaveBlp(image, targetPath, settings);
    }

    public static void SaveBlp(Image<Rgba32> image, string targetPath, ConversionSettings settings)
    {
        using var fs = new FileStream(targetPath, FileMode.Create, FileAccess.Write);
        using var bw = new BinaryWriter(fs, Encoding.ASCII);

        // Write BLP2 header
        bw.Write(0x32504c42); // "BLP2" magic
        bw.Write((uint)1); // version

        // Determine encoding based on settings
        BlpColorEncoding encoding;
        BlpPixelFormat pixelFormat;

        switch (settings.BlpCompressionFormat)
        {
            case BlpCompressionFormat.Dxt1:
                encoding = BlpColorEncoding.Dxt;
                pixelFormat = BlpPixelFormat.Dxt1;
                break;
            case BlpCompressionFormat.Dxt3:
                encoding = BlpColorEncoding.Dxt;
                pixelFormat = BlpPixelFormat.Dxt3;
                break;
            case BlpCompressionFormat.Dxt5:
                encoding = BlpColorEncoding.Dxt;
                pixelFormat = BlpPixelFormat.Dxt5;
                break;
            default:
                encoding = BlpColorEncoding.Argb8888;
                pixelFormat = BlpPixelFormat.Argb8888;
                break;
        }

        bw.Write((byte)encoding);
        bw.Write((byte)(settings.PreserveAlpha ? 8 : 0)); // alpha depth
        bw.Write((byte)pixelFormat);
        bw.Write((byte)(settings.GenerateMipmaps ? 1 : 0)); // has mipmaps

        bw.Write(image.Width);
        bw.Write(image.Height);

        // Calculate mipmap levels
        int mipLevels = 1;
        if (settings.GenerateMipmaps)
        {
            int size = Math.Max(image.Width, image.Height);
            while (size > 1)
            {
                size /= 2;
                mipLevels++;
            }
        }

        // Reserve space for mipmap offsets and sizes
        long offsetsPosition = fs.Position;
        for (int i = 0; i < 16; i++)
            bw.Write((uint)0);
        for (int i = 0; i < 16; i++)
            bw.Write((uint)0);

        // Write mipmap data
        uint[] offsets = new uint[16];
        uint[] sizes = new uint[16];

        var currentImage = image.Clone();
        for (int i = 0; i < mipLevels; i++)
        {
            offsets[i] = (uint)fs.Position;

            byte[] pixelData = new byte[currentImage.Width * currentImage.Height * 4];
            currentImage.CopyPixelDataTo(pixelData);

            // Convert RGBA to BGRA for uncompressed format
            if (encoding == BlpColorEncoding.Argb8888)
            {
                ARGBColor8.ConvertToBGRA(pixelData);
                bw.Write(pixelData);
                sizes[i] = (uint)pixelData.Length;
            }
            else
            {
                // For DXT compression, we'll write uncompressed for now
                // A full implementation would require DXT compression
                ARGBColor8.ConvertToBGRA(pixelData);
                bw.Write(pixelData);
                sizes[i] = (uint)pixelData.Length;
            }

            // Generate next mipmap level
            if (i < mipLevels - 1)
            {
                var nextWidth = Math.Max(1, currentImage.Width / 2);
                var nextHeight = Math.Max(1, currentImage.Height / 2);

                var nextImage = currentImage.Clone(ctx =>
                    ctx.Resize(nextWidth, nextHeight));

                currentImage.Dispose();
                currentImage = nextImage;
            }
        }
        currentImage.Dispose();

        // Write offsets and sizes
        fs.Position = offsetsPosition;
        for (int i = 0; i < 16; i++)
            bw.Write(offsets[i]);
        for (int i = 0; i < 16; i++)
            bw.Write(sizes[i]);
    }
}
