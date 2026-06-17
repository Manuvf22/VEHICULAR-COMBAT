using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Enemies")]
    private int totalEnemies;
    private int enemiesKilled = 0;
    private bool gameOver = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Contar todos los enemigos al inicio
        totalEnemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None).Length;

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
    }

    public void EnemyKilled()
    {
        if (gameOver) return;

        enemiesKilled++;
        Debug.Log($"Enemigos eliminados: {enemiesKilled}/{totalEnemies}");

        if (enemiesKilled >= totalEnemies)
            Win();
    }

    public void PlayerDied()
    {
        if (gameOver) return;
        Lose();
    }

    void Win()
    {
        gameOver = true;
        Debug.Log("¡Victoria!");
        Time.timeScale = 0f;
        if (winPanel != null) winPanel.SetActive(true);
    }

    void Lose()
    {
        gameOver = true;
        Debug.Log("Game Over");
        Time.timeScale = 0f;
        if (losePanel != null) losePanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}