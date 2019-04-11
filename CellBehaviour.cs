using Cats;
using UI.Windows.Cell;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cells
{
    public interface ICellBehaviour
    { 
        ICellUIBuilder builder { get; }
        void VisualizeSpawn(bool isActive);
        void VisualizeIsFilled();
        void VisualizeDeath();
    }

    public class CellBehaviour : MonoBehaviour, ICellBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private GameObject mouseIcon;

        public ICellUIBuilder builder { get; set; }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            builder.Build().UpdateUI(transform.position);
        }

        /**
         * Shows mouse icon when mouse is spawned. 
         */
        public void VisualizeSpawn(bool isActive)
        {
            mouseIcon.SetActive(isActive);
            mouseIcon.transform.Rotate(new Vector3(0, 0, 0));
            SetBigMouse(false);
        }

        public void VisualizeIsFilled()
        {
            SetBigMouse(true);
        }

        private void SetBigMouse(bool setBig)
        {
            float scale;
            if (setBig)
                scale = 0.5f;
            else
                scale = 0.3f;
            Vector3 icon = mouseIcon.transform.localScale;
            icon.x = icon.y = scale;
            mouseIcon.transform.localScale = icon;
        }

        public void VisualizeDeath()
        {
            mouseIcon.transform.Rotate(new Vector3(0, 0, 180f));
        }

        public void CreateUIBuilder(ICatsFabric catsFabric)
        {
            builder = new CellUIBuilder(catsFabric);
        }
    }
}
