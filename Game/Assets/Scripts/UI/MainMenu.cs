﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir.FoxIt.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_StartMenu;
        [SerializeField] private GameObject m_credits;

        public void BtnBackToMenu_Click()
        {
            this.m_credits.SetActive(false);
            this.m_StartMenu.SetActive(true);
        }

        public void BtnCredits_Click()
        {
            this.m_credits.SetActive(true);
            this.m_StartMenu.SetActive(false);
        }

        public void BtnPlay_Click()
        {
            SceneManager.LoadScene(1);
        }

        public void BtnQuit_Click()
        {
            Application.Quit();
        }
    }
}