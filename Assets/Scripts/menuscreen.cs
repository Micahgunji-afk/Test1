using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    private bool showMenu = true;         // Menu visible?
    private bool gameRunning = false;     // Is the game running?
    private bool gameWon = false;         // Did the player win?
    private bool gameLost = false;        // Did the player lose?

    private List<FallingWord> fallingWords = new List<FallingWord>();

    void Update()
    {
        // Only update falling words if the game is in win/loss state
        if (gameWon || gameLost)
        {
            // Move each falling word
            for (int i = fallingWords.Count - 1; i >= 0; i--)
            {
                fallingWords[i].position.y += fallingWords[i].speed * Time.deltaTime;
                if (fallingWords[i].position.y > Screen.height) // remove if off screen
                    fallingWords.RemoveAt(i);
            }
        }

        // Here you can update your player/enemy logic
        if (gameRunning)
        {
            // Example: check win condition
            // if (AllEnemiesFallen()) WinGame();
            // if (PlayerFallen()) LoseGame();
        }
    }

    void OnGUI()
    {
        // --- White Background ---
        GUI.backgroundColor = Color.white;
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), GUIContent.none);

        // --- Menu ---
        if (showMenu)
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 40;
            buttonStyle.normal.textColor = Color.white;
            buttonStyle.hover.textColor = Color.yellow;
            buttonStyle.normal.background = MakeTex(2, 2, Color.green);

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "START", buttonStyle))
            {
                StartGame();
            }
        }

        // --- Falling Words ---
        if (gameWon || gameLost)
        {
            GUIStyle wordStyle = new GUIStyle(GUI.skin.label);
            wordStyle.fontSize = 30;
            wordStyle.alignment = TextAnchor.MiddleCenter;
            wordStyle.normal.textColor = (gameWon) ? Color.green : Color.red;

            foreach (FallingWord fw in fallingWords)
            {
                GUI.Label(new Rect(fw.position.x, fw.position.y, 200, 50), fw.text, wordStyle);
            }
        }
    }

    // --- Start Game ---
    void StartGame()
    {
        showMenu = false;
        gameRunning = true;
        gameWon = false;
        gameLost = false;
        fallingWords.Clear();

        // Reset your player/enemies here
        // Example: ResetPlayer(); ResetEnemies();
    }

    // --- Win Game ---
    public void WinGame()
    {
        gameRunning = false;
        gameWon = true;
        showMenu = true;

        GenerateFallingWords(true);
    }

    // --- Lose Game ---
    public void LoseGame()
    {
        gameRunning = false;
        gameLost = true;
        showMenu = true;

        GenerateFallingWords(false);
    }

    // --- Generate Falling Words ---
    void GenerateFallingWords(bool congratulatory)
    {
        fallingWords.Clear();
        string[] words;

        if (congratulatory)
        {
            words = new string[] { "WINNER!", "CONGRATS!", "GREAT JOB!", "AMAZING!", "YOU DID IT!" };
        }
        else
        {
            words = new string[] { "LOSER!", "FAIL!", "TRY AGAIN!", "OH NO!", "SAD!" };
        }

        for (int i = 0; i < 20; i++)
        {
            FallingWord fw = new FallingWord();
            fw.text = words[Random.Range(0, words.Length)];
            fw.position = new Vector2(Random.Range(0, Screen.width - 100), -Random.Range(50, 300));
            fw.speed = Random.Range(50f, 150f); // pixels per second
            fallingWords.Add(fw);
        }
    }

    // --- Utility to create colored button texture ---
    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    // --- Class for falling words ---
    private class FallingWord
    {
        public string text;
        public Vector2 position;
        public float speed;
    }

    // --- Example win/loss triggers ---
    // Call these from your game logic:
    // FindObjectOfType<GameController>().WinGame();
    // FindObjectOfType<GameController>().LoseGame();
}
