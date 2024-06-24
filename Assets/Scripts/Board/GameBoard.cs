using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_categoryText;
    [SerializeField] private GameTile m_tilePrefab;
    [SerializeField] private int[] m_rows = { 12, 14, 14, 12 };

    [SerializeField] private Sprite m_letterOpen;
    [SerializeField] private Sprite m_letterClosed;

    private List<List<GameTile>> m_tiles;

    private bool m_initalized = false;

    private List<GameTile> solvingTiles = new List<GameTile>();

    public void SolveLetter(char letter)
    {
        foreach(var row in m_tiles) 
        {
            foreach(var tile in row)
            {
                if (tile.State == GameTile.TileState.open)
                {
                    tile.Solve(letter.ToString());
                    solvingTiles.Add(tile);
                    return;
                }
            }
        }
    }

    public void UndoLetter()
    {
        solvingTiles.Last().ResetSolve();
        solvingTiles.RemoveAt(solvingTiles.Count - 1);
    }

    public void StopSolve()
    {
        foreach (var tile in solvingTiles)
        {
            tile.ResetSolve();
        }
    }

    public bool IsBoardCorrect()
    {
        foreach (var row in m_tiles)
        {
            foreach (var tile in row)
            {
                if (!tile.IsCorrect())
                {
                    Debug.Log("Tile " + tile.gameObject.name + " was incorrect");
                    return false;
                }
            }
        }
        return true;
    }

    public void InitializeBoard()
    {
        if (m_tiles == null)
        {
            m_tiles = CreateBoard(m_tilePrefab, m_rows);
        }

        foreach(var row in m_tiles)
        {
            foreach(var tile in row)
            {
                tile.Initialize(m_letterClosed, m_letterOpen);
                tile.Close();
            }
        }

        m_initalized = true;
    }

    public void ResetBoard()
    {
        foreach (var row in m_tiles)
        {
            foreach (var tile in row)
            {
                tile.Close();
            }
        }
    }

    public void SetSolution(string category, string solution)
    {
        int maxRows = m_tiles.Count;
        m_categoryText.text = category;

        string[] tempSolution = new string[maxRows];
        int[] tempRowCount = new int[maxRows];
        m_rows.CopyTo(tempRowCount, 0);

        int currentRow = 0;

        bool firstWord = true;
        var words = solution.Split(' ');
        for (int index = 0; index < words.Length; index++)
        {
            string word = words[index];
            if (tempRowCount[currentRow] >= word.Length)
            {
                if (firstWord)
                {
                    firstWord = false;
                }
                else
                {
                    tempSolution[currentRow] += " ";
                }

                tempSolution[currentRow] += word;
                tempRowCount[currentRow] -=(word.Length + 1);
            }
            else
            {
                index--;
                currentRow++;

                if (currentRow >= maxRows)
                {
                    throw new System.IndexOutOfRangeException("Solution does not fit in board");
                }

                firstWord = true;
            }
        }

        //Center the solution
        int leftoverRows = maxRows - currentRow - 1; // 0 based so - 1 is necessary
        for (int shiftAmount = 0; shiftAmount < (leftoverRows / 2); shiftAmount++) 
        {
            if (tempSolution[shiftAmount] == null) //Skip if rows are null, already centered cause of size issues
                continue;

            if (CanShiftDown(tempSolution, m_rows))
                ShiftDown(tempSolution);
            else
                break;
        }

        //Add to the board
        for (int index = 0; index < tempSolution.Length; index++)
        {
            string row = tempSolution[index];
            if (row == null)
                continue;

            int leftoverSpaces = ((m_rows[index] - row.Length) / 2);

            for(int letterIndex = 0; letterIndex < row.Length; letterIndex++)
            {
                char letter = row[letterIndex];
                int boardPos = letterIndex + leftoverSpaces;

                if (letter == ' ')
                {
                    continue;
                }

                m_tiles[index][boardPos].SetLetter("" + letter);

                if (char.IsPunctuation(letter))
                {
                    m_tiles[index][boardPos].Reveal();
                }
                
            }
        }
    }

    public int OpenLetter(string letter)
    {
        int foundCount = 0;
        foreach(var row in m_tiles)
        {
            foreach(var tile in row)
            {
                if (tile.IsLetter(letter))
                {
                    foundCount++;
                    tile.Reveal();
                }
            }
        }
        return foundCount;
    }

    List<List<GameTile>> CreateBoard(GameTile tile, int[] rows)
    {
        //float tileWidth = tile.GetWidth();
        //float tileHeight = tile.GetHeight();

        float tileWidth = 1.0f;
        float tileHeight = 1.0f + (1f / 3f);

        Vector2 referencePos = transform.position;
        int rowCenter = rows.Length / 2; //TODO: Support uneven row count (maybe?)

        List<List<GameTile>> retVal = new List<List<GameTile>>();

        for (int row = 0; row < rows.Length; row++)
        {
            List<GameTile> rowCollection = new List<GameTile>();
            var columnWidth = rows[row]; //TODO: Support uneven row sizes (maybe?)
            if (columnWidth % 2 != 0)
            {
                Debug.LogError("Columnwidth is not even. Unsupported");
            }

            
            int columnCenter = columnWidth / 2 - 1;
            float yPos = ((rowCenter - row) * tileHeight) - tileHeight / 2;
            for (int column = 0; column < columnWidth; column++)
            {
                float xPos = ((-columnCenter + column) * tileWidth) - tileWidth / 2;
                string tileName = "Tile [" + row + "," + column + "]";
                GameTile temp = GameObject.Instantiate(m_tilePrefab,
                    this.transform) 
                    as GameTile;
                temp.transform.localPosition = new Vector2(xPos, yPos);
                temp.transform.name = tileName;
                rowCollection.Add(temp);
            }
            retVal.Add(rowCollection);
        }

        return retVal;
    }

    bool CanShiftDown(string[] solution, int[] sizes)
    {
        Debug.Assert(solution.Length == sizes.Length);

        for (int index = 0; index < solution.Length; index++)
        {
            string row = solution[index];

            if (index == sizes.Length - 1)
            {
                if (row == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            int length = row == null ? 0 : row.Length;

            if (length > sizes[index + 1])
            {
                return false;
            }
        }
        return true; 
    }

    void ShiftDown(string[] solution)
    {
        for (int index = solution.Length - 1; index > 0; index--)
        {
            solution[index] = solution[index - 1];
            solution[index - 1] = null;
        }
    }
}
