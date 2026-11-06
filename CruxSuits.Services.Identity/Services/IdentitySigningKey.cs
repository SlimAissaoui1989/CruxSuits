using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace CruxSuits.Services.Identity.Services
{
    public sealed class IdentitySigningKey : IDisposable
    {
        private readonly RSA _rsa;

        public IdentitySigningKey(IConfiguration configuration, IHostEnvironment environment)
        {
            Issuer = configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("Configuration value 'Jwt:Issuer' is required.");
            Audience = configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("Configuration value 'Jwt:Audience' is required.");

            if (!Uri.TryCreate(Issuer, UriKind.Absolute, out var issuerUri))
            {
                throw new InvalidOperationException("Configuration value 'Jwt:Issuer' must be an absolute URL.");
            }

            if (!environment.IsDevelopment() && issuerUri.Scheme != Uri.UriSchemeHttps)
            {
                throw new InvalidOperationException("The JWT issuer must use HTTPS outside Development.");
            }

            var privateKeyPem = configuration["Jwt:PrivateKeyPem"];
            var privateKeyPath = configuration["Jwt:PrivateKeyPath"];

            if (string.IsNullOrWhiteSpace(privateKeyPem) && !string.IsNullOrWhiteSpace(privateKeyPath))
            {
                privateKeyPem = File.ReadAllText(privateKeyPath);
            }

            if (string.IsNullOrWhiteSpace(privateKeyPem))
            {
                if (!environment.IsDevelopment())
                {
                    throw new InvalidOperationException(
                        "Configure 'Jwt:PrivateKeyPem' or 'Jwt:PrivateKeyPath' with a persistent RSA private key.");
                }

                privateKeyPem = LoadOrCreateDevelopmentKey(environment.ContentRootPath);
            }

            _rsa = RSA.Create();
            _rsa.ImportFromPem(privateKeyPem);

            var keyId = Base64UrlEncoder.Encode(SHA256.HashData(_rsa.ExportSubjectPublicKeyInfo()))[..22];
            SigningKey = new RsaSecurityKey(_rsa) { KeyId = keyId };
        }

        public string Issuer { get; }

        public string Audience { get; }

        public RsaSecurityKey SigningKey { get; }

        public object CreateDiscoveryDocument()
        {
            return new
            {
                issuer = Issuer,
                jwks_uri = $"{Issuer.TrimEnd('/')}/.well-known/jwks.json",
            };
        }

        public object CreateJsonWebKeySet()
        {
            var parameters = _rsa.ExportParameters(includePrivateParameters: false);

            return new
            {
                keys = new[]
                {
                    new
                    {
                        kty = "RSA",
                        use = "sig",
                        kid = SigningKey.KeyId,
                        alg = SecurityAlgorithms.RsaSha256,
                        n = Base64UrlEncoder.Encode(parameters.Modulus!),
                        e = Base64UrlEncoder.Encode(parameters.Exponent!),
                    },
                },
            };
        }

        public void Dispose() => _rsa.Dispose();

        private static string LoadOrCreateDevelopmentKey(string contentRootPath)
        {
            var keyDirectory = Path.Combine(contentRootPath, ".keys");
            var keyPath = Path.Combine(keyDirectory, "jwt-signing-key.pem");
            Directory.CreateDirectory(keyDirectory);

            if (!File.Exists(keyPath))
            {
                using var newRsa = RSA.Create(2048);
                try
                {
                    using var keyFile = new FileStream(keyPath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                    using var writer = new StreamWriter(keyFile);
                    writer.Write(newRsa.ExportPkcs8PrivateKeyPem());
                }
                catch (IOException) when (File.Exists(keyPath))
                {
                    // Another local Identity process created the development key first.
                }
            }

            return File.ReadAllText(keyPath);
        }
    }
}
