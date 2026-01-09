using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndNoUI : MonoBehaviour
{
    public string playerTag = "Player";
    public string enemyTag = "Enemy";

    public float pulseSpeed = 3f;
    public float pulseScale = 0.15f;

    private bool gameEnded = false;

    void Update()
    {
        if (gameEnded) return;

        if (AllEnemiesDead() || AllPlayersDead())
        {
            EndGame();
        }
    }

    bool AllEnemiesDead()
    {
        return GameObject.FindGameObjectsWithTag(enemyTag).Length == 0;
    }

    bool AllPlayersDead()
    {
        return GameObject.FindGameObjectsWithTag(playerTag).Length == 0;
    }

    void EndGame()
    {
        gameEnded = true;
        Time.timeScale = 0f; // pause game
    }

    void OnGUI()
    {
        if (!gameEnded) return;

        // Pulsing scale
        float pulse = 1f + Mathf.Sin(Time.unscaledTime * pulseSpeed) * pulseScale;

        float baseWidth = 200f;
        float baseHeight = 80f;

        float width = baseWidth * pulse;
        float height = baseHeight * pulse;

        float x = (Screen.width - width) / 2f;
        float y = (Screen.height - height) / 2f;

        // Button style
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fontSize = 32;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.white;

        GUI.backgroundColor = Color.green;

        if (GUI.Button(new Rect(x, y, width, height), "START", style))
        {
            RestartGame();
        }

        GUI.backgroundColor = Color.white;
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
