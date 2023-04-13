using Microsoft.VisualStudio.Services.OAuthWhitelist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Extensions
{
    public class TempExtension
    {
        public class Sentence
        {
            public Sentence(int sentenceRank, string sentenceContent, string sentenceCN)
            {
                SentenceRank = sentenceRank;
                SentenceContent = sentenceContent;
                SentenceCN = sentenceCN;
            }

            /// <summary>
            /// 例句序号
            /// </summary>
            public int SentenceRank { get; set; }
            /// <summary>
            /// 例句内容
            /// </summary>
            public string SentenceContent { get; set; }
            /// <summary>
            /// 例句翻译
            /// </summary>
            public string SentenceCN { get; set; }
        }
        public class Translation
        {
            public Translation(int transRank, string transCN, string partOfSpeech)
            {
                TransRank = transRank;
                TransCN = transCN;
                PartOfSpeech = partOfSpeech;
            }

            /// <summary>
            /// 释义序号
            /// </summary>
            public int TransRank { get; set; }
            /// <summary>
            /// 释义内容
            /// </summary>
            public string TransCN { get; set; }

            /// <summary>
            /// 词性
            /// </summary>
            public string PartOfSpeech { get; set; }
        }
        public class Word
        {
            public Word(int wordRank, string wordContent, string phoneticSymbol,
                int combo, bool isLearned,
                int groupId, int numOfSentences,
                List<Sentence> sentences, int numOfTranslations,
                List<Translation> translations, Word nextWord)
            {
                WordRank = wordRank;
                WordContent = wordContent;
                PhoneticSymbol = phoneticSymbol;
                Combo = combo;
                IsLearned = isLearned;
                GroupId = groupId;
                NumOfSentences = numOfSentences;
                Sentences = sentences;
                NumOfTranslations = numOfTranslations;
                Translations = translations;
                NextWord = nextWord;
            }

            /// <summary>
            /// 单词序号
            /// </summary>
            public int WordRank { get; set; }
            /// <summary>
            /// 单词内容
            /// </summary>
            public string WordContent { get; set; }
            /// <summary>
            /// 音标
            /// </summary>
            public string PhoneticSymbol { get; set; }
            /// <summary>
            /// 连击次数
            /// </summary>
            public int Combo { get; set; }
            /// <summary>
            /// 是否学过
            /// </summary>
            public bool IsLearned { get; set; }
            /// <summary>
            /// 群组序号
            /// </summary>
            public int GroupId { get; set; }
            /// <summary>
            /// 例句数
            /// </summary>
            public int NumOfSentences { get; set; }
            /// <summary>
            /// 例句
            /// </summary>
            public List<Sentence> Sentences { get; set; }
            /// <summary>
            /// 释义数
            /// </summary>
            public int NumOfTranslations { get; set; }
            /// <summary>
            /// 释义
            /// </summary>
            public List<Translation> Translations { get; set; }
            /// <summary>
            /// 下一个单词，
            /// </summary>
            public Word NextWord { get; set; }

        }


        public static Sentence SentenceCreate(int sentenceRank, string sentenceContent, string sentenceCN)
        {
            return new Sentence(sentenceRank, sentenceContent, sentenceCN);
        }
        public static Translation TranslationCreate(int transRank, string transCN, string partOfSpeech)
        {
            return new Translation(transRank, transCN, partOfSpeech);
        }
        public static Word WordCreate(int wordRank, string wordContent, string phoneticSymbol,
                int combo, bool isLearned,
                int groupId, int numOfSentences,
                List<Sentence> sentences, int numOfTranslations,
                List<Translation> translations, Word nextWord)
        {
            return new Word(wordRank, wordContent, phoneticSymbol, combo, isLearned, groupId, numOfSentences, sentences, numOfTranslations, translations, nextWord);
        }
        public static Word WordListHeadCreate()
        {
            return new Word(-1, "", "", -1, false, -1, -1, null, -1, null, null);
        }
        public static void InsertWordToFront(Word wordListHead, Word newWord)
        {
            newWord.NextWord = wordListHead.NextWord;
            wordListHead.NextWord = newWord;
        }
        public static void DeleteByAppoint(Word wordListHead, int wordRank)
        {
            Word pWord = wordListHead.NextWord;
            while (pWord != null && pWord.WordRank != wordRank)
            {
                pWord = pWord.NextWord;
            }

            if (pWord != null)
            {
                pWord.NextWord = pWord.NextWord.NextWord;

            }
        }
        public static int SearchByWordContent(Word wordListHead, string wordContent)
        {
            Word pWord = wordListHead.NextWord;
            while (pWord != null && pWord.WordContent.Equals(wordContent, StringComparison.OrdinalIgnoreCase))
            {
                pWord = pWord.NextWord;
            }
            if (pWord != null)
            {
                return pWord.WordRank;
            }
            else
            {
                return -1;
            }
        }
        public static string SearchByWordRank(Word wordListHead, int wordRank)
        {
            Word pWord = wordListHead.NextWord;
            while (pWord != null && pWord.WordRank != wordRank)
            {
                pWord = pWord.NextWord;
            }
            if (pWord != null)
            {
                return pWord.WordContent;
            }
            else
            {
                return "null";
            }
        }



    }
}
