
using Aspose.Cells;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;


namespace Bitter.Tools.Utils
{
    /********************************************************************************
    ** auth： Jason
    ** date： 2016/10/20 10:01:01
    ** desc：
    ** Ver.:  V1.0.0
    ** Copyright (C) 2016 Bitter 版权所有。
    *********************************************************************************/

    public static class DtToExcel
    {
        /// <summary>
        /// 根据datatable输出Excel
        /// </summary>
        /// <param name="page">输出excel aspx页</param>
        /// <param name="dt">数据源datatable</param>
        /// <param name="Titile">excel标题</param>
        public static void OutExcelInPage(Page page, DataTable dt, string Titile = null)
        {
            System.IO.MemoryStream ms = DtToExcel.OutFileToStream(dt, Titile);
            string name = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";//以当前时间为excel表命名
            if (ms.Length > 0)
            {
                string filename = DateTime.Now.ToFileTime().ToString() + ".xls";
                page.Response.Clear();
                page.Response.ClearHeaders();
                page.Response.ClearContent();
                page.Response.AddHeader("Content-Length", ms.Length.ToString());//文件长度
                page.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);//文件名称
                page.Response.ContentType = "vnd.ms-excel.numberformat:yyyy-MM-dd ";
                byte[] buffer = new byte[65536];
                ms.Position = 0;
                int num;
                do
                {
                    num = ms.Read(buffer, 0, buffer.Length);
                    page.Response.OutputStream.Write(buffer, 0, num);
                }
                while (num > 0); page.Response.Flush();
            }
            page.Response.Close();//关闭
        }

        /// <summary>
        /// 导出数据到内存流中
        /// </summary>
        /// <param name="dt">要导出的数据</param>
        /// <param name="tableName">表格标题</param>
        /// <param name="path">保存路径</param>
        public static System.IO.MemoryStream OutFileToStream(DataTable dt, string tableName = null)
        {
            return GetExcelStream(dt, tableName);
        }

