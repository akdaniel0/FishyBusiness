using UnityEngine;
using UnityEngine.UI;

namespace SceneLoading
{
    public class SceneLoadingUIController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Slider progressBar;

        private bool _finished;

        private void Start()
        {
            SceneLoader.SceneLoadFinished += OnFinished;
        }

        private void OnDestroy()
        {
            SceneLoader.SceneLoadFinished -= OnFinished;
        }

        private void Update()
        {
            if (_finished) return;
            progressBar.value = SceneLoader.Instance.LoadProgress;
        }

        private void OnFinished()
        {
            _finished = true;
            progressBar.value = 1;
            animator.SetTrigger("Finished");
        }
    }
}