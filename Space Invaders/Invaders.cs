using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Space_Invaders
{
    class Invaders
    {
        private Image[,] invaderGrid;
        private Image[] invaderBullet;

        private bool isMovingLeft;
        private bool isMovingDown;
        private bool toggleSprite;

        private int count;

        public Invaders(Canvas canvas)
        {
            invaderGrid = new Image[11, 5];

            isMovingLeft = isMovingDown = toggleSprite = false;

            count = 0;

            for (int c = 0; c < 11; c++)
            {
                for (int r = 0; r < 5; r++)
                {
                    Image invader = new Image();
                    BitmapImage bitmapSource;

                    if (r < 1)
                    {
                        invader.Height = 24;
                        invader.Tag = new BitmapImage(new Uri("ms-appx:///Assets/sprites/alien-1-2.png"));
                        bitmapSource = new BitmapImage(new Uri("ms-appx:///Assets/sprites/alien-1-1.png"));
                    }
                    else if (r < 3)
                    {
                        invader.Height = 28;
                        invader.Tag = new BitmapImage(new Uri("ms-appx:///Assets/sprites/alien-2-2.png"));
                        bitmapSource = new BitmapImage(new Uri("ms-appx:///Assets/sprites/alien-2-1.png"));
                    }
                    else
                    {
                        invader.Height = 32;
                        invader.Tag = new BitmapImage(new Uri("ms-appx:///Assets/sprites/alien-3-2.png"));
                        bitmapSource = new BitmapImage(new Uri("ms-appx:///Assets/sprites/alien-3-1.png"));
                    }

                    Canvas.SetLeft(invader, 50 + (50 * c));
                    Canvas.SetTop(invader, 50 + (50 * r));

                    invader.Width = 32;
                    invader.Source = bitmapSource;

                    canvas.Children.Add(invader);
                    invaderGrid[c, r] = invader;
                }
            }
        }

        public void Draw(Canvas canvas, Image player)
        {
            if (count % 45 == 1) toggleSprite = true;

            if (isMovingDown)
            {
                for (int r = 0; r < 11; r++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        isMovingDown = false;
                        Canvas.SetTop(invaderGrid[r, c], Canvas.GetTop(invaderGrid[r, c]) + 50);
                    }
                }
            }

            if (isMovingLeft)
            {
                for (int r = 0; r < 11; r++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        if (Canvas.GetLeft(invaderGrid[r, c]) <= 0 + invaderGrid[r, c].Width)
                        {
                            isMovingDown = true;
                            isMovingLeft = false;
                        }
                        if (toggleSprite) toggle(invaderGrid[r, c]);

                        Canvas.SetLeft(invaderGrid[r, c], Canvas.GetLeft(invaderGrid[r, c]) - 1);
                    }
                }
            }
            else
            {
                for (int r = 0; r < 11; r++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        if (Canvas.GetLeft(invaderGrid[r, c]) >= Window.Current.Bounds.Width - invaderGrid[r, c].Width)
                        {
                            isMovingDown = true;
                            isMovingLeft = true;
                        }
                        if (toggleSprite) toggle(invaderGrid[r, c]);

                        Canvas.SetLeft(invaderGrid[r, c], Canvas.GetLeft(invaderGrid[r, c]) + 1);
                    }
                }
            }

            toggleSprite = false;
            count++;
        }

        private void toggle(Image invader)
        {
            BitmapImage oldImage = (BitmapImage)invader.Source;
            BitmapImage newImage = (BitmapImage)invader.Tag;

            invader.Tag = oldImage;
            invader.Source = newImage;
        }

        public Image[,] getInvaderGrid()
        {
            return invaderGrid;
        }
    }
}
