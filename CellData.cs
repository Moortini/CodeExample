using UnityEngine;

namespace Cells
{
    public enum CellDifficultEnum
    {
        Low,
        Middle,
        High
    }

    public interface ICellData
    {
        string cellName { get; }
        Sprite icon { get; }
        int priority { get; }
        ICellDifficult difficult { get; }
        int maxMousesCount { get; }
    }

    [CreateAssetMenu(fileName = "CellData", menuName = "ScriptableObjects/Cells/CellData")]
    public class CellData : ScriptableObject, ICellData
    {
        public string cellName
        {
            get { return name; }
        }

        [SerializeField]
        private Sprite _icon;
        public Sprite icon
        {
            get { return _icon; }
        }

        [SerializeField]
        private int _priority;
        public int priority
        {
            get { return _priority; }
        }

        [SerializeField]
        private CellDifficultEnum _difficult;
        public ICellDifficult difficult
        {
            get { return CellDifficultFabric.Create(_difficult); }
        }

        [SerializeField]
        private int _maxMousesCount;
        public int maxMousesCount
        {
            get { return _maxMousesCount; }
        }
    }
}
