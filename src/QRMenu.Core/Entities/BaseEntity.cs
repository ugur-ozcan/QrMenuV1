// QRMenu.Core/Entities/BaseEntity.cs
using System;

namespace QRMenu.Core.Entities
{
    // Tüm varlıklar için temel sınıf
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } // Oluşturan kullanıcı
        public DateTime CreatedDate { get; set; } // Oluşturulma tarihi
        public string UpdatedBy { get; set; } // Güncelleyen kullanıcı
        public DateTime? UpdatedDate { get; set; } // Güncelleme tarihi
        public string DeletedBy { get; set; } // Silen kullanıcı
        public DateTime? DeletedDate { get; set; } // Silinme tarihi
        public bool IsDeleted { get; set; } // Silinme durumu
        public bool IsActive { get; set; } = true; // Aktiflik durumu
        public string IPAddress { get; set; } // IP adresi
        public bool IsConnected { get; set; } // Bağlantı durumu
        public DateTime? LastHealthCheck { get; set; } // Son sağlık kontrolü
    }
}