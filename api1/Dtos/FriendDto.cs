namespace api1.Dtos
{
    public class FriendDto
    {
        public int? RequestId { get; set; }
        public int FriendId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string? LastMessage { get; set; }
        public DateTime? LastMessageTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

