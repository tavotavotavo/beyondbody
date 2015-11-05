using Common.PriorityAlgorithm;
namespace Detection.PriorityAlgorithm
{
    public class ProfileFacePriorityItem : PriorityItem<ProfileFaceItem>
    {
        public ProfileFacePriorityItem(ProfileFaceItem item)
            : base(item, 0)
        {
        }
    }
}