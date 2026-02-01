using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject mainPanel;

        private void Start()
        {
            GameManager.Instance.onPause.AddListener(EnablePausePanel);
            GameManager.Instance.onResume.AddListener(DisablePausePanel);
            
            DisablePausePanel();
        }

        private void OnDestroy()
        {
            GameManager.Instance.onPause.RemoveListener(EnablePausePanel);
            GameManager.Instance.onResume.RemoveListener(DisablePausePanel);
        }

        private void EnablePausePanel()
        {
            mainPanel.SetActive(true);
        }
        
        private void DisablePausePanel()
        {
            mainPanel.SetActive(false);
        }
    }
}