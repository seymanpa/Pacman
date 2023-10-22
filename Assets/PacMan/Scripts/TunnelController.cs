using UnityEngine;

namespace PacMan
{
    public class TunnelController : MonoBehaviour
    {
        [SerializeField]
        private Transform connection;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Vector3 newPosition = this.connection.position;
            collision.transform.position = newPosition;
        }
    }
}

