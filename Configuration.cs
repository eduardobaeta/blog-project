namespace Blog
{
    public static class Configuration
    {
        public static string JwtKey { get; set; } = "FSOdtkDbj024vgSAci4m4A==";
        public static string ApiKeyName { get; set; } = "api_key";
        public static string ApiKey { get; set; } = "curso_api_FSOdtkDbj024vgSAci4m4A==";
        public static SmtpConfiguration Smtp = new();

        public class SmtpConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; } = 25;
            public string UserName { get; set; }
            public string Password { get; set; }
        }

    }

}