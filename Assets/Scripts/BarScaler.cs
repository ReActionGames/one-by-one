using UnityEngine;

public class BarScaler : MonoBehaviour
{
    [SerializeField] private BoxCollider2D leftCollider, centerCollider, rightCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Scale(float scale)
    {
        centerCollider.size = new Vector2(scale, centerCollider.size.y);
        leftCollider.offset = new Vector2(-GetOffset(scale), leftCollider.offset.y);
        rightCollider.offset = new Vector2(GetOffset(scale), rightCollider.offset.y);

        spriteRenderer.size = new Vector2(scale, spriteRenderer.size.y);
    }

    private float GetOffset(float scale)
    {
        return (scale * 0.5f) + 3.5f;
    }
}