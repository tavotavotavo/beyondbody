namespace Processing.States
{
    abstract internal class State<TDomain>
    {
        internal abstract void Next(TDomain face);
    }
}