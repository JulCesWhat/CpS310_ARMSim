﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace armsim.Extra_Classes
{
    class TraceLog
    {
        int traceCounter;
        bool isTraceLogEnabled;
        TextWriterTraceListener traceLog;

        public TraceLog()
        {
            // enable trace and debug logs in local directory
            traceLog = new TextWriterTraceListener(System.IO.File.CreateText("trace.log"));

            Debug.Listeners.Add(traceLog);

            isTraceLogEnabled = true;
            traceCounter = 0;
        }

        internal Program Program
        {
            get => default(Program);
            set
            {
            }
        }

        internal Program Program1
        {
            get => default(Program);
            set
            {
            }
        }

        public void resetTraceCounterToOne()
        {
            traceCounter = 1;

            traceLog.Flush(); // flush & close trace log file to be opened for review
            traceLog.Close();

            traceLog = new TextWriterTraceListener(System.IO.File.CreateText("trace.log"));
            Debug.Listeners.Add(traceLog);
        }

        public void WriteLineToLog(string str)
        {
            traceLog.WriteLine(str);
        }

        internal bool isEnabled()
        {
            return isTraceLogEnabled;
        }

        internal void turnOffTraceLog()
        {
            isTraceLogEnabled = false;
            traceLog.Flush(); // flush & close trace log file to be opened for review
            traceLog.Close();

        }

        internal void turnOnTraceLog()
        {
            // recreate trace log file in local directory
            if (isTraceLogEnabled)
            {
                traceLog.Flush(); // flush & close trace log file to be opened for review
                traceLog.Close();
            }

            // TODO: might
            traceLog = new TextWriterTraceListener(System.IO.File.CreateText("trace.log"));

            isTraceLogEnabled = true;
        }

        internal void updateStepCounterByOne()
        {
            traceCounter++;
        }

        internal int getTraceCounter()
        {
            return traceCounter;
        }

        internal void flush()
        {
            traceLog.Flush();
        }

        internal void close()
        {
            traceLog.Close();
        }
    }

    class DebugLog
    {
        TextWriterTraceListener debugLog;

        public DebugLog()
        {
            debugLog = new TextWriterTraceListener(System.IO.File.CreateText("debug.log"));
            Debug.Listeners.Add(debugLog);
        }

        internal Program Program
        {
            get => default(Program);
            set
            {
            }
        }

        internal Program Program1
        {
            get => default(Program);
            set
            {
            }
        }

        public void WriteLineToLog(string str)
        {
            debugLog.WriteLine(str);
        }

        internal void flush()
        {
            debugLog.Flush();
        }

        internal void close()
        {
            debugLog.Close();
        }
    }
}
