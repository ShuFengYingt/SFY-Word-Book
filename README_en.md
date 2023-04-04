# *4.4Project Log*
[Original Chinese log](https://github.com/ShuFengYingt/SFY-Word-Book/blob/master/README.md)
# **Implemented the following features:**

1. Users can navigate to corresponding pages by selecting menu items in the left side menu bar.
2. The left side menu bar is collapsed after navigation.
3. Users can use the "→" and "←" keys on the navigation bar to navigate forward and backward (implemented using navigation log).

# **Pitfalls:**

1. Use "UserControl" to create child pages, instead of using "Window".
2. Follow the naming convention: pages under the Views folder should be named as xxxView, and pages under the ViewModel folder should be named as xxxViewModel.
3. After creating a page, be sure to register it in App.xaml.cs, otherwise navigation will not work!

# **Key Points:**

## **1. Registration**

(0) Create a child page (UserControl) under View and create its corresponding Model.

(1) In the "RegisterTypes" method overridden in App.xaml.cs, be sure to register the created child page (UserControl). The example code is as follows:

```csharp
containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
```

In the generics, fill in "HomeView" and its corresponding "HomeViewModel" for automatic association, which is a feature of the Prism framework.

(2) In MainView.xaml, register the main page area. Only after registration can specific navigation operations be performed. The registration XAML code for the main page area is as follows:

```xml
<ContentControl prism:RegionManager.RegionName="{x:Static extensions:PrismManager.MainViewRegionName}" />
```

Note the declaration of "PrismManager.MainViewRegionName," which we need to declare ourselves. The declaration process is as follows:

I. Create an Extensions folder.

II. Create PrismManager.cs inside it and declare it as "public". Create a read-only string named MainViewRegionName under it, with its content as MainViewRegion. This naming format should be necessary.

III. Declare the extension namespace in MainView.xaml, as follows:

```xml
xmlns:extensions="clr-namespace:SFY_Word_Book.Extensions".
```

IV. Register the area as above.

(3) In MainViewModel, create a read-only IRegionManager interface variable regionManager, as follows:

```csharp
private readonly IRegionManager regionManager;
```

And instantiate it in the constructor, as follows:

```csharp
this.regionManager = regionManager;
```

With this, the area registration is complete, and menu navigation can now be implemented.

# 2. Implement Menu Navigation:

(1) Make sure you have the method CreateMenuBar, which contains statements for building menu elements, such as:

```csharp
MenuBars.Add(new MenuBar() { Icon = "Home", Title = "Homepage", NameSpace = "HomeView" });
```

This code is bound to the MenuBar class, which has three properties: Icon, Title, and NameSpace. Note that NameSpace needs to be consistent with the child page name (consistent with the class registered in App.xaml.cs).

(2) Ensure that you have the following code to update the menu:

```csharp
// Declare and update menu on the main page
private ObservableCollection<MenuBar> menuBars;
public ObservableCollection<MenuBar> MenuBars
{
    get { return menuBars; }
    set { menuBars = value; RaisePropertyChanged(); }
}
```

(3) Declare a navigation delegate with a generic type of MenuBar:

```csharp
public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
```

(4) Construct the navigation method Navigate, which takes a menuBar of type MenuBar. Note that if it is null or the namespace is not declared, it should return.

Then go to xaml to complete the implementation.

(5) In MainView.xaml, check whether the ListBox of the menu body has declared its name, such as x:Name="MenuBar".

(6) Complete the interaction behavior trigger, as follows:

```xml
<behaviors:Interaction.Triggers>
    <behaviors:EventTrigger EventName="SelectionChanged">
        <behaviors:InvokeCommandAction Command="{Binding NavigateCommand}" 
            CommandParameter="{Binding ElementName=MenuBar, Path=SelectedItem}" />
    </behaviors:EventTrigger>
</behaviors:Interaction.Triggers>
```

This step requires the Microsoft behavior to be imported, i.e., declare the namespace:

```xml
xmlns:behaviors="http://schemas.microsoft.com/xaml/behaviors"
```

At this point, the menu functionality is fully implemented.

# 3. Back and Forward Navigation

Navigation journal is needed for this feature.

(1) Declare the navigation journal:

```csharp
private IRegionNavigationJournal journal;
```

(2) Use it in the Navigate method:

```csharp
regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(menuBar.NameSpace, back =>
            {
                journal = back.Context.NavigationService.Journal;
            });
```

(3) Declare the DelegateCommand properties for going back and forward:

```csharp
public DelegateCommand GoBackCommand { get; private set; }
public DelegateCommand GoForwardCommand { get; private set; }
```

(4) Implement the feature, for example:

```csharp
GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                {
                    journal.GoBack();
                }
            });
```

(5) Make sure the names of the command properties bound to the up and down keys match the names of the DelegateCommand properties in MainViewModel.cs.

With this, the Back and Forward navigation feature is implemented.