        private static System.IO.MemoryStream GetExcelStream(DataTable dt, string titleName)
        {
            #region //格式化单元格

            //0	 General	 General
            //1	 Decimal	 0
            //2	 Decimal	 0.00
            //3	 Decimal	 #,##0
            //4	 Decimal	 #,##0.00
            //5	 Currency	 $#,##0;$-#,##0
            //6	 Currency	 $#,##0;[Red]$-#,##0
            //7	 Currency	 $#,##0.00;$-#,##0.00
            //8	 Currency	 $#,##0.00;[Red]$-#,##0.00
            //9	 Percentage	 0%
            //10	 Percentage	 0.00%
            //11	 Scientific	 0.00E+00
            //12	 Fraction	 # ?/?
            //13	 Fraction	 # /
            //14	 Date	 m/d/yy
            //15	 Date	 d-mmm-yy
            //16	 Date	 d-mmm
            //17	 Date	 mmm-yy
            //18	 Time	 h:mm AM/PM
            //19	 Time	 h:mm:ss AM/PM
            //20	 Time	 h:mm
            //21	 Time	 h:mm:ss
            //22	 Time	 m/d/yy h:mm
            //37	 Currency	 #,##0;-#,##0
            //38	 Currency	 #,##0;[Red]-#,##0
            //39	 Currency	 #,##0.00;-#,##0.00
            //40	 Currency	 #,##0.00;[Red]-#,##0.00
            //41	 Accounting	 _ * #,##0_ ;_ * "_ ;_ @_
            //42	 Accounting	 _ $* #,##0_ ;_ $* "_ ;_ @_
            //43	 Accounting	 _ * #,##0.00_ ;_ * "??_ ;_ @_
            //44	 Accounting	 _ $* #,##0.00_ ;_ $* "??_ ;_ @_
            //45	 Time	 mm:ss
            //46	 Time	 h :mm:ss
            //47	 Time	 mm:ss.0
            //48	 Scientific	 ##0.0E+00
            //49	 Text	 @

            #endregion //格式化单元格

            Workbook workbook = new Workbook(); //工作簿
            Worksheet sheet = workbook.Worksheets[0]; //工作表
            Cells cells = sheet.Cells;//单元格

            //为标题设置样式
            Style styleTitle = workbook.Styles[workbook.Styles.Add()];//新增样式
            styleTitle.HorizontalAlignment = TextAlignmentType.Center;//文字居中
            styleTitle.Font.Name = "宋体";//文字字体
            styleTitle.Font.Size = 18;//文字大小
            styleTitle.Font.IsBold = true;//粗体

            //样式2
            Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式
            style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中
            style2.Font.Name = "宋体";//文字字体
            style2.Font.Size = 14;//文字大小
            style2.Font.IsBold = true;//粗体
            style2.IsTextWrapped = false;//单元格内容自动换行
            style2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式3
            Style style3 = workbook.Styles[workbook.Styles.Add()];//新增样式
            style3.HorizontalAlignment = TextAlignmentType.Center;//文字居中
            style3.Font.Name = "宋体";//文字字体
            style3.Font.Size = 12;//文字大小
            style3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式数值
            Style style4 = workbook.Styles[workbook.Styles.Add()];//新增样式
            style4.HorizontalAlignment = TextAlignmentType.Center;//文字居中
            style4.Font.Name = "宋体";//文字字体
            style4.Font.Size = 12;//文字大小
            style4.Number = 1;
            style4.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style4.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style4.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style4.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式数值
            Style style5 = workbook.Styles[workbook.Styles.Add()];//新增样式
            style5.HorizontalAlignment = TextAlignmentType.Center;//文字居中
            style5.Font.Name = "宋体";//文字字体
            style5.Font.Size = 12;//文字大小
            style5.Number = 2;
            style5.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style5.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style5.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style5.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //样式数值
            Style style6 = workbook.Styles[workbook.Styles.Add()];//新增样式
            style6.HorizontalAlignment = TextAlignmentType.Left;//文字居中
            style6.Font.Name = "宋体";//文字字体
            style6.Font.Size = 12;//文字大小
            style6.Font.Color = Color.Red;
            style6.Number = 2;

            int Colnum = dt.Columns.Count;//表格列数
            int Rownum = dt.Rows.Count;//表格行数

            var FHeaderRemark = string.Empty;
            if (dt.Columns.Contains("FHeaderRemark"))
            {
                if (dt.Rows.Count > 0)
                {
                    FHeaderRemark = dt.Rows[0]["FHeaderRemark"].ToString();
                }
                dt.Columns.Remove("FHeaderRemark");
                Colnum--;
            }
            if (dt.Columns.Contains("FReportDetailCondition"))
            {
                dt.Columns.Remove("FReportDetailCondition");
                Colnum--;
            }
            int StartIndex = 0;//记录开始行数
            if (!string.IsNullOrEmpty(titleName))
            {
                //生成行1 标题行
                cells.Merge(StartIndex, 0, 1, Colnum);//合并单元格
                cells[0, 0].PutValue(titleName);//填写内容
                cells[0, 0].SetStyle(styleTitle);
                cells.SetRowHeight(0, 38);
            }

            if (FHeaderRemark != "")
            {
                string[] Remark = FHeaderRemark.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (Remark.Length > 0)
                {
                    for (int i = 0; i < Remark.Length; i++)
                    {
                        StartIndex++;
                        cells.Merge(StartIndex, 0, 1, Colnum);//合并单元格
                        cells[StartIndex, 0].PutValue(Remark[i]);//填写内容
                        cells[StartIndex, 0].SetStyle(style6);
                        cells.SetRowHeight(0, 25);
                    }
                }
            }

            //多表头输出  新增-By：Hdj 2017年4月5日11:42:03
            var dataParentHeadIndex = StartIndex + 1;
            int ParentHead = 0;
            for (int i = 0; i < Colnum; i++)
            {
                cells[dataParentHeadIndex, i].SetStyle(style3);
                //获取当前列名
                string clounName = dt.Columns[i].ColumnName;
                //找特殊符位置
                int m = clounName.IndexOf("|");
                //如果存在特殊符则为两级目录
                if (m > 0)
                {
                    //获取上级目录名称
                    ParentHead = 1;
                    string paCloun = clounName.Substring(0, m);
                    int r = 0;//设置colspan
                    while (true)
                    {
                        //获取当前列名
                        string sonClounName = dt.Columns[i].ColumnName;
                        //如果包含父级目录
                        if (sonClounName.Contains(paCloun))
                        {
                            cells[dataParentHeadIndex, i].SetStyle(style2);
                            r += 1;
                            i += 1;
                        }
                        else
                        {
                            i -= 1;
                            break;
                        }
                    }
                    cells.Merge(dataParentHeadIndex, i - r + 1, 1, r);
                    cells[dataParentHeadIndex, i - r + 1].PutValue(paCloun);

                    cells.SetRowHeight(dataParentHeadIndex, 25);
                }
            }
            if (ParentHead == 1) { StartIndex++; }
            AutoFitterOptions options = new AutoFitterOptions();
            options.OnlyAuto = true;
            options.AutoFitMergedCells = true;
            sheet.AutoFitRows(options);
            var dataHeadIndex = StartIndex + 1;
            StartIndex++;
            //生成行2 列名行
            for (int i = 0; i < Colnum; i++)
            {
                var cloumnName = string.Empty;
                if (dt.Columns[i].ColumnName.IndexOf("|") > 0)
                {
                    cloumnName = dt.Columns[i].ColumnName.Substring((dt.Columns[i].ColumnName.IndexOf("|") + 1));
                }
                else
                {
                    cloumnName = dt.Columns[i].ColumnName;
                }

                cells[dataHeadIndex, i].PutValue(cloumnName);
                cells[dataHeadIndex, i].SetStyle(style2);
                cells.SetRowHeight(dataHeadIndex, 25);
            }

            var dataIndex = StartIndex + 1;
            //生成数据行
            for (int i = 0; i < Rownum; i++)
            {
                for (int k = 0; k < Colnum; k++)
                {
                    if (dt.Columns[k].DataType == (new int()).GetType())
                    {
                        cells[dataIndex + i, k].SetStyle(style4);
                        cells[dataIndex + i, k].PutValue(dt.Rows[i][k].ToString(), true);
                    }
                    else if (dt.Columns[k].DataType == (new decimal()).GetType())
                    {
                        cells[dataIndex + i, k].SetStyle(style5);
                        cells[dataIndex + i, k].PutValue(dt.Rows[i][k].ToString(), true);
                    }
                    else
                    {
                        cells[dataIndex + i, k].SetStyle(style3);
                        cells[dataIndex + i, k].PutValue(dt.Rows[i][k].ToString());
                    }
                }
                cells.SetRowHeight(dataIndex + i, 24);
            }

            return workbook.SaveToStream();
            //workbook.Save(path);
        }
    }
}