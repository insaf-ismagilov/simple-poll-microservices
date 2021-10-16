namespace SimplePoll.Common.RabbitMq
{
    public static class RoutingKeys
    {
        public const string Request = ".request";
        public const string Response = ".response";
        public const string Deadletter = ".deadletter";
    }
}