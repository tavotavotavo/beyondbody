namespace Common.PriorityAlgorithm
{
    public abstract class PriorityItem<T>
    {
        public PriorityItem(T detector, uint priority)
        {
            // TODO: Complete member initialization
            this.Item = detector;
            this.Priority = priority;
        }

        public T Item { get; set; }

        public uint Priority { get; set; }
    }
}