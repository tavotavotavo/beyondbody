namespace Detection.PriorityAlgorithm
{
    public class RotatedFacePriorityItem : PriorityItem<RotatedFaceItem>
    {
        public RotatedFacePriorityItem(RotatedFaceItem item)
            : base(item, 0)
        {
        }
    }
}