using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public Health playerHealth;          
    public TextMeshProUGUI hpText;       

    void Start()
    {
        if (!playerHealth)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p) playerHealth = p.GetComponent<Health>();
        }
        if (!hpText) hpText = GetComponent<TextMeshProUGUI>();
        UpdateHP();
    }

    void Update()
    {
        UpdateHP();
    }

    void UpdateHP()
    {
        if (playerHealth && hpText)
            hpText.text = $"HP: {playerHealth.Current}/{playerHealth.max}";
    }
}
