// QRMenu.Application/DTOs/CompanyDTO.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace QRMenu.Application.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Şirket adı zorunludur.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Şirket adı 2 ile 100 karakter arasında olmalıdır.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Slug zorunludur.")]
        [RegularExpression(@"^[a-z0-9]+(?:-[a-z0-9]+)*$", ErrorMessage = "Slug sadece küçük harfler, sayılar ve tire içerebilir.")]
        public string Slug { get; set; }

        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }

        [Url(ErrorMessage = "Geçerli bir URL giriniz.")]
        public string WebsiteUrl { get; set; }

        [Required(ErrorMessage = "Lisans bitiş tarihi zorunludur.")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Lisans bitiş tarihi gelecekte bir tarih olmalıdır.")]
        public DateTime LicenseEndDate { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Sunucu adresi zorunludur.")]
        public string ServerAddress { get; set; }

        [Required(ErrorMessage = "Veritabanı adı zorunludur.")]
        public string DatabaseName { get; set; }

        [Required(ErrorMessage = "Veritabanı kullanıcı adı zorunludur.")]
        public string DatabaseUsername { get; set; }

        [Required(ErrorMessage = "Veritabanı şifresi zorunludur.")]
        public string DatabasePassword { get; set; }

        [Required(ErrorMessage = "Kategori görünüm adı zorunludur.")]
        public string CategoryViewName { get; set; }

        [Required(ErrorMessage = "Ürün görünüm adı zorunludur.")]
        public string ProductViewName { get; set; }

        public bool UseTableNumbers { get; set; }

        [Required(ErrorMessage = "Arka plan rengi zorunludur.")]
        [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Geçerli bir renk kodu giriniz (örn. #FF5733).")]
        public string BackgroundColor { get; set; }

        [Required(ErrorMessage = "Buton rengi zorunludur.")]
        [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Geçerli bir renk kodu giriniz (örn. #FF5733).")]
        public string ButtonColor { get; set; }

        [Required(ErrorMessage = "Metin rengi zorunludur.")]
        [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Geçerli bir renk kodu giriniz (örn. #FF5733).")]
        public string TextColor { get; set; }

        public string LogoUrl { get; set; }

        public bool AutoSelectColors { get; set; }

        [Required(ErrorMessage = "Font ailesi zorunludur.")]
        public string FontFamily { get; set; }

        [Required(ErrorMessage = "Şablon ID'si zorunludur.")]
        public string TemplateId { get; set; }

        public DateTime LastDataRefresh { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Yenileme aralığı günü negatif olamaz.")]
        public int RefreshIntervalDays { get; set; }

        [Range(0, 23, ErrorMessage = "Yenileme aralığı saati 0 ile 23 arasında olmalıdır.")]
        public int RefreshIntervalHours { get; set; }

        [Range(0, 59, ErrorMessage = "Yenileme aralığı dakikası 0 ile 59 arasında olmalıdır.")]
        public int RefreshIntervalMinutes { get; set; }

        public bool IsConnected { get; set; }

        public DateTime LastSyncTime { get; set; }

        [Required(ErrorMessage = "API anahtarı zorunludur.")]
        public string ApiKey { get; set; }

        [Required(ErrorMessage = "API gizli anahtarı zorunludur.")]
        public string ApiSecret { get; set; }

        [Required(ErrorMessage = "Bayi ID'si zorunludur.")]
        public int DealerId { get; set; }

        // BaseEntity'den gelen alanlar
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string IPAddress { get; set; }
        public DateTime? LastHealthCheck { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is DateTime date && date > DateTime.Now;
        }
    }
}