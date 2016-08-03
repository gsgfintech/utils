using System;

namespace Net.Teirlinck.Utils
{
    public interface ITimeProvider
    {
        DateTime Now();

        DateTime Today();

        void SetCurrentTime(DateTime time);

        void SetCurrentTime(int year, int month, int day, int hours, int minutes = 0, int seconds = 0);
    }

    public class SystemTimeProvider : ITimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }

        public DateTime Today()
        {
            return DateTime.Today;
        }

        public void SetCurrentTime(DateTime time)
        {
            throw new NotImplementedException($"Not implemented for {nameof(SystemTimeProvider)}");
        }

        public void SetCurrentTime(int year, int month, int day, int hours, int minutes = 0, int seconds = 0)
        {
            throw new NotImplementedException($"Not implemented for {nameof(SystemTimeProvider)}");
        }
    }

    public class ManualTimeProvider : ITimeProvider
    {
        private DateTime _currentTime;

        /// <summary>
        /// Default constructor initializes the current time to the current system time
        /// THis will need to be overriden to the desired time using method SetCurrentTime
        /// </summary>
        public ManualTimeProvider()
        {
            _currentTime = DateTime.Now;
        }

        public DateTime Now()
        {
            return _currentTime;
        }

        public void SetCurrentTime(DateTime time)
        {
            _currentTime = time;
        }

        public void SetCurrentTime(int year, int month, int day, int hours, int minutes = 0, int seconds = 0)
        {
            _currentTime = new DateTime(year, month, day, hours, minutes, seconds);
        }

        public DateTime Today()
        {
            return _currentTime.Date;
        }
    }
}
