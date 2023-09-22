using ChessGameDDD.Domain.Core;
using ChessGameDDD.Domain.Domain.Entities;
using ChessGameDDD.Events;
using System;
using System.Collections.Generic;

namespace ChessGameDDD.Domain.Entities
{
    public class Move
        : ValueObject
    {
        public BoardLocation FromLocation { get; set; }
        public BoardLocation ToLocation { get; set; }

        private Move()
        {
        }

        public static Move Create(BoardLocation fromLocation, BoardLocation toLocation)
        {
            var move = new Move
            {
                FromLocation = fromLocation,
                ToLocation = toLocation
            };

            if (move.FromLocation.Rank == move.ToLocation.Rank &&
                move.FromLocation.File == move.ToLocation.File)
            {
                throw new BusinessRuleViolationException("Move is invalid because to and from are the same");
            }

            return move;
        }

        internal void IsAllowed(Board board, Piece pieceToMove)
        {
            // TODO: Check is move is valid on board
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FromLocation;
            yield return ToLocation;
        }

        public override string ToString()
        {
            return FromLocation.ToString() + " -> " + ToLocation.ToString();
        }
        
        internal Event MapToMoveMade()
        {
            return new MoveMadeEvent();
        }
    }
}