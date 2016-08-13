using System;

namespace Net.Teirlinck.Utils
{
    public interface ITimeProvider
    {
        DateTimeOffset Now();

        DateTimeOffset Today();

        void SetCurrentTime(DateTimeOffset time);

        void SetCurrentTime(int year, int month, int day, int hours, TimeSpan offset);

        void SetCurrentTime(int year, int month, int day, int hours, int minutes, int seconds, TimeSpan offset);
    }

    public class SystemTimeProvider : ITimeProvider
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.Now;
        }

        public DateTimeOffset Today()
        {
            return DateTime.Today;
        }

        public void SetCurrentTime(DateTimeOffset time)
        {
            throw new NotImplementedException($"Not implemented for {nameof(SystemTimeProvider)}");
        }

        public void SetCurrentTime(int year, int month, int day, int hours, TimeSpan offset)
        {
            throw new NotImplementedException($"Not implemented for {nameof(SystemTimeProvider)}");
        }

        public void SetCurrentTime(int year, int month, int day, int hours, int minutes, int seconds, TimeSpan offset)
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

        public void SetCurrentTime(int year, int month, int day, int hours, TimeSpan offset)
        {
            _currentTime = new DateTimeOffset(year, month, day, hours, 0, 0, offset);
        }

        public void SetCurrentTime(int year, int month, int day, int hours, int minutes, int seconds, TimeSpan offset)
        {
            _currentTime = new DateTimeOffset(year, month, day, hours, minutes, seconds, offset);
        }

        public DateTimeOffset Today()
        {
            return _currentTime.Date;
        }
    }
}
