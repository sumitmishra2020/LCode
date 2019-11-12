using System;
using System.Collections.Generic;
using System.Text;

namespace DiscussGoogle
{

    // Definition for an Interval.
    public class Interval
    {
        public int start;
        public int end;

        public Interval() { }
        public Interval(int _start, int _end)
        {
            start = _start;
            end = _end;
        }
    }

    partial class Program
    {
        public static IList<Interval> EmployeeFreeTime(IList<IList<Interval>> schedule)
        {
            const int START = 0;
            const int END = 1;
            IList<Interval> freeIntervals = new List<Interval>();
            List<List<int>> allIntervals = new List<List<int>>();
            foreach(List<Interval> employeeTime in schedule)
            {
                foreach (Interval span in employeeTime)
                {
                    allIntervals.Add(new List<int>() { span.start, START });
                    allIntervals.Add(new List<int>() { span.end, END });
                }
            }

            allIntervals.Sort((x, y) => x[0] == y[0] ? (x[0] - y[0]) : (x[1] - y[1]));

            int past = -1;
            int balance = 0;
            foreach(List<int> span in allIntervals)
            {
                if(balance == 0 && past >= 0)
                {
                    freeIntervals.Add(new Interval(past, span[0]));
                }
                balance += span[1] == START ? 1 : -1;
                past = span[0];
            }
            return freeIntervals;
        }
    }
}
