using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Continuous
{
    public class BarScaler
    {
        private Transform left, right;

        private BoxCollider2D centerCollider;
        public BarScaler(Transform left, Transform right)
        {
            this.left = left;
            this.right = right;
        }

        public void Scale(float size)
        {

            left.localPosition = new Vector2(-GetOffset(size), left.localPosition.y);
            right.localPosition = new Vector2(GetOffset(size), right.localPosition.y);
        }

        private static float GetOffset(float scale)
        {
            return (scale * 0.5f) + 10.5f;
        }
    }
}