using System.Collections;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_InputField answerInput;

    public PlayerMovement player;

    [SerializeField] AudioClip correctClip;
    [SerializeField] AudioClip wrongClip;

    string[] questions =
    {
        "angka = [10, 20, 30]\nprint(angka[0])",
        "kata = \"Belajar\"\nprint(len(kata))",
        "hewan = [\"Kucing\", \"Ayam\"]\nhewan.append(\"Sapi\")\nprint(hewan[2])",
        "x = 2\nfor i in range(3):\n    x = x + i\nprint(x)", 
        "def hitung(x):\n    return x * 2\n\nprint(hitung(5))",
        "a = [1, 2, 3]\n\nprint(a * 2)",
        "a = \"Hello\"\nb = \"World\"\nprint(a + \" \" + b)",
        "teks = \"Python\"\n\nprint(teks[::-1])",
        "kata = \"PROGRAM\"\n\nprint(kata.lower())",
        "teks = \"Algoritma\"\nif \"go\" in teks:\n    print(\"Ada\")\nelse:\n    print(\"Tidak Ada\")",
        "teks = \"123\"\n\nprint(teks + \"4\")"
    };

    string[] answers =
    {
        "10",
        "7",
        "Sapi",
        "5", 
        "10",
        "[1, 2, 3, 1, 2, 3]",
        "Hello World",
        "nohtyP",
        "program",
        "Ada",
        "1234"
    };

    int currentQuestion;
    bool isSubmitting = false;

    private void OnEnable()
    {
        isSubmitting = false;
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
        if (isSubmitting) return;

        if (IsCorrect())
        {
            isSubmitting = true;

            Debug.Log("Jawaban Benar!");

            player.AddScore(100);

            StartCoroutine(CorrectDelay());
        }
        else
        {
            Debug.Log("Jawaban Salah!");

            if (wrongClip != null)
            {
                AudioSource.PlayClipAtPoint(wrongClip, Camera.main.transform.position);
            }

            player.LoseLife();

            currentQuestion = Random.Range(0, questions.Length);
            questionText.text = questions[currentQuestion];
            answerInput.text = "";
        }
    }

    IEnumerator CorrectDelay()
    {
        Time.timeScale = 1f;

        if (correctClip != null)
        {
            AudioSource.PlayClipAtPoint(correctClip, Camera.main.transform.position, 1f);
        }

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}