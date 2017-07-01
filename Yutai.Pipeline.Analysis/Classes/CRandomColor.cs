using System;
using System.Drawing;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class CRandomColor
    {
        public CRandomColor()
        {
        }

        public Color GetRandColor()
        {
            Random random = new Random((int) DateTime.Now.Ticks);
            int num = random.Next()%256;
            int num1 = random.Next()%256;
            int num2 = random.Next()%256;
            Color color = new Color();
            return Color.FromArgb(num, num1, num2);
        }
    }
}