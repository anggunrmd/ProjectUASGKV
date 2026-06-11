using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_InputField answerInput;

    public PlayerMovement player;

    string[] questions =
    {
        "angka = [10, 20, 30]\nprint(angka[0])",
        "kata = \"Belajar\"\nprint(len(kata))",
        "hewan = [\"Kucing\", \"Ayam\"]\nhewan.append(\"Sapi\")\nprint(hewan[2])",
        "x = 2\n\nfor i in range(3):\n    x = x + i\n\nprint(x)",
        "nilai = 80\n\nif nilai >= 85:\n    print(\"A\")\nelif nilai >= 70:\n    print(\"B\")\nelse:\n    print(\"C\")",
        "def hitung(x):\n    return x * 2\n\nprint(hitung(5))",
        "a = [1, 2, 3]\n\nprint(a * 2)",
        "a = \"Hello\"\nb = \"World\"\n\nprint(a + \" \" + b)",
        "teks = \"Python\"\n\nprint(teks[::-1])",
        "kata = \"PROGRAM\"\n\nprint(kata.lower())",
        "teks = \"Algoritma\"\n\nif \"go\" in teks:\n    print(\"Ada\")\nelse:\n    print(\"Tidak Ada\")",
        "teks = \"123\"\n\nprint(teks + \"4\")"
    };

    string[] answers =
    {
        "10",
        "7",
        "Sapi",
        "5",
        "B",
        "10",
        "[1, 2, 3, 1, 2, 3]",
        "Hello World",
        "nohtyP",
        "program",
        "Ada",
        "1234"
    };

    int currentQuestion;

    private void OnEnable()
    {
        currentQuestion = Random.Range(0, questions.Length);
        questionText.text = questions[currentQuestion];
        answerInput.text = "";
    }

    public bool IsCorrect()
    {
        return answerInput.text.Trim().ToLower() ==
               answers[currentQuestion].Trim().ToLower();
    }

    public void SubmitAnswer()
    {
        if (IsCorrect())
        {
            Debug.Log("Jawaban Benar!");

            player.AddScore(100);

            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Jawaban Salah!");

            player.LoseLife();

            // ganti soal baru, panel tetap terbuka
            currentQuestion = Random.Range(0, questions.Length);
            questionText.text = questions[currentQuestion];
            answerInput.text = "";
        }
    }
}