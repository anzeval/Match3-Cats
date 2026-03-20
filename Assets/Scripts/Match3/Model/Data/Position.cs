using System;

namespace Match3.Model.Data
{
  public struct Position
  {
    public int Row { get; private set;}
    public int Column { get; private set;}

    public Position(int row, int col)
    {
      Row = row;
      Column = col;
    }

    public Position Offset(int dRow, int dCol)
    {
      return new Position(Row + dRow, Column + dCol);
    } 

    private int DistanceBetweenCells(Position other)
    {
      return Math.Abs(Row - other.Row) + Math.Abs(Column - other.Column);
    }

    public bool IsNeighbor4(Position other)
    {
        return DistanceBetweenCells(other) == 1;
    }
  }  
}