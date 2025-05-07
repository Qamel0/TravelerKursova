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
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file), "File cannot be null.");
            }

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
