using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MeetingApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isSidebarVisible = true;

    [ObservableProperty]
    private bool _isTranslationVisible = false;

    [ObservableProperty]
    private double _sidebarWidth = 250;

    [ObservableProperty]
    private string _transcriptionText =
        "[00:00:01] Speaker A: Good morning everyone, let's get started.\n" +
        "[00:00:05] Speaker B: Sure, I'm ready.\n" +
        "[00:00:09] Speaker A: Today we'll review the quarterly results.\n" +
        "[00:00:14] Speaker B: Sounds good. The numbers look promising.\n" +
        "[00:00:20] Speaker A: We exceeded our targets by 12 percent this quarter.\n";

    [ObservableProperty]
    private string _translationText =
        "[00:00:01] スピーカー A: おはようございます。では始めましょう。\n" +
        "[00:00:05] スピーカー B: はい、準備できています。\n" +
        "[00:00:09] スピーカー A: 本日は四半期の結果を確認します。\n" +
        "[00:00:14] スピーカー B: 了解です。数字は有望に見えます。\n" +
        "[00:00:20] スピーカー A: 今四半期は目標を12%上回りました。\n";

    [RelayCommand]
    private void ToggleSidebar()
    {
        IsSidebarVisible = !IsSidebarVisible;
    }

    [RelayCommand]
    private void ToggleSplit()
    {
        IsTranslationVisible = !IsTranslationVisible;
    }
}
