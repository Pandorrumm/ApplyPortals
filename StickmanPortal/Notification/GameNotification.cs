using UnityEngine;
using UnityEngine.UI;

namespace StickmanPortal
{
    public class GameNotification : MonoBehaviour
    {
        private string offlineText;
        private Text notificationText = null;

        private void Start()
        {
            notificationText = GetComponent<Text>();
            offlineText = notificationText.text;

            if (NotificationController.Instance)
                NotificationController.Instance.MakeNotification(Application.productName, offlineText, 60.0f * 24.0f, true); //60.0f * 20.0f
        }
    }
}
