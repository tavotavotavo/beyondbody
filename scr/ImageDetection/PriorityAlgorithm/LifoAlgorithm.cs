using System.Collections.Generic;
using System.Linq;

namespace Detection.PriorityAlgorithm
{
    public class LifoAlgorithm<T>
    {
        private IList<PriorityItem<T>> priorityElements;
        private PriorityItem<T> lastReturned;

        public LifoAlgorithm()
        {
            this.priorityElements = new List<PriorityItem<T>>();
        }

        public void AddAlgorithmItem(PriorityItem<T> priorityItem)
        {
            if (priorityItem.Priority == 0)
            {
                priorityItem.Priority = (uint)this.priorityElements.Count + 1;
            }

            this.priorityElements.Add(priorityItem);
        }

        public int GetCount()
        {
            return this.priorityElements.Count;
        }

        public T Next()
        {
            if (this.lastReturned != null)
                return this.Get(lastReturned.Priority + 1);
            else
                return this.Get(1);
        }

        public void SetFirst()
        {
            if (this.lastReturned.Priority != 1)
            {
                var first = this.priorityElements.First(x => x.Priority == 1);

                first.Priority = this.lastReturned.Priority;

                this.lastReturned.Priority = 1;
            }

            this.lastReturned = null;
        }

        private T Get(uint priority)
        {
            var item = this.priorityElements.FirstOrDefault(x => x.Priority == priority);

            if (item == null)
            {
                this.lastReturned = this.priorityElements.FirstOrDefault(x => x.Priority == 1);
                return this.lastReturned.Item;
            }
            else
            {
                this.lastReturned = item;
                return item.Item;
            }
        }
    }
}