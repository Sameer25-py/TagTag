using TMPro;
using UnityEngine;

namespace UI
{
    public class RoundText : MonoBehaviour
    {
        public TMP_Text Text;

        public void SetRound(int round)
        {
            Text.text = "Round: " + round;
        }
    }
}