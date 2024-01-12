using Microsoft.AspNetCore.Identity;

namespace ReserveApp.Data
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string name { get; set; }
        public string passportNo { get; set; }
    }
}
