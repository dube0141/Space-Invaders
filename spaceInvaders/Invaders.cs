using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace spaceInvaders
{
    class Invaders
    {
        public ArrayList invaderGrid;

        private readonly Uri alien1AUri = new Uri("ms-appx:///Assets/sprites/alien-1-1.png");
        private readonly Uri alien1BUri = new Uri("ms-appx:///Assets/sprites/alien-1-2.png");

        private BitmapImage alien1A;
        private BitmapImage alien1B;

        double speed = 1;

        public Invaders(double sizeRatio)
        {
            alien1A = new BitmapImage(alien1AUri);
            alien1B = new BitmapImage(alien1BUri);

            invaderGrid = new ArrayList();

            for (int r = 1; r < 10; r++)
            {
                for (int c = 1; c < 5; c++)
                {
                    Image invader = new Image();

                    alien1A.ImageOpened += (sender, e) =>
                    {
                        invader.Width = alien1A.PixelWidth * sizeRatio;
                        invader.Height = alien1A.PixelHeight * sizeRatio;
                    };

                    Canvas.SetLeft(invader, 60 * r);
                    Canvas.SetTop(invader, 40 * c);

                    invaderGrid.Add(invader);
                    invader.Source = alien1A;
                }
            }
        }

        public void moveLeft()
        {
            foreach (Image i in invaderGrid)
            {
                Canvas.SetLeft(i, Canvas.GetLeft(i) - speed);
            }
        }

        public void moveRight()
        {
            foreach (Image i in invaderGrid)
            {
                Canvas.SetLeft(i, Canvas.GetLeft(i) + speed);
            }
        }

        public void moveDown()
        {
            foreach (Image i in invaderGrid)
            {
                Canvas.SetTop(i, Canvas.GetTop(i) + i.Height);
            }

            speed += 0.25;
        }

        public void toggleSprite(double c)
        {
            if (c % (30 / Math.Round(speed)) == 1)
            {
                if (alien1A.UriSource == alien1AUri) alien1A.UriSource = alien1BUri;
                else alien1A.UriSource = alien1AUri;
            }
        }

        public bool collision()
        {
            foreach (Image i in invaderGrid)
            {
                if (Canvas.GetLeft(i) <= 0 || Canvas.GetLeft(i) >= Window.Current.Bounds.Width - i.Width)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
