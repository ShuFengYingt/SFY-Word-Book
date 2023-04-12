extern _declspec(dllexport) int _add(int a, int b);

extern _declspec(dllexport) struct _Sentence* _CreateSentenceInstance(int _sentenceRank,  char* _sentenceContent,  char* _senteceCN);

extern _declspec(dllexport) struct _Translation* _CreateTranslationInstance(int _transRank, const char* _partOfSpeech, const char* _transCN);

extern _declspec(dllexport) struct _Word* _CreateWordListHead();

extern _declspec(dllexport) struct _Word* _CreateWordInstance(int _wordRank, char* _wordContent,
	char* _phoneticSymbol, char* _phoneSpeech,
	int _combo, bool _isLearned, int _groupId,
	int numOfSentences, struct _Sentence* _sentences,
	int _numOfTranslations, struct _Translations* _translations);

extern _declspec(dllexport) void _InsertWordToFront(struct _Word* _listHeadWord, struct _Word* _newWord);

extern _declspec(dllexport) void _DeleteByAppoint(struct _Word* _listHeadWord, int _wordRank);

extern _declspec(dllexport) int _SearchByWordContent(struct _Word* _listHeadWord, char* _wordContent);

extern _declspec(dllexport) void _PrintfWordList(struct _Word* _headWord);

extern _declspec(dllexport) void _CreateWordBooks();
