using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MetroModernUIDemo
{
    class MultiHeaderDataGridView : MetroGrid
    {
        public class DataGridViewRowTopRow
        {
            public DataGridView GridView { get; set; }

            public List<DataGridViewCellTopRow> Cells { get; set; }

            public DataGridViewRowTopRow(DataGridView gridView)
            {
                GridView = gridView;
                Cells = new List<DataGridViewCellTopRow>();

                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    Cells.Add(new DataGridViewCellTopRow(gridView) { Index = i, ColumnIndex = i, ColumnSpan = 1, HeaderText = string.Empty });
                }
            }

            public class DataGridViewCellTopRow
            {
                public DataGridView GridView { get; set; }

                public int Index { get; set; }

                public int ColumnSpan { get; set; }

                public string HeaderText { get; set; }

                public int ColumnIndex { get; set; }

                public int SpanPreColumnWith
                {
                    get
                    {
                        int width = 0;

                        for (int i = ColumnIndex; i < Index; i++)
                        {
                            width += GridView.Columns[i].Width;
                        }

                        return width;
                    }
                }

                public int SpanColumnWith
                {
                    get
                    {
                        int width = 0;

                        for (int i = ColumnIndex; i < ColumnIndex + ColumnSpan; i++)
                        {
                            width += GridView.Columns[i].Width;
                        }

                        return width;
                    }
                }

                public DataGridViewCellTopRow(DataGridView gridView)
                {
                    GridView = gridView;
                }
            }
        }

        public DataGridViewRowTopRow TopRow { get; set; }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {

            //行标题
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                base.OnCellPainting(e);

                return;
            }

            //绘制一级标题、二级标题
            if (TopRow.Cells.Count > 0 && e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                using (Brush gridBrush = new SolidBrush(this.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        //擦除背景
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                        //绘制底部边框线
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom, e.CellBounds.Right, e.CellBounds.Bottom);

                        if (TopRow.Cells[e.ColumnIndex].ColumnSpan > 1)
                        {
                            if (e.ColumnIndex == this.Columns.Count - 1)
                            {
                                //绘制二级标题右边框线
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right, e.CellBounds.Top + e.ClipBounds.Height / 2, e.CellBounds.Right, e.CellBounds.Bottom);
                            }
                            else
                            {
                                //绘制二级标题右边框线
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top + e.ClipBounds.Height / 2, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                            }

                            //绘制二级标题顶部边框线
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - e.CellBounds.Height / 2, e.CellBounds.Right, e.CellBounds.Bottom - e.CellBounds.Height / 2);

                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Center;

                            //绘制一级标题文本
                            if (TopRow.Cells[e.ColumnIndex].ColumnIndex > -1 && !string.IsNullOrWhiteSpace(TopRow.Cells[e.ColumnIndex].HeaderText))
                            {
                                Rectangle rect = new Rectangle(e.CellBounds.X - TopRow.Cells[e.ColumnIndex].SpanPreColumnWith, e.CellBounds.Y, TopRow.Cells[e.ColumnIndex].SpanColumnWith, e.CellBounds.Height / 2);
                                e.Graphics.DrawString(TopRow.Cells[e.ColumnIndex].HeaderText, e.CellStyle.Font, new SolidBrush(e.CellStyle.ForeColor), rect, sf);
                            }

                            //绘制二级标题文本
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, new SolidBrush(e.CellStyle.ForeColor), new Rectangle(e.CellBounds.X, e.CellBounds.Y + e.CellBounds.Height / 2, e.CellBounds.Width, e.CellBounds.Height / 2), sf);
                        }
                        else
                        {
                            if (e.ColumnIndex == this.Columns.Count - 1)
                            {
                                //绘制一级标题右边框线
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right, e.CellBounds.Top, e.CellBounds.Right, e.CellBounds.Bottom);
                            }
                            else
                            {
                                //绘制一级标题右边框线
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                            }

                            //绘制二级标题文本
                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Center;
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, new SolidBrush(e.CellStyle.ForeColor), e.CellBounds, sf);
                        }

                        e.Handled = true;
                    }
                }
            }
        }
    }
}
