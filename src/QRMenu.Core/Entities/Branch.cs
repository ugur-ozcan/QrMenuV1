// QRMenu.Core/Entities/Branch.cs
using QRMenu.Core.Enums;
using System;
using System.Collections.Generic;

namespace QRMenu.Core.Entities
{
    // Şube bilgilerini tutan varlık
    public class Branch : BaseEntity
    {
        public int CompanyId { get; set; } // Bağlı olduğu şirket kimliği
        public string Name { get; set; } // Şube adı
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
        public bool IsConnected { get; set; } // Bağlantı durumu
        public DateTime LastSyncTime { get; set; } // Son senkronizasyon zamanı
        public string ApiKey { get; set; } // API anahtarı
        public string ApiSecret { get; set; } // API gizli anahtarı
        public virtual Company Company { get; set; } // Bağlı olduğu şirket
        public virtual ICollection<Category> Categories { get; set; } // Kategoriler
        public virtual ICollection<Product> Products { get; set; } // Ürünler
        public DateTime LicenseEndDate { get; set; } // Lisans bitiş tarihi
        public LicenseType LicenseType { get; set; } // Lisans türü (enum)
    }
}