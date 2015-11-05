using System.Collections.Generic;
using System.Linq;

namespace Common.PriorityAlgorithm
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

        public void Clear()
        {
            this.lastReturned = null;
            this.priorityElements.Clear();
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
                this.SwapPriority(1, this.lastReturned);
            }

            this.lastReturned = null;
        }

        public void SetSecond(PriorityItem<T> priorityItem)
        {
            if (priorityItem.Priority != 2)
            {
                this.SwapPriority(2, priorityItem);
            }
        }

        public void SetFirst(PriorityItem<T> priorityItem)
        {
            if (priorityItem.Priority != 1)
            {
                this.SwapPriority(1, priorityItem);
            }
        }

        private void SwapPriority(uint priority, PriorityItem<T> itemWithPriority)
        {
            var item = this.priorityElements.First(x => x.Priority == priority);

            item.Priority = itemWithPriority.Priority;

            itemWithPriority.Priority = priority;
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