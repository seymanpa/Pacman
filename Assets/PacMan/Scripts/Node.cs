using System.Collections.Generic;
using UnityEngine;

namespace PacMan
{
    public class Node : MonoBehaviour
    {
        public List<Vector2> possibleDirections { get; private set; }
        [SerializeField]
        private LayerMask _wallLayer;

        private void Start()
        {
            this.possibleDirections = new List<Vector2>();
            CheckPossibleDirection(Vector2.up);
            CheckPossibleDirection(Vector2.down);
            CheckPossibleDirection(Vector2.left);
            CheckPossibleDirection(Vector2.right);
        }

        private void CheckPossibleDirection(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, this._wallLayer);
            if (hit.collider == null)
                this.possibleDirections.Add(direction);
        }
    }
}

