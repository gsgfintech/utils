using System;

namespace Net.Teirlinck.Utils
{
    public interface ITimeProvider
    {
        DateTime Now();
    }

    public class CurrentTimeProvider : ITimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
