using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PortalManager : MonoBehaviour
{
    public static PortalManager instance;
    
    [Header("Loading Screen")]
    public GameObject loadingScreen;
    public Image loadingBarFill;
    public Text loadingText;
    
    [Header("Fade Settings")]
    public float fadeDuration = 1f;
    public Image fadePanel;
    
    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Hide loading screen and fade panel at start
        if (loadingScreen != null) loadingScreen.SetActive(false);
        if (fadePanel != null) fadePanel.gameObject.SetActive(false);
    }

    // Load scene with loading screen
    public void LoadSceneWithLoading(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    
    // Load scene with fade transition
    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(LoadSceneWithFadeEffect(sceneName));
    }
    
    // Instant scene load
    public void LoadSceneInstant(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Load scene asynchronously with loading bar
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingScreen.SetActive(true);
        
        // Start loading the scene in background
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        
        float progress = 0f;
        
        // Update loading progress
        while (!asyncLoad.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncLoad.progress, Time.deltaTime);
            loadingBarFill.fillAmount = progress;
            loadingText.text = $"Loading... {(progress * 100):0}%";
            
            // When loading is almost complete
            if (progress >= 0.9f)
            {
                loadingText.text = "Press any key to continue";
                if (Input.anyKeyDown)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
            
            yield return null;
        }
    }

    // Load scene with fade effect
    private IEnumerator LoadSceneWithFadeEffect(string sceneName)
    {
        fadePanel.gameObject.SetActive(true);
        
        // Fade out
        float elapsedTime = 0f;
        Color startColor = new Color(0, 0, 0, 0);
        Color endColor = new Color(0, 0, 0, 1);
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            fadePanel.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        
        // Load the new scene
        SceneManager.LoadScene(sceneName);
        
        // Fade in
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            fadePanel.color = Color.Lerp(endColor, startColor, t);
            yield return null;
        }
        
        fadePanel.gameObject.SetActive(false);
    }

    // Reload current scene
    public void ReloadCurrentScene()
    {
        LoadSceneWithLoading(SceneManager.GetActiveScene().name);
    }

    // Load next scene in build index
    public void LoadNextScene()
    {
        int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        LoadSceneWithLoading(SceneManager.GetSceneByBuildIndex(nextSceneIndex).name);
    }

    // Quit game
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}