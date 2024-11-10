// QRMenu.Core/Interfaces/IEncryptionService.cs
namespace QRMenu.Core.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string text);
        string Decrypt(string encryptedText);
    }
}