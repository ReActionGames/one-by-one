using UnityEngine;

namespace Continuous
{
    public class DoubleBarScaler : IScaler
    {
        private Transform left, right, center;

        private SpriteRenderer centerRenderer;
        private BoxCollider2D centerCollider;

        public DoubleBarScaler(Transform left, Transform right, Transform center)
        {
            this.left = left;
            this.right = right;
            this.center = center;

            centerRenderer = center.GetComponent<SpriteRenderer>();
            centerCollider = center.GetComponent<BoxCollider2D>();
        }

        public void Scale(float size)
        {
            left.localPosition = new Vector2(-GetOffset(size), left.localPosition.y);
            right.localPosition = new Vector2(GetOffset(size), right.localPosition.y);

            centerRenderer.size = centerRenderer.size.With(x: GetCenterSize(size));
            centerCollider.size = centerCollider.size.With(x: GetCenterSize(size));
        }

        private static float GetOffset(float scale)
        {
            // Predetermined formula
            return (scale * 0.5f) + 10.5f + 2;
        }

        private static float GetCenterSize(float scale)
        {
            // Predetermined formula
            return ((1f / 3f) * scale) + 0.5f;
        }
    }
}