using System.Collections.Generic;
using System.Web.Mvc;

namespace VoteSystem.Clients.MVC.Infrastructure.NotificationSystem
{
    public static class NotificationExtensions
    {
        private static readonly IDictionary<NotificationType, string> NotificationKey = new Dictionary<NotificationType, string>
        {
            { NotificationType.Error, "App.Notifications.Error" },
            { NotificationType.Warning, "App.Notifications.Warning" },
            { NotificationType.Success, "App.Notifications.Success" },
            { NotificationType.Info, "App.Notifications.Info" }
        };

        public static void AddNotification(this ControllerBase controller, string message, NotificationType notificationType)
        {
            var notificationKey = NotificationKey[notificationType];

            var messages = controller.TempData[notificationKey] as ICollection<string>;

            if (messages == null)
            {
                messages = new HashSet<string>();
                controller.TempData[notificationKey] = messages;
            }

            messages.Add(message);
        }

        public static IEnumerable<string> GetNotifications(this HtmlHelper htmlHelper, NotificationType notificationType)
        {
            var notificationKey = NotificationKey[notificationType];

            return htmlHelper.ViewContext.Controller.TempData[notificationKey] as ICollection<string>;
        }
    }
}
