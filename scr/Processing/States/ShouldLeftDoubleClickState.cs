using Domain;
using Processing.Actions;
using System.Diagnostics;

namespace Processing.States
{
    internal class ShouldLeftDoubleClickState : ShouldLeftClickState
    {
        internal ShouldLeftDoubleClickState(CursorAction<FaceState> cursorAction)
            : base(cursorAction)
        {
        }
    }
}