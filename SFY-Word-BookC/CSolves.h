/// <summary>
/// 创建链表头节点，Null
/// </summary>
extern _declspec(dllexport) struct _Word* _CreateWordListHead();

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
extern _declspec(dllexport) struct _Word* _CreateWordInstance(int _wordRank, char* _wordContent,
	char* _phoneticSymbol, char* _phoneSpeech,
	int _combo, bool _isLearned, int _groupId);

/// <summary>
/// 在表头插入新节点,用于单词书初始化以及复习书增加单词
/// </summary>
/// <param name="头单词节点"></param>
/// <param name="插入单词节点"></param>
extern _declspec(dllexport) void _InsertWordToFront(struct _Word* _listHeadWord, struct _Word* _newWord);

/// <summary>
/// 指定位置删除，用于单词转移
/// </summary>
/// <param name="头单词节点（指定列表）"></param>
/// <param name="单词序号"></param>
extern _declspec(dllexport) void _DeleteByAppoint(struct _Word* _listHeadWord, int _wordRank);

/// <summary>
/// 查找节点，很多地方要用，不过暂时无法实现模糊查找,wpf可能有自带的模糊查找
/// </summary>
/// <param name="头单词节点（指定列表）"></param>
/// <param name="查找内容"></param>
/// <returns>单词序号</returns>
extern _declspec(dllexport) int _SearchByWordContent(struct _Word* _listHeadWord, char* _wordContent);

/// <summary>
/// 创建单词书
/// </summary>
extern _declspec(dllexport) void _CreateWordBooks();
