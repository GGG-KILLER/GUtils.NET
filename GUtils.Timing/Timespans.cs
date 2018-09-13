/*
 * Copyright © 2016 GGG KILLER <gggkiller2@gmail.com>
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software
 * and associated documentation files (the “Software”), to deal in the Software without
 * restriction, including without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom
 * the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or
 * substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
 * BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System;

namespace GUtils.Timing
{
    public static class Timespans
    {
        public const Double TicksPerHour = TimeSpan.TicksPerHour;
        public const Double TicksPerMinute = TicksPerHour / 60D;
        public const Double TicksPerSecond = TicksPerMinute / 60D;
        public const Double TicksPerMillisecond = TicksPerSecond / 1000D;
        public const Double TicksPerMicrosecond = TicksPerMillisecond / 1000D;
        public const Double TicksPerNanosecond = TicksPerMicrosecond / 1000D;

        public static String Format ( Int64 Ticks, String Format = "{0:##00.00}{1}" )
        {
            if ( Ticks > TicksPerHour )
                return String.Format ( Format, Ticks / TicksPerHour, "h" );
            else if ( Ticks > TicksPerMinute )
                return String.Format ( Format, Ticks / TicksPerMinute, "m" );
            else if ( Ticks > TicksPerSecond )
                return String.Format ( Format, Ticks / TicksPerSecond, "s" );
            else if ( Ticks > TicksPerMillisecond )
                return String.Format ( Format, Ticks / TicksPerMillisecond, "ms" );
            else if ( Ticks > TicksPerMicrosecond )
                return String.Format ( Format, Ticks / TicksPerMicrosecond, "μs" );
            else
                return String.Format ( Format, Ticks / TicksPerNanosecond, "ns" );
        }
    }
}
