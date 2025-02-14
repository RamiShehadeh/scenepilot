using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
using System;

public class UIInputHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField userInputField;
    [SerializeField] private Button sendButton;
    [SerializeField] private AICommandController aiCommandController;
    [SerializeField] private GameObject loadingIndicator; 

    private void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClicked);
        if (loadingIndicator != null)
            loadingIndicator.SetActive(false);
    }

    private async void OnSendButtonClicked()
    {
        string userQuery = userInputField.text;
        if (!string.IsNullOrEmpty(userQuery))
        {
            if (loadingIndicator != null)
                loadingIndicator.SetActive(true);

            sendButton.interactable = false;

            string response = "";

            try
            {
                //  Run LLM inference on a background thread to avoid UI freeze
                response = await Task.Run(() => LLMManager.Instance.GetResponseAsync(userQuery));
            }
            catch (Exception ex)
            {
                Debug.LogError($"[UIInputHandler] Error running LLM inference: {ex.Message}");
            }

            if (loadingIndicator != null)
                loadingIndicator.SetActive(false);

            sendButton.interactable = true;

            if (!string.IsNullOrEmpty(response))
            {
                aiCommandController.ProcessUserQuery(response);
            }
        }
    }
}
