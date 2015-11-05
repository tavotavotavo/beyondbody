using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace Domain.Extensions
{
    public static class DrawingExtensions
    {
        public static Point Center(this Rectangle rect)
        {
            return new Point(rect.X + rect.Width / 2,
                             rect.Y + rect.Height / 2);
        }

        public static Point Center(this Image<Bgr, byte> image)
        {
            return new Point(image.ROI.X + image.ROI.Width / 2,
                             image.ROI.Y + image.ROI.Height / 2);
        }

        public static bool HasPoint(this Rectangle rect, Point point)
        {
            return point.X > rect.X && point.X < rect.X + rect.Width &&
                point.Y > rect.Y && point.Y < rect.Y + rect.Height;
        }
    }
}