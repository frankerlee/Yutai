using System;
using System.Threading;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.SystemUI;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Geodatabase
{
	internal class MyFeatureDataConverter : IFeatureProgress_Event, IFeatureDataConverter, IFeatureDataConverter2
	{
		private ICommandHost icommandHost_0 = new CommandHost();

		private IFeatureProgress_StepEventHandler ifeatureProgress_StepEventHandler_0;

		public MyFeatureDataConverter()
		{
		}

		public IEnumInvalidObject ConvertFeatureClass(IFeatureClassName ifeatureClassName_0, IQueryFilter iqueryFilter_0, IFeatureDatasetName ifeatureDatasetName_0, IFeatureClassName ifeatureClassName_1, IGeometryDef igeometryDef_0, IFields ifields_0, string string_0, int int_0, int int_1)
		{
			IEnumFieldError enumFieldError;
			IFields field;
			string str;
			IEnumInvalidObject enumInvalidObject = null;
			IFieldChecker fieldCheckerClass = new FieldChecker();
			IWorkspaceName workspaceName = null;
			workspaceName = (ifeatureDatasetName_0 == null ? (ifeatureClassName_1 as IDatasetName).WorkspaceName : (ifeatureDatasetName_0 as IDatasetName).WorkspaceName);
			IWorkspace workspace = (workspaceName as IName).Open() as IWorkspace;
			fieldCheckerClass.ValidateWorkspace = workspace;
			fieldCheckerClass.Validate(ifields_0, out enumFieldError, out field);
			string name = (ifeatureClassName_1 as IDatasetName).Name;
			fieldCheckerClass.ValidateTableName(name, out str);
			name = str;
			IField field1 = null;
			int num = 0;
			while (true)
			{
				if (num >= field.FieldCount)
				{
					break;
				}
				else if (field.Field[num].Type == esriFieldType.esriFieldTypeGeometry)
				{
					field1 = field.Field[num];
					break;
				}
				else
				{
					num++;
				}
			}
			IFeatureClass featureClass = null;
			if (ifeatureDatasetName_0 == null)
			{
				featureClass = (workspace as IFeatureWorkspace).CreateFeatureClass(name, field, null, null, esriFeatureType.esriFTSimple, field1.Name, string_0);
			}
			else
			{
				IFeatureDataset featureDataset = (ifeatureDatasetName_0 as IName).Open() as IFeatureDataset;
				featureClass = featureDataset.CreateFeatureClass(name, field, null, null, esriFeatureType.esriFTSimple, field1.Name, string_0);
			}
			IFeatureClass featureClass1 = (ifeatureClassName_0 as IName).Open() as IFeatureClass;
			IFeatureCursor featureCursor = featureClass1.Search(iqueryFilter_0, false);
			IFeature feature = featureCursor.NextFeature();
			int num1 = 0;
			IFeatureCursor featureCursor1 = featureClass.Insert(true);
			while (feature != null)
			{
				IFeatureBuffer featureBuffer = featureClass.CreateFeatureBuffer();
				if (feature.Shape != null)
				{
					try
					{
						this.method_0(featureCursor1, featureBuffer, field, feature);
					}
					catch (Exception exception)
					{
						Logger.Current.Error("",exception, "");
					}
				}
				num1++;
				if (num1 == int_0)
				{
					num1 = 0;
					featureCursor1.Flush();
				}
				feature = featureCursor.NextFeature();
				if (this.ifeatureProgress_StepEventHandler_0 == null)
				{
					continue;
				}
				this.ifeatureProgress_StepEventHandler_0();
			}
			if (num1 > 0)
			{
				featureCursor1.Flush();
			}
			ComReleaser.ReleaseCOMObject(featureCursor);
			ComReleaser.ReleaseCOMObject(featureCursor1);
			return enumInvalidObject;
		}

		public void ConvertFeatureDataset(IFeatureDatasetName ifeatureDatasetName_0, IFeatureDatasetName ifeatureDatasetName_1, IGeometryDef igeometryDef_0, string string_0, int int_0, int int_1)
		{
		}

		public IEnumInvalidObject ConvertTable(IDatasetName idatasetName_0, IQueryFilter iqueryFilter_0, IDatasetName idatasetName_1, IFields ifields_0, string string_0, int int_0, int int_1)
		{
			IEnumFieldError enumFieldError;
			IFields field;
			string str;
			IEnumInvalidObject enumInvalidObject = null;
			IFieldChecker fieldCheckerClass = new FieldChecker();
			IWorkspace workspace = (idatasetName_1.WorkspaceName as IName).Open() as IWorkspace;
			fieldCheckerClass.ValidateWorkspace = workspace;
			fieldCheckerClass.Validate(ifields_0, out enumFieldError, out field);
			fieldCheckerClass.ValidateTableName(idatasetName_1.Name, out str);
			string str1 = str;
			ITable table = null;
			table = (workspace as IFeatureWorkspace).CreateTable(str1, field, null, null, string_0);
			ITable table1 = (idatasetName_0 as IName).Open() as ITable;
			ICursor cursor = table1.Search(iqueryFilter_0, false);
			int num = 0;
			ICursor cursor1 = table.Insert(true);
			IRow row = cursor.NextRow();
			while (row != null)
			{
				IRowBuffer rowBuffer = table.CreateRowBuffer();
				try
				{
					this.method_1(cursor1, rowBuffer, field, row);
				}
				catch (Exception exception)
				{
					Logger.Current.Error("",exception, "");
				}
				num++;
				if (num == int_0)
				{
					num = 0;
					cursor1.Flush();
				}
				row = cursor.NextRow();
				if (this.ifeatureProgress_StepEventHandler_0 == null)
				{
					continue;
				}
				this.ifeatureProgress_StepEventHandler_0();
			}
			if (num > 0)
			{
				cursor1.Flush();
			}
			ComReleaser.ReleaseCOMObject(cursor);
			ComReleaser.ReleaseCOMObject(cursor1);
			return enumInvalidObject;
		}

		IEnumInvalidObject ESRI.ArcGIS.Geodatabase.IFeatureDataConverter2.ConvertFeatureClass(IDatasetName idatasetName_0, IQueryFilter iqueryFilter_0, ISelectionSet iselectionSet_0, IFeatureDatasetName ifeatureDatasetName_0, IFeatureClassName ifeatureClassName_0, IGeometryDef igeometryDef_0, IFields ifields_0, string string_0, int int_0, int int_1)
		{
			return null;
		}

		IEnumInvalidObject ESRI.ArcGIS.Geodatabase.IFeatureDataConverter2.ConvertTable(IDatasetName idatasetName_0, IQueryFilter iqueryFilter_0, ISelectionSet iselectionSet_0, IDatasetName idatasetName_1, IFields ifields_0, string string_0, int int_0, int int_1)
		{
			return null;
		}

		private void method_0(IFeatureCursor ifeatureCursor_0, IFeatureBuffer ifeatureBuffer_0, IFields ifields_0, IFeature ifeature_0)
		{
			IFields fields = ifeatureBuffer_0.Fields;
			IFields field = ifeature_0.Fields;
			for (int i = 0; i < field.FieldCount; i++)
			{
				IField field1 = field.Field[i];
				if ((field1.Type == esriFieldType.esriFieldTypeGeometry || field1.Type == esriFieldType.esriFieldTypeOID ? false : field1.Editable))
				{
					int value = fields.FindField(ifields_0.Field[i].Name);
					try
					{
						ifeatureBuffer_0.Value[value] = ifeature_0.Value[i];
					}
					catch (Exception exception)
					{
						Logger.Current.Error("",exception, "");
					}
				}
			}
			ifeatureBuffer_0.Shape = ifeature_0.ShapeCopy;
			ifeatureCursor_0.InsertFeature(ifeatureBuffer_0);
		}

		private void method_1(ICursor icursor_0, IRowBuffer irowBuffer_0, IFields ifields_0, IRow irow_0)
		{
			IFields fields = irowBuffer_0.Fields;
			IFields field = irow_0.Fields;
			for (int i = 0; i < field.FieldCount; i++)
			{
				IField field1 = field.Field[i];
				if ((field1.Type == esriFieldType.esriFieldTypeOID ? false : field1.Editable))
				{
					int value = fields.FindField(ifields_0.Field[i].Name);
					try
					{
						irowBuffer_0.Value[value] = irow_0.Value[i];
					}
					catch (Exception exception)
					{
						Logger.Current.Error("",exception, "");
					}
				}
			}
			icursor_0.InsertRow(irowBuffer_0);
		}

		public event IFeatureProgress_StepEventHandler Step
		{
			add
			{
				IFeatureProgress_StepEventHandler featureProgressStepEventHandler;
				IFeatureProgress_StepEventHandler ifeatureProgressStepEventHandler0 = this.ifeatureProgress_StepEventHandler_0;
				do
				{
					featureProgressStepEventHandler = ifeatureProgressStepEventHandler0;
					IFeatureProgress_StepEventHandler featureProgressStepEventHandler1 = (IFeatureProgress_StepEventHandler)Delegate.Combine(featureProgressStepEventHandler, value);
					ifeatureProgressStepEventHandler0 = Interlocked.CompareExchange<IFeatureProgress_StepEventHandler>(ref this.ifeatureProgress_StepEventHandler_0, featureProgressStepEventHandler1, featureProgressStepEventHandler);
				}
				while ((object)ifeatureProgressStepEventHandler0 != (object)featureProgressStepEventHandler);
			}
			remove
			{
				IFeatureProgress_StepEventHandler featureProgressStepEventHandler;
				IFeatureProgress_StepEventHandler ifeatureProgressStepEventHandler0 = this.ifeatureProgress_StepEventHandler_0;
				do
				{
					featureProgressStepEventHandler = ifeatureProgressStepEventHandler0;
					IFeatureProgress_StepEventHandler featureProgressStepEventHandler1 = (IFeatureProgress_StepEventHandler)Delegate.Remove(featureProgressStepEventHandler, value);
					ifeatureProgressStepEventHandler0 = Interlocked.CompareExchange<IFeatureProgress_StepEventHandler>(ref this.ifeatureProgress_StepEventHandler_0, featureProgressStepEventHandler1, featureProgressStepEventHandler);
				}
				while ((object)ifeatureProgressStepEventHandler0 != (object)featureProgressStepEventHandler);
			}
		}
	}
}