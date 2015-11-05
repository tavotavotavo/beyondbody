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
    }
}