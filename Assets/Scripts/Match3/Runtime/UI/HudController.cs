using TMPro;
using UnityEngine;

namespace Match3.Runtime.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text movesText;

        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

        public void UpdateHud(int score, int moves)
        {
            if (scoreText != null)
                scoreText.text = score.ToString();

            if (movesText != null)
                movesText.text = moves.ToString();
        }

        public void ShowWin()
        {
            if (winPanel != null)
                winPanel.SetActive(true);
        }

        public void ShowLose()
        {
            if (losePanel != null)
                losePanel.SetActive(true);
        }

        public void HideAll()
        {
            if (winPanel != null) winPanel.SetActive(false);
            if (losePanel != null) losePanel.SetActive(false);
        }
    }
}