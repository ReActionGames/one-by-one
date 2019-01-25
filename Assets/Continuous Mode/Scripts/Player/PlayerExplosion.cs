using UnityEngine;

namespace Continuous
{
    public class PlayerExplosion : MonoBehaviour
    {
        [SerializeField] private Explodable exploderPrefab;
        [SerializeField] private Transform exploderParent;

        private Explodable exploder;
        private ExplosionForce explosionForce;

        private void OnEnable()
        {
            Player.Die += DoExplosion;
            GameManager.GameStart += OnGameStartOrRestart;
            GameManager.GameRestart += OnGameStartOrRestart;
        }

        private void OnDisable()
        {
            Player.Die -= DoExplosion;
            GameManager.GameStart -= OnGameStartOrRestart;
            GameManager.GameRestart -= OnGameStartOrRestart;
        }

        private void OnGameStartOrRestart()
        {
            SpawnExploder();
        }

        private void SpawnExploder()
        {
            exploderParent.DestroyChildren();
            exploder = Instantiate(exploderPrefab, exploderParent);
            exploder.transform.localPosition = Vector3.zero;
            exploder.transform.localRotation = Quaternion.identity;
            explosionForce = exploder.GetComponent<ExplosionForce>();
        }

        private void DoExplosion()
        {
            exploder.explode();
            explosionForce.doExplosion(transform.position + (Vector3)UnityEngine.Random.insideUnitCircle);
        }
    }
}