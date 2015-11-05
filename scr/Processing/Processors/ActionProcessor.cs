using Domain;
using MouseSimulation.Simulators;
using Processing.Actions;
using Processing.States;
using System;
using System.Collections.Generic;

namespace Processing.Processors
{
    internal abstract class ActionProcessor
    {
        public ActionProcessor()
        {
        }

        internal abstract void Process(Face detectedFace);

        internal virtual void Reset() { }
    }
}