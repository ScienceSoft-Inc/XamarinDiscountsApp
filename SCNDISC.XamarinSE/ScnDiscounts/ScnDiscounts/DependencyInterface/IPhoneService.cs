﻿using Xamarin.Forms;

namespace ScnDiscounts.DependencyInterface
{
    public interface IPhoneService
    {
        string DeviceId { get; }
        Thickness SafeAreaInsets { get; }
        bool HasSafeAreaSupport { get; }
        bool CheckNotificationPermission();
        void DialNumber(string number);
        void SendEmail(string toEmail, string subject = null, string text = null);
        void OpenGpsSettings();
        void LaunchApp(string appId, string appUrl, string marketUrl, string webUrl);
    }
}
