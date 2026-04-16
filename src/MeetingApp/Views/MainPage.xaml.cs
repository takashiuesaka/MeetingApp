using MeetingApp.ViewModels;

namespace MeetingApp.Views;

public partial class MainPage : ContentPage
{
    private const double MinSidebarWidth = 160;
    private const double MaxSidebarWidth = 600;
    private const double SplitterThickness = 4;
    private const double MinPanelHeight = 80;

    private double _sidebarWidthAtDragStart;
    private double _transcriptHeightAtDragStart;
    private double _translationHeightRatio = 0.4;

    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        var vm = (MainViewModel)BindingContext;
        if (e.PropertyName == nameof(MainViewModel.IsTranslationVisible))
        {
            UpdateSplitLayout(vm.IsTranslationVisible);
        }
        else if (e.PropertyName == nameof(MainViewModel.IsSidebarVisible))
        {
            if (vm.IsSidebarVisible)
            {
                SidebarColumn.Width = new GridLength(vm.SidebarWidth);
                BodyGrid.ColumnDefinitions[1].Width = new GridLength(SplitterThickness);
            }
            else
            {
                SidebarColumn.Width = GridLength.Zero;
                BodyGrid.ColumnDefinitions[1].Width = GridLength.Zero;
            }
        }
    }

    private void UpdateSplitLayout(bool show)
    {
        if (show)
        {
            double totalHeight = MainContentGrid.Height;
            if (totalHeight <= 0)
                totalHeight = 600;

            double translationHeight = totalHeight * _translationHeightRatio;
            double transcriptHeight = totalHeight - SplitterThickness - translationHeight;

            TranscriptRow.Height = new GridLength(transcriptHeight);
            HorizontalSplitterRow.Height = new GridLength(SplitterThickness);
            TranslationRow.Height = new GridLength(translationHeight);

            HorizontalSplitter.IsVisible = true;
            TranslationPanel.IsVisible = true;
        }
        else
        {
            TranscriptRow.Height = GridLength.Star;
            HorizontalSplitterRow.Height = GridLength.Zero;
            TranslationRow.Height = GridLength.Zero;

            HorizontalSplitter.IsVisible = false;
            TranslationPanel.IsVisible = false;
        }
    }

    // ── Vertical splitter (sidebar resize) ──────────────────────────────

    private void OnVerticalSplitterPanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        var viewModel = (MainViewModel)BindingContext;

        switch (e.StatusType)
        {
            case GestureStatus.Started:
                _sidebarWidthAtDragStart = viewModel.SidebarWidth;
                break;

            case GestureStatus.Running:
                double newWidth = _sidebarWidthAtDragStart + e.TotalX;
                newWidth = Math.Clamp(newWidth, MinSidebarWidth, MaxSidebarWidth);
                viewModel.SidebarWidth = newWidth;
                SidebarColumn.Width = new GridLength(newWidth);
                break;
        }
    }

    // ── Horizontal splitter (transcript / translation resize) ────────────

    private void OnHorizontalSplitterPanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                _transcriptHeightAtDragStart = TranscriptRow.Height.Value;
                break;

            case GestureStatus.Running:
                double totalHeight = MainContentGrid.Height;
                if (totalHeight <= 0)
                    return;

                double newTranscriptHeight = _transcriptHeightAtDragStart + e.TotalY;
                double maxTranscript = totalHeight - SplitterThickness - MinPanelHeight;
                newTranscriptHeight = Math.Clamp(newTranscriptHeight, MinPanelHeight, maxTranscript);

                double newTranslationHeight = totalHeight - SplitterThickness - newTranscriptHeight;

                TranscriptRow.Height = new GridLength(newTranscriptHeight);
                TranslationRow.Height = new GridLength(newTranslationHeight);

                _translationHeightRatio = newTranslationHeight / totalHeight;
                break;
        }
    }
}
