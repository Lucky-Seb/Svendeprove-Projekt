using OtpNet;
using System;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class TotpService : ITotpService
    {
        // Constructor
        public TotpService() { }

        // Generate TOTP based on a secret key
        public string GenerateTotp(string secretKey)
        {
            var totp = new Totp(Base32Encoding.ToBytes(secretKey));  // Convert secretKey to bytes
            return totp.ComputeTotp();  // Generate TOTP code
        }

        // Validate TOTP by comparing it to the expected token
        public bool ValidateTotp(string secretKey, string token)
        {
            var totp = new Totp(Base32Encoding.ToBytes(secretKey));  // Convert secretKey to bytes
            return totp.VerifyTotp(token, out _);  // Validate TOTP code (returns true if valid)
        }

        // Generate a QR code URL for TOTP setup (using Google Chart API)
        public string GenerateQrCodeUrl(string secretKey, string accountName)
        {
            var issuer = "MyApp"; // You can replace this with your app name
            var totp = new Totp(Base32Encoding.ToBytes(secretKey));

            // Construct the URL for the Google Chart API (QR Code generation)
            var encodedAccountName = Uri.EscapeDataString(accountName);
            var encodedIssuer = Uri.EscapeDataString(issuer);
            var url = $"https://chart.googleapis.com/chart?chs=200x200&cht=qr&chl=otpauth://totp/{encodedIssuer}:{encodedAccountName}?secret={secretKey}&issuer={encodedIssuer}";

            return url; // Return the URL for generating the QR code
        }

        // Generate a new TOTP secret
        public string GenerateSecret()
        {
            var key = KeyGeneration.GenerateRandomKey(20);  // Generates a new random key
            return Base32Encoding.ToString(key);  // Return secret as a Base32 encoded string
        }
    }
}
