namespace twitter.api.domain.Constants
{
    public class Errors
    {
        public const string UserNameIsRequired = "User name is required";
        public const string UserEmailIsRequired = "User email is required";
        public const string UserNickNameMustBeBetween5And20CharsLength = "User NickName must be between 5 and 20 chars length";
        public const string CannotFollowYourself = "Cannot follow yourself";
        public const string PostDescriptionCannotBeNullOrWhiteSpace = "Post description cannot be null or white space";
        public const string PostDescriptionCannotBeMoreThan2000Chars = "Post description cannot be more than 2000 chars";
        public const string UserNotFound = "User not found";
        public const string FollowerUserNotFound = "Follower user not found";
        public const string UserToFollowNotFound = "User to follow not found";
        public const string AlreadyFollowingUser = "Already following uaser";
        public const string FollowRelationshipNotFound = "Follow relationship not found";
        public const string InvalidUserEmail = "Invalid user email";
        public const string UserEmailRepeated = "User email repeated";
        public const string UserNameRepeated = "User name repeated";
        public const string InvalidRefreshToken = "The provided token is invalid.";
        public const string FailedLogin = "It does not exist user with the given username and password.";
    }
}
