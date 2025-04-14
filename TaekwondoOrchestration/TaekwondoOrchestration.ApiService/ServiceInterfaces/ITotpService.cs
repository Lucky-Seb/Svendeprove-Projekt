namespace TaekwondoOrchestration.ApiService.ServiceInterfaces
{
    public interface ITotpService
    {
        // Method to generate the TOTP from a secret key
        string GenerateTotp(string secretKey);

        // Method to validate the TOTP token
        bool ValidateTotp(string secretKey, string token);

        // Method to generate a QR code URL for TOTP setup
        string GenerateQrCodeUrl(string secretKey, string accountName);

        // Method to generate a new TOTP secret key
        string GenerateSecret();
    }
}
