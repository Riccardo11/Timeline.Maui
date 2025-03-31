using System.Collections;
using Microsoft.Maui.Layouts;
using Timeline.Maui.Internals;
using Timeline.Maui.Internals.Views;

namespace Timeline.Maui;

public partial class TimelineView : ContentView
{
    public TimelineView()
    {
        InitializeComponent();
        TimelineContainer.ChildAdded += UpdateTimelineView;
        TimelineContainer.ChildRemoved += UpdateTimelineView;
        TimelineContainer.ChildrenReordered += UpdateTimelineView;
    }

    private void UpdateTimelineView(object? sender, EventArgs e)
    {
        if (sender != TimelineContainer)
            return;

        UpdateItemsLayout(this);
        UpdateItemsPosition(this);
        UpdateItemsSpacing(this);
    }

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource), typeof(IEnumerable), typeof(TimelineView), 
        propertyChanged: OnItemsSourceChanged);

    public IEnumerable? ItemsSource
    {
        get => (IEnumerable?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
    
    private static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TimelineView timelineView || newvalue is not IEnumerable itemsSource)
            return;
        
        BindableLayout.SetItemsSource(timelineView.TimelineContainer, itemsSource);
    }
    
    
    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
        nameof(ItemTemplate), typeof(DataTemplate), typeof(TimelineView),
        propertyChanged: OnItemTemplateChanged);

    public DataTemplate? ItemTemplate
    {
        get => (DataTemplate?)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    private static void OnItemTemplateChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TimelineView timelineView || newvalue is not DataTemplate itemTemplate)
            return;

        BindableLayout.SetItemTemplate(timelineView.TimelineContainer, new DataTemplate(() =>
        {
            var itemView = new TimelineItemView
            {
                UserItemTemplate = itemTemplate,
                LineColor = timelineView.LineColor,
                IndicatorColor = timelineView.IndicatorColor,
                IndicatorView = timelineView.IndicatorTemplate?.LoadTemplate() as View,
                ItemIndicatorSpacing = timelineView.ItemIndicatorSpacing,
                Orientation = timelineView.Orientation
            };

            return itemView;
        }));
    }


    public static readonly BindableProperty OrientationProperty = BindableProperty.Create(
        nameof(Orientation), typeof(TimelineOrientation), typeof(TimelineView), TimelineOrientation.Vertical, 
        propertyChanged: OnOrientationChanged);

    public TimelineOrientation Orientation
    {
        get => (TimelineOrientation) GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }
    
    private static void OnOrientationChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TimelineView timelineView || newvalue is not TimelineOrientation orientation)
            return;
        
        timelineView.TimelineContainer.Direction = orientation switch
        {
            TimelineOrientation.Horizontal => FlexDirection.Row,
            TimelineOrientation.Vertical => FlexDirection.Column,
            _ => FlexDirection.Column,
        };

        // timelineView.TimelineContainer.Orientation = orientation switch
        // {
        //     TimelineOrientation.Horizontal => StackOrientation.Horizontal,
        //     TimelineOrientation.Vertical => StackOrientation.Vertical,
        //     _ => StackOrientation.Vertical,
        // };
        
        foreach (var itemView in timelineView.TimelineContainer.Children)
        {
            if (itemView is not TimelineItemView timelineItemView)
                continue;
            
            timelineItemView.Orientation = orientation;
        }
    }
    
    
    public static readonly BindableProperty ItemsPositionProperty = BindableProperty.Create(
        nameof(ItemsPosition), typeof(TimelineItemsPosition), typeof(TimelineView), TimelineItemsPosition.Start,
        propertyChanged: OnItemsPositionChanged);

    public TimelineItemsPosition ItemsPosition
    {
        get => (TimelineItemsPosition) GetValue(ItemsPositionProperty);
        set => SetValue(ItemsPositionProperty, value);
    }
    
    private static void OnItemsPositionChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TimelineView view || newvalue is not TimelineItemsPosition)
            return;
        
        UpdateItemsPosition(view);
    }
    

    public static readonly BindableProperty LineColorProperty = BindableProperty.Create(
        nameof(LineColor), typeof(Color), typeof(TimelineView), Constants.DefaultColor,
        propertyChanged: OnLineColorChanged);

    public Color LineColor
    {
        get => (Color)GetValue(LineColorProperty);
        set => SetValue(LineColorProperty, value);
    }
    
    private static void OnLineColorChanged(BindableObject bindable, object? oldvalue, object newvalue)
    {
        if (bindable is not TimelineView view || newvalue is not Color color)
            return;

        foreach (var itemView in view.TimelineContainer.Children)
        {
            if (itemView is not TimelineItemView timelineItemView)
                continue;
            
            timelineItemView.LineColor = color;
        }
    }
    
    
    public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(
        nameof(IndicatorColor), typeof(Color), typeof(TimelineView), Constants.DefaultColor,
        propertyChanged: OnIndicatorColorChanged);

    public Color IndicatorColor
    {
        get => (Color)GetValue(IndicatorColorProperty);
        set => SetValue(IndicatorColorProperty, value);
    }
    
    private static void OnIndicatorColorChanged(BindableObject bindable, object? oldvalue, object newvalue)
    {
        if (bindable is not TimelineView view || newvalue is not Color color)
            return;

        foreach (var itemView in view.TimelineContainer.Children)
        {
            if (itemView is not TimelineItemView timelineItemView)
                continue;
            
            timelineItemView.IndicatorColor = color;
        }
    }
    
    
    public static readonly BindableProperty SpacingProperty = BindableProperty.Create(
        nameof(Spacing), typeof(double), typeof(TimelineView), 0d,
        propertyChanged: OnSpacingChanged);

    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }
    
    private static void OnSpacingChanged(BindableObject bindable, object? oldvalue, object newvalue)
    {
        if (bindable is not TimelineView view || newvalue is not double)
            return;

        UpdateItemsSpacing(view);
    }
    
    
    public static readonly BindableProperty ItemIndicatorSpacingProperty = BindableProperty.Create(
        nameof(ItemIndicatorSpacing), typeof(double), typeof(TimelineView), Constants.DefaultItemIndicatorSpacing,
        propertyChanged: OnItemIndicatorSpacingChanged);

    public double ItemIndicatorSpacing
    {
        get => (double)GetValue(ItemIndicatorSpacingProperty);
        set => SetValue(ItemIndicatorSpacingProperty, value);
    }
    
    private static void OnItemIndicatorSpacingChanged(BindableObject bindable, object? oldvalue, object newvalue)
    {
        if (bindable is not TimelineView view || newvalue is not double itemIndicatorSpacing)
            return;

        foreach (var itemView in view.TimelineContainer.Children)
        {
            if (itemView is not TimelineItemView timelineItemView)
                continue;

            timelineItemView.ItemIndicatorSpacing = itemIndicatorSpacing;
        }
    }
    
    
    public static readonly BindableProperty IndicatorTemplateProperty = BindableProperty.Create(
        nameof(IndicatorTemplate), typeof(DataTemplate), typeof(TimelineView),
        propertyChanged: OnIndicatorTemplateChanged);
    
    public DataTemplate? IndicatorTemplate
    {
        get => (DataTemplate?)GetValue(IndicatorTemplateProperty);
        set => SetValue(IndicatorTemplateProperty, value);
    }
    
    private static void OnIndicatorTemplateChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not TimelineView view || newvalue is not DataTemplate indicatorView)
            return;
    
        foreach (var itemView in view.TimelineContainer.Children)
        {
            if (itemView is not TimelineItemView timelineItemView)
                continue;
            
            timelineItemView.IndicatorView = indicatorView.LoadTemplate() as View;
        }
    }

    private void ReleaseResources(object? sender, EventArgs e)
    {
        TimelineContainer.ChildAdded -= UpdateTimelineView;
        TimelineContainer.ChildRemoved -= UpdateTimelineView;
        TimelineContainer.ChildrenReordered -= UpdateTimelineView;
    }

    private static void UpdateItemsSpacing(TimelineView view)
    {
        var spacing = view.Spacing;
        
        for (var i = 0; i < view.TimelineContainer.Children.Count; i++)
        {
            var itemView = view.TimelineContainer.Children[i];
            if (itemView is not TimelineItemView timelineItemView)
                continue;
            
            timelineItemView.StartSpacing = spacing / 2;
            timelineItemView.EndSpacing = spacing / 2;
            
            // if (i > 0)
            //     timelineItemView.TopSpacing = spacing / 2;
            //
            // if (i < view.TimelineContainer.Children.Count)
            //     timelineItemView.BottomSpacing = spacing / 2;
        }
    }

    private static void UpdateItemsLayout(TimelineView view)
    {
        for (var i = 0; i < view.TimelineContainer.Children.Count; i++)
        {
            var itemView = view.TimelineContainer.Children[i] as TimelineItemView;

            if (itemView is null)
                continue;
            
            if (i == 0)
                itemView.IsFirstItem = true;

            if (i == view.TimelineContainer.Children.Count - 1)
                itemView.IsLastItem = true;

            if (i > 0)
                itemView.IsFirstItem = false;

            if (i < view.TimelineContainer.Children.Count - 1)
                itemView.IsLastItem = false;
        }
    }

    private static void UpdateItemsPosition(TimelineView view)
    {
        for (var i = 0; i < view.TimelineContainer.Children.Count; i++)
        {
            if (view.TimelineContainer.Children[i] is not TimelineItemView itemView)
                continue;
            
            switch (view.ItemsPosition)
            {
                case TimelineItemsPosition.Start:
                    itemView.TimelineItemPosition = TimelineSingleItemPosition.Start;
                    break;
                case TimelineItemsPosition.Alternate:
                    itemView.TimelineItemPosition = i % 2 == 0 ? TimelineSingleItemPosition.Start : TimelineSingleItemPosition.End;
                    break;
                case TimelineItemsPosition.End:
                    itemView.TimelineItemPosition = TimelineSingleItemPosition.End;
                    break;
                default:
                    continue;
            }
        }
    }
}