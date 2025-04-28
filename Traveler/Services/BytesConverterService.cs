using Traveler.Interfaces;

namespace Traveler.Services
{
    public abstract class BytesConverter<T> : IBytesConverterService<T>
    {
        public abstract byte[] ConvertToBytes(T file);
    }

    public class PhotoConverterToBytes : BytesConverter<IFormFile>
    {
        public override byte[] ConvertToBytes(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
