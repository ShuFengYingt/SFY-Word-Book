using System;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace SFY_Word_Book.Extensions
{
    public class CSolves
    {
        //实现CSolve中的结构体
        [StructLayout(LayoutKind.Sequential,CharSet = CharSet.Ansi, Size = 16)]
        
        public struct Sentence
        {
            private int _sentenceRank;
            private IntPtr _sentenceContent;//使用IntPtr接收char*
            private IntPtr _sentenceCN;

            /// <summary>
            /// 例句序号
            /// </summary>
            public int SentenceRank
            {
                get { return _sentenceRank; }
            }

            /// <summary>
            /// 例句内容
            /// </summary>
            public string SentenceContent
            {
                get { return Marshal.PtrToStringUTF8(_sentenceContent); }
            }

            /// <summary>
            /// 例句翻译
            /// </summary>
            public string SentenceCN
            {
                get { return Marshal.PtrToStringUTF8(_sentenceCN); }
            }
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
            public int TransRank
            {
                get { return _transRank; }
            }
            /// <summary>
            /// 释义内容
            /// </summary>
            public string TransCN
            {
                get { return Marshal.PtrToStringUTF8(_transCN); }
            }

            /// <summary>
            /// 词性
            /// </summary>
            public string PartOfSpeech
            {
                get { return Marshal.PtrToStringUTF8(_partOfSpeech); }

            }


        };


        [StructLayout(LayoutKind.Sequential)]
        public struct _Word
        {


            public int _wordRank;
            public String _wordContent;
            public String _phoneticSymbol;
            public String _phoneSpeech;
            public int _combo;
            public bool _isLearned;
            public int _groupId;

            public int _numOfSentences;
            //使用MarshalAs属性指定数组大小
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public Sentence[] _sentences;

            public int _numOfTranslations;
            //使用MarshalAs属性指定数组大小
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public Translation[] _translations;

            public IntPtr _nextWord; //用InputPtr代替struct _Word*
        }

        [DllImport("CSolves.dll", EntryPoint = "_CreateSentenceInstance", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr _CreateSentenceInstance(int _sentenceRank, byte[] _sentenceContent, byte[] _sentenceCN);
        
        /// <summary>
        /// 创建例句结构体实例
        /// </summary>
        /// <param name="sentenceRank"></param>
        /// <param name="sentenceContent"></param>
        /// <param name="sentenceCN"></param>
        /// <returns>Sentence Struct</returns>
        public static Sentence SentenceCreate(int sentenceRank,string sentenceContent,string sentenceCN)
        {
            byte[] _sentenceContentByte = Encoding.UTF8.GetBytes(sentenceContent);
            byte[] _sentenceCNByte = Encoding.UTF8.GetBytes(sentenceCN);
            IntPtr sentencePtr = _CreateSentenceInstance(sentenceRank, _sentenceContentByte, _sentenceCNByte);
            Sentence sentence = Marshal.PtrToStructure<Sentence>(sentencePtr);

            return sentence;
        }

        [DllImport("CSolves.dll", EntryPoint = "_CreateTranslationInstance")]
        private static extern IntPtr _CreateTranslationInstance(int _transRank, byte[] _partOfSpeech,
                                                            byte[] _transCN);

        /// <summary>
        /// 创建释义实例
        /// </summary>
        /// <param name="transRank"></param>
        /// <param name="partOfSpeech"></param>
        /// <param name="transCN"></param>
        /// <returns>Translation struct</returns>
        public static Translation TranslationCreate(int transRank,string partOfSpeech,string transCN)
        {
            byte[] _partOfSpeechByte = Encoding.UTF8.GetBytes(partOfSpeech);
            byte[] _transCNByte = Encoding.UTF8.GetBytes(transCN);

            IntPtr translationPtr = _CreateSentenceInstance(transRank, _partOfSpeechByte, _transCNByte);
            return (Translation)Marshal.PtrToStructure(translationPtr, typeof(Translation));


        }



        [DllImport("CSolves.dll", EntryPoint = "_CreateWordListHead")]
        private static extern IntPtr _CreateWordListHead();



        [DllImport("CSolves.dll", EntryPoint = "_CreateWordInstance")]

        public static extern IntPtr _CreateWordInstance(int _wordRank,
            [MarshalAs(UnmanagedType.LPStr)] String _wordContent,
            [MarshalAs(UnmanagedType.LPStr)] String _phoneticSymbol,
            [MarshalAs(UnmanagedType.LPStr)] String _phoneSpeech,
            int _combo, bool _isLearned, int _groupId, int _numOfSentences, IntPtr _sentences, int _numOfTranslation, IntPtr _translations);

        [DllImport("CSolves.dll", EntryPoint = "_InsertWordToFront")]
        public static extern IntPtr _InsertWordToFront(IntPtr _listHeadWord, IntPtr _newWord);

        [DllImport("CSolves.dll", EntryPoint = "_DeleteByAppoint")]
        public static extern IntPtr _InsertWordToFront(IntPtr _listHeadWord, int _wordRank);

        [DllImport("CSolves.dll", EntryPoint = "_SearchByWordContent")]
        public static extern IntPtr _SearchByWordContent(IntPtr _listHeadWord, [MarshalAs(UnmanagedType.LPStr)] String _wordRank);

        [DllImport("CSolves.dll", EntryPoint = "_PrintfWordList")]
        public static extern void _PrintfWordList(IntPtr _headWord);


        [DllImport("CSolves.dll", EntryPoint = "_CreateWordBooks")]
        public static extern void _CreateWordBooks();

        [DllImport("CSolves.dll", EntryPoint = "_add")]
        public static extern int _add(int a, int b);


    }
}
