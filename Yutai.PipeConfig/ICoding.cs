namespace Yutai.PipeConfig
{
	public interface ICoding
	{
		int getCode(int iGroup, string sName);

		int getCodeCount(int iGroup);

		int getCodeGroup(string sKind);

		CodeItem getCodeItem(int iGroup, int idx);

		int getCodeKind(int iCode);

		string getCodeName(int iCode);

		int getCodeSize(int iCode);

		bool ReadCodeFile(string szFile);
	}
}