using UnityEngine;
using UnityEngine.UI;

public class PromptCardManager : MonoBehaviour
{
    // Array to store the text for each prompt card
    public string[] promptTexts = { "Hello", "Goodbye", "What do you do?", "Do you like dogs?", "Do you play any sport?" };

    // Prefab for the prompt card button
    public GameObject promptCardPrefab;

    // Parent object to hold the prompt card buttons
    public Transform promptCardParent;

    // Number of cards per row
    public int cardsPerRow = 5;

    void Start()
    {
        // Populate prompt cards
        PopulatePromptCards();
    }

    // Method to populate prompt cards
    void PopulatePromptCards()
    {
        // Calculate the total number of rows needed
        int numRows = Mathf.CeilToInt((float)promptTexts.Length / cardsPerRow);

        // Loop through each row
        for (int row = 0; row < numRows; row++)
        {
            // Loop through each card in the current row
            for (int col = 0; col < cardsPerRow; col++)
            {
                // Calculate the index of the current prompt text
                int index = row * cardsPerRow + col;

                // Check if the index is valid
                if (index < promptTexts.Length)
                {
                    // Instantiate prompt card prefab
                    GameObject promptCard = Instantiate(promptCardPrefab, promptCardParent);

                    // Get the RectTransform component of the prompt card
                    RectTransform cardTransform = promptCard.GetComponent<RectTransform>();

                    //// Calculate the position of the card
                    //float posX = col * (cardTransform.rect.width + cardTransform.GetComponent<LayoutElement>().spacing);
                    //float posY = -row * (cardTransform.rect.height + cardTransform.GetComponent<LayoutElement>().spacing);

                    //// Set the position of the card
                    //cardTransform.anchoredPosition = new Vector2(posX, posY);

                    // Get the Text component of the prompt card button
                    Text buttonText = promptCard.GetComponentInChildren<Text>();

                    // Set the text of the prompt card button
                    buttonText.text = promptTexts[index];

                    // Add click listener to the prompt card button
                    promptCard.GetComponent<Button>().onClick.AddListener(() => OnPromptCardClick(index));
                }
            }
        }
    }


    // Method to handle click events on prompt cards
    void OnPromptCardClick(int index)
    {
        // Get the selected prompt text based on the index
        string selectedPromptText = promptTexts[index];

        // You can now use the selectedPromptText for further processing (e.g., sending a message)
        Debug.Log("Selected prompt text: " + selectedPromptText);

        // Call the BuildUserMessage method of the MessageBuilder script
        MessageBuilder messageBuilder = FindObjectOfType<MessageBuilder>(); // Assuming there's only one MessageBuilder script in the scene
        if (messageBuilder != null)
        {
            messageBuilder.SelectUserMessage(selectedPromptText);
        }
    }
}
