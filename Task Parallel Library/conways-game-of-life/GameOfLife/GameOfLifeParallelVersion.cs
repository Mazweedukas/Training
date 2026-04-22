namespace GameOfLife;

/// <summary>
/// Represents Conway's Game of Life in a parallel version.
/// The class provides methods to simulate the game's evolution based on simple rules.
/// </summary>
public sealed class GameOfLifeParallelVersion
{
    private readonly int numberOfRows;
    private readonly int numberOfColumns;

    private readonly bool[,] initialGrid;
    private bool[,] currentGrid;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameOfLifeParallelVersion"/> class with the specified number of rows and columns of the grid. The initial state of the grid is randomly set with alive or dead cells.
    /// </summary>
    /// <param name="rows">The number of rows in the grid.</param>
    /// <param name="columns">The number of columns in the grid.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the number of rows or columns is less than or equal to 0.</exception>
    public GameOfLifeParallelVersion(int rows, int columns)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(rows, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(columns, 1);

        this.numberOfRows = rows;
        this.numberOfColumns = columns;

        bool[,] constructorGrid = new bool[rows, columns];

        Random random = new();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                constructorGrid[i, j] = random.Next(2) == 0;
            }
        }

        this.initialGrid = CopyGrid(constructorGrid);
        this.currentGrid = CopyGrid(constructorGrid);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GameOfLifeParallelVersion"/> class with the given grid.
    /// </summary>
    /// <param name="grid">The 2D array representing the initial state of the grid.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <param name="grid"/> is null.</exception>
    public GameOfLifeParallelVersion(bool[,] grid)
    {
        ArgumentNullException.ThrowIfNull(grid);

        this.numberOfColumns = grid.GetLength(1);
        this.numberOfRows = grid.GetLength(0);

        this.initialGrid = CopyGrid(grid);
        this.currentGrid = CopyGrid(grid);
    }

    /// <summary>
    /// Gets the current generation grid as a separate copy.
    /// </summary>
    public bool[,] CurrentGeneration
    {
        get
        {
            return CopyGrid(this.currentGrid);
        }
    }

    /// <summary>
    /// Gets the current generation number.
    /// </summary>
    public int Generation { get; private set; }

    /// <summary>
    /// Restarts the game by resetting the current grid to the initial state.
    /// </summary>
    public void Restart()
    {
        this.currentGrid = CopyGrid(this.initialGrid);
        this.Generation = 0;
    }

    /// <summary>
    /// Advances the game to the next generation based on the rules of Conway's Game of Life.
    /// </summary>
    public void NextGeneration()
    {
        bool[,] newGrid = new bool[this.numberOfRows, this.numberOfColumns];

        Parallel.For(0, this.numberOfRows, i =>
        {
            for (int j = 0; j < this.numberOfColumns; j++)
            {
                bool isAlive = this.currentGrid[i, j];
                int neighbors = this.CountAliveNeighbors(i, j);

                if (isAlive)
                {
                    newGrid[i, j] = neighbors == 2 || neighbors == 3;
                }
                else
                {
                    newGrid[i, j] = neighbors == 3;
                }
            }
        });

        this.currentGrid = newGrid;
        this.Generation++;
    }

    private static bool[,] CopyGrid(bool[,] source)
    {
        int rows = source.GetLength(0);
        int cols = source.GetLength(1);

        var copy = new bool[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                copy[i, j] = source[i, j];
            }
        }

        return copy;
    }

    /// <summary>
    /// Counts the number of alive neighbors for a given cell in the grid.
    /// </summary>
    /// <param name="row">The row index of the cell.</param>
    /// <param name="column">The column index of the cell.</param>
    /// <returns>The number of alive neighbors for the specified cell.</returns>
    private int CountAliveNeighbors(int row, int column)
    {
        int aliveCount = 0;

        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = column - 1; j <= column + 1; j++)
            {
                if (i < 0 || j < 0 || i >= this.numberOfRows || j >= this.numberOfColumns)
                {
                    continue;
                }

                if (i == row && j == column)
                {
                    continue;
                }

                if (this.currentGrid[i, j])
                {
                    aliveCount++;
                }
            }
        }

        return aliveCount;
    }
}
