//
//  ConsoleTaskReporter.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using St = MCART.Resources.Strings;
namespace MCART.Types.TaskReporter
{
    public class ConsoleTaskReporter : TaskReporter
    {
        short sze = 20;
        bool ap;
        MCART.Types.Point p;
        public short ProgressSize
        {
            get { return sze; }
            set
            {
                if (value.IsBetween((short)1, (short)Console.BufferWidth)) sze = value;
                else throw new ArgumentOutOfRangeException(nameof(value));
            }
        }
        public MCART.Types.Point? PBarLocation
        {
            get { return p; }
            set
            {
                if (!(bool)value?.FitsInBox(1, 1, Console.BufferWidth, Console.BufferHeight))
                    throw new ArgumentOutOfRangeException(nameof(value));
                if (OnDuty) throw new InvalidOperationException();
                p = value.Value;
            }
        }
        public bool Verbose;

        void DoBegin()
        {
            SetOnDuty(true);
            Console.CursorVisible = false;
            if (Verbose) Console.Write(St.CtrlCCancel);
            if (p.IsNull())
            {
                ap = true;
                p = new MCART.Types.Point(Console.CursorLeft, Console.CursorTop);
            }
            else ap = false;
            for (short j = 1; j <= sze; j++) Console.Write("░");
        }
        void DoEnd(string msg = null, ConsoleColor c = ConsoleColor.Gray)
        {
            SetOnDuty(false);
            if (ap) p = default(MCART.Types.Point);
            Console.WriteLine();
            if (Verbose && !msg.IsNull())
            {
                Console.ForegroundColor = c;
                Console.WriteLine(msg);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        void DoRprt(string msg, float? pgr)
        {
            Console.SetCursorPosition((int)p.X, (int)p.Y);
            if (!pgr.IsNull())
            {
                int cnt = (int)(sze * 3 * pgr);
                short j = 0;
                while (j < sze | cnt > 0)
                {
                    if (cnt > 2) Console.Write('█');
                    else if (cnt > 1) Console.Write('▓');
                    else Console.Write('▒');
                    cnt -= 3;
                    j++;
                }
                for (; j < sze; j++) Console.Write('░');
            }
            else for (short j = 0; j < sze; j++) Console.Write('-');
            Console.Write(msg);
            while (Console.CursorLeft < Console.BufferWidth) Console.Write(' ');

        }
        public override void Begin()
        {
            if (OnDuty) throw new InvalidOperationException();
            DoBegin();
        }
        public override void BeginNonStop()
        {
            if (OnDuty) throw new InvalidOperationException();
            DoBegin();
        }
        public override void End() { DoEnd(); }
        public override void EndWithError(Exception ex = null)
        {
            if (ex.IsNull()) ex = new Exception();
            DoEnd(ex.StackTrace + "\n" + ex.Message, ConsoleColor.DarkRed);
        }
        public override void Report(ProgressEventArgs e)
        {
            DoRprt(e.HelpText, e.Progress);
        }
        public override void Report(string helpText)
        {
            DoRprt(helpText, null);
        }
        public override void Report(float? progress = default(float?), string helpText = null)
        {
            DoRprt(helpText, progress);
        }
        public override void Stop(ProgressEventArgs e)
        {
            DoEnd(e?.HelpText ?? St.UsrCncl);
        }
        public override void Stop(float? progress = default(float?), string helpText = null)
        {
            DoEnd(helpText ?? St.UsrCncl);
        }
        public override void Stop(string helpText)
        {
            DoEnd(helpText);
        }
    }
}