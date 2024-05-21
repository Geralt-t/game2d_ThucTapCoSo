using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class Timer
    {
        private float startTime;
        private float duration;
        private float targetTime;
        private bool isActive;
        public event Action OnTimerDone;
        public Timer(float duration)
        {
            this.duration = duration;
        }
        public void StartTimer()
        {
            startTime = Time.time;
            targetTime = startTime + duration;
            isActive = true;
        }
        public void StopTimer()
        {
            isActive = false;
        }
        public void Tick()
        {
            if (!isActive) return;
            if (Time.time >= targetTime)
            {
                OnTimerDone?.Invoke();
                StopTimer();
            }
        }
    }
}
