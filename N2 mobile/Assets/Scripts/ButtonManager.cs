using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;

    public int cenaAtual;

    private void Start()
    {
        soundManager = GetComponent<SoundManager>();
        cenaAtual = SceneManager.GetActiveScene().buildIndex;
    }



    #region Menu inicial
    public void Fase1()
    {
        soundManager.PlaySound(SoundManager.SoundType.TypeBTN);
        SceneManager.LoadScene("Fase 1");
    }

    public void FecharJogo()
    {
        Application.Quit();
        Debug.Log("Saiu do jogo");
    }
    #endregion




    #region In Game
    public void Reiniciar()
    {
        soundManager.PlaySound(SoundManager.SoundType.TypeBTN);
        SceneManager.LoadScene(cenaAtual);
    }

    public void Menu()
    {
        soundManager.PlaySound(SoundManager.SoundType.TypeBTN);
        SceneManager.LoadScene("Menu");
    }

    public void ProximoNivel()
    {
        soundManager.PlaySound(SoundManager.SoundType.TypeBTN);
        Debug.Log("Foi para o proximo");
    }

    public void Pause()
    {
        soundManager.PlaySound(SoundManager.SoundType.TypeBTN);
    }

    #endregion
}
