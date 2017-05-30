using System;

namespace Yutai.Catalog
{
	public interface IGxGDSConnection
	{
		object DataServerManager
		{
			get;
		}

		bool IsAdministrator
		{
			get;
		}

		bool IsConnected
		{
			get;
		}

		string ServerName
		{
			get;
		}

		void AttachGeoDatabase(string string_0, string string_1);

		void Connect();

		void CreateGeoDatabase(string string_0, string string_1, int int_0);

		void Disconnect();

		void LoadFromFile(string string_0);

		void Pause();

		void RestoreGeodatabase(string string_0, string string_1, string string_2);

		void Resume();

		void Start();

		void Stop();
	}
}