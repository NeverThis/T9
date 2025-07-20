using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using UI.ViewModels;

namespace UI.Views
{
    /// <summary>
    /// A UserControl that displays an animated sine wave loading indicator.
    /// The animation is driven by the IsAnimating property of the LoadingViewModel DataContext.
    /// </summary>
    public partial class LoadingView : UserControl
    {
        private double phaseShift = 0;

        private Polyline? sineWave;
        private PointCollection? points;

        /// <summary>
        /// The current ViewModel bound to this view.
        /// Used to subscribe/unsubscribe to property change notifications.
        /// </summary>
        private LoadingViewModel? currentViewModel;

        public LoadingView()
        {
            InitializeComponent();
            DataContextChanged += LoadingView_DataContextChanged;
            Unloaded += LoadingView_Unloaded;
        }

        /// <summary>
        /// Handles changes to the DataContext of the control.
        /// Unsubscribes from previous ViewModel property changes and subscribes to the new one.
        /// Starts or stops animation based on the new ViewModel's IsAnimating property.
        /// </summary>
        /// <param name="sender">The source of the event, this control.</param>
        /// <param name="e">Event data containing old and new DataContext values.</param>
        private void LoadingView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (currentViewModel != null)
            {
                currentViewModel.PropertyChanged -= ViewModel_PropertyChanged;
                StopAnimation();
            }

            currentViewModel = e.NewValue as LoadingViewModel;

            if (currentViewModel != null)
            {
                currentViewModel.PropertyChanged += ViewModel_PropertyChanged;

                if (currentViewModel.IsAnimating)
                    StartAnimation();
                else
                    StopAnimation();
            }
        }

        /// <summary>
        /// Handles PropertyChanged events from the ViewModel.
        /// Starts or stops the animation when the IsAnimating property changes.
        /// </summary>
        /// <param name="sender">The ViewModel sending the event.</param>
        /// <param name="e">PropertyChanged event arguments.</param>
        private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoadingViewModel.IsAnimating) && sender is LoadingViewModel vm)
            {
                if (vm.IsAnimating)
                    StartAnimation();
                else
                    StopAnimation();
            }
        }

        /// <summary>
        /// Handles the Unloaded event of the control.
        /// Cleans up event subscriptions and stops animation to prevent memory leaks.
        /// </summary>
        /// <param name="sender">The source of the event, this control.</param>
        /// <param name="e">Event data.</param>
        private void LoadingView_Unloaded(object sender, RoutedEventArgs e)
        {
            if (currentViewModel != null)
            {
                currentViewModel.PropertyChanged -= ViewModel_PropertyChanged;
                currentViewModel = null;
            }

            DataContextChanged -= LoadingView_DataContextChanged;
            Unloaded -= LoadingView_Unloaded;

            StopAnimation();
        }

        private void StartAnimation()
        {
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void StopAnimation()
        {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
        }

        /// <summary>
        /// Called on each frame render; updates the phase shift and redraws the sine wave.
        /// </summary>
        /// <param name="sender">The event source, CompositionTarget.</param>
        /// <param name="e">Event arguments.</param>
        private void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            phaseShift += 0.1;
            DrawSineWave();
        }

        /// <summary>
        /// Initializes the Polyline and PointCollection used for drawing the sine wave.
        /// Adds the Polyline to the WaveCanvas if it has not already been created.
        /// </summary>
        private void InitializeSineWave()
        {
            if (sineWave == null)
            {
                sineWave = new Polyline
                {
                    Stroke = Brushes.Ivory,
                    StrokeThickness = 2,
                    Opacity = 0.2
                };

                points = new PointCollection();
                sineWave.Points = points;
                WaveCanvas.Children.Add(sineWave);
            }
        }

        /// <summary>
        /// Redraws the sine wave on the canvas by updating the Polyline points based on the current phase shift.
        /// </summary>
        private void DrawSineWave()
        {
            if (WaveCanvas.ActualWidth == 0 || WaveCanvas.ActualHeight == 0)
                return;

            InitializeSineWave();

            double amplitude = 20;
            double frequency = 0.05;
            double xMax = WaveCanvas.ActualWidth;
            double yCenter = WaveCanvas.ActualHeight / 2;

            points!.Clear();

            for (double x = 0; x <= xMax; x += 10)
            {
                double y = yCenter + amplitude * Math.Sin(frequency * x + phaseShift);
                points.Add(new Point(x, y));
            }
        }
    }
}
