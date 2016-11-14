using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;

namespace GraphTesting
{
    internal static class RoundHelper
    {
        internal static int GetDifferenceLog(double min, double max)
        {
            return (int)Math.Log(Math.Abs(max - min));
        }

        internal static double Round(double number, int rem)
        {
            if (rem <= 0)
            {
                rem = MathHelper.Clamp(-rem, 0, 15);
                return Math.Round(number, rem);
            }
            else
            {
                double pow = Math.Pow(10, rem - 1);
                double val = pow * Math.Round(number / Math.Pow(10, rem - 1));
                return val;
            }
        }

        internal static RoundingInfo CreateRoundedRange(double min, double max)
        {
            double delta = max - min;

            if (delta == 0)
                return new RoundingInfo { Min = min, Max = max, Log = 0 };

            int log = (int)Math.Round(Math.Log10(Math.Abs(delta))) + 1;

            double newMin = Round(min, log);
            double newMax = Round(max, log);
            if (newMin == newMax)
            {
                log--;
                newMin = Round(min, log);
                newMax = Round(max, log);
            }

            return new RoundingInfo { Min = newMin, Max = newMax, Log = log };
        }
    }


    internal sealed class RoundingInfo
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public int Log { get; set; }
    }

    internal static class ArrayExtensions
    {
        internal static T Last<T>(this T[] array)
        {
            return array[array.Length - 1];
        }

        internal static T[] CreateArray<T>(int length, T defaultValue)
        {
            T[] res = new T[length];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = defaultValue;
            }
            return res;
        }

        internal static IEnumerable<Range<T>> GetPairs<T>(this T[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                yield return new Range<T>(array[i], array[i + 1]);
            }
        }
    }
}
