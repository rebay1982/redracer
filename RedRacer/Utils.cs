using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer
{
  public static class Utils
  {
    public static double GetCurrentTimeStamp()
    {
      return (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
    }
  }
}
