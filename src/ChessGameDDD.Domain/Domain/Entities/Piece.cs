using ChessGameDDD.Domain.Core;

namespace ChessGameDDD.Domain.Entities
{
    public abstract class Piece : ValueObject
    {
        internal virtual void CanMakeMove(Move move, Board board)
        {
            // Move is on board
            // Field doesn't contain own piece
            // Move checks king
        }
    }
}