using Cats;
using Cells;
using DG.Tweening;
using Mouses;
using System;
using System.Collections.Generic;
using TMPro;
using UI.WindowManager;
using UI.Windows.CatList;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Cell
{
    public interface ICellWindow
    {
        void Open(Vector2 position);
        void SetCellData(ICellData data);
        void SetMousePopulation(string populationString);
        void ShowMouse(bool isShown);
        void SetMouseData(IMouseData data);
        void SetCatsInfo(List<ICatData> cats);
        void ResetCats();
        void SetActionOnDeleteCat(Action<ICatData> onDeleteCat);
        void SetActionOnTryAddCat(Action<ICatData> onTryAddCat);
        void SetActionOnClose(Action onClose);
    }

    public class CellWindow : Window, ICellWindow
    {
        [SerializeField]
        private GameObject background;
        [SerializeField]
        private TextMeshProUGUI cellName;
        [SerializeField]
        private Image cellIcon;
        [SerializeField]
        private TextMeshProUGUI difficulty;
        [SerializeField]
        private TextMeshProUGUI quantityOfMouses;

        [SerializeField]
        private GameObject mouseInfoObject;
        [SerializeField]
        private TextMeshProUGUI mouseName;
        [SerializeField]
        private Image mouseIcon;
        [SerializeField]
        private TextMeshProUGUI mouseSpeed;
        [SerializeField]
        private TextMeshProUGUI populizationRate;

        [SerializeField]
        private List<CatSlot> catSlots;

        [SerializeField]
        private Camera mainCamera;

        private Action<ICatData> onTryAddCat;
        private Action onClose;

        public void Open(Vector2 position)
        {
            background.SetActive(true);
            gameObject.SetActive(true);
            PlayOpenAnimation(position);
        }

        private void PlayOpenAnimation(Vector3 position)
        {
            Sequence scaleSequence = DOTween.Sequence();
            Vector3 endPosition = transform.position;
            transform.position = mainCamera.WorldToScreenPoint(position);
            transform.localScale = Vector3.zero;
            scaleSequence
                .Append(transform.DOScale(0.3f, 0.5f).SetEase(Ease.OutQuad))
                .Append(transform.DOScale(1f, 0.5f).SetEase(Ease.InQuad));
            transform.DOMove(endPosition, 1f).SetEase(Ease.OutSine);
        }

        public void SetCellData(ICellData cellData)
        {
            cellName.text = cellData.cellName;
            cellIcon.sprite = cellData.icon;
            difficulty.text = cellData.difficult.uiString;
        }

        public void SetMousePopulation(string populationString)
        {
            quantityOfMouses.text = populationString;
        }

        public void ShowMouse(bool isShown)
        {
            mouseInfoObject.SetActive(isShown);
        }

        public void SetMouseData(IMouseData data)
        {
            mouseName.text = data.mouseName;
            mouseIcon.sprite = data.icon;
            mouseSpeed.text = data.speed.ToString("F1");
            populizationRate.text = data.populizationRate.ToString("F1");
        }

        public void SetCatsInfo(List<ICatData> catsData)
        {
            for (int i = 0; i < catsData.Count; i++)
                catSlots[i].SetCat(catsData[i]);
            for (int i = catsData.Count; i < catSlots.Count; i++)
                catSlots[i].ResetCatSlot();
        }

        public void ResetCats()
        {
            foreach (CatSlot slot in catSlots)
                slot.ResetCatSlot();
        }

        public void SetActionOnTryAddCat(Action<ICatData> onTryAddCat)
        {
            this.onTryAddCat = onTryAddCat;
        }

        public void SetActionOnDeleteCat(Action<ICatData> onDeleteCat)
        {
            foreach (CatSlot slot in catSlots)
                slot.SetActionOnDelete(onDeleteCat);
        }

        public void SetActionOnClose(Action onClose)
        {
            this.onClose = onClose;
        }

        public void AddCat(ICatData cat)
        {
            if (onTryAddCat != null)
                onTryAddCat.Invoke(cat);
        }

        public void Сlose()
        {
            background.SetActive(false);
            gameObject.SetActive(false);
            if (onClose != null)
                onClose.Invoke();
        }
    }
}
