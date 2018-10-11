using Android.App;
using Android.Content;
using Android.Graphics;
using Firebase.Messaging;
using Android.Support.V4.App;
using System;

namespace Weavy.Droid.Notifications {
    /// <summary>
    /// Handle incoming notifications
    /// </summary>
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseNotificationService : FirebaseMessagingService {
        
        /// <summary>
        /// A notification is received
        /// </summary>
        /// <param name="message"></param>
        public override void OnMessageReceived(RemoteMessage message) {
    
            // Pull message body out of the template
            var messageBody = message.Data["message"];
            var url = message.Data["url"];
            var badge = message.Data["badge"];

            if (string.IsNullOrWhiteSpace(messageBody))
                return;

            if (!AppInForeground()) {
                DisplayNotification(messageBody, url);
            }
            
        }

        /// <summary>
        /// Display the notification
        /// </summary>
        /// <param name="messageBody"></param>
        /// <param name="url"></param>
        private void DisplayNotification(string messageBody, string url) {
            int notificationId = DateTime.Now.Millisecond;            
            var intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("url", url);

            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, DateTime.Now.Millisecond, intent, PendingIntentFlags.OneShot);

            // custom notification sound
            string pathToPushSound = $"android.resource://{ApplicationContext.PackageName}/raw/{Resource.Raw.notification}";
            global::Android.Net.Uri soundUri = global::Android.Net.Uri.Parse(pathToPushSound);
            var builder = new NotificationCompat.Builder(this, Constants.NOTIFICATION_CHANNEL_ID)
                           .SetSmallIcon(Resource.Drawable.ic_notification)
                           .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.ic_launcher))
                           .SetContentTitle(Helpers.Constants.DisplayShortName)
                           .SetContentText(messageBody)
                           .SetGroup(Constants.NOTIFICATION_GROUP_ID)
                           .SetSound(soundUri)
                           .SetPriority((int)NotificationPriority.High)
                           .SetDefaults((int)NotificationDefaults.Vibrate)
                           .SetAutoCancel(true)
                           .SetVisibility((int)NotificationVisibility.Public)                           
                           .SetContentIntent(pendingIntent);

            NotificationManagerCompat notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(notificationId, builder.Build());


            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.N) {
                //Call API supported by Nougat and above, but not by lower API's

                var inbox = new NotificationCompat.InboxStyle();
                inbox.SetSummaryText($"You have notifications from {Helpers.Constants.DisplayName}");
                inbox.SetBigContentTitle(Helpers.Constants.DisplayName);
                var summaryNotification = new NotificationCompat.Builder(this, Constants.NOTIFICATION_CHANNEL_ID)
                    .SetContentTitle(Helpers.Constants.DisplayName)

                    .SetContentText($"You have notifications from {Helpers.Constants.DisplayName}")
                    .SetSmallIcon(Resource.Drawable.ic_notification)
                    .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.ic_launcher))
                    //build summary info into InboxStyle template
                    .SetStyle(inbox)

                    //specify which group this notification belongs to
                    .SetGroup(Constants.NOTIFICATION_GROUP_ID)
                    //set this notification as the summary for the group
                    .SetGroupSummary(true);

                notificationManager.Notify(9999, summaryNotification.Build());
            }
        }

        /// <summary>
        /// Check if app is in foreground
        /// </summary>
        /// <returns></returns>
        private bool AppInForeground() {
            var context = Application.Context;

            KeyguardManager km = (KeyguardManager)context.GetSystemService(Context.KeyguardService);
            if (!km.InKeyguardRestrictedInputMode()) {

                var activityManager = (ActivityManager)context.GetSystemService(Context.ActivityService);
                var appProcesses = activityManager.RunningAppProcesses;
                if (appProcesses == null) {
                    return false;
                }

                var packageName = context.PackageName;
                foreach (ActivityManager.RunningAppProcessInfo appProcess in appProcesses) {
                    if (appProcess.Importance == Importance.Foreground && appProcess.ProcessName == packageName) {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}