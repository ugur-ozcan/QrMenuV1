// QRMenu.Core/Extensions/DateTimeExtensions.cs
namespace QRMenu.Core.Extensions
{
    /// <summary>
    /// DateTime için extension metodları
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// DateTime'ı UTC'ye çevirir
        /// </summary>
        public static DateTime UtcNow => DateTime.UtcNow;

        /// <summary>
        /// Verilen tarihin geçmiş olup olmadığını kontrol eder
        /// </summary>
        public static bool IsPast(this DateTime dateTime)
        {
            return dateTime < DateTime.UtcNow;
        }

        /// <summary>
        /// Verilen tarihin gelecek olup olmadığını kontrol eder
        /// </summary>
        public static bool IsFuture(this DateTime dateTime)
        {
            return dateTime > DateTime.UtcNow;
        }

        /// <summary>
        /// İki tarih arasındaki farkı gün olarak hesaplar
        /// </summary>
        public static int DaysBetween(this DateTime startDate, DateTime endDate)
        {
            return (endDate - startDate).Days;
        }

        /// <summary>
        /// Verilen tarihin bugün olup olmadığını kontrol eder
        /// </summary>
        public static bool IsToday(this DateTime dateTime)
        {
            return dateTime.Date == DateTime.UtcNow.Date;
        }
    }
}