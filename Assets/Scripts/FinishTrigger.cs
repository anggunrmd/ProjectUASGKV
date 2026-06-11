using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public GameObject finishPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            finishPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}