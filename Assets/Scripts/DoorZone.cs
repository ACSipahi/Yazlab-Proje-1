using UnityEngine;

public class DoorZone : MonoBehaviour
{
    public CompanionFollow companion;
    public float searchRadius = 30f;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("[DoorZone] Trigger'a girdi: " + other.name);

        if (!companion) { Debug.LogError("[DoorZone] Companion alanı boş!"); return; }
        if (!companion.player) { Debug.LogError("[DoorZone] CompanionFollow'da Player alanı boş!"); return; }

        bool isPlayer = other.transform == companion.player
                     || other.transform.IsChildOf(companion.player)
                     || companion.player.IsChildOf(other.transform);

        if (!isPlayer)
        {
            Debug.LogWarning("[DoorZone] Giren obje oyuncu değil. Girenin adı: " + other.name + " / Player alanı: " + companion.player.name);
            return;
        }

        companion.FindAndPress(searchRadius);
    }
}