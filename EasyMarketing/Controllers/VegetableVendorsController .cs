using EasyMarketing.Data;
using EasyMarketing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyMarketing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VegetableVendorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VegetableVendorsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetVendors()
        {
            return Ok(await _context.VegetableVendors.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendor(int id)
        {
            var vendor = await _context.VegetableVendors.FindAsync(id);
            if (vendor == null) return NotFound();
            return Ok(vendor);
        }

        [HttpPost]
        public async Task<IActionResult> AddVendor(VegetableVendor vendor)
        {
            _context.VegetableVendors.Add(vendor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVendor), new { id = vendor.VendorId }, vendor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(int id, VegetableVendor vendor)
        {
            if (id != vendor.VendorId) return BadRequest();

            _context.Entry(vendor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var vendor = await _context.VegetableVendors.FindAsync(id);
            if (vendor == null) return NotFound();

            _context.VegetableVendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
