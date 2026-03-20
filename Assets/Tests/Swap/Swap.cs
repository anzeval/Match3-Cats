using NUnit.Framework;
//using Match3.Gameplay.System;

public class Swap
{
    [Test]
    public void Swap_Filled_With_Empty_ShouldSucceed()
    {
        // Cell[,] grid = new Cell[5, 5];

        // //Cash possible values 
        // pieceTypes = (PieceType[])Enum.GetValues(typeof(PieceType));

        // // 1 row
        // grid[0,0] = new Cell(0, 0, new Piece( PieceType.Blue));
        // grid[0,1] = new Cell(0, 1, new Piece( PieceType.Blue));
        // grid[0,2] = new Cell(0, 2, new Piece( PieceType.Green));
        // grid[0,3] = new Cell(0, 3, new Piece( PieceType.Blue));
        // grid[0,4] = new Cell(0, 4, new Piece( PieceType.Pink));

        // // 2 row
        // grid[1,0] = new Cell(1, 0, new Piece( PieceType.Pink));
        // grid[1,1] = new Cell(1, 1, new Piece( PieceType.Pink));
        // grid[1,2] = new Cell(1, 2, new Piece( PieceType.Blue));
        // grid[1,3] = new Cell(1, 3, null);
        // grid[1,4] = new Cell(1, 4, new Piece( PieceType.Pink));

        // // 3 row
        // grid[2,0] = new Cell(2, 0, new Piece( PieceType.LBomb));
        // grid[2,1] = new Cell(2, 1, new Piece( PieceType.Green));
        // grid[2,2] = new Cell(2, 2, new Piece( PieceType.Pink));
        // grid[2,3] = new Cell(2, 3, new Piece( PieceType.Green));
        // grid[2,4] = new Cell(2, 4, new Piece( PieceType.Blue));

        // // 4 row
        // grid[3,0] = new Cell(3, 0, new Piece( PieceType.Blue));
        // grid[3,1] = new Cell(3, 1, new Piece( PieceType.Pink));
        // grid[3,2] = new Cell(3, 2, new Piece( PieceType.Pink));
        // grid[3,3] = new Cell(3, 3, new Piece( PieceType.Blue));
        // grid[3,4] = new Cell(3, 4, new Piece( PieceType.Pink));

        // // 5 row
        // grid[4,0] = new Cell(4, 0, new Piece( PieceType.Blue));
        // grid[4,1] = new Cell(4, 1, new Piece( PieceType.Blue));
        // grid[4,2] = new Cell(4, 2, new Piece( PieceType.Green));
        // grid[4,3] = new Cell(4, 3, new Piece( PieceType.Pink));
        // grid[4,4] = new Cell(4, 4, new Piece( PieceType.Green));
        // var gridModel = new GridModel();

        //bool result = gridModel.Swap();
        //Assert.AreEqual(true, result);

        // public void DebugGrid()
        // {
        //     string rowstr = "";

        //     for (int row = 0; row < grid.GetLength(0); row++)
        //     {
        //         for (int column = 0; column < grid.GetLength(1); column++)
        //         {
        //             PieceType? pieceType = grid[row,column].GetPieceType();
        //             rowstr += (pieceType == null)? "." : pieceType.ToString()[0]; 
        //         }

        //         Debug.Log(rowstr);
        //         rowstr = "";
        //     }
        // }
    }

    [Test]
    public void Swap_Filled_With_Filled_ShouldSucceed()
    {
        
    }

    [Test]
    public void Swap_Filled_With_NonNeighbor_ShouldFail()
    {
        
    }

    [Test]
    public void Swap_OutOfBounds_ShouldFail()
    {
        
    }
}
