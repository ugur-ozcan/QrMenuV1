// QRMenu.Core/Extensions/EntityExtensions.cs
using QRMenu.Core.Entities;
using System;

namespace QRMenu.Core.Extensions
{
    public static class EntityExtensions
    {
        public static DateTime GetLicenseEndDate(this Branch branch)
        {
            return branch.LicenseEndDate;
        }

        public static DateTime GetLicenseEndDate(this Company company)
        {
            return company.LicenseEndDate;
        }

        public static bool IsLicenseExpired(this Branch branch)
        {
            return branch.LicenseEndDate <= DateTime.UtcNow;
        }

        public static bool IsLicenseExpired(this Company company)
        {
            return company.LicenseEndDate <= DateTime.UtcNow;
        }

        public static bool IsLicenseExpiringWithin(this Branch branch, int days)
        {
            return branch.LicenseEndDate <= DateTime.UtcNow.AddDays(days);
        }

        public static bool IsLicenseExpiringWithin(this Company company, int days)
        {
            return company.LicenseEndDate <= DateTime.UtcNow.AddDays(days);
        }
    }
}