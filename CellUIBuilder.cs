using Cats;
using Cells;
using Mouses;
using Profile;
using UI.WindowManager;
using UI.Windows.CatList;

namespace UI.Windows.Cell
{
    public interface ICellUIBuilder
    {
        void Add(ICellData cellData);
        void Add(IMousePopulation population);
        void Add(IMouseData mouse);
        void Add(ICatsOnCell cats);
        void Add(IGold gold);
        void ResetCell();
        ICellUIController Build();
    }

    public class CellUIBuilder : ICellUIBuilder
    {
        private ICellData cellData;
        private IMousePopulation population;
        private IMouseData mouse;
        private ICatsOnCell cats;
        private IGold gold;

        private ICatsFabric catsFabric;

        public CellUIBuilder(ICatsFabric catsFabric)
        {
            this.catsFabric = catsFabric;
        }

        public void Add(ICellData cellData)
        {
            this.cellData = cellData;
        }

        public void Add(IMousePopulation population)
        {
            this.population = population;
        }

        public void Add(IMouseData mouse)
        {
            this.mouse = mouse;
        }

        public void Add(ICatsOnCell cats)
        {
            this.cats = cats;
        }

        public void Add(IGold gold)
        {
            this.gold = gold;
        }

        public void ResetCell()
        {
            population.DeletePopulation();
            population = null;
            mouse = null;
            cats = null;
        }

        public ICellUIController Build()
        {
            ICellWindow cellWindow = WindowsManager.Get<CellWindow>();

            if (population == null)
                return new EmptyCellUIController(cellData, cellWindow);
            else if (population.isEmpty)
            {
                IAwardWindow awardWindow = WindowsManager.Get<AwardWindow>();
                return new СleanedСellUIController(awardWindow, gold, cats, this);
            }

            ICatListWindow catList = WindowsManager.Get<CatListWindow>();
            return new FilledCellUIController(cellData, cellWindow, population, mouse, cats, catsFabric, catList);
        }
    }
}
