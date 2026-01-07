using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameRules : MonoBehaviour
{
    // Game state
    private bool gameEnded = false;
    private string endMessage = "";
    private float timer = 0f;
    private bool showStartButton = false;

    // End text fade
    private float textAlpha = 0f;
    public float textFadeSpeed = 1.0f;

    // Start button pulsing
    private float buttonPulse = 1.0f;
    public float buttonPulseSpeed = 2.0f;

    // Falling phrases
    private class FallingPhrase
    {
        public Vector2 position;
        public float speed;
        public string text;
    }
    private List<FallingPhrase> phrases = new List<FallingPhrase>();
    public int maxPhrases = 50;
    public float phraseSpawnInterval = 0.5f; // lower spawn rate
    public float phraseFallSpeedMin = 50f;
    public float phraseFallSpeedMax = 150f;
    private string[] winPhrases = { "Good Job!", "Congrats!", "Well Done!", "You Rock!", "Amazing!" };
    private string[] losePhrases = { "Loser!", "Bad Job!", "Try Again!", "Ouch!", "Epic Fail!" };

    // Settings
    public float waitTimeBeforeStartButton = 5f;

    void Update()
    {
        if (!gameEnded)
        {
            if (GameObject.FindWithTag("Player") == null)
                EndGame("YOU LOSE");
            else if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                EndGame("YOU WIN");
        }
        else
        {
            timer += Time.deltaTime;

            // Fade in end text
            if (textAlpha < 1f)
                textAlpha += Time.deltaTime * textFadeSpeed;

            // Show start button after wait time
            if (timer >= waitTimeBeforeStartButton)
                showStartButton = true;

            // Spawn falling phrases
            if (showStartButton && phrases.Count < maxPhrases)
            {
                if (timer % phraseSpawnInterval < Time.deltaTime)
                {
                    FallingPhrase p = new FallingPhrase();
                    p.position = new Vector2(Random.Range(0, Screen.width - 150), -30);
                    p.speed = Random.Range(phraseFallSpeedMin, phraseFallSpeedMax);
                    p.text = (endMessage == "YOU WIN") ? winPhrases[Random.Range(0, winPhrases.Length)]
                                                       : losePhrases[Random.Range(0, losePhrases.Length)];
                    phrases.Add(p);
                }
            }

            // Move falling phrases
            for (int i = 0; i < phrases.Count; i++)
            {
                phrases[i].position.y += phrases[i].speed * Time.deltaTime;
            }
        }
    }

    void EndGame(string message)
    {
        gameEnded = true;
        endMessage = message;
        timer = 0f;
        textAlpha = 0f;
        phrases.Clear();
    }

    void OnGUI()
    {
        if (!gameEnded) return;

        // Draw fading end text
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 100;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = new Color(
            (endMessage == "YOU WIN") ? 0f : 1f,
            (endMessage == "YOU WIN") ? 1f : 0f,
            0f,
            textAlpha
        );
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), endMessage, style);

        // Draw start button with pulsing
        if (showStartButton)
        {
            buttonPulse = 1f + 0.1f * Mathf.Sin(Time.time * buttonPulseSpeed);
            float buttonWidth = 150 * buttonPulse;
            float buttonHeight = 100 * buttonPulse;
            Rect buttonRect = new Rect(
                Screen.width / 2 - buttonWidth / 2,
                Screen.height - 150,
                buttonWidth,
                buttonHeight
            );

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = Mathf.RoundToInt(30 * buttonPulse);

            GUI.backgroundColor = Color.green;
            if (GUI.Button(buttonRect, "START", buttonStyle))
            {
                RestartGame();
            }
        }

        // Draw falling phrases
        GUIStyle phraseStyle = new GUIStyle(GUI.skin.label);
        phraseStyle.fontSize = 30;
        phraseStyle.normal.textColor = Color.white;

        foreach (FallingPhrase p in phrases)
        {
            GUI.Label(new Rect(p.position.x, p.position.y, 200, 30), p.text, phraseStyle);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
