namespace BackgroundAgent.Processing.Services
{
    public interface IAsymmetricalCryptoService
    {
        byte[] Encrypt(byte[] content);

        byte[] Decrypt(byte[] content);
    }
}