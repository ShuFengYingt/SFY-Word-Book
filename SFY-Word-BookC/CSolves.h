/// <summary>
/// ��������ͷ�ڵ㣬Null
/// </summary>
extern _declspec(dllexport) struct _Word* _CreateWordListHead();

/// <summary>
/// ��������ʵ�� 
/// </summary>
/// <param name="�������"></param>
/// <param name="��������"></param>
/// <param name="����"></param>
/// <param name="��������API-Url"></param>
/// <param name="���Դ���"></param>
/// <param name="�Ƿ�ѧϰ��"></param>
/// <param name="����Ⱥ��"></param>
/// <returns>ʵ�����ĵ���</returns>
extern _declspec(dllexport) struct _Word* _CreateWordInstance(int _wordRank, char* _wordContent,
	char* _phoneticSymbol, char* _phoneSpeech,
	int _combo, bool _isLearned, int _groupId);

/// <summary>
/// �ڱ�ͷ�����½ڵ�,���ڵ������ʼ���Լ���ϰ�����ӵ���
/// </summary>
/// <param name="ͷ���ʽڵ�"></param>
/// <param name="���뵥�ʽڵ�"></param>
extern _declspec(dllexport) void _InsertWordToFront(struct _Word* _listHeadWord, struct _Word* _newWord);

/// <summary>
/// ָ��λ��ɾ�������ڵ���ת��
/// </summary>
/// <param name="ͷ���ʽڵ㣨ָ���б�"></param>
/// <param name="�������"></param>
extern _declspec(dllexport) void _DeleteByAppoint(struct _Word* _listHeadWord, int _wordRank);

/// <summary>
/// ���ҽڵ㣬�ܶ�ط�Ҫ�ã�������ʱ�޷�ʵ��ģ������,wpf�������Դ���ģ������
/// </summary>
/// <param name="ͷ���ʽڵ㣨ָ���б�"></param>
/// <param name="��������"></param>
/// <returns>�������</returns>
extern _declspec(dllexport) int _SearchByWordContent(struct _Word* _listHeadWord, char* _wordContent);

/// <summary>
/// ����������
/// </summary>
extern _declspec(dllexport) void _CreateWordBooks();
