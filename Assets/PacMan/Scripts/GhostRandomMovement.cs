using UnityEngine;

namespace PacMan
{
    public class GhostRandomMovement : GhostState
    {
        private void OnDisable()
        {
            this.ghost.follow.Enable();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Node node = collision.GetComponent<Node>();

            if (node != null && enabled && !this.ghost.scared.enabled)
            {
                int index = Random.Range(0, node.possibleDirections.Count);

                if (node.possibleDirections[index] == -this.ghost.movement.direction && node.possibleDirections.Count > 1)
                {
                    index++;

                    if (index >= node.possibleDirections.Count)
                        index = 0;
                }

                this.ghost.movement.SetDirection(node.possibleDirections[index]);
            }
        }
    }
}

