using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okx.Net.Extensions
{
    internal static class OkxExtensions
    {
        public static long ToUnixTimeMilliSeconds(this DateTime @this)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((@this - epoch).TotalSeconds) * 1000 + @this.Millisecond;
        }
    }
}
