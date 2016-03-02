﻿using UnityEngine;
using Debug = UnityEngine.Debug;

namespace kOS.AddOns.KerbalAlarmClock
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class KACEventHandler : MonoBehaviour
    {
        public void Start()
        {
            KACWrapper.InitKACWrapper();
            if (KACWrapper.APIReady)
            {
                //All good to go
                //register Event Handler
                KACWrapper.KAC.onAlarmStateChanged += KAC_onAlarmStateChanged;

                Debug.Log(string.Format("{0} Kerbal Alarm Clock found, Alarms Count {1}", KSPLogger.LOGGER_PREFIX, KACWrapper.KAC.Alarms.Count));
            }
        }

        public void OnDestroy()
        {
            if (KACWrapper.APIReady)
            {
                //destroy the event hook
                KACWrapper.KAC.onAlarmStateChanged -= KAC_onAlarmStateChanged;
            }
        }

        private void KAC_onAlarmStateChanged(KACWrapper.KACAPI.AlarmStateChangedEventArgs e)
        {
            //output whats happened
            Debug.Log(string.Format("{0}, caught Event from alarm {1}, event type {2}", KSPLogger.LOGGER_PREFIX, e.alarm.Name, e.eventType));
        }
    }
}