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

namespace spaceInvaders
{
    class Invaders
    {
        public ArrayList invaderGrid;

        private readonly Uri alien1AUri = new Uri("ms-appx:///Assets/sprites/alien-1-1.png");
        private readonly Uri alien1BUri = new Uri("ms-appx:///Assets/sprites/alien-1-2.png");
        private readonly Uri alien2AUri = new Uri("ms-appx:///Assets/sprites/alien-2-1.png");
        private readonly Uri alien2BUri = new Uri("ms-appx:///Assets/sprites/alien-2-2.png");
        private readonly Uri alien3AUri = new Uri("ms-appx:///Assets/sprites/alien-3-1.png");
        private readonly Uri alien3BUri = new Uri("ms-appx:///Assets/sprites/alien-3-2.png");

        private BitmapImage alien1A;
        private BitmapImage alien1B;
        private BitmapImage alien2A;
        private BitmapImage alien2B;
        private BitmapImage alien3A;

        public Invaders()
        {
            alien1A = new BitmapImage(alien1AUri);
            alien1B = new BitmapImage(alien1BUri);

            invaderGrid = new ArrayList();
            
            for (int y = 0; y < 1; y++)
            {
                Image invader = new Image();

                alien1A.ImageOpened += (sender, e) =>
                {
                    invader.Width = alien1A.PixelWidth;
                    invader.Height = alien1A.PixelHeight;
                };

                Canvas.SetLeft(invader, 60 * y);
                Canvas.SetTop(invader, 60);
                invaderGrid.Add(invader);

                invader.Source = alien1A;
            }
        }

        public void moveLeft()
        {
            foreach (Image i in invaderGrid)
            {
                Canvas.SetLeft(i, Canvas.GetLeft(i) + 1);
            }
        }

        public void moveRight()
        {
            foreach (Image i in invaderGrid)
            {
                Canvas.SetLeft(i, Canvas.GetLeft(i) + 1);
            }
        }

        public void moveDown()
        {
            foreach (Image i in invaderGrid)
            {
                Canvas.SetTop(i, Canvas.GetTop(i) + 20);
            }
        }

        public void toggleSprite()
        {
            if (alien1A.UriSource == alien1AUri) alien1A.UriSource = alien1BUri;
            else alien1A.UriSource = alien1AUri;
        }
    }
}
