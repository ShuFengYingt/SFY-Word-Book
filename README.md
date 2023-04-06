[English log translate by ChatGPT](https://github.com/ShuFengYingt/SFY-Word-Book/blob/master/README_en.md)

# 4.6项目日志
1. 大部分实现了每日文章的功能，调用的是API，显示图片，标题，内容信息（还有很多bug等待明天和后天去修复）

等待修复的内容：
1. 圆角图片的实现
2. 文本长度的控制
3. 三大键位置

## 坑点
1. ItemTmplate造成的填充不满问题（原因未知，还要学）
2. API调用

## API调用心得
为了实现自动从网上获取每日资讯，我找到了一个API（只有500次token）。
我定义了一个DailyPage类在Model中（实际上叫DailyArticle更合适），包括以下几个属性（Property）
1. Image 图片
2. Title 标题
3. Content 内容
4. Flow 源地址（应该叫url更合适）

而后在HomeView当中，进行通知更新声明
```csharp
        //每日文章通知更新
        private ObservableCollection<DailyPage> dailyPages;
        public ObservableCollection<DailyPage> DailyPages
        {
            get { return dailyPages; }
            set { dailyPages = value; RaisePropertyChanged(); }
        }
```
为了调用API，建立了异步方法：
```csharp
        async void CreateDailyPage(){}

```
在其中，用如下代码体获取API-JSON信息
```csharp
 using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(apiUrlString);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseContent = await responseMessage.Content.ReadAsStringAsync();
                    //解析Json
                    JObject jsonResponse = JObject.Parse(responseContent);
                    JArray articles = (JArray)jsonResponse["data"];
                }
            }
```
解析Json的类库CSharp并不自备，需要用Nuget引入，名为NewtonSoft.Json。这样就能用上面的方法体了。

由于我的Json是个数据集合，所以需要
```csharp
JArray articles = (JArray)jsonResponse["data"];
```
对data进行拆分。而后我写下了以下方法体
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
之所以要用到一个循环，是为了避免出现以下情况：
1. 图片乱码无法加载
2. 图片被加密过
3. 文字太多
4. 文字太少

这里面添加了实例化方法，即如下所示：
```csharp
DailyPages.Add(new DailyPage
                            {
                                Image = (string)article["image"],
                                Title = (string)article["title"],
                                Content = content,
                                Flow = (string)article["url"]
                                
                            });     
```
请注意这个语法
```csharp
Image = (string)article["image"]
```
这是很重要的Json解析语法，即将Json中的"image"标注数据专化为字符串赋值给属性当中。



# 4.5 项目日志
1. 完成了首页的UI
2. 将窗口替换为圆角
3. 遇到了有关MetarialDesign的Style的一大堆坑，艹！！！！




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
