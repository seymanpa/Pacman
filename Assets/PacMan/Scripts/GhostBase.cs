using System.Collections;
using UnityEngine;

namespace PacMan
{
    public class GhostBase : GhostState
    {
        public Transform inside;
        public Transform outside;

        private void OnEnable()
        {
            StopAllCoroutines();
        }

        private void OnDisable()
        {
            if(this.gameObject.activeSelf)
            {
                StartCoroutine(ExidTansition());
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(enabled && collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                this.ghost.movement.SetDirection(-this.ghost.movement.direction);
            }
        }

        private IEnumerator ExidTansition()
        {
            this.ghost.movement.SetDirection(Vector2.up, true);
            this.ghost.movement.rigidbody.isKinematic = true;
            this.ghost.movement.enabled = false;

            Vector3 position = this.transform.position;

            float duration = 0.5f;
            float elapsed = 0.0f;

            while (elapsed < duration)
            {
                this.ghost.SetPosition(Vector3.Lerp(position, this.inside.position, elapsed / duration));
                elapsed += Time.deltaTime;
                yield return null;
            }

            elapsed = 0.0f;

            while (elapsed < duration)
            {
                this.ghost.SetPosition(Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration));
                elapsed += Time.deltaTime;
                yield return null;
            }

            this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
            this.ghost.movement.rigidbody.isKinematic = false;
            this.ghost.movement.enabled = true;
        }
    }
}

