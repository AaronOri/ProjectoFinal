using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxVides = 3;
    [SerializeField] private GameObject[] vidaIcons;

    private int videsActuals;

    void Start()
    {
        videsActuals = maxVides;
        ActualitzarHUD();
    }

    public void RebDany()
    {
        videsActuals--;

        if (videsActuals <= 0)
        {
            videsActuals = 0;
            ActualitzarHUD();
            Morir();
        }
        else
        {
            ActualitzarHUD();
        }
    }

    private void ActualitzarHUD()
    {
        for (int i = 0; i < vidaIcons.Length; i++)
        {
            vidaIcons[i].SetActive(i < videsActuals);
        }
    }

    private void Morir()
    {
        Debug.Log("Jugador ha mort!");
        // Aquí pots afegir explosió, desactivar jugador, pantalla de Game Over, etc.
        gameObject.SetActive(false);
    }
}


