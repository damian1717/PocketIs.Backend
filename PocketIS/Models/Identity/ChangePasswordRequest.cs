namespace PocketIS.Models.Identity
{
    public class ChangePasswordRequest
    {
        public Guid UserId { get; }
        public string CurrentPassword { get; }
        public string NewPassword { get; }
    }
}
