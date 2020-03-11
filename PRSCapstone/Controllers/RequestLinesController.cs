using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRSCapstone.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RequestLinesController : ControllerBase {
        private readonly AppDbContext _context;

        public RequestLinesController(AppDbContext context) {
            _context = context;
        }

        // GET: api/RequestLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestLines>>> GetRequestLines() {
            return await _context.RequestLines.ToListAsync();
        }

        // GET: api/RequestLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLines>> GetRequestLines(int id) {
            var requestLines = await _context.RequestLines.FindAsync(id);

            if (requestLines == null) {
                return NotFound();
            }

            return requestLines;
        }

        // PUT: api/RequestLines/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestLines(int id, RequestLines requestLines) {
            if (id != requestLines.Id) {
                return BadRequest();
            }
            _context.Entry(requestLines).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!RequestLinesExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
            UpdateTotal(requestLines.RequestId);
            
            return NoContent();
        }

        // POST: api/RequestLines
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RequestLines>> PostRequestLines(RequestLines requestLines) {
            _context.RequestLines.Add(requestLines);
            await _context.SaveChangesAsync();
            UpdateTotal(requestLines.RequestId);
            return CreatedAtAction("GetRequestLines", new { id = requestLines.Id }, requestLines);
        }

        // DELETE: api/RequestLines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RequestLines>> DeleteRequestLines(int id) {
            var requestLines = await _context.RequestLines.FindAsync(id);
            if (requestLines == null) {
                return NotFound();
            }

            _context.RequestLines.Remove(requestLines);
            await _context.SaveChangesAsync();
            UpdateTotal(requestLines.RequestId);
            return requestLines;
        }

        private bool RequestLinesExists(int id) {
            return _context.RequestLines.Any(e => e.Id == id);
        }

        private void UpdateTotal(int requestId) {
            var request = _context.Requests.Find(requestId);
            var total = _context.RequestLines.Where(rl => rl.RequestId == requestId).Sum(x => x.Qty * x.Product.Price);
            request.Total = total;
            _context.SaveChanges();
        }
        public void CreatePO(int vendorId) {
            var vendorPO =
                from vendor in _context.Vendors
                join prod in _context.Products on vendor.Id equals prod.VendorId
                join requestline in _context.RequestLines on prod.Id equals requestline.ProductId
                where vendor.Id == vendorId
                select _context.Requests;

            Console.WriteLine(vendorPO);
                
        }
    }
}
