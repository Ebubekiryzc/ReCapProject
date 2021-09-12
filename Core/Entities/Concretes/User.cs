using Core.Entities.Abstracts;

namespace Core.Entities.Concretes
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
    }
}
