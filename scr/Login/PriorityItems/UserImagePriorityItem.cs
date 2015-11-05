using Common.PriorityAlgorithm;

namespace Login.PriorityItems
{
    public class UserImagePriorityItem : PriorityItem<UserImageItem>
    {
        public UserImagePriorityItem(UserImageItem item)
            : base(item, 0)
        {
        }
    }
}