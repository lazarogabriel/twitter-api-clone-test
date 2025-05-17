using System;

namespace twitter.api.web.Models.Responses
{
    public class CreateFollowerResponse
    {
        public Guid Id { get; }

        public Guid FollowerId { get; }

        public Guid FollowedId { get; }

        public DateTime FollowedAt { get; }
    }
}
