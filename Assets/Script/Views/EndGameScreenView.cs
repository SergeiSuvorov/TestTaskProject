using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class EndGameScreenView: MonoBehaviour
    {

        [SerializeField] private Button _buttonBackToMainMenu;
        [SerializeField] private TMP_Text _winInfo;
        [SerializeField] private TMP_Text _loseInfo;
        public Button ButtonBackToMainMenu => _buttonBackToMainMenu;

        public void SeeResultText(bool isWin)
        {
            if(isWin)
                _winInfo.gameObject.SetActive(true);
            else
                _loseInfo.gameObject.SetActive(true);
        }
    }
}