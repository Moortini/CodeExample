using Cats;
using Profile;
using UnityEngine;

namespace UI.Windows.Cell
{
    public class СleanedСellUIController : ICellUIController
    {
        private IAwardWindow awardWindow;
        private IGold gold;
        private ICatsOnCell cats;
        ICellUIBuilder builder;

        public СleanedСellUIController(IAwardWindow awardWindow, IGold gold, ICatsOnCell cats, ICellUIBuilder builder)
        {
            this.awardWindow = awardWindow;
            this.gold = gold;
            this.cats = cats;
            this.builder = builder;
        }

        public void UpdateUI(Vector2 cellPosition)
        {
            awardWindow.Open(cellPosition);
            awardWindow.SetOnOkButtonAction(OnGetAward);
        }

        private void OnGetAward()
        {
            gold.AddGold(1000);
            cats.FreeAllCats();
            builder.ResetCell();
        }
    }
}