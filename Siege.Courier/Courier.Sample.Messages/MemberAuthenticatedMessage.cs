﻿using Siege.Courier.Messages;

namespace Courier.Sample.Messages
{
    public class MemberAuthenticatedMessage : IMessage
    {
        public string UserName { get; set; }
        public bool RememberMe { get; set; }
    }
}