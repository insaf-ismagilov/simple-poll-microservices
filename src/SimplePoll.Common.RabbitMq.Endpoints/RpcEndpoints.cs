﻿namespace SimplePoll.Common.RabbitMq.Endpoints
{
    public static class RpcEndpoints
    {
        public static class PollGetById
        {
            public const string Exchange = "rpc_poll_getbyid_exchange";
            public const string RequestQueue = "rpc_poll_getbyid_request_queue";
            public const string ResponseQueue = "rpc_poll_getbyid_response_queue";
        }
    }
}