using Domain.Extensions;
using System.Drawing;

namespace Domain
{
    public class Face : ZoneEntity
    {
        private Eye rightEye;
        private Eye leftEye;
        private Mouth mouth;
        private Nose nose;

        public bool IsFrontal { get; set; }

        public bool IsProfile { get { return this.IsLeftProfile || this.IsRightProfile; } }

        public bool IsRotated { get { return this.IsLeftRotated || this.IsRightRotated; } }

        public bool IsLeftRotated { get; set; }

        public bool IsRightRotated { get; set; }

        public bool IsLeftProfile { get; set; }

        public bool IsRightProfile { get; set; }

        public bool HasBothEyes
        {
            get
            {
                return !this.RightEye.IsEmpty &&
                    !this.LeftEye.IsEmpty;
            }
        }

        public Eye RightEye
        {
            get
            {
                return this.GetValueOrDefault<Eye>(this.rightEye);
            }

            set
            {
                this.rightEye = value;
            }
        }

        public Eye LeftEye
        {
            get
            {
                return this.GetValueOrDefault<Eye>(this.leftEye);
            }

            set
            {
                this.leftEye = value;
            }
        }

        public Mouth Mouth
        {
            get
            {
                return this.GetValueOrDefault<Mouth>(this.mouth);
            }

            set
            {
                this.mouth = value;
            }
        }

        public Nose Nose
        {
            get
            {
                return this.GetValueOrDefault<Nose>(this.nose);
            }

            set
            {
                this.nose = value;
            }
        }

        public bool IsBlinkingRightEye
        {
            get
            {
                return this.RightEye.IsEmpty &&
                    !this.LeftEye.IsEmpty;
            }
        }

        public bool IsBlinkingLeftEye
        {
            get
            {
                return !this.RightEye.IsEmpty &&
                    this.LeftEye.IsEmpty;
            }
        }

        public bool HasBothEyesOpen
        {
            get
            {
                return !this.RightEye.IsEmpty &&
                    !this.LeftEye.IsEmpty;
            }
        }

        public bool HasBothEyesClosed
        {
            get
            {
                return this.RightEye.IsEmpty &&
                    this.LeftEye.IsEmpty;
            }
        }

        public bool IsBlinking
        {
            get { return this.IsBlinkingLeftEye || this.IsBlinkingRightEye; }
        }

        public bool IsFake { get; set; }

        public bool HasEyesCentered
        {
            get
            {
                var rightEyeCondition = this.RightEye.Center.X < Image.ROI.Center().X;
                var leftEyeCondition = this.LeftEye.Center.X > Image.ROI.Center().X;

                if (this.LeftEye.IsEmpty && this.RightEye.IsEmpty)
                    return true;

                if (this.LeftEye.IsEmpty)
                    return rightEyeCondition;

                if (this.RightEye.IsEmpty)
                    return leftEyeCondition;

                return leftEyeCondition
                    && rightEyeCondition;
            }
        }

        public Point EyeCenterPoint
        {
            get
            {
                var eye = this.RightEye.IsEmpty ? this.LeftEye : this.RightEye;

                return new Point
                {
                    Y = eye.Center.Y,
                    X = this.Image.Center().X,
                };
            }
        }

        public bool IsIntoControlZone
        {
            get
            {
                return this.ControlZone.X < this.Center.X &&
                    this.ControlZone.Y < this.Center.Y &&
                    this.ControlZone.X + this.ControlZone.Width > this.Center.X &&
                    this.ControlZone.Y + this.ControlZone.Height > this.Center.Y;
            }
        }

        public Rectangle MouthZone
        {
            get
            {
                var newDivisor = 4;

                var newRectangle = new Rectangle()
                {
                    X = this.Zone.X + this.Zone.Width / newDivisor,
                    Width = this.Zone.Width - this.Zone.Width / newDivisor - this.Zone.Width / newDivisor,
                    Y = this.Zone.Y + this.Zone.Height / 2 + this.Zone.Height / 2 / 4,
                    Height = this.Zone.Height / 2 - this.Zone.Height / 2 / 4
                };

                return newRectangle;
            }
        }

