namespace GroupLearning.Extensions;

public static class FileExtensions
{
  public static byte[] ConvertIFoamFileToByteArray(this IFormFile fileRequest)
  {
    byte[] fileBytes = [];

    if (fileRequest != null)
    {
      using (var memoryStream = new MemoryStream())
      {
        fileRequest.CopyTo(memoryStream);
        fileBytes = memoryStream.ToArray();
      }
    }

    return fileBytes;
  }
}
