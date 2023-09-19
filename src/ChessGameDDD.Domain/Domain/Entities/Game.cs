using ChessGameDDD.Domain.BusinessRules;
using ChessGameDDD.Domain.Core;
using ChessGameDDD.Events;
using System;
using System.Collections.Generic;

namespace ChessGameDDD.Domain.Entities
{
    public class Game : AggregateRoot<Guid>
    {
        private Board Board = Board.Create();

        private Game(List<Event> events) :
            base(Guid.NewGuid(), events)
        {
        }

        public static Game Create(List<Event> oldEvents)
        {
            return new Game(oldEvents);
        }

        public void MakeMove(Move move)
        {
            var pieceToMove = Board.GetPieceToMove(move.FromLocation);

            // Check business rules
            pieceToMove.CanMakeMove(move, Board);
            move.IsAllowed(Board, pieceToMove);

            // Create event
            var eventResult = move.MapToMoveMade();

            // Raise event
            RaiseEvent(eventResult);
        }

        protected override void When(dynamic @event)
        {
            switch (@event)
            {
                case MoveMadeEvent:
                    When(@event as MoveMadeEvent);
                    break;

                default:
                    break;
            }
        }

        private void When(MoveMadeEvent @event)
        {
            Board.UpdateBoardPosition(@event.Move);
        }
    }
}