        public Rectangle LoginZone
        {
            get
            {
                var y = this.Zone.Y + this.Zone.Height / 8;
                var x = this.MouthZone.X;
                var width = this.MouthZone.Width;

                if (this.HasBothEyesOpen)
                {
                    y = this.RightEye.Zone.Y > this.LeftEye.Zone.Y ? this.LeftEye.Zone.Y : this.RightEye.Zone.Y;
                    x = this.RightEye.Center.X;
                    width = this.LeftEye.Center.X - x;
                }
                else if (this.IsBlinkingLeftEye)
                {
                    y = this.RightEye.Zone.Y;
                    x = this.RightEye.Center.X;
                    width = (this.Center.X - this.RightEye.Center.X) * 2;
                }
                else if (this.IsBlinkingRightEye)
                {
                    y = this.LeftEye.Zone.Y;
                    x = this.LeftEye.Center.X - ((this.LeftEye.Center.X - this.Center.X) * 2);
                    width = (this.LeftEye.Center.X - this.Center.X) * 2;
                }

                var height = this.MouthZone.Y + this.MouthZone.Height - y;

                return new Rectangle { X = x, Y = y, Width = width, Height = height };
            }
        }

        public Rectangle ControlZone
        {
            get
            {
                var middleX = this.Image.ROI.X + this.Image.ROI.Width / 2;
                var middleY = this.Image.ROI.Y + this.Image.ROI.Height / 2;
                var distanceX = 0;

                //Lo comento porque nunca tiene los ojos cargados
                if (this.IsFrontal)
                {
                    if (this.HasBothEyesOpen)
                    {
                        if (this.HasEyesCentered)
                        {
                            distanceX = (int)((this.LeftEye.Center.X - this.RightEye.Center.X) / 1.5);
                        }
                        else
                        {
                            distanceX = (int)((this.LeftEye.Center.X - this.RightEye.Center.X) / 1.5);
                        }
                    }
                    else
                    {
                        if (this.IsBlinking)
                            if (this.HasEyesCentered)
                                distanceX = this.Zone.Width / 3;
                            else
                                distanceX = this.Zone.Width / 7;
                        else
                            distanceX = this.Zone.Width / 5;
                    }
                }
                else
                {
                    distanceX = this.Zone.Width / 7;
                }

                var distanceY = middleY;
                var newRectangle = new Rectangle()
                {
                    X = middleX - distanceX,
                    Width = distanceX * 2,
                    Y = middleY - distanceY,
                    Height = distanceY * 2
                };

                return newRectangle;
            }
        }

        public Rectangle RotatedControlZone
        {
            get
            {
                return this.ControlZone;
            }
        }

        public bool IsIntoRotatedControlZone
        {
            get
            {
                if (this.ControlZone.X < this.Center.X &&
                    this.ControlZone.Y < this.Center.Y &&
                    this.ControlZone.X + this.ControlZone.Width > this.Center.X &&
                    this.ControlZone.Y + this.ControlZone.Height > this.Center.Y)
                {
                    return this.RotatedControlZone.X < this.Center.X &&
                            this.RotatedControlZone.Y < this.Center.Y &&
                            this.RotatedControlZone.X + this.RotatedControlZone.Width > this.Center.X &&
                            this.RotatedControlZone.Y + this.RotatedControlZone.Height > this.Center.Y;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool EyesAreRotated
        {
            get
            {
                return !this.RightEye.IsEmpty &&
                    !this.LeftEye.IsEmpty &&
                    (!this.RightEye.Zone.HasPoint(this.LeftEye.Center) ||
                    !this.LeftEye.Zone.HasPoint(this.RightEye.Center));
            }
        }

        public void Convert()
        {
            if (this.HasBothEyesOpen && this.IsFrontal)
            {
                if (this.LeftEye.Center.Y < this.RightEye.Zone.Y)
                    this.IsLeftRotated = true;
                if (this.RightEye.Center.Y < this.LeftEye.Zone.Y)
                    this.IsRightRotated = true;

                if (this.IsLeftRotated || this.IsRightRotated)
                {
                    this.IsFrontal = false;
                }
            }
        }

        public bool IsZoneOutOfControl
        {
            get
            {
                if (this.ControlZone.IsEmpty)
                    return false;

                return (this.Zone.X > this.ControlZone.X + this.ControlZone.Width ||
                    this.Zone.X + this.Zone.Width < this.ControlZone.X) ||
                    (this.Center.X > this.ControlZone.Center().X + this.ControlZone.Width + this.ControlZone.Width / 3 ||
                    this.Center.X < this.ControlZone.Center().X - this.ControlZone.Width - this.ControlZone.Width / 3);
            }
        }

        public bool IsOuttaControl { get; set; }

        public void FlipZoneHorizontally()
        {
            var x = this.Image.Width - this.Zone.X - this.Zone.Width;

            this.Zone = new Rectangle 
            { 
                X = x,
                Y = this.Zone.Y,
                Height = this.Zone.Height,
                Width = this.Zone.Width
            };
        }

        public Rectangle ReplacedZone { get; set; }
    }
}