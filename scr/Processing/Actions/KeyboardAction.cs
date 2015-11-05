using Domain;
using Processing.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Processing.Actions
{
    internal abstract class KeyboardAction<TState> : BaseAction<TState, Face>
        where TState : FaceState
    {
    }
}