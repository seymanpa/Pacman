using UnityEngine;

namespace PacMan
{
    public class GhostFollow : GhostState
    {
        private void OnDisable()
        {
            this.ghost.randomMovement.Enable();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Node node = collision.GetComponent<Node>();

            if (node != null && enabled && !this.ghost.scared.enabled)
            {
                Vector2 direction = Vector2.zero;
                float minDistance = float.MaxValue;

                foreach (Vector2 possibleDirection in node.possibleDirections)
                {
                    Vector3 newPosition = this.transform.position + new Vector3(possibleDirection.x, possibleDirection.y, 0f);
                    float distance = (this.ghost.pacman.position - newPosition).sqrMagnitude;
                    if (distance < minDistance)
                    {
                        direction = possibleDirection;
                        minDistance = distance;
                    }
                }

                this.ghost.movement.SetDirection(direction);
            }
        }

    }
}

