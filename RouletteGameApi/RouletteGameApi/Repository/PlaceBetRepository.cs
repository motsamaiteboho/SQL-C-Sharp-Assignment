﻿using Dapper;
using RouletteGameApi.Context;
using RouletteGameApi.Contracts;
using RouletteGameApi.Dto;
using RouletteGameApi.Entities;
using System.Data;

namespace RouletteGameApi.Repository
{
    public class PlaceBetRepository : IPlaceBetRepository
    {
        private readonly IBetsDBContext _context;
        public PlaceBetRepository(IBetsDBContext context)
        {
            _context = context;
        }

        public async Task<PlaceBet> PlaceBet(PlaceBetDto bet)
        {
            var query = "INSERT INTO PlacedBets  (BetName, NumbersOnTheTable, BetAmount)" +
                "VALUES (@BetName, @NumbersOnTheTable,@BetAmount);" +
                "SELECT last_insert_rowid();";

            var parameters = new DynamicParameters();
            parameters.Add("@BetName", bet.BetName, DbType.String);
            parameters.Add("@NumbersOnTheTable", bet.NumbersOnTheTable, DbType.Int64);
            parameters.Add("@BetAmount", bet.BetAmount, DbType.Decimal);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleOrDefaultAsync<int>(query, parameters);

                var placedBet = new PlaceBet
                {
                    Id = id,
                    BetName =bet.BetName,
                    NumbersOnTheTable = bet.NumbersOnTheTable,
                    BetAmount = bet.BetAmount,
                };
                return placedBet;
            }
        }
        public async Task<IEnumerable<PlaceBet>> GetPlacedBets()
        {
            var query = "SELECT * FROM PlacedBets";

            using (var connection = _context.CreateConnection())
            {
                var placedbets = await connection.QueryAsync<PlaceBet>(query);
                return placedbets.ToList();
            }
        }

        public async Task<PlaceBet> GetPlacedBet(int Id)
        {
            var query = "SELECT * FROM PlacedBets WHERE  Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var placebet = await connection.QuerySingleOrDefaultAsync<PlaceBet>(query, new { Id });
                 return placebet;
            }
        }

        public async Task UpdateBet(int id, UpdateBetDto bet)
        {
            var query = "UPDATE PlacedBets SET BetName = @BetName ," +
               "NumbersOnTheTable = @NumbersOnTheTable," +
               "BetAmount=@BetAmount WHERE Id = @Id;";

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);
            parameters.Add("@BetName", bet.BetName, DbType.String);
            parameters.Add("@NumbersOnTheTable", bet.NumbersOnTheTable, DbType.Int64);
            parameters.Add("@BetAmount", bet.BetAmount, DbType.Decimal);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteBet(int Id)
        {
            var query = "DELETE FROM PlacedBets WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id });
            }
        }

    }
}
