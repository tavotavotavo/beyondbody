using Emgu.CV;
using Emgu.CV.Structure;
namespace Login.PriorityItems
{
    public class UserImageItem
    {
        public UserImageItem(Image<Bgr, byte> image, double resizeScale, string path)
        {
            this.Image = image;
            this.ResizeScale = resizeScale;
            this.Path = path;
        }

        public Image<Bgr, byte> Image { get; set; }

        public double ResizeScale { get; set; }

        public string Path { get; set; }

        public int Brightness { get; set; }
    }
}