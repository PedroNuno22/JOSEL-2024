using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AutoScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public RectTransform viewport;

    //// Method to scroll to the first message that overflows
    //public void ScrollToOverflow()
    //{
    //    // Check if content height is greater than viewport height
    //    if (content.rect.height > viewport.rect.height)
    //    {
    //        Debug.Log("Content height is greater than viewport height");

    //        // Calculate normalized position of overflowed message (assuming vertical layout)
    //        float normalizedPosition = 1f - (viewport.rect.height / content.rect.height);

    //        Debug.Log("normalizedPosition:"+ normalizedPosition);

    //        // Set verticalNormalizedPosition of ScrollRect to scroll to overflowed message
    //        scrollRect.verticalNormalizedPosition = normalizedPosition;

    //    }
    //}

    public void ScrollToLastMessage()
    {

        // Check if the Scroll Rect component exists
        if (scrollRect != null)
        {
            // Get the content RectTransform from the Scroll Rect
            RectTransform contentRectTransform = scrollRect.content;

            // Calculate the height of the content
            float contentHeight = contentRectTransform.rect.height;

            // Get the viewport height of the scroll view
            float viewportHeight = scrollRect.viewport.rect.height;

            // Calculate the scroll position to show the last message
            float scrollPosition = Mathf.Max(contentHeight - viewportHeight, 0f);

            // Set the vertical scroll position of the Scroll Rect
            scrollRect.verticalNormalizedPosition = 1f - (scrollPosition / contentHeight);
        }
        else
        {
            Debug.LogWarning("Scroll Rect component not found on ScrollView.");
        }

    }

    //public void ScrollToBottom(Vector2 lastMessagePosition)
    //{
    //    ScrollView scrollView = Instantiate(scrollView, canvas);

    //    // Check if the scroll view exists
    //    if (scrollView != null)
    //    {
    //        // Get the content element from the scroll view
    //        VisualElement contentElement = scrollView.contentViewport.contentContainer;

    //        // Calculate the target scroll position
    //        float targetScrollPosition = lastMessagePosition.y;

    //        // Scroll to the target position
    //        scrollView.scrollOffset = new Vector2(0f, targetScrollPosition);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Scroll View is null. Cannot scroll to bottom.");
    //    }
    //}


}

