// QRMenu.Core/Entities/Company.cs
using QRMenu.Core.Enums;
using System;
using System.Collections.Generic;

namespace QRMenu.Core.Entities
{
    // Şirket bilgilerini tutan varlık
    public class Company : BaseEntity
    {
        public string Name { get; set; } // Şirket adı
        public string Slug { get; set; } // URL-dostu benzersiz tanımlayıcı
        public string Address { get; set; } // Adres
        public string Email { get; set; } // E-posta
        public string InstagramLink { get; set; } // Instagram bağlantısı
        public string PhoneNumber { get; set; } // Telefon numarası
        public string ServerAddress { get; set; } // Sunucu adresi
        public string DatabaseName { get; set; } // Veritabanı adı
        public string DatabaseUsername { get; set; } // Veritabanı kullanıcı adı
        public string DatabasePassword { get; set; } // Veritabanı şifresi
        public string CategoryViewName { get; set; } // Kategori görünüm adı
        public string ProductViewName { get; set; } // Ürün görünüm adı
        public bool HasBranches { get; set; } // Şubeleri var mı?
        public bool UseTableNumbers { get; set; } // Masa numaraları kullanılsın mı?
        public string BackgroundColor { get; set; } // Arka plan rengi
        public string ButtonColor { get; set; } // Düğme rengi
        public string TextColor { get; set; } // Metin rengi
        public string LogoUrl { get; set; } // Logo URL'si
        public bool AutoSelectColors { get; set; } // Renkleri otomatik seç
        public string FontFamily { get; set; } // Yazı tipi ailesi
        public string TemplateId { get; set; } // Şablon kimliği
        public DateTime LastDataRefresh { get; set; } // Son veri yenileme tarihi
        public int RefreshIntervalDays { get; set; } // Yenileme aralığı (gün)
        public int RefreshIntervalHours { get; set; } // Yenileme aralığı (saat)
        public int RefreshIntervalMinutes { get; set; } // Yenileme aralığı (dakika)
        public int? DealerId { get; set; } // Bayi kimliği
        public virtual Dealer Dealer { get; set; } // Bayi ilişkisi
        public int TotalRefreshIntervalMinutes { get; set; } // Toplam yenileme aralığı (dakika)
        public virtual ICollection<Branch> Branches { get; set; } // Şubeler
        public virtual ICollection<Category> Categories { get; set; } // Kategoriler
        public virtual ICollection<Product> Products { get; set; } // Ürünler
        public DateTime LicenseEndDate { get; set; } // Lisans bitiş tarihi
        public LicenseType LicenseType { get; set; } // Lisans türü (enum)
    }
}