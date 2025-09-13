using api1.Data;
using api1.Dtos;
using api1.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
namespace api1.Controllers;


[Route("api/[controller]")]
[ApiController]
public class FriendRequestController : ControllerBase
    {
        private readonly AppDbContext _context;
        public FriendRequestController(AppDbContext context)
        {
            _context = context;
        }
    [HttpPost("send-request")]
    public async Task<IActionResult> SendRequest([FromQuery] int senderId, [FromQuery] int receiverId)
    {
        if (senderId == receiverId)
        {
            return BadRequest("Kendinize istek gönderemezsiniz.");
        }

        bool alreadyFriends = await _context.Friends
            .AnyAsync(x => (x.UserID == senderId && x.FriendID == receiverId) ||
                           (x.UserID == receiverId && x.FriendID == senderId));
        if (alreadyFriends)
        {
            return BadRequest("Zaten mevcut arkadaşlık.");
        }

        var request = await _context.FriendRequests
            .FirstOrDefaultAsync(x =>
                (x.SenderId == senderId && x.ReceiverId == receiverId) ||
                (x.SenderId == receiverId && x.ReceiverId == senderId));

        if (request != null)
        {
            // Karşılıklı istek varsa doğrudan arkadaş yap
            var friend1 = new Friend { UserID = senderId, FriendID = receiverId, Status = 1 };
            var friend2 = new Friend { UserID = receiverId, FriendID = senderId, Status = 1 };

            _context.Friends.AddRange(friend1, friend2);
            _context.FriendRequests.Remove(request);
            await _context.SaveChangesAsync();

            return Ok("Karşılıklı istek bulundu, arkadaş oldunuz.");
        }
        else
        {
            // Yeni istek gönder
            var newRequest = new FriendRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId
            };
            _context.FriendRequests.Add(newRequest);
            await _context.SaveChangesAsync();
            return Ok("İstek gönderildi.");
        }

        // Artık tüm yollar bir return ile bitiyor.
    }


    [HttpDelete("reject/{requestid}")]
    public async Task<IActionResult> RejectRequest(int requestid)
    {
        var request = await _context.FriendRequests.FindAsync(requestid);
        if (request == null) return NotFound();
        _context.FriendRequests.Remove(request);
        await _context.SaveChangesAsync();
        return Ok("istek reddedildi.");

    }

    [HttpPost("accept/{requestid}")]
    public async Task<IActionResult> AcceptRequest(int requestid)
    {
        var request = await _context.FriendRequests.FindAsync(requestid);
        if (request == null) return NotFound();
        var friend1 = new Friend { UserID = request.SenderId, FriendID = request.ReceiverId, Status = 1 };
        var friend2 = new Friend { UserID = request.ReceiverId, FriendID = request.SenderId, Status = 1 };

        _context.Friends.AddRange(friend1, friend2);
        _context.FriendRequests.Remove(request);

        await _context.SaveChangesAsync();
        return Ok("istek kabul edildi.");

    }
    [HttpGet("pending/{userid}")]
    public async Task<IActionResult> GetIncomingRequest(int userid)
    {
        var request = await _context.FriendRequests.Where(
            r => r.ReceiverId == userid).Select(
            r=> new FriendDto
            {
                RequestId = r.Id,
                FriendId = r.SenderId,
                Username = r.Sender.Username,
                EmailAddress = r.Sender.EmailAddress,
                ProfilePictureUrl = r.Sender.ProfilePictureUrl,
                CreatedAt = r.Sender.CreadetAt
            }
            ).ToListAsync();
        
        return Ok(request);
    }
    }

