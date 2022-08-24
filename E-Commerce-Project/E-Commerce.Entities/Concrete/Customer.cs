using E_Commerce.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class Customer:EntityBase<int>,IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string? IpAddress { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime LastLogin { get; set; }
        public int ShoppingCartID { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int CustomerPictureID { get; set; }
        public CustomerPicture CustomerPicture { get; set; }
        public ICollection<FavoriteAndCustomer> Favorites { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<CustomerAndOperationClaim> CustomerAndOperationClaims { get; set; }
        public ICollection<UserToken> UserTokens { get; set; }

    }
}
