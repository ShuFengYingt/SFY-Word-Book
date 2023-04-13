using Microsoft.TeamFoundation.Dashboards.WebApi;
using System;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using System.Windows;

namespace SFY_Word_Book.Extensions
{
    public class CSolves
    {
        //实现CSolve中的结构体
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 16)]

        public struct Sentence
        {
            private int _sentenceRank;
            private IntPtr _sentenceContent;//使用IntPtr接收char*
            private IntPtr _sentenceCN;

            /// <summary>
            /// 例句序号
            /// </summary>
            public int SentenceRank { get { return _sentenceRank; } }

            /// <summary>
            /// 例句内容
            /// </summary>
            public string SentenceContent { get { return Marshal.PtrToStringUTF8(_sentenceContent); } }

            /// <summary>
            /// 例句翻译
            /// </summary>
            public string SentenceCN { get { return Marshal.PtrToStringUTF8(_sentenceCN); } }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Translation
        {
            private int _transRank;
            private IntPtr _partOfSpeech;
            private IntPtr _transCN;

            /// <summary>
            /// 释义序号
            /// </summary>
            public int TransRank { get { return _transRank; } }
            /// <summary>
            /// 释义内容
            /// </summary>
            public string TransCN { get { return Marshal.PtrToStringUTF8(_transCN); } }

            /// <summary>
            /// 词性
            /// </summary>
            public string PartOfSpeech { get { return Marshal.PtrToStringUTF8(_partOfSpeech); } }


        };


        [StructLayout(LayoutKind.Sequential)]
        public struct Word
        {


            private int _wordRank;
            private IntPtr _wordContent;
            private IntPtr _phoneticSymbol;
            private IntPtr _phoneSpeech;
            private int _combo;
            private bool _isLearned;
            private int _groupId;

            private int _numOfSentences;
            //使用MarshalAs属性指定数组大小
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            private Sentence[] _sentences;

            private int _numOfTranslations;
            //使用MarshalAs属性指定数组大小
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            private Translation[] _translations;

            private IntPtr _nextWord; //用InputPtr代替struct _Word*

            public GCHandle next;


            /// <summary>
            /// 单词序号
            /// </summary>
            public int WordRank { get { return _wordRank; } }
            /// <summary>
            /// 单词内容
            /// </summary>
            public string WordContent { get { return Marshal.PtrToStringUTF8(_wordContent); } }
            /// <summary>
            /// 音标
            /// </summary>
            public string PhoneticSymbol { get { return Marshal.PtrToStringUTF8(_phoneticSymbol); } }
            /// <summary>
            /// 连击次数
            /// </summary>
            public int Combo { get { return _combo; } }
            /// <summary>
            /// 是否学过
            /// </summary>
            public bool IsLearned { get { return _isLearned; } }
            /// <summary>
            /// 群组序号
            /// </summary>
            public int GroupId { get { return _groupId; } }
            /// <summary>
            /// 例句数
            /// </summary>
            public int NumOfSentences { get { return _numOfSentences; } }
            /// <summary>
            /// 例句
            /// </summary>
            public Sentence[] Sentences { get { return _sentences; } }
            /// <summary>
            /// 释义数
            /// </summary>
            public int NumOfTranslations { get { return _numOfTranslations; } }
            /// <summary>
            /// 释义
            /// </summary>
            public Translation[] Translations { get { return _translations; } }
            /// <summary>
            /// 下一个单词，结构体属性
            /// </summary>
            public Word NextWord { get { return Marshal.PtrToStructure<Word>(_nextWord); } }
            /// <summary>
            /// 下一个单词节点地址属性
            /// </summary>
            public IntPtr PNext { get { return _nextWord; } set {_nextWord = value; } }

        }

        [DllImport("CSolves.dll", EntryPoint = "_CreateSentenceInstance", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr _CreateSentenceInstance(int _sentenceRank, byte[] _sentenceContent, byte[] _sentenceCN);

        [DllImport("CSolves.dll", EntryPoint = "_CreateTranslationInstance")]
        private static extern IntPtr _CreateTranslationInstance(int _transRank, byte[] _partOfSpeech, byte[] _transCN);

        [DllImport("CSolves.dll", EntryPoint = "_CreateWordListHead")]
        private static extern IntPtr _CreateWordListHead();

        [DllImport("CSolves.dll", EntryPoint = "_CreateWordInstance")]
        private static extern IntPtr _CreateWordInstance(int _wordRank, byte[] _wordContent, byte[] _phoneticSymbol, byte[] _phoneSpeech,
            int _combo, bool _isLearned, int _groupId, int _numOfSentences, IntPtr _sentences, int _numOfTranslation, IntPtr _translations);

        [DllImport("CSolves.dll", EntryPoint = "_InsertWordToFront")]
        private static extern void _InsertWordToFront(IntPtr _wordListHead, IntPtr _newWord);

        [DllImport("CSolves.dll", EntryPoint = "_DeleteByAppoint")]
        private static extern void _DeleteByAppoint(IntPtr _wordListHead, int _wordRank);

        [DllImport("CSolves.dll", EntryPoint = "_SearchByWordContent")]
        private static extern int _SearchByWordContent(IntPtr _wordListHead, byte[] _wordContent);

        [DllImport("CSolves.dll", EntryPoint = "_SearchByWordRank")]
        private static extern IntPtr _SearchByWordRank(IntPtr _wordListHead, int _wordRank);

        [DllImport("CSolves.dll", EntryPoint = "_PrintfWordList")]
        private static extern void _PrintfWordList(IntPtr _listHeadWord);

        [DllImport("CSolves.dll", EntryPoint = "_CreateWordBooks")]
        public static extern void _CreateWordBooks();


        /// <summary>
        /// 创建单词实例
        /// </summary>
        /// <param name="wordRank"></param>
        /// <param name="wordContent"></param>
        /// <param name="phoneticSymbol"></param>
        /// <param name="phoneSpeech"></param>
        /// <param name="combo"></param>
        /// <param name="isLearned"></param>
        /// <param name="groupId"></param>
        /// <param name="numOfSentences"></param>
        /// <param name="sentences"></param>
        /// <param name="numOfTranslation"></param>
        /// <param name="translations"></param>
        /// <returns>Word Struct</returns>
        public static Word WordCreate(int wordRank, string wordContent, string phoneticSymbol, string phoneSpeech, int combo, bool isLearned, int groupId,
            int numOfSentences, Sentence[] sentences, int numOfTranslation, Translation[] translations)
        {
            byte[] _wordContent = Encoding.UTF8.GetBytes(wordContent);
            byte[] _phoneticSymbol = Encoding.UTF8.GetBytes(phoneticSymbol);
            byte[] _phoneSpeech = Encoding.UTF8.GetBytes(phoneSpeech);

            // 将托管数组转换为非托管指针
            IntPtr sentencePtr = Marshal.AllocHGlobal(sentences.Length * Marshal.SizeOf<Sentence>());
            for (int i = 0; i < sentences.Length; i++)
            {
                Marshal.StructureToPtr(sentences[i], IntPtr.Add(sentencePtr, i * Marshal.SizeOf<Sentence>()), false);
            }
            IntPtr translationPtr = Marshal.AllocHGlobal(translations.Length * Marshal.SizeOf<Translation>());
            for (int i = 0; i < translations.Length; i++)
            {
                Marshal.StructureToPtr(translations[i], IntPtr.Add(translationPtr, i * Marshal.SizeOf<Translation>()), false);
            }

            IntPtr wordPtr = _CreateWordInstance(wordRank, _wordContent, _phoneticSymbol, _phoneSpeech, combo, isLearned, groupId, numOfSentences, sentencePtr, numOfTranslation, translationPtr);
            Word word = Marshal.PtrToStructure<Word>(wordPtr);
            word.PNext = Marshal.AllocHGlobal(Marshal.SizeOf<Word>());
            return word;
        }


        /// <summary>
        /// 创建例句结构体实例
        /// </summary>
        /// <param name="sentenceRank"></param>
        /// <param name="sentenceContent"></param>
        /// <param name="sentenceCN"></param>
        /// <returns>Sentence Struct</returns>
        public static Sentence SentenceCreate(int sentenceRank, string sentenceContent, string sentenceCN)
        {
            byte[] _sentenceContentByte = Encoding.UTF8.GetBytes(sentenceContent);
            byte[] _sentenceCNByte = Encoding.UTF8.GetBytes(sentenceCN);
            IntPtr sentencePtr = _CreateSentenceInstance(sentenceRank, _sentenceContentByte, _sentenceCNByte);
            Sentence sentence = Marshal.PtrToStructure<Sentence>(sentencePtr);

            return sentence;
        }

        /// <summary>
        /// 创建释义实例
        /// </summary>
        /// <param name="transRank"></param>
        /// <param name="partOfSpeech"></param>
        /// <param name="transCN"></param>
        /// <returns>Translation struct</returns>
        public static Translation TranslationCreate(int transRank, string partOfSpeech, string transCN)
        {
            byte[] _partOfSpeechByte = Encoding.ASCII.GetBytes(partOfSpeech);
            byte[] _transCNByte = Encoding.ASCII.GetBytes(transCN);

            IntPtr translationPtr = _CreateSentenceInstance(transRank, _partOfSpeechByte, _transCNByte);
            return (Translation)Marshal.PtrToStructure(translationPtr, typeof(Translation));


        }

        /// <summary>
        /// 创建单词链表头节点
        /// </summary>
        /// <returns>单词链表头节点，为空</returns>
        public static Word WordListHeadCreate()
        {
            IntPtr wordListHeadPtr = _CreateWordListHead();
            Word wordHead = Marshal.PtrToStructure<Word>(wordListHeadPtr);
            wordHead.PNext = Marshal.AllocHGlobal(Marshal.SizeOf<Word>());

            return wordHead;
        }


        /// <summary>
        /// 插入单词结构体到链表头
        /// </summary>
        /// <param name="wordListHead"></param>
        /// <param name="newWord"></param>
        public static void InsertWordToFront(Word wordListHead, Word newWord)
        {
            IntPtr wordListHeadPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Word>());
            Marshal.StructureToPtr(wordListHead, wordListHeadPtr, false);

            IntPtr newWordPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Word>());
            Marshal.StructureToPtr(newWord, newWordPtr, false);

            _InsertWordToFront(wordListHeadPtr, newWordPtr);
            //Marshal.StructureToPtr(wordListHead.NextWord, newWord.PNext, false);
            //Marshal.StructureToPtr(newWord, wordListHead.PNext, false);
        }

        /// <summary>
        /// 根据序号删除单词
        /// </summary>
        /// <param name="wordListHead"></param>
        /// <param name="_wordRank"></param>
        public static void DeleteByAppoint(Word wordListHead, int _wordRank)
        {
            IntPtr wordListHeadPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Word>());
            Marshal.StructureToPtr(wordListHead, wordListHeadPtr, false);

            _DeleteByAppoint(wordListHeadPtr, _wordRank);
        }

        /// <summary>
        /// 根据单词内容查找单词
        /// </summary>
        /// <param name="wordListHead"></param>
        /// <param name="wordContent"></param>
        /// <returns>单词序号</returns>
        public static int SearchByWordContent(Word wordListHead, string wordContent)
        {
            IntPtr wordListHeadPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Word>());
            Marshal.StructureToPtr(wordListHead, wordListHeadPtr, false);

            byte[] wordContentByte = Encoding.UTF8.GetBytes(wordContent);
            int wordRank = _SearchByWordContent(wordListHeadPtr, wordContentByte);
            return wordRank;

        }

        /// <summary>
        /// 根据单词序号进行查找
        /// </summary>
        /// <param name="wordListHead"></param>
        /// <param name="wordRank"></param>
        /// <returns>单词内容</returns>
        public static string SearchByWordRank(Word wordListHead, int wordRank)
        {
            IntPtr wordListHeadPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Word>());
            Marshal.StructureToPtr(wordListHead, wordListHeadPtr, false);

            IntPtr wordContentPtr = _SearchByWordRank(wordListHeadPtr, wordRank);
            string wordContent = Marshal.PtrToStringUTF8(wordContentPtr);
            return wordContent;

        }

        public static void InsertToFront(Word wordListHead,Word newWord)
        {
            newWord.PNext = wordListHead.PNext;
            IntPtr newWordPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Word>());
            Marshal.StructureToPtr<Word>(newWord,newWordPtr, false);


        }


        public static void PrintfWordList(Word listHeadWord)
        {
            IntPtr listHeadWordPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Word>());
            Marshal.StructureToPtr(listHeadWord, listHeadWordPtr, false);

            _PrintfWordList(listHeadWordPtr);
        }

    }
}
