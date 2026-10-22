using UnityEngine;

public class SearchTrigger : MonoBehaviour
{
    public CompanionFollow companion;
    public Collider searchArea;   // boşsa bu objenin kendi trigger'ı kullanılır

    void Reset() { searchArea = GetComponent<Collider>(); }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == companion.player || other.transform.IsChildOf(companion.player))
        {
            if (!searchArea) searchArea = GetComponent<Collider>();
            companion.StartSearch(searchArea.bounds);
        }
    }
}