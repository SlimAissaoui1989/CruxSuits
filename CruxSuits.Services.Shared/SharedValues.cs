namespace CruxSuits.Services.Shared
{
    public static class SharedValues
    {
        public static readonly Guid SuperAdminUserId = Guid.Parse("00000001-0001-0001-0001-000000000001");

        public static readonly string SuperAdminUserName = "slim.aissaoui";

        public static readonly string SuperAdminFirstName = "Slim";

        public static readonly string SuperAdminLastName = "Aissaoui";

        public static readonly string SuperAdminFullName = "Slim Aissaoui";

        public static readonly string SuperAdminPhone = "58 890 780";

        public static readonly string SuperAdminEmail = "slim.aissaoui89@gmail.com";

        public static readonly DateTime DefaultCreatedDate = new DateTime(2000,01,01);

        public const string AuthTokenCookieTag = "authRefreshToken";
    }
}
