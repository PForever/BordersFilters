using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ViewModel.Additional;
using Brushes = System.Windows.Media.Brushes;
using Image = System.Windows.Controls.Image;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace BordersFilters
{
    public class LoupControl
    {
        public double Scale;

        public System.Windows.Size SelectedPartSize;

        public ImageBrush InputImageBrush;
        public ImageBrush OutputImageBrush;

        public Grid Parent;
        public bool IsActive = false;

        public Canvas InputCanvas;
        public Canvas OutputCanvas;

        public Rectangle InputRectangle;
        public Rectangle OutputRectangle;

        public LoupControl()
        {
            
            
        }

        public void OnMouseEnter(Grid parentOfImage,Image outputImage /*TabControlVm tabControl*/)
        {
            Scale = 1;
            SelectedPartSize = new Size(100,100);

            if (Equals(Parent, parentOfImage))
            {
                return;
            }

            outputImage.CaptureMouse();
            Parent = parentOfImage;

            #region Brushes

            InputImageBrush = new ImageBrush
            {
                Transform = new ScaleTransform(Scale, Scale),                
                ImageSource = TabControlVm.InputImageSource,
                Stretch = Stretch.UniformToFill,
                ViewboxUnits = BrushMappingMode.RelativeToBoundingBox,
                AlignmentX = AlignmentX.Center,
                AlignmentY = AlignmentY.Center
            };

            OutputImageBrush = new ImageBrush
            {
                Transform = new ScaleTransform(Scale, Scale),
                ImageSource = outputImage.Source,
                Stretch = Stretch.UniformToFill,
                ViewboxUnits = BrushMappingMode.RelativeToBoundingBox,
                AlignmentX = AlignmentX.Center,
                AlignmentY = AlignmentY.Center
            };

                #endregion

            #region InputImage

            InputRectangle = new Rectangle
            {
                Width = SelectedPartSize.Width,
                Height = SelectedPartSize.Height,
                Fill = InputImageBrush,
                Visibility = Visibility.Visible,
                Stroke = Brushes.DarkOrange
            };

            InputCanvas = new Canvas
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Children = { InputRectangle },
                Width = outputImage.ActualWidth
            };

            Parent.Children.Add(InputCanvas);

            InputCanvas.SetValue(Grid.ColumnProperty, 0);

            #endregion

            #region OutputImage

            OutputRectangle = new Rectangle
            {
                Width = SelectedPartSize.Width,
                Height = SelectedPartSize.Height,
                Fill = OutputImageBrush,
                Visibility = Visibility.Visible,
                Stroke = Brushes.DarkOrange
            };

            OutputCanvas = new Canvas
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Children = { OutputRectangle },
                Width = outputImage.ActualWidth
            };

            Parent.Children.Add(OutputCanvas);

            OutputCanvas.SetValue(Grid.ColumnProperty, 1);

            #endregion
        }

        public void SetLoup(Point position, Image image)
        {
            if (position.X <= 0 || position.X >= image.ActualWidth || position.Y <= 0 || position.Y >= image.ActualHeight)
            {
                image.ReleaseMouseCapture();
                this.Dispose();
                return;
            }

            var deltaX = (Scale-1) * (20 / image.ActualWidth);
            var deltaY = (Scale-1) * (20 / image.ActualHeight);

            InputImageBrush.Viewbox = new Rect(
                (position.X - SelectedPartSize.Width / 2) / image.ActualWidth - deltaX,
                (position.Y  - SelectedPartSize.Height / 2) / image.ActualHeight - deltaY, 
                SelectedPartSize.Width / image.ActualWidth,
                SelectedPartSize.Height / image.ActualHeight);
            OutputImageBrush.Viewbox = new Rect(
                (position.X  - SelectedPartSize.Width / 2) / image.ActualWidth - deltaX,
                (position.Y  - SelectedPartSize.Height / 2) / image.ActualHeight - deltaY, 
                SelectedPartSize.Width / image.ActualWidth,
                SelectedPartSize.Height / image.ActualHeight);

            InputRectangle.SetValue(Canvas.TopProperty, position.Y - InputRectangle.Height / 2);
            InputRectangle.SetValue(Canvas.LeftProperty, position.X - InputRectangle.Width / 2);

            OutputRectangle.SetValue(Canvas.TopProperty, position.Y - OutputRectangle.Height / 2);
            OutputRectangle.SetValue(Canvas.LeftProperty, position.X - OutputRectangle.Width / 2);
        }

        //public void OpenNewItem(Grid parent)
        //{
        //    Parent = parent;


        //    #region InputImage

        //    InputLoupRectangle = new Rectangle
        //    {
        //        Height = 100,
        //        Width = 100,
        //        Fill = Brushes.DarkGreen,
        //        Visibility = Visibility.Hidden
        //    };

        //    InputSourceRectangle = new Rectangle
        //    {
        //        Height = 100,
        //        Width = 100,
        //        Fill = Brushes.Green,
        //        Visibility = Visibility.Hidden
        //    };

        //    InputCanvas = new Canvas
        //    {
        //        HorizontalAlignment = HorizontalAlignment.Left,
        //        VerticalAlignment = VerticalAlignment.Top,
        //        Children = { InputLoupRectangle, InputSourceRectangle }
        //    };

        //    Parent.Children.Add(InputCanvas);

        //    InputCanvas.SetValue(Grid.ColumnProperty, 0);

        //    #endregion

        //    #region OutputImage


        //    OutputLoupRectangle = new Rectangle
        //    {
        //        Height = 100,
        //        Width = 100,
        //        Fill = Brushes.DarkBlue,
        //        Visibility = Visibility.Hidden
        //    };

        //    OutputSourceRectangle = new Rectangle
        //    {
        //        Height = 100,
        //        Width = 100,
        //        Fill = Brushes.Blue,
        //        Visibility = Visibility.Hidden
        //    };

        //    OutputCanvas = new Canvas
        //    {
        //        HorizontalAlignment = HorizontalAlignment.Left,
        //        VerticalAlignment = VerticalAlignment.Top,
        //        Children = { OutputLoupRectangle, OutputSourceRectangle }
        //    };

        //    Parent.Children.Add(OutputCanvas);

        //    OutputCanvas.SetValue(Grid.ColumnProperty, 1);

        //    #endregion
        //}


        public void Dispose()
        {
            var collection = new Collection<DependencyObject>
            {
                InputRectangle,
                OutputRectangle,
                InputRectangle,
                OutputRectangle,
                InputCanvas,
                OutputCanvas,
                InputImageBrush,
                OutputImageBrush,
                Parent
            };

            Parent = null;

            foreach (var obj in collection)
            {
                if (!Equals(obj, null))
                {
                    ClearDependencyProperties(obj);
                }                
            }
        }

        public void ClearDependencyProperties(DependencyObject ob)
        {
            LocalValueEnumerator locallySetProperties = ob.GetLocalValueEnumerator();
            while (locallySetProperties.MoveNext())
            {
                DependencyProperty propertyToClear = locallySetProperties.Current.Property;
                if (!propertyToClear.ReadOnly)
                {
                    ob.ClearValue(propertyToClear);
                }
            }
        }
    }
}
