using Common.Domains.Entities;
using Dapper.Contrib.Extensions;
using Domain.Enums;

namespace DotNetTraining.Domains.Entities
{
    [Table("Users")]
    public class User:SystemLogEntity<Guid>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Roles { get; set; } = "GUEST";
        public UserStatus Status { get; set; } = UserStatus.Active;
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string FullName => $"{FirstName} {LastName}";
        public DateTime? LastLoggedIn { get; set; }
    }
}
