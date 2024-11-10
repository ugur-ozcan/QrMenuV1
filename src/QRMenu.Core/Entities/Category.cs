// QRMenu.Core/Entities/Category.cs
using System.Collections.Generic;

namespace QRMenu.Core.Entities
{
    // Kategori bilgilerini tutan varlık
    public class Category : BaseEntity
    {
        public int CompanyId { get; set; } // Bağlı olduğu şirket kimliği
        public int? BranchId { get; set; } // Bağlı olduğu şube kimliği (opsiyonel)
        public string ExternalId { get; set; } // Dış sistem kimliği
        public string ParentExternalId { get; set; } // Üst kategori dış sistem kimliği
        public string Name { get; set; } // Kategori adı
        public int DisplayOrder { get; set; } // Görüntüleme sırası
        public string Language { get; set; } // Dil
        public virtual Company Company { get; set; } // Bağlı olduğu şirket
        public virtual Branch Branch { get; set; } // Bağlı olduğu şube
        public virtual ICollection<Product> Products { get; set; } // Ürünler
    }
}