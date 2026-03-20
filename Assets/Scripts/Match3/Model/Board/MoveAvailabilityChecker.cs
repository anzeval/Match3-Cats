using System.Collections.Generic;
using Match3.Model.Data;

namespace Match3.Model.Board
{
   public class MoveAvailabilityChecker
    {
        private SwapValidator swapValidator;

        public MoveAvailabilityChecker(SwapValidator swapValidator)
        {
            this.swapValidator = swapValidator;
        }

        public bool HasAnyValidMove(BoardState board)
        {
            for (int row = 0; row < board.Height; row++)
            {
                for (int column = 0; column < board.Width; column++)
                {
                    Position currentPos = new Position(row, column);
                    Position rightPos = new Position(row, column + 1);
                    Position downPos = new Position(row + 1, column);

                    if(column < board.Width - 1)
                    {
                        if(swapValidator.CanSwap(board, currentPos, rightPos)) 
                            return true;
                    }

                    if(row < board.Height - 1)
                    {
                        if(swapValidator.CanSwap(board, currentPos, downPos)) 
                            return true;
                    }
                }
            }

            return false;
        }

        public List<(Position a, Position b)> FindAllValidMoves(BoardState board)
        {
            List<(Position a, Position b)> movePositions = new List<(Position a, Position b)>();

            Position currentPos;
            Position rightPos;
            Position downPos;

            for (int row = 0; row < board.Height; row++)
            {
                for (int column = 0; column < board.Width; column++)
                {
                    currentPos = new Position(row, column);
                    rightPos = new Position(row, column + 1);
                    downPos = new Position(row + 1, column);

                    if(column < board.Width - 1)
                    {
                        if(swapValidator.CanSwap(board, currentPos, rightPos)) 
                            movePositions.Add((currentPos, rightPos));
                    }

                    if(row < board.Height - 1)
                    {
                        if(swapValidator.CanSwap(board, currentPos, downPos)) 
                            movePositions.Add((currentPos, downPos));
                    }
                }
            }
    
            return movePositions;
        }
    } 
}