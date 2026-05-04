namespace CruxSuits.BackAPI
{
    public static class ConstsValues
    {
        public const string AuthTokenCookieTag = "authRefreshToken";

        public static TimeSpan AccessTokenExpiredTime => TimeSpan.FromMinutes(5);
        public static TimeSpan RefreshTokenExpiredTime => TimeSpan.FromDays(7);

    }
}
