// QRMenu.Web/Controllers/CompanyController.cs
using Microsoft.AspNetCore.Mvc;
using QRMenu.Application.DTOs;
using QRMenu.Application.Interfaces;
using System.Threading.Tasks;

namespace QRMenu.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Tüm şirketleri getirir.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAllAsync();
            return Ok(companies);
        }

        /// <summary>
        /// Belirtilen ID'ye sahip şirketi getirir.
        /// </summary>
        /// <param name="id">Şirket ID'si</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _companyService.GetByIdAsync(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }

        /// <summary>
        /// Belirtilen slug'a sahip şirketi getirir.
        /// </summary>
        /// <param name="slug">Şirket slug'ı</param>
        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var company = await _companyService.GetBySlugAsync(slug);
            if (company == null)
                return NotFound();
            return Ok(company);
        }

        /// <summary>
        /// Yeni bir şirket oluşturur.
        /// </summary>
        /// <param name="companyDto">Oluşturulacak şirket bilgileri</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyDTO companyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCompany = await _companyService.CreateCompanyAsync(companyDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCompany.Id }, createdCompany);
        }

        /// <summary>
        /// Belirtilen ID'ye sahip şirketi günceller.
        /// </summary>
        /// <param name="id">Güncellenecek şirket ID'si</param>
        /// <param name="companyDto">Güncellenmiş şirket bilgileri</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CompanyDTO companyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedCompany = await _companyService.UpdateAsync(id, companyDto);
            if (updatedCompany == null)
                return NotFound();
            return Ok(updatedCompany);
        }

        /// <summary>
        /// Belirtilen ID'ye sahip şirketi siler.
        /// </summary>
        /// <param name="id">Silinecek şirket ID'si</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _companyService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Belirtilen ID'ye sahip şirketin durumunu günceller.
        /// </summary>
        /// <param name="id">Güncellenecek şirket ID'si</param>
        /// <param name="isActive">Yeni durum</param>
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] bool isActive)
        {
            await _companyService.UpdateStatusAsync(id, isActive);
            return NoContent();
        }

        /// <summary>
        /// Belirtilen slug'ın benzersiz olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="slug">Kontrol edilecek slug</param>
        /// <param name="excludeId">Hariç tutulacak şirket ID'si (opsiyonel)</param>
        [HttpGet("check-slug")]
        public async Task<IActionResult> CheckSlug([FromQuery] string slug, [FromQuery] int? excludeId = null)
        {
            var isUnique = await _companyService.IsSlugUniqueAsync(slug, excludeId);
            return Ok(new { isUnique });
        }

        /// <summary>
        /// Belirtilen ID'ye sahip şirketin verilerini senkronize eder.
        /// </summary>
        /// <param name="id">Senkronize edilecek şirket ID'si</param>
        [HttpPost("{id}/sync")]
        public async Task<IActionResult> SyncCompanyData(int id)
        {
            await _companyService.SyncCompanyDataAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Belirtilen ID'ye sahip şirketin bağlantısını test eder.
        /// </summary>
        /// <param name="id">Test edilecek şirket ID'si</param>
        [HttpPost("{id}/test-connection")]
        public async Task<IActionResult> TestConnection(int id)
        {
            var isConnected = await _companyService.TestConnectionAsync(id);
            return Ok(new { isConnected });
        }
    }
}