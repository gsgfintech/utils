using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
