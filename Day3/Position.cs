struct Position
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Position(int x = 0, int y = 0)
    {
        X = x;
        Y = y;
    }

    public void MoveUp() => ++Y;
    public void MoveRight() => ++X;
    public void MoveDown() => --Y;
    public void MoveLeft() => --X;

    public override readonly string ToString() =>
        $"({X}; {Y})";
}
