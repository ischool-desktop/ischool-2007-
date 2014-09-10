using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using SmartSchool.TagManage;

namespace SmartSchool
{
    class DataGridViewTagCell:DataGridViewTextBoxCell
    {
        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, null, null, errorText, cellStyle, advancedBorderStyle, paintParts);
            if (value is IEnumerable<TagInfo>)
            {
                SmoothingMode mode = graphics.SmoothingMode;//跟妳說喔，NotNetBar很機車喔，如果你把SmoothingMode改掉沒改回去，格線會亂亂劃喔。
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                IEnumerable<TagInfo> tags = (IEnumerable<TagInfo>)value;
                List<Color> colors = new List<Color>();
                foreach (TagInfo tag in tags)
                {
                    colors.Add(tag.Color);
                }
                colors.Reverse();
                if (colors.Count > 0)
                {
                    int w = cellBounds.Height - 9;
                    int totleW = cellBounds.Width - 6;
                    double spceace = colors.Count > 1 ? (double)(totleW - w * colors.Count) / (colors.Count - 1) : 0.0;
                    for (int i = 0; i < colors.Count; i++)
                    {
                        int x = cellBounds.X + 3 + w * (colors.Count - 1 - i) + (int)(spceace < 0 ? spceace * (colors.Count - 1 - i) : 0),
                            y = cellBounds.Y + 4;
                        Color[] myColors = { colors[i], Color.White, colors[i], colors[i] };
                        float[] myPositions = { 0.0f, 0.15f, 0.6f, 1.0f };
                        ColorBlend myBlend = new ColorBlend();
                        myBlend.Colors = myColors;
                        myBlend.Positions = myPositions;
                        using (LinearGradientBrush brush = new LinearGradientBrush(new Point(x + 2, y + 2), new Point(x + w - 2, y + w - 2), colors[i], colors[i]))
                        {
                            brush.InterpolationColors = myBlend;
                            brush.GammaCorrection = true;
                            graphics.FillEllipse(brush, x, y, w, w);
                        }
                        graphics.DrawEllipse(new Pen(Color.Black), x, y, w, w);
                    }
                }
                graphics.SmoothingMode = mode;
            }
        }

        public override Type EditType
        {
            get
            {
                return null;
            }
        }
    }


    class DataGridViewTagCellColumn : DataGridViewColumn
    {
        public DataGridViewTagCellColumn()
        {
            this.CellTemplate = new DataGridViewTagCell();
        }
    }
}
