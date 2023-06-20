using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace View
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private List<InventorySlotView> _inventorySlotsViews;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _removeButton;

        public Button BackButton=> _backButton;
        public Button RemoveButton=> _removeButton;
        public List<InventorySlotView> InventorySlotsViews => _inventorySlotsViews;

        public void SetActiveRemoveButton(bool ActiveState)
        {
            _removeButton.gameObject.SetActive(ActiveState);
        }

        private void OnDestroy()
        {
            _backButton.onClick?.RemoveAllListeners();
            _removeButton.onClick?.RemoveAllListeners();
        }
    }
}