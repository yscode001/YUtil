// Author：yaoshuai
// Email：yscode@126.com
// Date：2024-5-29
// ------------------------------
using System;
using System.Collections.Generic;

namespace YGame.BlockPuzzle
{
    public partial class Shape
    {
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }
        public int BlockCount { get; private set; }
        public Coordinate[] BlockArray { get; private set; }
        public FillType[] FillTypes { get; private set; }

        public int PutonRowMaxIdx { get; private set; }
        public int PutonYMax { get; private set; }
        public int PutonColMaxIdx { get; private set; }
        public int PutonXMax { get; private set; }

        private readonly ShapePreview ShapePreview = new ShapePreview();

        public Shape(Coordinate[] blockArray, FillType[] fillTypes)
        {
            RowCount = ColCount = BlockCount = 0;
            BlockArray = null;
            PutonRowMaxIdx = PutonYMax = PutonColMaxIdx = PutonXMax = 0;
            Init(blockArray);
            SetupFillTypes(fillTypes);
        }
        public Shape(Coordinate[] blockArray, List<Coordinate> excepts)
        {
            RowCount = ColCount = BlockCount = 0;
            BlockArray = null;
            PutonRowMaxIdx = PutonYMax = PutonColMaxIdx = PutonXMax = 0;
            if (excepts == null || excepts.Count == 0)
            {
                Init(blockArray);
            }
            else
            {
                if (blockArray == null || blockArray.Length == 0)
                {
                    throw new Exception("error");
                }
                List<Coordinate> list = new List<Coordinate>();
                foreach (var block in blockArray)
                {
                    bool exceptContains = false;
                    foreach (var exceptBlock in excepts)
                    {
                        if (exceptBlock.EqualCoordinate(block))
                        {
                            exceptContains = true;
                            break;
                        }
                    }
                    if (!exceptContains)
                    {
                        list.Add(block);
                    }
                }
                Init(list.ToArray());
            }
        }

        private void Init(Coordinate[] blockArray)
        {
            if (blockArray == null || blockArray.Length == 0)
            {
                throw new Exception("error");
            }

            BlockCount = blockArray.Length;
            BlockArray = new Coordinate[BlockCount];
            int minX = int.MaxValue;
            int maxX = int.MinValue;
            int minY = int.MaxValue;
            int maxY = int.MinValue;

            for (int i = 0; i < BlockCount; i++)
            {
                Coordinate coordinate = blockArray[i];
                BlockArray[i] = coordinate;

                if (minX > coordinate.X) { minX = coordinate.X; }
                if (maxX < coordinate.X) { maxX = coordinate.X; }
                if (minY > coordinate.Y) { minY = coordinate.Y; }
                if (maxY < coordinate.Y) { maxY = coordinate.Y; }
            }

            RowCount = maxY - minY + 1;
            ColCount = maxX - minX + 1;
            PutonYMax = PutonRowMaxIdx = ChessBoard.RowCount - RowCount;
            PutonXMax = PutonColMaxIdx = ChessBoard.ColCount - ColCount;
        }

        public void SetupFillTypes(FillType[] fillTypes)
        {
            if (fillTypes == null || fillTypes.Length != BlockArray.Length)
            {
                throw new Exception("error");
            }
            FillTypes = fillTypes;
        }
    }
    public partial class Shape
    {
        public ShapePreview PreviewRowCol(int startRowIdx, int startColIdx)
        {
            ShapePreview.SetupData(startRowIdx, startColIdx, this);
            return ShapePreview;
        }
        public ShapePreview PreviewXY(int startX, int startY)
        {
            ShapePreview.SetupData(startY, startX, this);
            return ShapePreview;
        }
    }
    public partial class Shape
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