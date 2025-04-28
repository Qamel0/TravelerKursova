namespace Traveler.Interfaces
{
    public interface IBytesConverterService<T>
    {
        byte[] ConvertToBytes(T file);
    }
}
