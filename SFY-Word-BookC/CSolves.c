#include<stdio.h>
#include<string.h>
#include<assert.h>
#include<stdlib.h>
#include<stdbool.h>
#include"CSolves.h"
#include<wchar.h>

int _add(int a, int b)
{
    return a + b;
}


/// <summary>
/// 例句类
/// </summary>
struct _Sentence
{
    /// <summary>
    /// 例句序号
    /// </summary>
    int _sentenceRank;

    /// <summary>
    /// 例句内容
    /// </summary>
    char* _sentenceContent;

    /// <summary>
    /// 例句释义
    /// </summary>
    char* _senteceCN;


};
/// <summary>
/// 创建例句实例
/// </summary>
/// <param name="例句内容"></param>
/// <param name="例句释义"></param>
/// <returns>例句实例</returns>
struct _Sentence* _CreateSentenceInstance(int _sentenceRank,const char* _sentenceContent,const char* _senteceCN)
{
    struct _Sentence* _newSentece = (struct _Sentence*)malloc(sizeof(struct _Sentence));
    assert(_newSentece);

    _newSentece->_sentenceRank = _sentenceRank;
    _newSentece->_senteceCN = _senteceCN;
    _newSentece->_sentenceContent = _sentenceContent;

    return _newSentece;
}





/// <summary>
/// 释义结构体
/// </summary>
struct _Translation
{
    /// <summary>
    /// 序号
    /// </summary>
    int _transRank;

    /// <summary>
    /// 词性
    /// </summary>
    char* _partOfSpeech;

    /// <summary>
    /// 释义文
    /// </summary>
    char* _transCN;
};

struct _Translation* _CreateTranslationInstance(int _transRank,  char* _partOfSpeech,  char* _transCN)
{
    struct _Translation* _newTranslation = (struct _Translation*)malloc(sizeof(struct _Translation));
    assert(_newTranslation);

    _newTranslation->_transRank = _transRank;
    _newTranslation->_transCN = _transCN;
    _newTranslation->_partOfSpeech = _partOfSpeech;
    return _newTranslation;
}


/// <summary>
/// 单词结构体
/// </summary>
struct _Word
{
    /// <summary>
    /// 单词序号
    /// </summary>
    int _wordRank;

    /// <summary>
    /// 单词内容
    /// </summary>
    char* _wordContent;

    /// <summary>
    /// 发音音标
    /// </summary>
    char* _phoneticSymbol;

    /// <summary>
    /// 对接发音API参数请求，url，解析端放在C#里？先这样
    /// </summary>
    char* _phoneSpeech;

    /// <summary>
    /// 连对次数
    /// </summary>
    int _combo;

    /// <summary>
    /// 是否学过
    /// </summary>
    bool _isLearned;

    /// <summary>
    /// 所在群组
    /// </summary>
    int _groupId;

    //例句组
    int _numOfSenteces;
    struct _Sentence _sentences[11];

    //释义组
    int _numOfTranslations;
    struct _Translation _translations[11];


    struct _Word* _nextWord;
     
};



/// <summary>
/// 创建链表头节点，Null
/// </summary>
struct _Word* _CreateWordListHead()
{
    //内存申请
    struct _Word* _headWord = (struct _Word*)malloc(sizeof(struct _Word));

    //断言处理
    assert(_headWord);

    //表头初始化
    _headWord->_nextWord = NULL;
    return _headWord;
};

/// <summary>
/// 创建单词实例 
/// </summary>
/// <param name="单词序号"></param>
/// <param name="单词内容"></param>
/// <param name="音标"></param>
/// <param name="发音请求API-Url"></param>
/// <param name="连对次数"></param>
/// <param name="是否学习过"></param>
/// <param name="所在群组"></param>
/// <returns>实例化的单词</returns>
struct _Word* _CreateWordInstance(int _wordRank,char* _wordContent,
                                    char* _phoneticSymbol, char* _phoneSpeech, 
                                    int _combo,bool _isLearned,int _groupId,
                                    int numOfSentences,struct _Sentence* _sentences,
                                    int _numOfTranslations,struct _Translations* _translations)

