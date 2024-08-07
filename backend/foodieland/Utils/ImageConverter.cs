namespace foodieland.Utils;

public static class ImageConverter
{
    public static string? ConvertByteArrayToBase64String(byte[]? imageData)
    {
        return imageData != null ? Convert.ToBase64String(imageData) : null;
    }
    
    public static byte[]? ConvertImageToByteArray(IFormFile? image)
    {
        if (image == null || image.Length == 0) return null;

        using var memoryStream = new MemoryStream();
        image.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}