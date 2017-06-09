using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.ExtendClass
{
	[ComVisible(true)]
	[Guid("8833EB09-E320-47e9-92D4-D73CD7DE7EB2")]
	[ProgId("JLK.ExtendClass.JLKCodeValueDomain")]
	public class JLKCodeValueDomain : IDomain, IJLKCodeValueDomain, ICodedValueDomain, IClone, IPersistStream, IPersist, ISchemaLock, IXMLSerialize
	{
		public const string GUID = "JLK.ExtendClass.JLKCodeValueDomain";

		private string m_strDescription = "";

		private int m_DomainID = -1;

		private esriFieldType m_FieldType = esriFieldType.esriFieldTypeString;

		private string m_Name = "";

		private string m_Owner = "";

		private string m_TableName = "";

		private string m_NameFieldName = "";

		private string m_ValueFieldName = "";

		private IWorkspace m_pWorkspace = null;

		public int CodeCount
		{
			get
			{
				return 0;
			}
		}

		string ESRI.ArcGIS.Geodatabase.IDomain.Description
		{
			get
			{
				return this.m_strDescription;
			}
			set
			{
				this.m_strDescription = value;
			}
		}

		int ESRI.ArcGIS.Geodatabase.IDomain.DomainID
		{
			get
			{
				return this.m_DomainID;
			}
			set
			{
				this.m_DomainID = value;
			}
		}

		esriFieldType ESRI.ArcGIS.Geodatabase.IDomain.FieldType
		{
			get
			{
				return this.m_FieldType;
			}
			set
			{
				this.m_FieldType = value;
			}
		}

		esriMergePolicyType ESRI.ArcGIS.Geodatabase.IDomain.MergePolicy
		{
			get
			{
				return esriMergePolicyType.esriMPTDefaultValue;
			}
			set
			{
			}
		}

		string ESRI.ArcGIS.Geodatabase.IDomain.Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		string ESRI.ArcGIS.Geodatabase.IDomain.Owner
		{
			get
			{
				return this.m_Owner;
			}
			set
			{
				this.m_Owner = value;
			}
		}

		esriSplitPolicyType ESRI.ArcGIS.Geodatabase.IDomain.SplitPolicy
		{
			get
			{
				return esriSplitPolicyType.esriSPTDefaultValue;
			}
			set
			{
			}
		}

		esriDomainType ESRI.ArcGIS.Geodatabase.IDomain.Type
		{
			get
			{
				return esriDomainType.esriDTCodedValue;
			}
		}

		public string NameFieldName
		{
			get
			{
				return this.m_NameFieldName;
			}
			set
			{
				this.m_NameFieldName = value;
			}
		}

		internal IPropertySet PropertySet
		{
			get
			{
				IPropertySet propertySetClass = new PropertySet();
				propertySetClass.SetProperty("TableName", this.m_TableName);
				propertySetClass.SetProperty("NameFieldName", this.m_NameFieldName);
				propertySetClass.SetProperty("ValueFieldName", this.m_ValueFieldName);
				if (this.m_pWorkspace != null)
				{
					if (this.m_pWorkspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
					{
						propertySetClass.SetProperty("WorkspaceType", "SDE");
					}
					else if (this.m_pWorkspace.Type != esriWorkspaceType.esriLocalDatabaseWorkspace)
					{
						propertySetClass.SetProperty("WorkspaceType", "NULL");
					}
					else
					{
						propertySetClass.SetProperty("WorkspaceType", "MDB");
					}
					propertySetClass.SetProperty("Workspace", this.m_pWorkspace.ConnectionProperties);
				}
				else
				{
					propertySetClass.SetProperty("WorkspaceType", "NULL");
				}
				return propertySetClass;
			}
			set
			{
				IWorkspaceFactory accessWorkspaceFactoryClass;
				IPropertySet propertySet = value;
				this.m_TableName = propertySet.GetProperty("TableName").ToString();
				this.m_NameFieldName = propertySet.GetProperty("NameFieldName").ToString();
				this.m_ValueFieldName = propertySet.GetProperty("ValueFieldName").ToString();
				string str = propertySet.GetProperty("WorkspaceType").ToString();
				if (str != "NULL")
				{
					IPropertySet property = propertySet.GetProperty("Workspace") as IPropertySet;
					if (!(str == "SDE"))
					{
						accessWorkspaceFactoryClass = new AccessWorkspaceFactory();
					}
					else
					{
						accessWorkspaceFactoryClass = new SdeWorkspaceFactory();
					}
					this.m_pWorkspace = accessWorkspaceFactoryClass.Open(property, 0);
				}
			}
		}

		public string TableName
		{
			get
			{
				return this.m_TableName;
			}
			set
			{
				this.m_TableName = value;
			}
		}

		public string ValueFieldName
		{
			get
			{
				return this.m_ValueFieldName;
			}
			set
			{
				this.m_ValueFieldName = value;
			}
		}

		public IWorkspace Workspace
		{
			get
			{
				return this.m_pWorkspace;
			}
			set
			{
				this.m_pWorkspace = value;
			}
		}

		public JLKCodeValueDomain()
		{
		}

		public void AddCode(object Value, string Name)
		{
		}

		/// <summary>
		/// Required method for ArcGIS Component Category registration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryRegistration(System.Type registerType)
		{
			string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
		}

		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(System.Type registerType)
		{
			string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
		}

		public void DeleteCode(object Value)
		{
		}

		void ESRI.ArcGIS.esriSystem.IClone.Assign(IClone src)
		{
		}

		IClone ESRI.ArcGIS.esriSystem.IClone.Clone()
		{
			return this;
		}

		bool ESRI.ArcGIS.esriSystem.IClone.IsEqual(IClone other)
		{
			return true;
		}

		bool ESRI.ArcGIS.esriSystem.IClone.IsIdentical(IClone other)
		{
			return true;
		}

		void ESRI.ArcGIS.esriSystem.IPersist.GetClassID(out Guid pClassID)
		{
			pClassID = new Guid("8833EB09-E320-47e9-92D4-D73CD7DE7EB2");
		}

		void ESRI.ArcGIS.esriSystem.IPersistStream.GetClassID(out Guid pClassID)
		{
			pClassID = new Guid("8833EB09-E320-47e9-92D4-D73CD7DE7EB2");
		}

		void ESRI.ArcGIS.esriSystem.IPersistStream.GetSizeMax(out _ULARGE_INTEGER pcbSize)
		{
			(new PropertySet() as IPersistStream).GetSizeMax(out pcbSize);
		}

		void ESRI.ArcGIS.esriSystem.IPersistStream.IsDirty()
		{
		}

		void ESRI.ArcGIS.esriSystem.IPersistStream.Load(IStream pstm)
		{
			IPropertySet propertySetClass = new PropertySet();
			(propertySetClass as IPersistStream).Load(pstm);
			this.PropertySet = propertySetClass;
		}

		void ESRI.ArcGIS.esriSystem.IPersistStream.Save(IStream pstm, int fClearDirty)
		{
			(this.PropertySet as IPersistStream).Save(pstm, fClearDirty);
		}

		void ESRI.ArcGIS.esriSystem.IXMLSerialize.Deserialize(IXMLSerializeData data)
		{
		}

		void ESRI.ArcGIS.esriSystem.IXMLSerialize.Serialize(IXMLSerializeData data)
		{
		}

		bool ESRI.ArcGIS.Geodatabase.IDomain.MemberOf(object Value)
		{
			return false;
		}

		void ESRI.ArcGIS.Geodatabase.ISchemaLock.ChangeSchemaLock(esriSchemaLock schemaLock)
		{
		}

		void ESRI.ArcGIS.Geodatabase.ISchemaLock.GetCurrentSchemaLocks(out IEnumSchemaLockInfo schemaLockInfo)
		{
			schemaLockInfo = null;
		}

		public string get_Name(int Index)
		{
			return "";
		}

		public object get_Value(int Index)
		{
			return null;
		}

		[ComRegisterFunction]
		[ComVisible(false)]
		private static void RegisterFunction(System.Type registerType)
		{
			JLKCodeValueDomain.ArcGISCategoryRegistration(registerType);
		}

		[ComUnregisterFunction]
		[ComVisible(false)]
		private static void UnregisterFunction(System.Type registerType)
		{
			JLKCodeValueDomain.ArcGISCategoryUnregistration(registerType);
		}
	}
}