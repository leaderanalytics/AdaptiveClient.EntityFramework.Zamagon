namespace Zamagon.WPF;

public class TabHeader : Control, INotifyPropertyChanged
{
    public string IconName
    {
        get { return (string)GetValue(IconNameProperty); }
        set { SetValue(IconNameProperty, value); }
    }

    public static readonly DependencyProperty IconNameProperty =
        DependencyProperty.Register("IconName", typeof(string), typeof(TabHeader), new PropertyMetadata(null));

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(TabHeader), new PropertyMetadata(null));


    public bool IsSelected
    {
        get { return (bool)GetValue(IsSelectedProperty); }
        set { SetValue(IsSelectedProperty, value); }
    }

    public static readonly DependencyProperty IsSelectedProperty =
    DependencyProperty.Register("IsSelected", typeof(bool), typeof(TabHeader), new FrameworkPropertyMetadata(false, IsSelectedCallback, CoerceSelectedCallback));


    public bool IsHighlighted
    {
        get { return (bool)GetValue(IsHighlightedProperty); }
        set { SetValue(IsHighlightedProperty, value); }
    }

    public static readonly DependencyProperty IsHighlightedProperty =
        DependencyProperty.Register("IsHighlighted", typeof(bool), typeof(TabHeader), new FrameworkPropertyMetadata(false, IsHighlightedCallback, CoerceHighlightedCallback));


    public Brush HighlightedBackground
    {
        get { return (Brush)GetValue(HighlightedBackgroundProperty); }
        set { SetValue(HighlightedBackgroundProperty, value); }
    }

    public static readonly DependencyProperty HighlightedBackgroundProperty =
        DependencyProperty.Register("HighlightedBackground", typeof(Brush), typeof(TabHeader), new PropertyMetadata(null));


    public Brush SelectedBackground
    {
        get { return (Brush)GetValue(SelectedBackgroundProperty); }
        set { SetValue(SelectedBackgroundProperty, value); }
    }

    public static readonly DependencyProperty SelectedBackgroundProperty =
        DependencyProperty.Register("SelectedBackground", typeof(Brush), typeof(TabHeader), new PropertyMetadata(null));

    private Brush NormalBackground { get; set; }

    static TabHeader()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TabHeader), new FrameworkPropertyMetadata(typeof(TabHeader)));
    }

    public TabHeader()
    {
        this.MouseEnter += TabHeader_MouseEnter;
        this.MouseLeave += TabHeader_MouseLeave;
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        NormalBackground = Background;
    }

    private void TabHeader_MouseLeave(object sender, MouseEventArgs e)
    {
        IsHighlighted = IsMouseOver && !IsSelected;
    }

    private void TabHeader_MouseEnter(object sender, MouseEventArgs e)
    {
        IsHighlighted = IsMouseOver && !IsSelected;
    }

    static void IsHighlightedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        TabHeader tabHeader = sender as TabHeader;
        tabHeader.IsHighlighted = (tabHeader.IsMouseOver && !tabHeader.IsSelected);
        SetBackground(tabHeader);
        tabHeader.RaisePropertyChanged("IsHighlighted");
        tabHeader.RaisePropertyChanged("IsSelected");
    }

    static object CoerceHighlightedCallback(DependencyObject sender, object val)
    {
        TabHeader tabHeader = sender as TabHeader;
        return (tabHeader.IsMouseOver && !tabHeader.IsSelected);
    }

    static void IsSelectedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        TabHeader tabHeader = sender as TabHeader;
        tabHeader.IsSelected = (bool)e.NewValue;
        SetBackground(tabHeader);
        tabHeader.RaisePropertyChanged("IsHighlighted");
        tabHeader.RaisePropertyChanged("IsSelected");
    }

    static object CoerceSelectedCallback(DependencyObject sender, object val)
    {
        TabHeader tabHeader = sender as TabHeader;
        return ((bool)val);
    }

    private static void SetBackground(TabHeader tabHeader)
    {
        if (tabHeader.IsSelected)
            tabHeader.Background = tabHeader.SelectedBackground;
        else if (tabHeader.IsHighlighted)
            tabHeader.Background = tabHeader.HighlightedBackground;
        else
            tabHeader.Background = tabHeader.NormalBackground;
    }

    #region ProperyChanged Implementation
    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
