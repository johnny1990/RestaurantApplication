using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Entities;
using RestaurantApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.Web.Controllers.API
{
    [Route("api/Vouchers")]
    [ApiController]
    public class VouchersApiController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public VouchersApiController(RestaurantDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetVouchers")]
        public IActionResult GetVouchers()
        {
            var meals = _context.Vouchers;
            return new OkObjectResult(meals);
        }
            
        [HttpGet]
        [Route("GetVoucherById")]
        public IActionResult GetVoucherById(int id)
        {
            var voucher = from a in _context.Vouchers.Where(p => p.Id == id).ToList()
                       select new
                       {
                           a.Id,
                           a.Name,
                           a.VoucherType,
                           a.Discount,
                           a.MinimumAmount,
                           a.IsActive

                       };
            return new OkObjectResult(voucher);
        }

        [HttpPost]
        [Route("CreateVoucher")]
        public IActionResult CreateVoucher([FromBody] Vouchers voucher)
        {
            _context.Add(voucher);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Vouchers), new { id = voucher.Id }, voucher);
        }

        [HttpPut]
        [Route("UpdateVoucher")]
        public IActionResult UpdateVoucher([FromBody] Vouchers voucher)
        {
            _context.Update(voucher);
            _context.SaveChanges();
            return new OkResult();
        }

        [HttpDelete]
        [Route("DeleteVoucher")]
        public IActionResult DeleteVoucher(int id)
        {
            var vouchers = _context.Vouchers.Find(id);
            _context.Vouchers.Remove(vouchers);
            _context.SaveChanges();
            return new OkResult();
        }
    }
}
