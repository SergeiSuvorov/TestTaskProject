using Interface;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class HealthPanelView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TMP_Text _enemyNameText;

        public Slider Slider => _healthSlider;
        public TMP_Text NameText => _enemyNameText;
    }
}

