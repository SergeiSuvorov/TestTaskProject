using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace View
{
    public sealed class GameInputView : MonoBehaviour
    {
        [SerializeField] private Button _openInventoryButton;
        [SerializeField] private Button _buttonBackToMainMenu;
        [SerializeField] private Button _shootButton;
        [SerializeField] private TMP_Text _ammoInfo;
        [SerializeField] private VariableJoystick joystick;

        public Button OpenInventoryButton => _openInventoryButton;
        public Button ButtonBackToMainMenu => _buttonBackToMainMenu;
        public Button ShootButton => _shootButton;
        public VariableJoystick Joystick=> joystick;
        public TMP_Text AmmoInfo=> _ammoInfo;

    }
}