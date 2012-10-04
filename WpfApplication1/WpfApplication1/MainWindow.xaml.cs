using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private Dictionary<TouchDevice, Ellipse> ellipseList = new Dictionary<TouchDevice, Ellipse>();
        private Dictionary<TouchDevice, Label> labelList = new Dictionary<TouchDevice, Label>();
        private Dictionary<TouchDevice, Line> lineVList = new Dictionary<TouchDevice, Line>();
        private Dictionary<TouchDevice, Line> lineHList = new Dictionary<TouchDevice, Line>();
 
        private void Canvas1_TouchDown(object sender, TouchEventArgs e)
        {
            var pt = e.GetTouchPoint(Canvas1);

            // 縦線を作ります。
            var lineV = new Line();
            lineV.X1 = lineV.X2 = pt.Position.X;
            lineV.Y1 = 0;
            lineV.Y2 = ActualHeight;
            lineV.Stroke = Brushes.Red;
            Canvas1.Children.Add(lineV);
            lineVList[e.TouchDevice] = lineV;

            // 横線を作ります。
            var lineH = new Line();
            lineH.X1 = 0;
            lineH.X2 = ActualWidth;
            lineH.Y1 = lineH.Y2 = pt.Position.Y;
            lineH.Stroke = Brushes.Red;
            Canvas1.Children.Add(lineH);
            lineHList[e.TouchDevice] = lineH;

            // 円を作ります。
            var ellipse = new Ellipse();
            ellipse.Width = 50;
            ellipse.Height = 50;
            ellipse.Fill = Brushes.Blue;
            ellipse.RenderTransform
                = new TranslateTransform(pt.Position.X - ellipse.RenderSize.Width / 2,
                                           pt.Position.Y - ellipse.RenderSize.Height / 2);
            Canvas1.Children.Add(ellipse);
            ellipseList[e.TouchDevice] = ellipse;
            
            // idを表すラベルを作ります。
            var label = new Label();
            label.Foreground = Brushes.White;
            label.FontSize = 36;
            label.Content = e.TouchDevice.Id;
            label.RenderTransform = new TranslateTransform(pt.Position.X - label.RenderSize.Width / 2,
                                           pt.Position.Y - label.RenderSize.Height / 2);
            Canvas1.Children.Add(label);
            labelList[e.TouchDevice] = label;

            Canvas1.InvalidateVisual();

            Canvas1.CaptureTouch(e.TouchDevice);
        } 
 
        private void Canvas1_TouchMove(object sender, TouchEventArgs e)
        { 
            if (e.TouchDevice.Captured == Canvas1)
            {
                var pt = e.GetTouchPoint(Canvas1);
               
                var ellipse1 = ellipseList[e.TouchDevice];
                ellipse1.RenderTransform = new TranslateTransform(pt.Position.X - ellipse1.RenderSize.Width / 2,pt.Position.Y - ellipse1.RenderSize.Height / 2);

                var label = labelList[e.TouchDevice];
                label.RenderTransform = new TranslateTransform(pt.Position.X - label.RenderSize.Width / 2, pt.Position.Y - label.RenderSize.Height / 2);

                var lineV = lineVList[e.TouchDevice];
                lineV.X1 = lineV.X2 = pt.Position.X;

                var lineH = lineHList[e.TouchDevice];
                lineH.Y1 = lineH.Y2 = pt.Position.Y;

                label1.Content = "CurrentTouchPoints:" + ellipseList.Count;

            }

        } 
 
        private void Canvas1_TouchUp(object sender, TouchEventArgs e)
        { 
            if (e.TouchDevice.Captured == Canvas1)
            { 
                Canvas1.ReleaseTouchCapture(e.TouchDevice);

                Canvas1.Children.Remove(ellipseList[e.TouchDevice]);
                ellipseList.Remove(e.TouchDevice);

                Canvas1.Children.Remove(labelList[e.TouchDevice]);
                labelList.Remove(e.TouchDevice);

                Canvas1.Children.Remove(lineVList[e.TouchDevice]);
                lineVList.Remove(e.TouchDevice);

                Canvas1.Children.Remove(lineHList[e.TouchDevice]);
                lineHList.Remove(e.TouchDevice);

                label1.Content = "CurrentTouchPoints:" + ellipseList.Count;
            } 
        }

    }
}

