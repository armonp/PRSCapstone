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
    public class RequestsController : ControllerBase {
        public const string StatusApproved = "APPROVED";
        public const string StatusEdit = "EDIT";
        public const string StatusRejected = "REJECTED";
        public const string StatusReview = "REVIEW";


        private readonly AppDbContext _context;

        public RequestsController(AppDbContext context) {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests() {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id) {
            var request = await _context.Requests.FindAsync(id);

            if (request == null) {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request) {
            if (id != request.Id) {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!RequestExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Requests
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request) {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(int id) {
            var request = await _context.Requests.FindAsync(id);
            if (request == null) {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        private bool RequestExists(int id) {
            return _context.Requests.Any(e => e.Id == id);
        }
        [HttpPut("review/{id}/{request}")]
        public Task<IActionResult> MarkReviewed(int id, Request request) {
            request.RejectionReason = null;
            if (request.Total < 50 ) 
                request.Status = StatusApproved;
            else
                request.Status = StatusReview;
            return PutRequest(id, request);
        
        }
        [HttpPut("reject/{id}/{request}")]
        public Task<IActionResult> MarkRejected(int id, Request request) {
            request.Status = StatusRejected;
            if (request.RejectionReason == null) {
                throw new Exception("Rejection reason must be included with Rejected requests");
            }
            return PutRequest(id, request);
        }
        [HttpPut("approve/{request}")]
        public Task<IActionResult> MarkApproved (Request request) {
            request.Status = StatusApproved;
            request.RejectionReason = null;
            return PutRequest(request.Id, request);
        }
        [HttpGet("reviews/{userId}")]
        public IEnumerable<Request> GetRequestsToBeReviewed (int userId) {
            var loggedinuser = _context.Users.Find(userId);
            if (loggedinuser.IsReviewer)
                return _context.Requests.Where(x => x.UserId != userId && x.Status == StatusReview);
            else return null;
        }
    }
}
