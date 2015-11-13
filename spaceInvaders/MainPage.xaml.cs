using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace spaceInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int x;
        DispatcherTimer dispatcherTimer;
        Boolean playerIsMovingLeft = false;

        //sprites
        private BitmapImage alien1A;
        private BitmapImage alien1B;

        public MainPage()
        {
            x = 0;
            this.InitializeComponent();

            alien1A = new BitmapImage(new Uri("ms-appx:///Assets/sprites/alien-1-1.png"));
            alien1B = new BitmapImage(new Uri("ms-appx:///Assets/sprites/alien-1-2.png"));
            player.Source = alien1A;

            Canvas.SetTop(image, Window.Current.Bounds.Height - (image.Height + 20));
            Canvas.SetLeft(image, Window.Current.Bounds.Width / 2);

            //run game loop
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(15);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            textBlock.Text = "x: " + x;
            //Debug.WriteLine("sw");

            if (x > 0 && x % 60 == 0)
            {
                if (player.Source == alien1A)
                {
                    player.Source = alien1B;
                }
                else
                {
                    player.Source = alien1A;
                }
            }

            if (playerIsMovingLeft)
            {
                if (Canvas.GetLeft(player) <= 0)
                {
                    playerIsMovingLeft = false;
                    Canvas.SetTop(player, Canvas.GetTop(player) + 20);
                }

                Canvas.SetLeft(player, x);
                x = x - 2;
            }
            else
            {
                if (Canvas.GetLeft(player) >= (c.ActualWidth - player.ActualWidth))
                {
                    playerIsMovingLeft = true;
                    Canvas.SetTop(player, Canvas.GetTop(player) + 20);
                }

                Canvas.SetLeft(player, x);
                x = x + 2;
            }
        }
    }
}
