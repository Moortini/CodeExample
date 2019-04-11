using UI.Windows.Cell;

namespace Cells
{
    public interface ICell
    {
        bool isEmpty { get; }
        int priority { get; }
        int maxMousesCount { get; }

        ICellUIBuilder uiBuilder { get; }

        void Fill();
        void SetIsFilled();
        void SetEmpty();
        void SetFree();
    }

    public class Cell : ICell
    {
        private ICellBehaviour cellBehaviour;
        private ICellData cellData;

        public int priority
        {
            get { return cellData.priority; }
        }

        public bool isEmpty { get; private set; }

        public int maxMousesCount
        {
            get { return cellData.maxMousesCount; }
        }

        public ICellUIBuilder uiBuilder
        {
            get { return cellBehaviour.builder; }
        }

        public Cell(ICellBehaviour cellBehaviour, ICellData cellData)
        {
            this.cellBehaviour = cellBehaviour;
            this.cellData = cellData;
            uiBuilder.Add(cellData);
            isEmpty = true;
        }

        public void Fill()
        {
            isEmpty = false;
            cellBehaviour.VisualizeSpawn(!isEmpty);
        }

        public void SetIsFilled()
        {
            cellBehaviour.VisualizeIsFilled();
        }

        public void SetEmpty()
        {
            isEmpty = true;
            cellBehaviour.VisualizeDeath();
        }
        
        public void SetFree()
        {
            cellBehaviour.VisualizeSpawn(!isEmpty);
        }
    }
}