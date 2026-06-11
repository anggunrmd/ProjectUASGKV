using UnityEngine;

public class LifeUI : MonoBehaviour
{
    public SpriteRenderer[] mushrooms;

    public void UpdateLives(int lives)
    {
        for (int i = 0; i < mushrooms.Length; i++)
        {
            if (i < lives)
            {
                mushrooms[i].color = Color.white;
            }
            else
            {
                mushrooms[i].color = Color.gray;
            }
        }
    }
}