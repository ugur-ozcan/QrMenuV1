// QRMenu.Core/Entities/Dealer.cs
using System.Collections.Generic;

namespace QRMenu.Core.Entities
{
    // Bayi bilgilerini tutan varlık
    public class Dealer : BaseEntity
    {
        public string Name { get; set; } // Bayi adı
        public string Email { get; set; } // Bayi e-posta adresi
        public string PhoneNumber { get; set; } // Bayi telefon numarası
        public string Address { get; set; } // Bayi adresi
        public decimal CommissionRate { get; set; } // Komisyon oranı
        public string ApiKey { get; set; } // API anahtarı
        public virtual ICollection<Company> Companies { get; set; } // Bayiye bağlı şirketler
    }
}