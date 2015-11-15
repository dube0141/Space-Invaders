using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace spaceInvaders
{
    class Player
    {
        public Image turret;

        private double xPos;
        private double yPos;
        
        public Player()
        {
            turret = new Image();
            turret.Width = 90;
            turret.Height = 34;
            turret.Source = new BitmapImage(new Uri("ms-appx:///Assets/sprites/player.png"));

            xPos = Window.Current.Bounds.Width / 2;
            yPos = Window.Current.Bounds.Height - (turret.Height + 20);

            Canvas.SetLeft(turret, xPos);
            Canvas.SetTop(turret, yPos);
        }

        public void moveRight()
        {
            xPos += 2;
            Canvas.SetLeft(turret, xPos);
        }

        public void moveLeft()
        {
            xPos += -2;
            Canvas.SetLeft(turret, xPos);
        }
    }
}
