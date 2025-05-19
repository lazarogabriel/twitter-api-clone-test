using System;

namespace twitter.api.web.Models.Requests
{
    public class RefreshRequest
    {
        public Guid RefreshToken { get; set; }
    }
}
