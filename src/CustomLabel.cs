using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Wolf_Server_Manager
{
    public class CustomLabel : Label
    {
        public class LineSeparator
        {
            public Point StartLocation;
            public Point EndLocation;

            public LineSeparator(Point startLocation, Point endLocation)
            {
                StartLocation = startLocation;
                EndLocation = endLocation;
            }
        }

        StringFormat MeasureStringFormat;

        Rectangle BorderRectangle;

        Pen LineSeparatorPen;
        Pen BorderPen;

        List<LineSeparator> LineSeparators;

        Color _borderColor;
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                BorderPen = new Pen(_borderColor);
            }
        }

        int _borderWidth;
        public int BorderWidth
        {
            get
            {
                return _borderWidth;
            }
            set
            {
                _borderWidth = value;
                BorderPen.Width = _borderWidth;
            }
        }

        Color _lineSeparatorColor;
        public Color LineSeparatorColor
        {
            get
            {
                return _lineSeparatorColor;
            }
            set
            {
                _lineSeparatorColor = value;
                LineSeparatorPen = new Pen(_lineSeparatorColor);
            }
        }

        int _lineSeparatorWidth;
        public int LineSeparatorWidth
        {
            get
            {
                return _lineSeparatorWidth;
            }
            set
            {
                _lineSeparatorWidth = value;

                if (LineSeparatorPen != null)
                {
                    LineSeparatorPen.Width = _lineSeparatorWidth;
                }
            }
        }

        public int ConsecutiveSpacesUntilLineSeparator { get; set; }

        public CustomLabel()
        {
            MeasureStringFormat = (StringFormat)StringFormat.GenericDefault.Clone();
            MeasureStringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            // Update border rectangle location/size
            BorderRectangle = new Rectangle(DisplayRectangle.Location, new Size(DisplayRectangle.Width - 1, DisplayRectangle.Height - 1));

            // Update line separator locations
            LineSeparators = new List<LineSeparator>();
            if (ConsecutiveSpacesUntilLineSeparator > 0)
            {
                int ConsecutiveSpaces = 0;
                var Graphics = CreateGraphics();
                for (int i = 0; i < Text.Length; ++i)
                {
                    if (Text[i] == ' ')
                    {
                        if (++ConsecutiveSpaces == ConsecutiveSpacesUntilLineSeparator)
                        {
                            var String = Text.Substring(0, i);
                            var StringSize = Graphics.MeasureString(String, Font, Width, MeasureStringFormat);
                            var LocationX = DisplayRectangle.X + (int)StringSize.Width;

                            LineSeparators.Add(new LineSeparator(new Point(LocationX, DisplayRectangle.Y),
                                new Point(LocationX, DisplayRectangle.Y + DisplayRectangle.Height)));
                            ConsecutiveSpaces = 0;
                        }
                    }
                    else
                    {
                        ConsecutiveSpaces = 0;
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Draws text and background
            base.OnPaint(e);

            // Draws border
            e.Graphics.DrawRectangle(BorderPen, BorderRectangle);

            // Draws line separators
            foreach (var LineSeparator in LineSeparators)
            {
                e.Graphics.DrawLine(LineSeparatorPen, LineSeparator.StartLocation, LineSeparator.EndLocation);
            }
        }
    }
}
