using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float timeToWait = 2.5f;
    [SerializeField] private float animationTime = 1f;
    private int _currentSceneIndex;
    private static readonly int FadeIn = Animator.StringToHash("FadeIn");
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");

    // Start is called before the first frame update
    void Start()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (_currentSceneIndex == 0)
        {
            PlayerPrefs.DeleteAll();
            StartCoroutine(LoadStartScene());
        }
        else
        {
            transition.SetTrigger(FadeIn);
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(_currentSceneIndex + 1));
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadScene(_currentSceneIndex));
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadScene(1));
    }

    public void LoadOptionsMenu()
    {
        StartCoroutine(LoadScene(10));
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        transition.SetTrigger(FadeOut);
        yield return new WaitForSeconds(animationTime);
        SceneManager.LoadScene(sceneIndex);
    }
    private IEnumerator LoadStartScene()
    {
        yield return new WaitForSeconds(timeToWait);
        StartCoroutine(LoadScene(1));
    }
}