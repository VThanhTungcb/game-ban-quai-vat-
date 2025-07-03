using TMPro;
using UnityEngine;

public class KillDisplay : MonoBehaviour
{
    public TMP_Text killText;

    void Update()
    {
        killText.text = "Giết : " + Player.enemiesKilled;
    }
}
