using EZCameraShake;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Continuous
{
    public class CameraShake : MonoBehaviour
    {
        [InlineEditor(InlineEditorModes.FullEditor)]
        [SerializeField] private CameraShakeData cameraShakeData;

        private void OnEnable()
        {
            Player.Die += Shake;
        }

        private void OnDisable()
        {
            Player.Die -= Shake;
        }

        [Button]
        private void Shake()
        {
            CameraShakeInstance shakeInstance = cameraShakeData.ShakeInstance;
            CameraShaker.Instance.ShakeOnce(shakeInstance.Magnitude, shakeInstance.Roughness,
                cameraShakeData.Duration / 2, cameraShakeData.Duration / 2,
                shakeInstance.PositionInfluence, shakeInstance.RotationInfluence);
        }
    }
}