using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MessageBuilder : MonoBehaviour
{
    // Reference to the background image prefab for user messages
    public GameObject userMessagePrefab;

    // Reference to the parent transform where messages will be instantiated
    public Transform messageParent;

    // Store the selected user message
    private string selectedUserMessage;

    // Reference to the AutoScroll script
    public AutoScroll autoScroll;

    // Store the position of the last sent message
    private Vector2 lastMessagePosition = Vector2.zero;

    private float lastMessageHeight = 0;

    private int initialMessageSpacing = 10;

    private int messageSpacing = 5;

    // Method to store the selected user message
    public void SelectUserMessage(string message)
    {
        // Store the selected user message
        selectedUserMessage = message;
        Debug.Log("SelectedUserMessage: " + selectedUserMessage);
    }

    // Method to display and send the selected user message
    public void SendUserMessage()
    {
        // Check if a user message is selected
        if (!string.IsNullOrEmpty(selectedUserMessage))
        {
            // Instantiate a new message GameObject from the prefab
            GameObject messagePanel = Instantiate(userMessagePrefab, messageParent);

            // Get the RectTransform component from the messagePanel
            RectTransform messageRectTransform = messagePanel.GetComponent<RectTransform>();

            // Set the anchored position of the message relative to the content component
            // Adjust the y-coordinate to position the message directly under the last message
            if (lastMessageHeight != 0)
            {
                messageRectTransform.anchoredPosition = lastMessagePosition - new Vector2(0f, lastMessageHeight + messageSpacing);
            }
            else
            {
                messageRectTransform.anchoredPosition = lastMessagePosition - new Vector2(0f, lastMessageHeight + initialMessageSpacing);
            }

            // Update the last message position to the current message position
            lastMessagePosition = messageRectTransform.anchoredPosition;

            // Get the Button component from the messagePanel
            Button userMessageBubble = messagePanel.GetComponentInChildren<Button>();

            // Get the Text component from the userMessageBubble
            Text textComponent = userMessageBubble.GetComponentInChildren<Text>();

            // Check if the Text component exists
            if (textComponent != null)
            {
                // Set the message text in the Text component
                textComponent.text = selectedUserMessage;
            }
            else
            {
                Debug.LogError("Text component not found in children of userMessageBubble.");
            }

            // Force a layout rebuild to ensure that the RectTransform is updated
            LayoutRebuilder.ForceRebuildLayoutImmediate(messagePanel.GetComponent<RectTransform>());

            // Delay the invocation of the SetLastMessageHeight function by two frames
            StartCoroutine(DelayedSetLastMessageHeight(messageRectTransform));

            // Reset the selected user message
            selectedUserMessage = null;

            // Scroll to the bottom of the content component
            autoScroll.ScrollToLastMessage();
        }
    }

    // Coroutine to delay the invocation of the SetLastMessageHeight function by two frames
    private IEnumerator DelayedSetLastMessageHeight(RectTransform messageRectTransform)
    {
        // Wait for two frames
        yield return null;
        yield return null;

        // Invoke the SetLastMessageHeight function
        SetLastMessageHeight(messageRectTransform);
    }

    public void SetLastMessageHeight(RectTransform messageRectTransform)
    {
        lastMessageHeight = messageRectTransform.rect.height;
    }

    // Method to build and display a character message based on the user message
    //public void BuildCharacterMessage(string userMessage)
    //{
    //    // Instantiate the character message background prefab
    //    GameObject background = Instantiate(characterMessagePrefab, transform);

    //    // Send request to backend to retrieve character message based on the user message
    //    //string characterMessage = FetchCharacterMessage(userMessage);
    //}

    // Method to simulate fetching character message from backend
    //private string FetchCharacterMessage(string userMessage)
    //{
    //    // Simulated backend response based on the user message
    //    // In a real implementation, this would involve sending a request to the backend
    //    // and receiving the appropriate character message
    //    return "Character message based on user message: " + userMessage;
    //}
}

