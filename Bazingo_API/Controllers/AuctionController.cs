using Bazingo_Core.DomainLogic;
using Bazingo_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bazingo_Application.DTOs.Auctions;
using Bazingo_Application.DTOs.Bids;

namespace Bazingo_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionController : ControllerBase
    {
        private readonly AuctionManager _auctionManager;

        public AuctionController(AuctionManager auctionManager)
        {
            _auctionManager = auctionManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuction([FromBody] AuctionCreateDTO auctionDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var auction = new Auction
            {
                ProductID = auctionDTO.ProductID ,
                StartPrice = auctionDTO.StartPrice ,
                EndTime = auctionDTO.EndTime
            };

            await _auctionManager.AddAuctionAsync(auction);
            return Ok(new { message = "Auction created successfully." });
        }

        [HttpPost("bid")]
        public async Task<IActionResult> PlaceBid([FromBody] BidCreateDTO bidDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var bid = new Bid
            {
                AuctionID = bidDTO.AuctionID ,
                UserID = bidDTO.UserID ,
                BidAmount = bidDTO.BidAmount
            };

            await _auctionManager.PlaceBidAsync(bid);
            return Ok(new { message = "Bid placed successfully." });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuctionDetails(int id)
        {
            var auction = await _auctionManager.GetAuctionByIdAsync(id);
            return Ok(auction);
        }
    }
}
