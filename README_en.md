[中文日志](https://github.com/ShuFengYingt/SFY-Word-Book/blob/master/README.md)：

# 4.14 Project Log

I temporarily gave up the implementation of the C language backend and replaced it with C# for the time being. I will try again in the future (

The following tasks have been completed:

1. Imported the CET6.json vocabulary.
2. Implemented some UI effects for the word learning interface.

![4.14](/READMEImage/4.14.png)

## How to hide a control when a button is pressed

When designing the frontend interface, it is necessary to hide two buttons ("I know" and "I don't know") after they are pressed. However, in the MVVM pattern, additional processing is required. How to do it?

Here, I will not go into the basics of button binding command Command. Instead, I will focus on the Visibility property.

This property has three enumeration values:

1. Visible
2. Hidden
3. Collapsed

The characteristics of these three values are not discussed here. The key point is to perform binding operations.

For a control, similar to the following:

```xaml	
<YourControl Visibility = "Visible"/>
```

To implement binding, we modify it as follows:

```xaml
<YourControl Visibility = "{Binding IsShow , Converter = {StaticResource BooleanToVisibilityConverter}}"/>
```

As can be seen, we bind the IsHidden property under a ViewModel (business separation under the Prism framework), and also use a boolean-to-visibility converter.

For the IsHidden property, we need to define it in the ViewModel as follows:

```c#
private bool isShow;
public bool IsShow
{
    get{return isShow;}
    set{SetProperty(ref isShow, value);}
    
}//Remember to initialize it in the constructor
```

In addition to this, in the function called by the Command, we need to write:

```c#
public void Show()
{
    //Skip other code
    
    IsShow = false;
    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsShow)));
}
```

This way, when calling the Command (the called function), we can change the Visibility of a control that binds the IsShow property.

# 4.11 Project Log

Completed the C language backend part, including

1. Word structure
2. Sentence structure
3. Definition structure
4. Linked list creation function
5. Add, delete, modify and query

On the CS side

1. Added a public class CSolve under Extension,

2. Referenced the C language encapsulated dll

3. Performed local calls (that is, not completed yet, and not tested)



## C language encapsulated dynamic link library

### Key points

First complete the CSolve.c file, then create a CSolve.h (same name) file in the header file, and reference it in CSolve.c as follows

```C
#include"CSolve.h"
```

Then reference the function in CSolve.h and add modifiers

```C
extern _declspec(dllexport) struct _Word* _CreateWordListHead();
```

The modifier is

```C
extern _declspec(dllexport)
```

This way you can complete the dll export.

Note that variables should be prefixed or suffixed with underscores to distinguish them from C#, so that you can clearly see which ones are internal and which ones are external.

### Pitfalls

1. Turn off precompiled headers in VS
2. Change the project type to dll in the project properties

After completing the above steps, rebuild the solution and you will get the dynamic link library source file.

## Csharp reference C language dynamic link library

### Basic operations

1. Put the dynamic link library file into the runtime environment of the CS project, which is /bin/Debug/.net7-windows

2. Create a dedicated management class, I created a CSolve class under Extension, parallel to PrismManager.cs

3. Import with DLLImport tag, example as follows:

   ```c#
   		[DllImport("CSloves.dll", EntryPoint = "_CreateWordBooks")]
            public static extern void _CreateWordBooks();

Remember to use EntryPoint to indicate the entry function

### Struct import points

The basic operation mode can only cope with the simplest processing functions, but many functions in the dynamic link library have pointer variables, structure nesting, etc., which require more knowledge for special processing.

#### 1.Re-declare structure

For the structure defined in the dynamic link library, we need to re-declare it again in C#, otherwise we cannot call the corresponding function. But because C language and C++ structure declarations are sequential (from top to bottom, if a structure named a_struct is placed at the end, then the previous structures and functions cannot call this structure named a_struct), so in C#, we also need to declare that structures are sequentially placed, as shown in the following example

```c#
    using System.Runtime.InteropServices;

	   [StructLayout(LayoutKind.Sequential)]
        public struct _Word
        {
            
        }

```

Here is my attempt to translate the markdown into English, keeping the markdown format.

#### 2. char* conversion (the most important)

In C language, in order to implement string functionality more conveniently, I used char* pointer to define strings in the structure, similar to the following

```C
            struct Sentence
            {
                 /// <summary>
    			/// Word content
    			/// </summary>
    			char* _sentenceContent;
            };
```

Although C# can use pointer variables under the unsafe modifier, who still uses pointers when using C# XD.

If you want to reference the char* of the dynamic link library in C# without using pointers, you need to use some new stuff.

For the **_sentenceContent** in the above example, we can use **IntPtr** to define it in the equivalent structure in C#

```c#
            public IntPtr _sentenceContent; 
```

This alone is not enough. When calling functions or generating methods, you need to send and receive char*. I have tried many ways to receive char* in C#, but only one is normal. All others will output garbled characters. Next, I will introduce how to receive them.

Suppose we have a function like (just an example, the actual code is not like this)

```c
struct _Sentence* _CreateSentenceInstance(char* _sentenceContent){}
```

Then we can do some encapsulation in the previous structure

```c#
struct Sentence {
    /// <summary>
    /// Content of the sentence
    /// </summary>
    private IntPtr _sentenceContent;
    public string SentenceContent {
        get {
            return Marshal.PtrToStringUTF8(_sentenceContent);
        }
    }
};

```

In this process, we have hidden the data member `_sentenceContent` and replaced it with a string type property `SentenceContent`, and used `Marshal.PtrToStringUTF8()` to convert it during return. This step is to convert the `IntPtr` type `_sentenceContent` to a UTF-8 encoded string.

Why do we need to do this? Let's look at the encapsulation of the function. In order to reference the `_CreateSentenceInstance` function that was given in the previous section, we should import it into C# as follows:

```c#
[DllImport("CSolves.dll", EntryPoint = "_CreateSentenceInstance", CallingConvention = CallingConvention.Cdecl)]
private static extern IntPtr _CreateSentenceInstance(byte[] _sentenceContent);

```

It is not difficult to see that for `char*` in C, we use a `byte[]` array in C# for receiving. This is because the essence of `char*` is a string of continuous addresses, so we can convert the string we need to pass in into a binary `byte` array, which specifies its address, and then pass it into the dynamic link library (DLL). (I guess this is the way to understand it.)

Furthermore, we can further encapsulate `_CreateSentenceInstance` to return a `struct`.

```c#
/// <summary>
/// Create instance of Sentence struct
/// </summary>
/// <param name="sentenceContent"></param>
/// <returns>Sentence Struct</returns>
public static Sentence SentenceCreate(string sentenceContent)
{
    byte[] _sentenceContentByte = Encoding.UTF8.GetBytes(sentenceContent);
    IntPtr sentencePtr = _CreateSentenceInstance(_sentenceContentByte);
    Sentence sentence = Marshal.PtrToStructure<Sentence>(sentencePtr);
    return sentence;
}

```

Here are three steps:

1. Convert the string passed in by the C# side into a binary array using the `Encoding.UTF8.GetBytes()` method.
2. Call the `_CreateSentenceInstance` method and pass in the `byte[]`. The dynamic link library automatically specializes the connection between `byte` and `char*`.
3. Use the `Marshal.PtrToStructure` method to complete the conversion of the pointer to a struct.

This encapsulation method is more suitable for calling in C#.


# 4.9 Project log

![4.90](/READMEImage/4.90.png)
1. Made a little UI interface for reciting words, but it needs to be improved
2. Configure the WebAPI
3. Created a word database, using SQLite, but I don’t quite understand how to add, delete, check and modify, and how to connect to C language.

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
