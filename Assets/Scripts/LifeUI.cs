using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    [SerializeField] Image[] mushrooms;

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