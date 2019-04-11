using Cells;
using UnityEngine;

namespace UI.Windows.Cell
{
    public interface ICellUIController
    {
        void UpdateUI(Vector2 cellPosition);
    }

    public abstract class CellUIController : ICellUIController
    {
        protected ICellData cellData;
        protected ICellWindow cellWindow;

        public CellUIController(ICellData cellData, ICellWindow cellWindow)
        {
            this.cellData = cellData;
            this.cellWindow = cellWindow;
        }

        public void UpdateUI(Vector2 cellPosition)
        {
            cellWindow.Open(cellPosition);
            SetBaseCellInfo();
            SetMouseOnCellInfo();
            SetCatsOnCell();
        }

        protected void SetBaseCellInfo()
        {
            cellWindow.SetCellData(cellData);
        }

        protected abstract void SetMouseOnCellInfo();
        protected abstract void SetCatsOnCell();
    }
}