using Bazingo_Core.Interfaces;
using Bazingo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Core.DomainLogic
{
    public class AuctionManager
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBidRepository _bidRepository;

        public AuctionManager(IAuctionRepository auctionRepository , IBidRepository bidRepository)
        {
            _auctionRepository = auctionRepository;
            _bidRepository = bidRepository;
        }

        public async Task<Auction> EndAuctionAsync(int auctionId)
        {
            var auction = await _auctionRepository.GetAuctionByIdAsync(auctionId);

            if (auction == null || auction.EndTime > DateTime.UtcNow)
            {
                throw new InvalidOperationException("Auction is either not found or has not ended yet.");
            }

            var highestBid = (await _bidRepository.GetBidsByAuctionIdAsync(auctionId))
                                .OrderByDescending(b => b.BidAmount)
                                .FirstOrDefault();

            if (highestBid != null)
            {
                auction.WinnerID = highestBid.UserID;
                auction.CurrentPrice = highestBid.BidAmount;

                await _auctionRepository.UpdateAuctionAsync(auction);
            }

            return auction;
        }
        public async Task AddAuctionAsync(Auction auction)
        {
            if (auction.EndTime <= DateTime.UtcNow)
            {
                throw new ArgumentException("Auction end time must be in the future.");
            }

            auction.CurrentPrice = auction.StartPrice;
            await _auctionRepository.AddAuctionAsync(auction);
        }
        public async Task PlaceBidAsync(Bid bid)
        {
            var auction = await _auctionRepository.GetAuctionByIdAsync(bid.AuctionID);

            if (auction == null)
            {
                throw new InvalidOperationException("Auction not found.");
            }

            if (auction.EndTime <= DateTime.UtcNow)
            {
                throw new InvalidOperationException("Auction has already ended.");
            }

            if (bid.BidAmount <= auction.CurrentPrice)
            {
                throw new InvalidOperationException("Bid amount must be higher than the current price.");
            }

            auction.CurrentPrice = bid.BidAmount;
            await _bidRepository.AddBidAsync(bid);
            await _auctionRepository.UpdateAuctionAsync(auction);
        }
        public async Task<Auction> GetAuctionByIdAsync(int auctionId)
        {
            return await _auctionRepository.GetAuctionByIdAsync(auctionId)
                ?? throw new KeyNotFoundException("Auction not found.");
        }

    }

}
