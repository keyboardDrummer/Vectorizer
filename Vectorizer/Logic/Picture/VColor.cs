using System;
using System.Drawing;
using System.Linq;

namespace Vectorizer.Logic.Picture
{
    [Serializable]
    public class VColor : IEquatable<VColor>
    {
        public const int COMPONENT_COUNT = 3;
        private readonly Byte[] components = new Byte[COMPONENT_COUNT];
        public enum Component { R, G, B }

        public Byte R { get { return components[0]; } set { components[0] = value; } }
        public Byte G { get { return components[1]; } set { components[1] = value; } }
        public Byte B { get { return components[2]; } set { components[2] = value; } }

        public VColor(Byte Rp, Byte Gp, Byte Bp)
        {
            R = Rp; G = Gp; B = Bp;
        }

        public VColor(Byte[] components)
        {
            for (int i = 0; i < COMPONENT_COUNT; i++)
            {
                this.components[i] = components[i];
            }
        }

        public VColor(VColor clone)
        {
            for (int i = 0; i < COMPONENT_COUNT; i++)
            {
                components[i] = clone.components[i];
            }
        }

        public VColor(Color clone)
        {
            R = clone.R; 
            G = clone.G; 
            B = clone.B;
        }
        public Byte this[int i]
        {
            get
            {
                return components[i];
            }
            set
            {
                components[i] = value;
            }
        }

        public int this[Component c]
        {
            get
            {
                return components[(int)c];
            }
        }

        public int Length
        {
            get
            {
                int error = 0;
                for (int i = 0; i < components.Length; i++)
                {
                    error += components[i];
                }
                return error;
            }
        }

        public int GetAbsColorError(VColor source)
        {
            return components.Select((t, i) => Math.Abs(source.components[i] - t)).Sum();
        }

        public Boolean Equals(VColor other)
        {
            for (int i = 0; i < components.Length; i++)
            {
                if (other.components[i] != components[i])
                    return false;
            }
            return true;
        }

        public static implicit operator VColor(Color vColor)
        {
            return new VColor(vColor);
        }

        public static implicit operator Color(VColor vColor) 
        {
            return Color.FromArgb(255, vColor.R, vColor.G, vColor.B); 
        }
    }
}
