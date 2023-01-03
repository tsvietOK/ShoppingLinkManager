﻿using Microsoft.UI.Xaml.Controls;

using ShoppingLinkManager.ViewModels;

namespace ShoppingLinkManager.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
