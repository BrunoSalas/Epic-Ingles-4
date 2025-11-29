using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;

    [Header("Life")]
    public int lifePoints = 10;
    public GameObject canvasGameOver;
    public GameObject objectAll;

    public List<GameObject> hearth = new List<GameObject>();

    void Start()
    {
        Instance = this;
    }
    private void Update()
    {
        if (lifePoints <= 0)
        {
            canvasGameOver.SetActive(true);
            objectAll.SetActive(false);
        }
    }

    public void RestarPoints()
    {
        lifePoints -= 1;
        RemoveLastHeart();

    }

    private void RemoveLastHeart()
    {
        if (hearth.Count == 0) return;

        // Obtener el último corazón
        GameObject lastHeart = hearth[hearth.Count - 1];

        // Desaparecerlo
        lastHeart.SetActive(false);

        // Opcional: quitarlo de la lista
        hearth.RemoveAt(hearth.Count - 1);
    }
}
