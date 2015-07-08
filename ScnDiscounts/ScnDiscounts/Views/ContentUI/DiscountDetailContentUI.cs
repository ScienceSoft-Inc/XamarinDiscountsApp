﻿using ScnDiscounts.Helpers;
using Xamarin.Forms;

namespace ScnDiscounts.Views.ContentUI
{
    class DiscountDetailContentUI : RootContentUI
    {
        public DiscountDetailContentUI()
        {
            _title = new PropertyLang("Information", "Информация", "інфармацыя");
            _txtShowOnMap = new PropertyLang("Show on map", "Показать на карте", "Паказаць на мапе");
        }

        private PropertyLang _txtShowOnMap;
        public string TxtShowOnMap
        {
            get { return _txtShowOnMap.ActualValue(); }
        }

        public string IconPhone
        {
            get { return Device.OnPlatform("Icon/phone.png", "ic_menu_phone.png", "Assets/Icon/phone.png"); }
        }

        public string ImgPercentLabel
        {
            get { return Device.OnPlatform("Image/discount_label.png", "discount_label.png", "Assets/Image/discount_label.png"); }
        }
    }
}
