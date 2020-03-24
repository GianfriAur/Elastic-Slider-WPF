using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RightElips.SizeChanged += (s, e) =>
            {
                RightElipsGeo.Rect = new Rect(e.NewSize.Width / 2, 0, e.NewSize.Width / 2, e.NewSize.Height);
            };

            LeftElips.SizeChanged += (s, e) =>
            {
                LeftElipsGeo.Rect = new Rect(0,0, e.NewSize.Width / 2, e.NewSize.Height);
            };


        }

        public double AnimStatus
        {
            get { return (double)GetValue(AnimStatusProperty); }
            set {
     
                SetValue(AnimStatusProperty, value); }
        }


        private static void OnAnimStatusCallBack(
        DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MainWindow c = sender as MainWindow;
            if (c != null)
            {
                c.OnAnimStatusChange(sender, e);
            }
        }
        protected virtual void OnAnimStatusChange(object o , DependencyPropertyChangedEventArgs e)
        {
            double NewValue = (double) e.NewValue;

            if (NewValue >= 0)
            {
                RightElips.Width = NewValue;
                LeftElips.Width = 0;
            }
            else
            {
                LeftElips.Width = NewValue * -1;
                RightElips.Width = 0;
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimStatusProperty =
            DependencyProperty.Register("AnimStatus", typeof(double), typeof(MainWindow),new PropertyMetadata(0.0, OnAnimStatusCallBack),new ValidateValueCallback((e)=> {  return true; }));

        Grid pressedElement = null;
        Point startpoint = new Point();


        private void MainMoveEvent(object sender, MouseEventArgs e)
        {

        }

        private void MainMoveeUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }



        private void LeftSlide_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                pressedElement = LeftSlide;
                startpoint = e.GetPosition(this);

                if (myStoryboard!=null)
                  myStoryboard.Stop(this);
                Point np = e.GetPosition(this);
                AnimStatus = (np.X - startpoint.X) >= 300 ? 300 : ((np.X - startpoint.X) <= -300) ? -300 : (np.X - startpoint.X);
            }
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            pressedElement = null;
            startpoint = new Point();
        }

        Storyboard myStoryboard;
        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {

                Point np = e.GetPosition(this);
                if (pressedElement == LeftSlide)
                {
                    System.Diagnostics.Debug.WriteLine(np.X - startpoint.X);
                    if (status && np.X - startpoint.X > 150)
                    {
                        change_status();
                        DoubleAnimation myDoubleAnimation = new DoubleAnimation();
                        myDoubleAnimation.From = -400;
                        myDoubleAnimation.To = 0.0;
                        myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                        ElasticEase elastic = new ElasticEase();

                        elastic.Oscillations = 3;
                        elastic.Springiness = 9;
                        elastic.EasingMode = EasingMode.EaseOut;
                        elastic.Changed += Elastic_Changed;

                        myDoubleAnimation.EasingFunction = elastic;
                        myStoryboard = new Storyboard();
                        myStoryboard.Children.Add(myDoubleAnimation);
                        Storyboard.SetTargetName(myDoubleAnimation, this.Name);
                        Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(MainWindow.AnimStatusProperty));
                        myStoryboard.Begin(this, true);
                    }
                    else if (!status && np.X - startpoint.X < -150)
                    {
                        change_status();
                        DoubleAnimation myDoubleAnimation = new DoubleAnimation();
                        myDoubleAnimation.From = 400;
                        myDoubleAnimation.To = 0.0;
                        myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                        ElasticEase elastic = new ElasticEase();

                        elastic.Oscillations = 3;
                        elastic.Springiness = 9;
                        elastic.EasingMode = EasingMode.EaseOut;
                        elastic.Changed += Elastic_Changed;

                        myDoubleAnimation.EasingFunction = elastic;
                        myStoryboard = new Storyboard();
                        myStoryboard.Children.Add(myDoubleAnimation);
                        Storyboard.SetTargetName(myDoubleAnimation, this.Name);
                        Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(MainWindow.AnimStatusProperty));
                        myStoryboard.Begin(this, true);
                    }
                    else
                    {
                        DoubleAnimation myDoubleAnimation = new DoubleAnimation();
                        myDoubleAnimation.From = (np.X - startpoint.X) >= 300 ? 300 : ((np.X - startpoint.X) <= -300) ? -300 : (np.X - startpoint.X);
                        myDoubleAnimation.To = 0.0;
                        myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                        ElasticEase elastic = new ElasticEase();

                        elastic.Oscillations = 3;
                        elastic.Springiness = 9;
                        elastic.EasingMode = EasingMode.EaseOut;
                        elastic.Changed += Elastic_Changed;

                        myDoubleAnimation.EasingFunction = elastic;
                        myStoryboard = new Storyboard();
                        myStoryboard.Children.Add(myDoubleAnimation);
                        Storyboard.SetTargetName(myDoubleAnimation, this.Name);
                        Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(MainWindow.AnimStatusProperty));
                        myStoryboard.Begin(this, true);
                    }
                }

                pressedElement = pressedElement = null; 
                startpoint = new Point();
            }
        }

        bool status = true;

        public void change_status()
        {
            if (status)
            {
                LeftSlide.Width = 340;
                RigthSlide.Width = 60;
                RigthSlide.Margin = new Thickness(340, 0, 0, 0);
                BubleSlide.Margin = new Thickness(140, 0, 0, 0);
                RigthSlideContent.Visibility = Visibility.Collapsed;
                LeftSlideContent.Visibility = Visibility.Visible;
            }
            else
            {
                LeftSlide.Width = 60;
                RigthSlide.Width = 340;
                RigthSlide.Margin = new Thickness(60, 0, 0, 0);
                BubleSlide.Margin = new Thickness(-140, 0, 0, 0);
                RigthSlideContent.Visibility = Visibility.Visible;
                LeftSlideContent.Visibility = Visibility.Collapsed;
            }
            status = !status;
        } 

        private void Elastic_Changed(object sender, EventArgs e)
        {
            
        }

        private void Window_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (pressedElement != null)
            {
                if (pressedElement == LeftSlide)
                {
                    Point np = e.GetPosition(this);
                    AnimStatus = (np.X - startpoint.X)>=300?300:((np.X - startpoint.X) <= -300)?-300 : (np.X - startpoint.X);

                  /*  if (np.X - startpoint.X > 0)
                        if (np.X - startpoint.X > 300)
                            RightElips.Width = 300;
                        else
                            RightElips.Width = np.X - startpoint.X;
                            */
                }
            }
        }
    }
}
