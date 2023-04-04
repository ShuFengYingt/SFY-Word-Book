# 4.4 项目日志

# 实现了以下功能：

1. 用户在左侧菜单栏选中菜单的元素后导航至对应页面。
2. 导航后收起左侧菜单栏。
3. 允许用户使用导航条上的“—>”和“<—”键完成上一步和下一步的导航（利用导航日志实现）

# 坑点：

1. 需要使用“UserControl”来创建子页面，而不能用Window进行创建。
2. 命名规范上，Views文件夹下的页面要命名为xxxView，ViewModel文件夹下的页面需要命名为xxxViewModel
3. 创建页面后，一定一定要去App.xaml.cs下进行注册，否则无法完成导航工作！！！

# 要点:

## 1.注册

( 0 )创建子页面( UserControl )于View下，同时创建其对应的Model

( 1 ). 一定要在App.xaml.cs下的重写”RegisterTypes“方法中注册创建的子页面（UserControl）,示例代码如下：

```csharp
containerRegistry.RegisterForNavigation<HomeView,HomeViewModel> ();
```

泛型中分别填入”HomeView“和其对应的”HomeViewModel“进行自动关联，这是Prism框架的一个特性。

( 2 ). 在MainView.xaml下，注册主页区域。只有注册完毕才能进行下一步导航的具体操作。
主页区域注册xaml代码如下：

```xml
<ContentControl prism:RegionManager.RegionName="{x:Static extensions:PrismManager.MainViewRegionName}" />
```

注意到”PrismManager.MainViewRegionName“这段声明，这个需要我们自己去声明，声明过程为：

I：创建Extensions文件夹。
II:在其中创建PrismManager.cs，并且声明为”public“。在其下创建一个只读字符串，名为MainViewRegionName，其内容为MainViewRegion，这个命名格式应该是必要的。
III:在MainView.xaml下声明extension命名空间，举例如下：

```xml
xmlns:extensions="clr-namespace:SFY_Word_Book.Extensions"。
```

IV:而后注册区域，如上。

( 3 ).在MainViewModel下创建只读的IRegionManager接口的变量regionManager

```csharp
private readonly IRegionManager regionManager;
```

并在构造函数中进行实例化，如下：

```csharp
this.regionManager = regionManager;
```

至此，区域注册完毕，接下来实现菜单导航

# 2.实现菜单导航:

( 1 ).确定自己有CreateMenuBar这个方法，里面有类似于这样的构建菜单元素语句

```csharp
MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "HomeView" });
```

这段代码绑定的是MemuBar类，类下有Icon，Title和NameSpace三个属性，其中的NameSpace要和子页面名称一致（与在App.xaml.cs中注册的类一致）

( 2 ).确保有如下菜单更新代码

```csharp
				//菜单在主页的声明和更新
        private ObservableCollection<MenuBar> menuBars;
        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }
```

( 3 ).声明导航委托,泛型类型为MenuBar

```csharp
public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
```

( 4 ).构造导航方法Navigate，传入MenuBar类型的menuBar，注意，若其为空或者命名空间未声明则需返回。

而后转至xaml中完成实现

( 5 ).在MainView.xaml中，查看菜单体的ListBox是否有声明其名称，例如x:Name = “MenuBar”

( 6 ).完成交互行为触发器，如下

```xml

                        <behaviors:Interaction.Triggers>
                            <behaviors:EventTrigger EventName="SelectionChanged">
                                <behaviors:InvokeCommandAction Command="{Binding NavigateCommand}" 
																															 CommandParameter="{Binding ElementName=MenuBar, 
																																													Path=SelectedItem}" />
                            </behaviors:EventTrigger>
                        </behaviors:Interaction.Triggers>
```

这一步需要引入微软的behavior，即声明命名空间

```xml
xmlns:behaviors="http://schemas.microsoft.com/xaml/behaviors"
```

至此，菜单功能完成实现

# 3.返回前进

需要用到导航日志

( 1 ).声明导航日志

```csharp
private IRegionNavigationJournal journal;
```

( 2 )在Navigate方法中引用

```csharp
regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(menuBar.NameSpace, back =>
            {
                journal = back.Context.NavigationService.Journal;
            });
```

( 3 )声明委托指令属性

```csharp
public DelegateCommand GoBackCommand { get; private set; }
public DelegateCommand GoForwardCommand { get; private set; }
```

( 4 )实现功能，例如

```csharp
						GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                {
                    journal.GoBack();
                }
            });
```

( 5 )确保上下键绑定的指令名称与MainViewModel.cs下的委托指令属性名称一致。

至此实现前进与返回

# *Project Log*

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