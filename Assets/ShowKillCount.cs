using UnityEngine;
using TMPro; // Chỉ cần nếu bạn dùng TextMeshPro

public class ShowKillCount : MonoBehaviour
{
    public TMP_Text killText; // Kéo UI Text vào đây

    void Start()
    {
        int kills = PlayerPrefs.GetInt("KillCount", 0);
        killText.text = "Bạn đã tiêu diệt: " + kills + " quái vật!";
    }
}
