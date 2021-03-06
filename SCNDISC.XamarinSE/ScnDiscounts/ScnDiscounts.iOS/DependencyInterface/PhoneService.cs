using Foundation;
using MessageUI;
using ScnDiscounts.DependencyInterface;
using ScnDiscounts.iOS.DependencyInterface;
using System;
using System.Threading.Tasks;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneService))]

namespace ScnDiscounts.iOS.DependencyInterface
{
    public class PhoneService : IPhoneService
    {
        public string DeviceId => UIDevice.CurrentDevice.IdentifierForVendor.AsString();

        private static double PixelsToDp(double value)
        {
            return value / UIScreen.MainScreen.Scale;
        }

        public Thickness SafeAreaInsets
        {
            get
            {
                Thickness result;

                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {
                    var insets = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets;

                    result = new Thickness(
                        PixelsToDp(insets.Left),
                        PixelsToDp(insets.Top),
                        PixelsToDp(insets.Right),
                        PixelsToDp(insets.Bottom));
                }
                else
                {
                    var statusBarHeight = UIApplication.SharedApplication.StatusBarFrame.Height;
                    result = new Thickness(0, statusBarHeight, 0, 0);
                }

                return result;
            }
        }

        public bool HasSafeAreaSupport => UIDevice.CurrentDevice.CheckSystemVersion(11, 0);

        public void DialNumber(string number)
        {
            var url = new NSUrl($"tel:{number}");
            UIApplication.SharedApplication.OpenUrl(url);
        }

        public void SendEmail(string toEmail, string subject = null, string text = null)
        {
            if (MFMailComposeViewController.CanSendMail)
            {
                var mailController = new MFMailComposeViewController();

                if (!string.IsNullOrEmpty(toEmail))
                    mailController.SetToRecipients(toEmail.Split(new[] {',', ';', ' '},
                        StringSplitOptions.RemoveEmptyEntries));

                if (!string.IsNullOrEmpty(subject))
                    mailController.SetSubject(subject);

                if (!string.IsNullOrEmpty(text))
                    mailController.SetMessageBody(text, false);

                mailController.Finished += (sender, args) =>
                {
                    var controller = (MFMailComposeViewController) sender;
                    controller.InvokeOnMainThread(() => controller.DismissViewController(true, null));
                };

                UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(mailController, true,
                    null);
            }
        }

        public void OpenGpsSettings()
        {
            var url = new NSUrl(UIApplication.OpenSettingsUrlString);
            UIApplication.SharedApplication.OpenUrl(url);
        }

        public void LaunchApp(string appId, string appUrl, string marketUrl, string webUrl)
        {
            var uri = new NSUrl(appUrl);
            var isOpened = UIApplication.SharedApplication.CanOpenUrl(uri) &&
                           UIApplication.SharedApplication.OpenUrl(uri);

            if (!isOpened)
            {
                uri = new NSUrl(marketUrl);
                isOpened = UIApplication.SharedApplication.CanOpenUrl(uri) &&
                           UIApplication.SharedApplication.OpenUrl(uri);

                if (!isOpened)
                    UIApplication.SharedApplication.OpenUrl(new NSUrl(webUrl));
            }
        }

        public bool CheckNotificationPermission()
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                UNUserNotificationCenter.Current.GetNotificationSettings(settings =>
                {
                    var result = settings.AuthorizationStatus == UNAuthorizationStatus.Authorized;
                    taskCompletionSource.SetResult(result);
                });
            }
            else
            {
                var types = UIApplication.SharedApplication.CurrentUserNotificationSettings.Types;
                var result = types != UIUserNotificationType.None;
                taskCompletionSource.SetResult(result);
            }

            taskCompletionSource.Task.Wait(5000);

            return taskCompletionSource.Task.Result;
        }
    }
}