using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Extensions
{
    public class CSolves
    {
        //实现CSolve中的结构体
        [StructLayout(LayoutKind.Sequential)]
        public struct _Sentence
        {
            public int _sentenceRank;

            public String _sentenceContent;//用String类代替char* 引用时使用[MarshalAs(UnmanagedType.LPStr)]指定字符串编码


            public String _senteceCN;        


        }

        [StructLayout(LayoutKind.Sequential)]
        public struct _Translation
        {
            public int _transRank;

            public String _partOfSpeech;        

            public String _transCN;       

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

            //使用MarshalAs属性指定数组大小
            [MarshalAs(UnmanagedType.ByValArray, SizeConst =11)]
            public _Sentence[] _sentences;

            //使用MarshalAs属性指定数组大小
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public _Translation[] _translations;

            public IntPtr _nextWord; //用InputPtr代替struct _Word*
        }

        [DllImport("CSolves.dll", EntryPoint = "_CreateWordListHead")]
        public static extern IntPtr _CreateWordListHead();

        [DllImport("CSolves.dll", EntryPoint = "_CreateWordInstance")]
        
        public static extern IntPtr _CreateWordInstance(int _wordRank, 
            [MarshalAs(UnmanagedType.LPStr)] String _wordContent, 
            [MarshalAs(UnmanagedType.LPStr)] String _phoneticSymbol, 
            [MarshalAs(UnmanagedType.LPStr)] String _phoneSpeech, 
            int _combo, bool _isLearned, int _groupId);

        [DllImport("CSolves.dll", EntryPoint = "_InsertWordToFront")]
        public static extern IntPtr _InsertWordToFront(IntPtr _listHeadWord, IntPtr _newWord);

        [DllImport("CSolves.dll", EntryPoint = "_DeleteByAppoint")]
        public static extern IntPtr _InsertWordToFront(IntPtr _listHeadWord, int _wordRank);

        [DllImport("CSolves.dll", EntryPoint = "_SearchByWordContent")]
        public static extern IntPtr _SearchByWordContent(IntPtr _listHeadWord, [MarshalAs(UnmanagedType.LPStr)] String _wordRank);

        [DllImport("CSloves.dll", EntryPoint = "_CreateWordBooks")]
        public static extern void _CreateWordBooks();


    }
}
