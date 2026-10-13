using UnityEngine;

public class Jump_boost : MonoBehaviour
{
    [SerializeField] float duration = 10f;

    bool collected;

    void OnTriggerEnter(Collider other)
    {
        TryCollect(other);
    }

    void OnCollisionEnter(Collision collision)
    {
        TryCollect(collision.collider);
    }

    void TryCollect(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (collected || player == null)
        {
            return;
        }

        collected = true;
        player.ActivateJumpBoost(duration);
        Destroy(gameObject);
    }
}
