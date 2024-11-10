// QRMenu.Application/DTOs/BranchDTO.cs
using System;
using Microsoft.AspNetCore.Http;

namespace QRMenu.Application.DTOs
{
    public class BranchDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string InstagramLink { get; set; }
        public string PhoneNumber { get; set; }
        public string ServerAddress { get; set; }
        public string DatabaseName { get; set; }
        public string DatabaseUsername { get; set; }
        public string DatabasePassword { get; set; }
        public string CategoryViewName { get; set; }
        public string ProductViewName { get; set; }
        public bool UseTableNumbers { get; set; }
        public string BackgroundColor { get; set; }
        public string ButtonColor { get; set; }
        public string TextColor { get; set; }
        public string LogoUrl { get; set; }
        public IFormFile LogoFile { get; set; }
        public bool AutoSelectColors { get; set; }
        public string FontFamily { get; set; }
        public string TemplateId { get; set; }
        public DateTime LastDataRefresh { get; set; }
        public int RefreshIntervalDays { get; set; }
        public int RefreshIntervalHours { get; set; }
        public int RefreshIntervalMinutes { get; set; }
        public bool IsConnected { get; set; }
        public DateTime LastSyncTime { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public bool IsActive { get; set; }
        public DateTime ServiceEndDate { get; set; }
    }
}