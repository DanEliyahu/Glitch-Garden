using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float timeToWait = 2.5f;
    [SerializeField] private float animationTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(LoadStartScene());
        }
        else
        {
            transition.SetTrigger("FadeIn");
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        transition.SetTrigger("FadeOut");
        yield return new WaitForSeconds(animationTime);
        SceneManager.LoadScene(sceneIndex);
    }
    private IEnumerator LoadStartScene()
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }
}