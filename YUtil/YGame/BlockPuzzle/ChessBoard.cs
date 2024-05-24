// Author：yaoshuai
// Email：yscode@126.com
// Date：2024-5-24
// ------------------------------
namespace YGame.BlockPuzzle
{
    public partial class ChessBoard
    {
        public static int RowCount { get; private set; }
        public static int RowMaxIdx { get; private set; }
        public static int ColCount { get; private set; }
        public static int ColMaxIdx { get; private set; }

        public static int[] RowIdxArray { get; private set; }
        public static int[] ColIdxArray { get; private set; }

        public static (int x, int y)[] SubScriptXY { get; private set; }
        public static (int rowIdx, int colIdx)[] SubScriptRowCol { get; private set; }

        public static Block[,] Panel { get; private set; }

        public static void Init(int rowCount, int colCount)
        {
            if (rowCount <= 0 || colCount <= 0)
            {
                throw new System.Exception("error");
            }

            BlockPuzzleTool.Init(rowCount, colCount);

            RowCount = rowCount;
            RowMaxIdx = rowCount - 1;
            ColCount = colCount;
            ColMaxIdx = colCount - 1;

            if (RowIdxArray == null || RowIdxArray.Length != rowCount)
            {
                RowIdxArray = new int[rowCount];
                for (int rowIdx = 0; rowIdx <= RowMaxIdx; rowIdx++)
                {
                    RowIdxArray[rowIdx] = rowIdx;
                }
            }
            if (ColIdxArray == null || ColIdxArray.Length != colCount)
            {
                ColIdxArray = new int[colCount];
                for (int colIdx = 0; colIdx <= ColMaxIdx; colIdx++)
                {
                    ColIdxArray[colIdx] = colIdx;
                }
            }

            if (SubScriptXY == null || SubScriptXY.Length != rowCount * colCount)
            {
                SubScriptXY = new (int x, int y)[rowCount * colCount];
                for (int rowIdx = 0; rowIdx <= RowMaxIdx; rowIdx++)
                {
                    for (int colIdx = 0; colIdx <= ColMaxIdx; colIdx++)
                    {
                        SubScriptXY[colIdx + rowIdx * colCount] = (colIdx, rowIdx);
                    }
                }
            }
            if (SubScriptRowCol == null || SubScriptRowCol.Length != rowCount * colCount)
            {
                SubScriptRowCol = new (int rowIdx, int colIdx)[rowCount * colCount];
                for (int rowIdx = 0; rowIdx <= RowMaxIdx; rowIdx++)
                {
                    for (int colIdx = 0; colIdx <= ColMaxIdx; colIdx++)
                    {
                        SubScriptRowCol[colIdx + rowIdx * colCount] = (rowIdx, colIdx);
                    }
                }
            }

            if (Panel == null || Panel.GetLength(0) * Panel.GetLength(1) != rowCount * colCount)
            {
                Panel = new Block[rowCount, colCount];
                for (int rowIdx = 0; rowIdx <= RowMaxIdx; rowIdx++)
                {
                    for (int colIdx = 0; colIdx <= ColMaxIdx; colIdx++)
                    {
                        Panel[rowIdx, colIdx] = new Block();
                    }
                }
            }
            for (int rowIdx = 0; rowIdx <= RowMaxIdx; rowIdx++)
            {
                for (int colIdx = 0; colIdx <= ColMaxIdx; colIdx++)
                {
                    Panel[rowIdx, colIdx].SetupFillData(FillType.Empty, FillState.Normal);
                }
            }
        }
    }
    public partial class ChessBoard
    {
        public static Block GetBlock(int rowIdx, int colIdx)
        {
            return Panel[rowIdx, colIdx];
        }
        public static Block[] GetRow(int rowIdx)
        {
            Block[] data = new Block[ColCount];
            foreach (var colIdx in ColIdxArray)
            {
                data[colIdx] = Panel[rowIdx, colIdx];
            }
            return data;
        }
        public static Block[] GetCol(int colIdx)
        {
            Block[] data = new Block[RowCount];
            foreach (var rowIdx in RowIdxArray)
            {
                data[rowIdx] = Panel[rowIdx, colIdx];
            }
            return data;
        }
    }
    public partial class ChessBoard
    {
        public static void SetupFillType(int rowIdx, int colIdx, FillType fillType)
        {
            Panel[rowIdx, colIdx].SetupFillType(fillType);
        }
        public static void SetupState(int rowIdx, int colIdx, FillState fillState)
        {
            Panel[rowIdx, colIdx].SetupFillState(fillState);
        }
        public static void SetupFillData(int rowIdx, int colIdx, FillType fillType, FillState fillState)
        {
            Panel[rowIdx, colIdx].SetupFillData(fillType, fillState);
        }
    }
}