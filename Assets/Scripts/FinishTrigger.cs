using System.Collections;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public GameObject finishPanel;
    public PlayerMovement player;

    [SerializeField] AudioSource finishSound;

    private bool finished = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (finished) return;

        if (collision.CompareTag("Player"))
        {
            finished = true;
            StartCoroutine(ShowFinish());
        }
    }

    IEnumerator ShowFinish()
    {
        // Mainkan suara finish
        if (finishSound != null && finishSound.clip != null)
        {
            finishSound.PlayOneShot(finishSound.clip);
        }

        // Tunggu sebentar agar suara selesai diputar
        yield return new WaitForSecondsRealtime(0.5f);

        // Tampilkan panel finish
        finishPanel.SetActive(true);

        // Pause game
        Time.timeScale = 0f;
    }
}