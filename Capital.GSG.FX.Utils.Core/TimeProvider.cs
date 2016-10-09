using System;

namespace Capital.GSG.FX.Utils.Core
{
    public interface ITimeProvider
    {
        DateTimeOffset Now();

        DateTime Today();

        void SetCurrentTime(DateTimeOffset time);

        void SetCurrentTime(int year, int month, int day, int hours, TimeSpan offset, int minutes = 0, int seconds = 0);
    }

    public class SystemTimeProvider : ITimeProvider
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.Now;
        }

        public DateTime Today()
        {
            return DateTime.Today;
        }

        public void SetCurrentTime(DateTimeOffset time)
        {
            throw new NotImplementedException($"Not implemented for {nameof(SystemTimeProvider)}");
        }

        public void SetCurrentTime(int year, int month, int day, int hours, TimeSpan offset, int minutes = 0, int seconds = 0)
        {
            throw new NotImplementedException($"Not implemented for {nameof(SystemTimeProvider)}");
        }
    }

    public class ManualTimeProvider : ITimeProvider
    {
        private DateTimeOffset _currentTime;

        /// <summary>
        /// Default constructor initializes the current time to the current system time
        /// THis will need to be overriden to the desired time using method SetCurrentTime
        /// </summary>
        public ManualTimeProvider()
        {
            _currentTime = DateTimeOffset.Now;
        }

        public DateTimeOffset Now()
        {
            return _currentTime;
        }

        public void SetCurrentTime(DateTimeOffset time)
        {
            _currentTime = time;
        }

        public void SetCurrentTime(int year, int month, int day, int hours, TimeSpan offset, int minutes = 0, int seconds = 0)
        {
            _currentTime = new DateTimeOffset(year, month, day, hours, minutes, seconds, offset);
        }

        public DateTime Today()
        {
            return _currentTime.Date;
        }
    }
}
