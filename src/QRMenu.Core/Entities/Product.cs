// QRMenu.Core/Entities/Product.cs
namespace QRMenu.Core.Entities
{
    // Ürün bilgilerini tutan varlık
    public class Product : BaseEntity
    {
        public int CompanyId { get; set; } // Bağlı olduğu şirket kimliği
        public int? BranchId { get; set; } // Bağlı olduğu şube kimliği (opsiyonel)
        public int CategoryId { get; set; } // Bağlı olduğu kategori kimliği
        public string ExternalId { get; set; } // Dış sistem kimliği
        public string Name { get; set; } // Ürün adı
        public string Description { get; set; } // Ürün açıklaması
        public decimal Price { get; set; } // Fiyat
        public string ImageUrl { get; set; } // Resim URL'si
        public int DisplayOrder { get; set; } // Görüntüleme sırası
        public string Tags { get; set; } // Etiketler
        public string Language { get; set; } // Dil
        public virtual Company Company { get; set; } // Bağlı olduğu şirket
        public virtual Branch Branch { get; set; } // Bağlı olduğu şube
        public virtual Category Category { get; set; } // Bağlı olduğu kategori
    }
}