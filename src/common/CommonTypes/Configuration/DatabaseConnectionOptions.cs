﻿namespace CommonTypes.Configuration
{
    public record DatabaseConnectionOptions
    {
        public string Host { get; init; }

        public int Port { get; init; }

        public string Login { get; init; }

        public string Password { get; init; }

        public string Database { get; init; }
    }
}
