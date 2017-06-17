using System.Collections;

public class Board : IEnumerator, IEnumerable
{

    private BoardPositionChoice[] gameboard;
    int boardPosition = -1;

    public Board(int size)
    {
        gameboard = new BoardPositionChoice[size];
    }

    public IEnumerator GetEnumerator()
    {
        return (IEnumerator) this;
    }

    public bool MoveNext()
    {
        boardPosition++;
        return boardPosition < gameboard.Length;
    }

    public bool isBoardPositionEmpty()
    {
        return gameboard[boardPosition] == 0;
    }

    public void Reset()
    {
        boardPosition = 0;
    }

    public object Current
    {
        get { return gameboard[boardPosition]; }
    }
}