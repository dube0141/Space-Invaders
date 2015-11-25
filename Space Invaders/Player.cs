using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Space_Invaders
{
    class Player
    {
        private Image playerSprite;
        private BitmapImage playerBitmapImage;

        private Image playerBullet;

        private bool isMovingLeft;
        private bool isMovingRight;
        private bool isShooting;

        public Player(Canvas canvas)
        {
            Window.Current.CoreWindow.KeyDown += onKeyDown;
            Window.Current.CoreWindow.KeyUp += onKeyUp;

            playerSprite = new Image();

            playerBitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Sprites/player.png"));
            playerBitmapImage.ImageOpened += (sender, e) =>
            {
                isMovingLeft = isMovingRight = isShooting = false;

                playerSprite.Width = playerBitmapImage.PixelWidth;
                playerSprite.Height = playerBitmapImage.PixelHeight;

                Canvas.SetLeft(playerSprite, Window.Current.Bounds.Width / 2);
                Canvas.SetTop(playerSprite, Window.Current.Bounds.Height - (playerSprite.Height * 2));
            };

            playerSprite.Source = playerBitmapImage;
            canvas.Children.Add(playerSprite);
        }

        public void Draw(Canvas canvas, Image[,] invaderGrid)
        {
            Canvas.SetTop(playerSprite, Window.Current.Bounds.Height - (playerSprite.Height * 2));

            if (isMovingLeft) Canvas.SetLeft(playerSprite, Canvas.GetLeft(playerSprite) - 5);
            if (isMovingRight) Canvas.SetLeft(playerSprite, Canvas.GetLeft(playerSprite) + 5);
            if (isShooting)
            {
                if (!canvas.Children.Contains(playerBullet))
                {
                    playerBullet = new Image();

                    playerBullet.Width = 2;
                    playerBullet.Height = 12;

                    playerBullet.Source = new BitmapImage(new Uri("ms-appx:///Assets/Sprites/player-bullet.png"));

                    Canvas.SetTop(playerBullet, Canvas.GetTop(playerSprite));
                    Canvas.SetLeft(playerBullet, Canvas.GetLeft(playerSprite) + (playerSprite.Width / 2 - 1));

                    canvas.Children.Add(playerBullet);
                }

                for (int r = 0; r < 11; r++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        if (Canvas.GetLeft(invaderGrid[r, c]) <= Canvas.GetLeft(playerBullet) && Canvas.GetLeft(invaderGrid[r, c]) + invaderGrid[r, c].Width >= Canvas.GetLeft(playerBullet))
                        {
                            if (Canvas.GetTop(invaderGrid[r, c]) + invaderGrid[r, c].Height >= Canvas.GetTop(playerBullet) && Canvas.GetTop(invaderGrid[r, c]) <= Canvas.GetTop(playerBullet))
                            {
                                if (invaderGrid[r, c].Width != 0)
                                {
                                    isShooting = false;
                                    canvas.Children.Remove(playerBullet);
                                    invaderGrid[r, c].Width = 0;
                                    invaderGrid[r, c].Height = 0;
                                }
                            }
                        }
                        else if (Canvas.GetTop(playerBullet) <= 0)
                        {
                            isShooting = false;
                            canvas.Children.Remove(playerBullet);
                        }
                    }
                }

                Canvas.SetTop(playerBullet, Canvas.GetTop(playerBullet) - 15);
            }
        }

        private void onKeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.Left) isMovingLeft = true;
            if (args.VirtualKey == Windows.System.VirtualKey.Right) isMovingRight = true;

            if (args.VirtualKey == Windows.System.VirtualKey.Up) isShooting = true;
            if (args.VirtualKey == Windows.System.VirtualKey.Space) isShooting = true;
        }

        private void onKeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.Left) isMovingLeft = false;
            if (args.VirtualKey == Windows.System.VirtualKey.Right) isMovingRight = false;
        }

        public Image getPlayer()
        {
            return playerSprite;
        }
    }
}
