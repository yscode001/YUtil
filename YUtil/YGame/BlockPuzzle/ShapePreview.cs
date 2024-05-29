// Author：yaoshuai
// Email：yscode@126.com
// Date：2024-5-29
// ------------------------------
namespace YGame.BlockPuzzle
{
    public partial class ShapePreview
    {
        public int BlockCount { get; private set; }
        public Coordinate[] BlockArray { get; private set; }
        public FillType[] FillTypes { get; private set; }

        public int RowCount { get; private set; }
        public int RowMinIdx { get; private set; }
        public int RowMaxIdx { get; private set; }

        public int ColCount { get; private set; }
        public int ColMinIdx { get; private set; }
        public int ColMaxIdx { get; private set; }

        internal ShapePreview() { }
        internal void SetupData(int startRowIdx, int startColIdx, Shape shape)
        {
            if (!BlockPuzzleTool.ValidatedRowCol(startRowIdx, startColIdx) || shape == null || shape.BlockArray == null || shape.BlockCount == 0)
            {
                throw new System.Exception("error");
            }
            if (!BlockPuzzleTool.ValidatedRowCol(startRowIdx + shape.RowCount - 1, startColIdx + shape.ColCount - 1))
            {
                throw new System.Exception("error");
            }
            BlockCount = shape.BlockCount;
            BlockArray = new Coordinate[BlockCount];
            FillTypes = new FillType[BlockCount];

            RowCount = shape.RowCount;
            RowMinIdx = startRowIdx;
            RowMaxIdx = startRowIdx + RowCount - 1;

            ColCount = shape.ColCount;
            ColMinIdx = startColIdx;
            ColMaxIdx = startColIdx + ColCount - 1;

            for (int i = 0; i < BlockCount; i++)
            {
                BlockArray[i] = new Coordinate(startColIdx + shape.BlockArray[i].X, startRowIdx + shape.BlockArray[i].Y);
                FillTypes[i] = shape.FillTypes[i];
            }
        }
    }
    public partial class ShapePreview
    {
        public bool BlockContainsXY(int x, int y)
        {
            foreach (var coordinate in BlockArray)
            {
                if (coordinate.EqualXY(x, y))
                {
                    return true;
                }
            }
            return false;
        }
        public bool BlockContainsRowCol(int rowIndex, int colIndex)
        {
            foreach (var coordinate in BlockArray)
            {
                if (coordinate.EqualRowCol(rowIndex, colIndex))
                {
                    return true;
                }
            }
            return false;
        }
        public bool BlockContainsCoordinate(Coordinate block)
        {
            foreach (var coordinate in BlockArray)
            {
                if (coordinate.EqualCoordinate(block))
                {
                    return true;
                }
            }
            return false;
        }
    }
}