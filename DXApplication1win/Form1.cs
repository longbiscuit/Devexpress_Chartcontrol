using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXApplication1win
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.xtraTabPage1.PageVisible = false;
            this.xtraTabPage2.PageVisible = false;
            this.xtraTabControl1.TabPages.Add("三角函数图");//添加一个page，在此之前可以直接用ui界面的本控件的remove属性删除默认的两个page
            ChartControl lineChartControl = new ChartControl();//实例化一个ChartControl
            lineChartControl.Legend.UseCheckBoxes = true;//图例可以勾选

            //产生数据
            List<double> xaxesList = new List<double>();//x轴数据
            List<double> ySinList = new List<double>();//系列1-y轴数据
            List<double> yCosList = new List<double>();//系列2-y轴数据
            List<double> ySinAddCosList = new List<double>();//系列3-y轴数据
            List<double> ySinMinCosList = new List<double>();

            for (int i = 0; i < 20; i++)
            {
                xaxesList.Add(i);
                ySinList.Add(Math.Sin(i));
                yCosList.Add(Math.Cos(i));
                ySinAddCosList.Add(Math.Sin(i) + Math.Cos(i));
                ySinMinCosList.Add(Math.Sin(i) - Math.Cos(i));
            }
            //将所有系列的y值放入一个列表，这个列表是列表的列表
            List<List<double>> YList = new List<List<double>>();
            YList.Add(ySinList);
            YList.Add(yCosList);
            YList.Add(ySinAddCosList);
            YList.Add(ySinMinCosList);

            //系列内容名称
            List<string> seriesTextList = new List<string>();//系列内容名称
            seriesTextList.Add("sin理论");
            seriesTextList.Add("cos理论");
            seriesTextList.Add("sin+cos理论");
            seriesTextList.Add("sin-cos理论");



            //系列名称
            List<Series> seriesNameList = new List<Series>();
            for (int i = 0; i < 4; i++)
            {
                //完全建立一个系列，以后直接用就行了
                seriesNameList.Add(new Series(seriesTextList[i], ViewType.Spline));//ScatterLine点划线，实验值用这种，理论值用line
            }



            //产生数据2
            List<double> xaxesList2 = new List<double>();//x轴数据
            List<double> ySinList2 = new List<double>();//系列1-y轴数据
            List<double> yCosList2 = new List<double>();//系列2-y轴数据
            List<double> ySinAddCosList2 = new List<double>();//系列3-y轴数据
            List<double> ySinMinCosList2 = new List<double>();
            for (int i = 0; i < 20; i++)
            {
                xaxesList2.Add(i);
                ySinList2.Add(0.8 * Math.Sin(i));
                yCosList2.Add(0.8 * Math.Cos(i));
                ySinAddCosList2.Add(0.8 * Math.Sin(i) + 0.8 * Math.Cos(i));
                ySinMinCosList2.Add(0.8 * Math.Sin(i) - 0.8 * Math.Cos(i));

            }
            //将所有系列的y值放入一个列表，这个列表是列表的列表
            List<List<double>> YList2 = new List<List<double>>();
            YList2.Add(ySinList2);
            YList2.Add(yCosList2);
            YList2.Add(ySinAddCosList2);
            YList2.Add(ySinMinCosList2);

            //系列内容名称
            List<string> seriesTextList2 = new List<string>();//系列内容名称
            seriesTextList2.Add("sin实验");
            seriesTextList2.Add("cos实验");
            seriesTextList2.Add("sin+cos实验");
            seriesTextList2.Add("sin-cos实验");

            //系列名称
            List<Series> seriesNameList2 = new List<Series>();
            for (int i = 0; i < 4; i++)
            {
                //完全建立一个系列，以后直接用就行了
                seriesNameList2.Add(new Series(seriesTextList2[i], ViewType.Spline));//ScatterLine点划线，实验值用这种，理论值用line
            }





            //将系列加入chartControl
            for (int seriesIndex = 0; seriesIndex < 4; seriesIndex++)
            {

                ShowSeries_yuce(lineChartControl, seriesNameList[seriesIndex], seriesTextList[seriesIndex], xaxesList, YList[seriesIndex], seriesIndex);

                ShowSeries(lineChartControl, seriesNameList2[seriesIndex], seriesTextList2[seriesIndex], xaxesList2, YList2[seriesIndex], seriesIndex);

            }
            //显示
            int tabPagesCount = this.xtraTabControl1.TabPages.Count - 1;//xtraTabControl2page的个数，为了删除默认的，可以用ui界面的remove
            this.xtraTabControl1.TabPages[tabPagesCount].Controls.Add(lineChartControl);//将ChartControl这个控件添加到这个page中
            this.xtraTabControl1.SelectedTabPageIndex = tabPagesCount;//切换到这个page为选中的page








        }
        /// <summary>
        /// 实验值画图
        /// </summary>
        /// <param name="lineChartControl">图</param>
        /// <param name="seriesName">系列名，可以理解为id号</param>
        /// <param name="seriesText">系列内容名，显示在图中</param>
        /// <param name="xAxes"></param>
        /// <param name="yAxes"></param>
        /// <param name="iMarkerKind">根据传入的int值来赋标记种类</param>
        public void ShowSeries(ChartControl lineChartControl, Series seriesName, string seriesText, List<double> xAxes,
    List<double> yAxes, int iMarkerKind)
        {
            //添加颜色系列
            List<KnownColor> seriesColor = new List<KnownColor>();
            seriesColor.AddRange(new List<KnownColor>{KnownColor.Black, KnownColor.Red,KnownColor.Green,KnownColor.Blue,KnownColor.Cyan,KnownColor.Magenta,
                KnownColor.Yellow,KnownColor.DarkOrange,KnownColor.Navy,KnownColor.Purple,KnownColor.Olive,KnownColor.DarkCyan,KnownColor.RoyalBlue,
            KnownColor.Violet,KnownColor.Pink,KnownColor.Gray,KnownColor.LightYellow,KnownColor.LightCyan,KnownColor.LightPink });
            
            seriesName = new Series(seriesText, ViewType.ScatterLine);//新建立一个系列
            seriesName.ArgumentScaleType = ScaleType.Numerical;//x轴数据类型，为数字
            ((LineSeriesView)seriesName.View).LineMarkerOptions.Kind = (MarkerKind)((iMarkerKind % 9));//mark类型  MarkerKind.Triangle
            ((LineSeriesView)seriesName.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            ((LineSeriesView)seriesName.View).LineStyle.DashStyle = DashStyle.Dash;
             // seriesName.View.Color = Color.FromKnownColor((KnownColor)((iMarkerKind + 1) * 35 % 174));
            seriesName.View.Color = Color.FromKnownColor(seriesColor[iMarkerKind % seriesColor.Count]);
           
            for (int pointIndex = 0; pointIndex < xAxes.Count; pointIndex++)
            {
                seriesName.Points.Add(new SeriesPoint(xAxes[pointIndex], yAxes[pointIndex]));
            }
            lineChartControl.Series.Add(seriesName);//往ChartControl控件上添加系列
            lineChartControl.Legend.Visible = true;//图例可见
            ((XYDiagram)lineChartControl.Diagram).Rotated = false;//ChartControl控件不旋转
            lineChartControl.Dock = DockStyle.Fill;//ChartControl控件在父控件内填满平铺
        }

        //理论值
        public void ShowSeries_yuce(ChartControl lineChartControl, Series seriesName, string seriesText, List<double> xAxes,
    List<double> yAxes, int iMarkerKind)
        {

            seriesName = new Series(seriesText, ViewType.Spline);//新建立一个系列
            seriesName.ArgumentScaleType = ScaleType.Numerical;//x轴数据类型，为数字
            seriesName.View.Color = Color.FromKnownColor((KnownColor)((iMarkerKind + 1) * 35 % 174));
            ((LineSeriesView)seriesName.View).LineStyle.DashStyle = DashStyle.Solid;//线型

            for (int pointIndex = 0; pointIndex < xAxes.Count; pointIndex++)
            {
                seriesName.Points.Add(new SeriesPoint(xAxes[pointIndex], yAxes[pointIndex]));
            }
            if (xAxes.Count>0)//系列有数据点才添加系列
            {
                lineChartControl.Series.Add(seriesName);//往ChartControl控件上添加系列
                lineChartControl.Legend.Visible = true;//图例可见  
            }

            ((XYDiagram)lineChartControl.Diagram).Rotated = false;//ChartControl控件不旋转
            lineChartControl.Dock = DockStyle.Fill;//ChartControl控件在父控件内填满平铺
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        //button  Github_Devexpress_Chartcontrol
    }
}

