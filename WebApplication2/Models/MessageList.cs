using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class MessageList
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public DateTime MsgTime { get; set; }
        public string MsgContent { get; set; }
    }
}
