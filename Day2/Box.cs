readonly struct Box
{
    public int Length { get; }
    public int Width { get; }
    public int Height { get; }

    public int Square { get; }
    public int SquareRibbon { get; }

    public Box(int length, int width, int height)
    {
        Length = length;
        Width = width;
        Height = height;

        int lengthByWidth = Length * Width;
        int widthByHeight = Width * Height;
        int heightByLength = Height * Length;

        int lengthPlusWidthPlusHeight = Length + Width + Height;
        int lengthByWidthByHeight = lengthByWidth * Height;

        int smallestSide = MinOf(lengthByWidth, widthByHeight, heightByLength);
        int biggestDimension = MaxOf(Length, Width, Height);

        Square = 2 * (lengthByWidth + widthByHeight + heightByLength) + smallestSide;
        SquareRibbon = 2 * (lengthPlusWidthPlusHeight - biggestDimension) + lengthByWidthByHeight;

        static int MinOf(params int[] values) =>
            values.Min();

        static int MaxOf(params int[] values) =>
            values.Max();
    }

    public override string ToString() =>
        $"{Length}x{Width}x{Height} | {Square} | {SquareRibbon}";
}
