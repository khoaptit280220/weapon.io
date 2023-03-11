using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Khoant
{
    public static class EventController
    {
        public static Action OnChangeModelSkin;

        public static Action<int> OnChangeViewSkin;

        public static Action MainSkin;

        public static Action Lose;

        public static Action Win;

        public static Action MainWeapon;

        public static Action AfterWeapon;
    }
}