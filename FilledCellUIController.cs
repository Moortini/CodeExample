using Cats;
using Cells;
using Mouses;
using UI.Windows.CatList;

namespace UI.Windows.Cell
{
    public class FilledCellUIController : CellUIController
    {
        private IMousePopulation population;
        private IMouseData mouse;
        private ICatsOnCell catsOnCell;
        private ICatsFabric catsFabric;
        private ICatListWindow catList;

        public FilledCellUIController(ICellData cellData, ICellWindow cellWindow, IMousePopulation population, IMouseData mouse, ICatsOnCell catsOnCell,
            ICatsFabric catsFabric, ICatListWindow catList)
            : base(cellData, cellWindow)
        {
            this.population = population;
            this.mouse = mouse;
            this.catsOnCell = catsOnCell;
            this.catsFabric = catsFabric;
            this.catList = catList;
        }

        protected override void SetMouseOnCellInfo()
        {
            cellWindow.SetMousePopulation(population.Get().ToString());
            cellWindow.ShowMouse(true);
            cellWindow.SetMouseData(mouse);
        }

        protected override void SetCatsOnCell()
        {
            if (!population.isFull)
                catList.SetAddButtonActive(true);
            cellWindow.SetCatsInfo(catsOnCell.GetCats());
            cellWindow.SetActionOnTryAddCat(OnTryAddCat);
            cellWindow.SetActionOnDeleteCat(OnDeleteCat);
            cellWindow.SetActionOnClose(OnClose);
        }

        private void OnTryAddCat(ICatData catData)
        {
            if (!catsOnCell.isFull)
                OnAddCat(catData);
        }
        
        private void OnAddCat(ICatData catData)
        {
            ICat cat = catsFabric.Create(catData, mouse.speed, cellData.difficult.coefficient);
            catsOnCell.AddCat(cat);
            catData.SetFree(false);
            cellWindow.SetCatsInfo(catsOnCell.GetCats());            
        }

        private void OnDeleteCat(ICatData catData)
        {
            catsOnCell.RemoveCat(catData);
            catData.SetFree(true);
        }

        private void OnClose()
        {
            if (!population.isFull)
                catList.SetAddButtonActive(false);
        }
    }
}
