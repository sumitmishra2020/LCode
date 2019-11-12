using System;
using System.Collections.Generic;

namespace DiscussGoogle
{
    //https://leetcode.com/discuss/interview-experience/424540/google-l5-mtv-oct-2019-offer
    partial class Program
    {
        static void Main(string[] args)
        {
            IList<Interval> schedules = new List<Interval>(new Interval[] { new Interval(1, 2), new Interval(5, 6), new Interval(1, 3), new Interval(4, 10)});
            IList<IList<Interval>> empSchedules = new List<IList<Interval>>();
            empSchedules.Add(schedules);
            var freeIntervals = EmployeeFreeTime(empSchedules);
            Console.WriteLine("Hello World!");
        }
    }
}
