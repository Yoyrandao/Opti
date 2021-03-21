namespace BackgroundAgent.Processing.Services
{
    public interface ISymmentricalCryptoService
    {
        byte[] Encrypt(byte[] key, byte[] iv, byte[] content);

        byte[] Decrypt(byte[] key, byte[] iv, byte[] content);
    }
}