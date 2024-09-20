using Oculus.Interaction.Input;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Oculus.Interaction.Samples
{
    public class PoseDirectionUseSample : MonoBehaviour
    {
        [SerializeField, Interface(typeof(IHmd))]
        private UnityEngine.Object _hmd;
        private IHmd Hmd { get; set; }

        [SerializeField]
        private ActiveStateSelector[] _poses;

        [SerializeField]
        private GameObject _poseActiveVisualPrefab;

        private GameObject[] _poseActiveVisuals;
        private Vector3[] _previousHandPositions;

        protected virtual void Awake()
        {
            Hmd = _hmd as IHmd;
        }

        protected virtual void Start()
        {
            this.AssertField(Hmd, nameof(Hmd));
            this.AssertField(_poseActiveVisualPrefab, nameof(_poseActiveVisualPrefab));

            _poseActiveVisuals = new GameObject[_poses.Length];
            _previousHandPositions = new Vector3[_poses.Length];

            for (int i = 0; i < _poses.Length; i++)
            {
                _poseActiveVisuals[i] = Instantiate(_poseActiveVisualPrefab);
                _poseActiveVisuals[i].GetComponentInChildren<TextMeshPro>().text = _poses[i].name;
                _poseActiveVisuals[i].SetActive(false);

                int poseNumber = i;
                _poses[i].WhenSelected += () => ShowVisuals(poseNumber);
                _poses[i].WhenUnselected += () => HideVisuals(poseNumber);
            }
        }

        private void ShowVisuals(int poseNumber)
        {
            if (!Hmd.TryGetRootPose(out Pose hmdPose))
            {
                return;
            }

            var hands = _poses[poseNumber].GetComponents<HandRef>();
            Vector3 currentHandPosition = Vector3.zero;

            foreach (var hand in hands)
            {
                hand.GetRootPose(out Pose wristPose);
                currentHandPosition += wristPose.position;
            }
            currentHandPosition /= hands.Length;

            // 计算手的移动方向
            Vector3 movementDirection = currentHandPosition - _previousHandPositions[poseNumber];
            _previousHandPositions[poseNumber] = currentHandPosition;

            // 如果移动方向有效，更新视觉对象的方向
            if (movementDirection != Vector3.zero)
            {
                _poseActiveVisuals[poseNumber].transform.position = currentHandPosition;
                _poseActiveVisuals[poseNumber].transform.rotation = Quaternion.LookRotation(movementDirection);

                // 例如，你可以在此处添加一个箭头或线段来表示移动方向
                // 或者在你的 _poseActiveVisualPrefab 中，动态调整其形状、大小等来可视化方向
            }

            _poseActiveVisuals[poseNumber].gameObject.SetActive(true);
        }

        private void HideVisuals(int poseNumber)
        {
            _poseActiveVisuals[poseNumber].gameObject.SetActive(false);
        }
    }
}