{
    //申请内存
    struct _Word* _newWord = (struct _Word*)malloc(sizeof(struct _Word));

    //断言
    assert(_newWord);

    //构造,C语言太麻烦了
    _newWord->_wordRank = _wordRank;
    _newWord->_wordContent = _wordContent;
    _newWord->_phoneticSymbol = _phoneticSymbol;
    _newWord->_phoneSpeech = _phoneSpeech;
    _newWord->_combo = _combo;
    _newWord->_isLearned = _isLearned;
    _newWord->_groupId = _groupId;
    _newWord->_numOfSenteces = numOfSentences;
    _newWord->_nextWord = NULL;
    _newWord->_numOfTranslations = _numOfTranslations;
    
    //使用memcpy复制数组
    memcpy(_newWord->_sentences, _sentences, sizeof(struct _Sentence) * numOfSentences);
    memcpy(_newWord->_translations, _translations, sizeof(struct _Translation) * _numOfTranslations);

    return _newWord;
}



/// <summary>
/// 在表头插入新节点,用于单词书初始化以及复习书增加单词
/// </summary>
/// <param name="头单词节点"></param>
/// <param name="插入单词节点"></param>
void _InsertWordToFront(struct _Word* _listHeadWord,struct _Word* _newWord)
{
    //常规链表插入操作
    _newWord->_nextWord = _listHeadWord->_nextWord;
    _listHeadWord->_nextWord = _newWord;
}

/// <summary>
/// 指定位置删除，用于单词转移
/// </summary>
/// <param name="头单词节点（指定列表）"></param>
/// <param name="单词序号"></param>
void _DeleteByAppoint(struct _Word* _listHeadWord,int _wordRank)
{
    //常规删除操作
    struct _Word* _preWord = _listHeadWord;
    struct _Word* _posword = _listHeadWord->_nextWord;
    while (_posword != NULL && _posword->_wordRank != _wordRank)
    {//记得判空
        _preWord = _posword;
        _posword = _posword->_nextWord;
    }
    if (_posword != NULL)
    {
        _preWord->_nextWord = _posword->_nextWord;
        free(_posword);
    }
}

/// <summary>
/// 查找节点，很多地方要用，不过暂时无法实现模糊查找,wpf可能有自带的模糊查找
/// </summary>
/// <param name="头单词节点（指定列表）"></param>
/// <param name="查找内容"></param>
/// <returns>单词序号</returns>
int _SearchByWordContent(struct _Word* _listHeadWord,char* _wordContent)
{
    struct _Word* pMove = _listHeadWord->_nextWord;
    while (pMove != NULL && strcmp(pMove->_wordContent, _wordContent) != 0)
    {
        pMove = pMove->_nextWord;
    }
    if (pMove != NULL)
    {
        return pMove->_wordRank;

    }
    else
    {
        return -1;
    }

}

char* _SearchByWordRank(struct _Word* _listHeadWord, int _wordRank)
{
    struct _Word* pMove = _listHeadWord->_nextWord;
    while (pMove != NULL && pMove->_wordRank != _wordRank)
    {
        pMove = pMove->_nextWord;
    }
    if (pMove != NULL)
    {
        return pMove->_wordContent;
    }
    else
    {
        return "-1";
    }
}



/// <summary>
/// 打印测试
/// </summary>
/// <param name="headWord"></param>
void _PrintfWordList(struct _Word* headWord)
{
    struct _Word* pMove = headWord->_nextWord; 
    while (pMove != NULL)
    {
        printf("%s\n", pMove->_wordContent);
        pMove = pMove->_nextWord; 
    }
}

/// <summary>
/// 创建单词书
/// </summary>
void _CreateWordBooks()
{
    //在学单词表
    struct _Word* _learningWordBook = _CreateWordListHead();

    //待复习单词表
    struct _Word* _ReviewWordBook = _CreateWordListHead();

    //已学习单词表
    struct _Word* _HasLearnedBook = _CreateWordListHead();

}

signed main()
{


    return 0;

}
