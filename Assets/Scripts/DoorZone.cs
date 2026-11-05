using UnityEngine;

public class DoorZone : MonoBehaviour
{
    public CompanionFollow companion;
    public float searchRadius = 30f;

    bool triggered;

    // Enter yerine Stay: oyuncu alanın içinde başlasa bile çalışır, ama sadece 1 kez
    void OnTriggerStay(Collider other)
    {
        if (triggered) return;
        if (!companion || !companion.player) return;

        bool isPlayer = other.transform == companion.player
                     || other.transform.IsChildOf(companion.player)
                     || companion.player.IsChildOf(other.transform);
        if (!isPlayer) return;

        triggered = true;
        Debug.Log("[DoorZone] Oyuncu algılandı, companion butonu arıyor");
        companion.FindAndPress(searchRadius);
    }

    // Alandan çıkınca sıfırla, tekrar girince yeniden denenebilsin
    void OnTriggerExit(Collider other)
    {
        if (!companion || !companion.player) return;

        bool isPlayer = other.transform == companion.player
                     || other.transform.IsChildOf(companion.player)
                     || companion.player.IsChildOf(other.transform);
        if (isPlayer) triggered = false;
    }
}