using UnityEngine;

public class RestartZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out var player))
        {
            player.transform.position = Vector3.zero;
        }
    }
}
