namespace Timeline.Maui.Internals.Views;

internal partial class TimelineItemView : ContentView
{
    public TimelineItemView()
    {
        InitializeComponent();
    }
    
    public static readonly BindableProperty UserItemTemplateProperty = BindableProperty.Create(
        nameof(UserItemTemplate), typeof(DataTemplate), typeof(TimelineItemView),
        propertyChanged: OnUserItemTemplateChanged);

    public DataTemplate? UserItemTemplate
    {
        get => (DataTemplate?)GetValue(UserItemTemplateProperty);
        set => SetValue(UserItemTemplateProperty, value);
    }
    
    private static void OnUserItemTemplateChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TimelineItemView view || newvalue is not DataTemplate userItemTemplate) 
            return;

        if (userItemTemplate.LoadTemplate() is not View userView)
            return;
        
        UpdateUserItemView(view, userView);
        
        view.TimelineItemContainer.Children[1] = userView;
    }
    
    
    public static readonly BindableProperty LineColorProperty = BindableProperty.Create(
        nameof(LineColor), typeof(Color), typeof(TimelineItemView), Constants.DefaultColor,
        propertyChanged: OnLineColorChanged);

    public Color LineColor
    {
        get => (Color)GetValue(LineColorProperty);
        set => SetValue(LineColorProperty, value);
    }
    
    private static void OnLineColorChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TimelineItemView view || newvalue is not Color color)
            return;
        
        view.StartLine.BackgroundColor = color;
        view.EndLine.BackgroundColor = color;
    }
    
    
    public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(
        nameof(IndicatorColor), typeof(Color), typeof(TimelineItemView), Constants.DefaultColor,
        propertyChanged: OnIndicatorColorChanged);

    public Color IndicatorColor
    {
        get => (Color)GetValue(IndicatorColorProperty);
        set => SetValue(IndicatorColorProperty, value);
    }
    
    private static void OnIndicatorColorChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TimelineItemView view || newvalue is not Color color)
            return;
        
        if (view.ItemIndicatorContainer.Content is null)
            return;
            
        view.ItemIndicatorContainer.Content.BackgroundColor = color;
    }
    
    
    public static readonly BindableProperty TimelineItemPositionProperty = BindableProperty.Create(
        nameof(TimelineItemPosition), typeof(TimelineSingleItemPosition), typeof(TimelineItemView), TimelineSingleItemPosition.End,
        propertyChanged: OnTimelineItemPositionChanged);

    public TimelineSingleItemPosition TimelineItemPosition
    {
        get => (TimelineSingleItemPosition)GetValue(TimelineItemPositionProperty);
        set => SetValue(TimelineItemPositionProperty, value);
    }
    
    private static void OnTimelineItemPositionChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TimelineItemView view || newvalue is not TimelineSingleItemPosition position)
            return;

        if (view.TimelineItemContainer.Children[1] is not View userView)
            return;

        UpdateUserItemView(view, userView);
    }
    
    
    public static readonly BindableProperty IsFirstItemProperty = BindableProperty.Create(
        nameof(IsFirstItem), typeof(bool), typeof(TimelineItemView), false,
        propertyChanged: OnIsFirstItemChanged);

    public bool IsFirstItem
    {
        get => (bool)GetValue(IsFirstItemProperty);
        set => SetValue(IsFirstItemProperty, value);
    }
    
    private static void OnIsFirstItemChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TimelineItemView view || newvalue is not bool isFirstItem)
            return;

        view.StartLine.IsVisible = !isFirstItem;
    }
    
    
    public static readonly BindableProperty IsLastItemProperty = BindableProperty.Create(
        nameof(IsLastItem), typeof(bool), typeof(TimelineItemView), false,
        propertyChanged: OnIsLastItemChanged);

    public bool IsLastItem
    {
        get => (bool)GetValue(IsLastItemProperty);
        set => SetValue(IsLastItemProperty, value);
    }
    
    private static void OnIsLastItemChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TimelineItemView view || newvalue is not bool isLastItem)
            return;

        view.EndLine.IsVisible = !isLastItem;
    }
    
    
    public static readonly BindableProperty OrientationProperty = BindableProperty.Create(
        nameof(Orientation), typeof(TimelineOrientation), typeof(TimelineItemView), TimelineOrientation.Vertical, 
        propertyChanged: OnOrientationChanged);

    public TimelineOrientation Orientation
    {
        get => (TimelineOrientation) GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }
    
    private static void OnOrientationChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TimelineItemView view || newvalue is not TimelineOrientation orientation)
            return;

        switch (orientation)
        {
            case TimelineOrientation.Vertical:
                SetVertical(view);
                break;
            case TimelineOrientation.Horizontal:
                SetHorizontal(view);
                break;
            default:
                SetVertical(view);
                break;
        }

        if (view.TimelineItemContainer.Children[1] is View userView)
            UpdateUserItemView(view, userView);
    }
    
    
    public static readonly BindableProperty EndSpacingProperty = BindableProperty.Create(
        nameof(EndSpacing), typeof(double), typeof(TimelineItemView), 0d,
        propertyChanged: OnEndSpacingChanged);

    public double EndSpacing
    {
        get => (double)GetValue(EndSpacingProperty);
        set => SetValue(EndSpacingProperty, value);
    }
    
    private static void OnEndSpacingChanged(BindableObject bindable, object? oldvalue, object newvalue)
    {
        if (bindable is not TimelineItemView view || newvalue is not double endSpacing)
            return;
        
        if (view.TimelineItemContainer.Children[1] is not View userItem)
            return;

        userItem.Margin = view.Orientation switch
        {
            TimelineOrientation.Vertical => new Thickness(userItem.Margin.Left, userItem.Margin.Top,
                userItem.Margin.Right, endSpacing),
            TimelineOrientation.Horizontal => new Thickness(userItem.Margin.Left, userItem.Margin.Top, endSpacing,
                userItem.Margin.Bottom),
            _ => userItem.Margin
        };
    }
    
    
    public static readonly BindableProperty StartSpacingProperty = BindableProperty.Create(
        nameof(StartSpacing), typeof(double), typeof(TimelineItemView), 0d,
        propertyChanged: OnStartSpacingChanged);

    public double StartSpacing
    {
        get => (double)GetValue(StartSpacingProperty);
        set => SetValue(StartSpacingProperty, value);
    }
    
    private static void OnStartSpacingChanged(BindableObject bindable, object? oldvalue, object newvalue)
    {
        if (bindable is not TimelineItemView view || newvalue is not double startSpacing)
            return;

        if (view.TimelineItemContainer.Children[1] is not View userItem)
            return;
        
        userItem.Margin = view.Orientation switch
        {
            TimelineOrientation.Vertical => new Thickness(userItem.Margin.Left, startSpacing, userItem.Margin.Right, userItem.Margin.Bottom),
            TimelineOrientation.Horizontal => new Thickness(startSpacing, userItem.Margin.Top, userItem.Margin.Right, userItem.Margin.Bottom),
            _ => userItem.Margin
        };
    }
    
    
    public static readonly BindableProperty ItemIndicatorSpacingProperty = BindableProperty.Create(
        nameof(ItemIndicatorSpacing), typeof(double), typeof(TimelineItemView), Constants.DefaultItemIndicatorSpacing,
        propertyChanged: OnItemIndicatorSpacingChanged);

    public double ItemIndicatorSpacing
    {
        get => (double)GetValue(ItemIndicatorSpacingProperty);
        set => SetValue(ItemIndicatorSpacingProperty, value);
    }
    
    private static void OnItemIndicatorSpacingChanged(BindableObject bindable, object? oldvalue, object newvalue)
    {
        if (bindable is not TimelineItemView view || newvalue is not double itemIndicatorSpacing)
            return;

        switch (view.Orientation)
        {
            case TimelineOrientation.Vertical:
                view.TimelineItemContainer.RowSpacing = 0;
                view.TimelineItemContainer.ColumnSpacing = itemIndicatorSpacing;
                break;
            case TimelineOrientation.Horizontal:
                view.TimelineItemContainer.ColumnSpacing = 0;
                view.TimelineItemContainer.RowSpacing = itemIndicatorSpacing;
                break;
        }
    }
    
    
    public static readonly BindableProperty IndicatorViewProperty = BindableProperty.Create(
        nameof(IndicatorView), typeof(View), typeof(TimelineItemView),
        propertyChanged: OnIndicatorViewChanged);

    public View? IndicatorView
    {
        get => (View?)GetValue(IndicatorViewProperty);
        set => SetValue(IndicatorViewProperty, value);
    }
    
    private static void OnIndicatorViewChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TimelineItemView view || newvalue is not View itemIndicator)
            return;
        
        view.ItemIndicatorContainer.Content = itemIndicator;
    }

    private static void UpdateUserItemView(TimelineItemView view, View userView)
    {
        switch (view.TimelineItemPosition)
        {
            case TimelineSingleItemPosition.Start:
                if (view.Orientation == TimelineOrientation.Vertical)
                {
                    Grid.SetRow(userView, 0);
                    Grid.SetColumn(userView, 0);
                    userView.VerticalOptions = LayoutOptions.Fill;
                    userView.HorizontalOptions = LayoutOptions.End;
                }
                else
                {
                    Grid.SetColumn(userView, 0);
                    Grid.SetRow(userView, 0);
                    userView.HorizontalOptions = LayoutOptions.Fill;
                    userView.VerticalOptions = LayoutOptions.End;
                }
                break;
            case TimelineSingleItemPosition.End:
                if (view.Orientation == TimelineOrientation.Vertical)
                {
                    Grid.SetRow(userView, 0);
                    Grid.SetColumn(userView, 2);
                    userView.VerticalOptions = LayoutOptions.Fill;
                    userView.HorizontalOptions = LayoutOptions.Start;
                }
                else
                {
                    Grid.SetColumn(userView, 0);
                    Grid.SetRow(userView, 2);
                    userView.HorizontalOptions = LayoutOptions.Fill;
                    userView.VerticalOptions = LayoutOptions.Start;
                }
                break;
            default:
                return;
        }
    }

    private static void SetVertical(TimelineItemView view)
    {
        // Items are on left or right
        view.TimelineItemContainer.RowDefinitions = [];
        view.TimelineItemContainer.ColumnDefinitions =
        [
            new ColumnDefinition(GridLength.Star),
            new ColumnDefinition(5),
            new ColumnDefinition(GridLength.Star)
        ];
        
        // Spacing is applied on vertical
        view.TimelineItemContainer.RowSpacing = 0;
        view.TimelineItemContainer.ColumnSpacing =
            Math.Abs(view.ItemIndicatorSpacing - Constants.DefaultItemIndicatorSpacing) < 0.00001
                ? Constants.DefaultItemIndicatorSpacing
                : view.ItemIndicatorSpacing;
        
        // Lines and indicator are placed vertically
        view.LinesAndIndicatorContainer.ColumnDefinitions = [];
        view.LinesAndIndicatorContainer.RowDefinitions =
        [
            new RowDefinition(GridLength.Star),
            new RowDefinition(GridLength.Auto),
            new RowDefinition(GridLength.Star)
        ];
        
        // Lines and indicator are on the second column
        Grid.SetRow(view.LinesAndIndicatorContainer, 0);
        Grid.SetColumn(view.LinesAndIndicatorContainer, 1);
        
        // Lines are on the first and third row, have width = 1 and are horizontally centered
        Grid.SetColumn(view.StartLine, 0);
        Grid.SetRow(view.StartLine, 0);
        view.StartLine.HeightRequest = -1;
        view.StartLine.WidthRequest = 1;
        view.StartLine.VerticalOptions = LayoutOptions.Fill;
        view.StartLine.HorizontalOptions = LayoutOptions.Center;
        
        Grid.SetColumn(view.EndLine, 0);
        Grid.SetRow(view.EndLine, 2);
        view.EndLine.HeightRequest = -1;
        view.EndLine.WidthRequest = 1;
        view.EndLine.VerticalOptions = LayoutOptions.Fill;
        view.EndLine.HorizontalOptions = LayoutOptions.Center;
        
        // Item indicator is on the second row
        Grid.SetColumn(view.ItemIndicatorContainer, 0);
        Grid.SetRow(view.ItemIndicatorContainer, 1);
    }
    
    private static void SetHorizontal(TimelineItemView view)
    {
        // Items are on top or botton
        view.TimelineItemContainer.ColumnDefinitions = [];
        view.TimelineItemContainer.RowDefinitions =
        [
            new RowDefinition(GridLength.Star),
            new RowDefinition(5),
            new RowDefinition(GridLength.Star)
        ];
        
        // Spacing is applied on horizontal
        view.TimelineItemContainer.ColumnSpacing = 0;
        view.TimelineItemContainer.RowSpacing =
            Math.Abs(view.ItemIndicatorSpacing - Constants.DefaultItemIndicatorSpacing) < 0.00001
                ? Constants.DefaultItemIndicatorSpacing
                : view.ItemIndicatorSpacing;
        
        // Lines and indicator are placed horizontally
        view.LinesAndIndicatorContainer.RowDefinitions = [];
        view.LinesAndIndicatorContainer.ColumnDefinitions =
        [
            new ColumnDefinition(GridLength.Star),
            new ColumnDefinition(GridLength.Auto),
            new ColumnDefinition(GridLength.Star)
        ];
        
        // Lines and indicator are on the second row
        Grid.SetColumn(view.LinesAndIndicatorContainer, 0);
        Grid.SetRow(view.LinesAndIndicatorContainer, 1);
        
        // Lines are on the first and third column, have height = 1 and are vertically centered
        Grid.SetRow(view.StartLine, 0);
        Grid.SetColumn(view.StartLine, 0);
        view.StartLine.WidthRequest = -1;
        view.StartLine.HeightRequest = 1;
        view.StartLine.HorizontalOptions = LayoutOptions.Fill;
        view.StartLine.VerticalOptions = LayoutOptions.Center;
        
        Grid.SetRow(view.EndLine, 0);
        Grid.SetColumn(view.EndLine, 2);
        view.EndLine.WidthRequest = -1;
        view.EndLine.HeightRequest = 1;
        view.EndLine.HorizontalOptions = LayoutOptions.Fill;
        view.EndLine.VerticalOptions = LayoutOptions.Center;

        // Item indicator is on the second column
        Grid.SetRow(view.ItemIndicatorContainer, 0);
        Grid.SetColumn(view.ItemIndicatorContainer, 1);
    }
}