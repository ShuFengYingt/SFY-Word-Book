using Microsoft.VisualStudio.Services.CircuitBreaker;
using Prism.Mvvm;
using SFY_Word_Book.Common.Commands;
using SFY_Word_Book.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Command = SFY_Word_Book.Common.Commands.Command;

namespace SFY_Word_Book.ViewModels
{
    public class WordStoryViewModel:BindableBase
    {
        public WordStoryViewModel()
        {
            WordStories = new ObservableCollection<WordStory>();
            WordStory = new ObservableCollection<WordStory>();
            ToNextStoryCommand = new Command(ToNextStory);
            ToLastStoryCommand = new Command(ToLastStory);

            StoryIndex = 0;

            InitiateWordStory();
        }

        private ObservableCollection<WordStory> wordStories = new ObservableCollection<WordStory>();
        /// <summary>
        /// 显示内容集合
        /// </summary>
        public ObservableCollection<WordStory> WordStories
        {
            get { return wordStories; }
            set { wordStories = value; RaisePropertyChanged(); }
        }

        
        private ObservableCollection<WordStory> wordStory;
        /// <summary>
        /// 显示的内容
        /// </summary>
        public ObservableCollection<WordStory> WordStory
        {
            get { return wordStory; }
            set { wordStory = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 界面显示内容的序号
        /// </summary>
        public int StoryIndex { get;set; }

        public void InitiateWordStory()
        {
            WordStory tempWordStory = new WordStory();
            tempWordStory.ForUser = "FOR " + "SHUFENGYINGT";
            tempWordStory.Title = "The Composer's Trade";
            tempWordStory.Date = "2023/5/9";
            tempWordStory.Story = "Once a slave to his own mind, the composer found solace in music. His passion turned into a lucrative trade, and he soon struck a deal with a renowned orchestra. But the consequent fame brought a hike in expectations, leading to relentless action. Despite the resultant exhaustion, he persevered, knowing that to abide by his craft was the only way to survive in a world full of grunts.";
            tempWordStory.ImageUrl = "/Images/Composers trade.jpg";
            tempWordStory.IsCurrentStory = true ;
            tempWordStory.StoryIndex = "01";

            WordStory tempWordStory1 = new WordStory();
            tempWordStory1.ForUser = "FOR " + "SHUFENGYINGT";
            tempWordStory1.Title = "The Block Party";
            tempWordStory1.Date = "2023/5/10";
            tempWordStory1.Story = "The initial plan was just a simple block party. But as the composition of the guest list grew, it became something more. Each person brought their own unique value to the gathering. At first, they stayed within their own circles, but soon found themselves mingling and laughing with those outside their usual groups. The outermost walls of their comfort zones began to crumble. It was an optimum display of unity and community. The block party was no longer just a physical event, but a symbol of breaking down barriers and building up relationships.";
            tempWordStory1.ImageUrl = "/Images/Story_2.jpg";
            tempWordStory1.IsCurrentStory = false ;
            tempWordStory1.StoryIndex = "02";

            //WordStory.Add(WordStories[StoryIndex]);
            WordStory.Add(tempWordStory);
            WordStories.Add(tempWordStory);
            WordStories.Add(tempWordStory1);
        }

        public Command ToNextStoryCommand {  get; set; }
        public void ToNextStory()
        {
            if (StoryIndex + 1 < wordStories.Count)
            {
                StoryIndex++;
                WordStory.RemoveAt(0);
                WordStory.Add(WordStories[StoryIndex]);

            }
        }

        public Command ToLastStoryCommand {  get; set; }
        public void ToLastStory()
        {
            if (StoryIndex - 1 >= 0)
            {
                StoryIndex--;
                WordStory.RemoveAt(0);
                WordStory.Add(WordStories[StoryIndex]);

            }
        }

    }
}
