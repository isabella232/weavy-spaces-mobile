namespace Weavy.Droid.Notifications {
    public class Constants {

        /// <summary>
        /// Azure Notification Hub connection string
        /// In the format: Endpoint=sb://myhub.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=[key]
        /// </summary>
        public const string ConnectionString = "";

        /// <summary>
        /// Azure Notification Hub path
        /// The first part of the connection string. In the example above this should be 'myhub'
        /// </summary>
        public const string NotificationHubPath = "";

        /// <summary>
        /// The channel id to use for notifications
        /// A unique string of your choice
        /// </summary>
        public static string NOTIFICATION_CHANNEL_ID = "com.mycompanyname.droid.GENERAL_NOTIFICATIONS";

        /// <summary>
        /// An id of the group to group the notifications in
        /// </summary>
        public static string NOTIFICATION_GROUP_ID = "com.mycompanyname.droid.NOTIFICATIONS";
    }
}