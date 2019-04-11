using Cells;

namespace UI.Windows.Cell
{
    public class EmptyCellUIController : CellUIController
    {
        public EmptyCellUIController(ICellData cellData, ICellWindow cellWindow)
            : base(cellData, cellWindow)
        {
        }

        protected override void SetMouseOnCellInfo()
        {
            cellWindow.SetMousePopulation("0");
            cellWindow.ShowMouse(false);
        }

        protected override void SetCatsOnCell()
        {
            cellWindow.ResetCats();
        }
    }
}
