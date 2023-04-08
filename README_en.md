[中文日志](https://github.com/ShuFengYingt/SFY-Word-Book/blob/master/README.md)
# 4.8 Project Log
1. Added opening animation effect for personalized modules.
2. Implemented navigation function for task bar in main window.
## Current Issues to be Resolved
1. Display issue with task bar (various containers still not properly organized).
2. Window pre-loading.
3. Display issue with main window.
## Pitfalls
Based on the previous Prism dependency injection navigation method, I attempted to enable task bar buttons to navigate to corresponding user controls. Navigation was indeed achieved, but the main user control and the new user control overlapped, which was very frustrating.

Later, I found that it was a problem with the region.

Previously, I defined a region under MainView, and used this region to navigate the menu bar to various pages.
```xml
                <ContentControl prism:RegionManager.RegionName="{x:Static extensions:PrismManager.MainViewRegionName}" />

```
And then I followed the same approach under HomeView:
```xml
                <ContentControl prism:RegionManager.RegionName="{x:Static extensions:PrismManager.HomeViewRegionName}" />

```
n the HomeViewModel, I wrote the method as follows:
```csharp
        private void Navigate(TaskBar taskBar)
        {
            if (taskBar == null || string.IsNullOrWhiteSpace(taskBar.NameSpace))
            {
                return;
            }
            regionManage.Regions[PrismManager.HomeViewRegion].RequestNavigate(taskBar.NameSpace);
        }

```
This led to the issue of overlapping user controls. Later, I attempted to remove the region registration from HomeView.xaml (actually it doesn't matter whether it's removed or not), and changed the navigation method from:
```csharp
regionManage.Regions[PrismManager.HomeViewRegion].RequestNavigate(taskBar.NameSpace);
```
to
```csharp
            regionManage.Regions[PrismManager.MainViewRegionName].RequestNavigate(taskBar.NameSpace);

```
which successfully resolved the issue of overlapping user controls.

This is because the region registered in MainView is the region of the entire page, with the area covering the entire window, and all the user controls loaded are displayed on the window. The window itself does not contain anything and is empty. On the other hand, the region registered in HomeView is the region of HomeView itself, and the user controls loaded in the HomeViewRegion will naturally be loaded onto HomeView, which leads to the overlap issue.

In the words of GPT:
![ChatGPT的解释](https://raw.githubusercontent.com/ShuFengYingt/SFY-Word-Book/master/READMEImage/%E5%B1%8F%E5%B9%95%E6%88%AA%E5%9B%BE%202023-04-08%20153337.png?token=GHSAT0AAAAAAB745OPHFEMMAVTGO2CL5H3OZBRD63A)

# 4.7 Project Log
1. New settings panel
2. Add personalized features
3. Fixed the details of the rounded window
![4.7_1](https://raw.githubusercontent.com/ShuFengYingt/SFY-Word-Book/master/READMEImage/4.7.png?token=GHSAT0AAAAAAB745OPGVXVZYGIOBBNX5NDOZBRD6EA)
and
![4.7_2](https://raw.githubusercontent.com/ShuFengYingt/SFY-Word-Book/master/READMEImage/4.7%202.png?token=GHSAT0AAAAAAB745OPH5ZBKZYXSCJYIQ4S2ZBRD6OQ)
# 4.6 Project Log
1. Most of the daily article functionality has been implemented, which calls an API to display image, title, and content information (there are still many bugs waiting to be fixed tomorrow and the day after tomorrow).
![4.6](https://raw.githubusercontent.com/ShuFengYingt/SFY-Word-Book/master/READMEImage/4.6.png?token=GHSAT0AAAAAAB745OPGSYHPKWIKIAEUWK4SZBRD5KQ)
## Contents waiting to be fixed:

1. Implementation of rounded corner images
2. Control of text length
3. Positioning of the three keys
## Pitfalls
1. The problem of incomplete filling caused by ItemTemplate (reason unknown, still needs to be learned)
2. API calls

## Experience with API Calls
In order to automatically obtain daily news from the internet, I found an API (with only 500 token calls available).
I defined a DailyPage class in the Model (actually DailyArticle is more appropriate), which includes the following properties:

1. Image
2. Title
3. Content
4. Flow (should be called url instead)
5. Then, in the HomeView, a notification update is declared.

After that, a notification update is declared in the HomeView:
```csharp
        // Daily article notification update
        private ObservableCollection<DailyPage> dailyPages;
        public ObservableCollection<DailyPage> DailyPages
        {
            get { return dailyPages; }
            set { dailyPages = value; RaisePropertyChanged(); }
        }

```
To call the API, an asynchronous method is created:
```csharp
        async void CreateDailyPage(){}
```
In it, the following code is used to obtain API-JSON information:
```csharp
 using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(apiUrlString);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseContent = await responseMessage.Content.ReadAsStringAsync();
                    //Parse JSON
                    JObject jsonResponse = JObject.Parse(responseContent);
                    JArray articles = (JArray)jsonResponse["data"];
                }
            }

```
The CSharp library for parsing JSON is not included by default, so it needs to be imported through NuGet, named NewtonSoft.Json. This way, the above method body can be used.

Since my JSON is a collection of data, I need to split it with the following code:
```csharp
JArray articles = (JArray)jsonResponse["data"];
```
Then, I wrote the following method body:
```csharp
foreach (JToken article in articles)
{
    if (articles.Count > 0)
    {
        string content = (string)article["description"];
        if (content.Length > 500)
        {
            content = content.Substring(0, 500) + "...";
        }
        string image = (string)article["image"];
        if (image == null || image.Contains('%') || image.Contains('&') || image.Contains('$'))
        {
            continue;
        }
        if (content.Length < 50)
        {
            continue;
        }

        DailyPages.Add(new DailyPage
        {
            Image = (string)article["image"],
            Title = (string)article["title"],
            Content = content,
            Flow = (string)article["url"]
        });
        break;
    }
}

```
The reason for using a loop is to avoid the following situations:

1. Garbled images cannot be loaded
2. Images are encrypted
3. Text is too long
4. Text is too short
5. An instantiation method is added here, as shown below:


Please note this syntax:
```csharp
Image = (string)article["image"]
```
This is an important syntax for parsing JSON, which specializes the data labeled as "image" in JSON as a string and assigns it to the corresponding property.



# *4.5 Project Log*
1. Finished the UI of the homepage.
2. Replaced the window with rounded corners.
3. Encountered a bunch of issues related to the MetarialDesign style. Damn it!
![4.5](https://raw.githubusercontent.com/ShuFengYingt/SFY-Word-Book/master/READMEImage/4.5.png?token=GHSAT0AAAAAAB745OPHJUO4KPQ7QPGCSE3AZBRD42A)

# *4.4Project Log*
# **Implemented the following features:**

1. Users can navigate to corresponding pages by selecting menu items in the left side menu bar.
2. The left side menu bar is collapsed after navigation.
3. Users can use the "→" and "←" keys on the navigation bar to navigate forward and backward (implemented using navigation log).
![4.4](https://raw.githubusercontent.com/ShuFengYingt/SFY-Word-Book/master/READMEImage/4.4.png?token=GHSAT0AAAAAAB745OPGUVQKBOHOBWLU263EZBRD73A)
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
