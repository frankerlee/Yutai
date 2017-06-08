namespace JLK.Geodatabase
{
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.DataSourcesGDB;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using System;
    using System.Windows.Forms;

    public class ChangeVersionUtil
    {
        public void ChangeHistoricalVersionByName(IGraphicsContainer igraphicsContainer_0, IFeatureWorkspace ifeatureWorkspace_0, string string_0)
        {
            try
            {
                if ((((igraphicsContainer_0 != null) && (ifeatureWorkspace_0 != null)) && (string_0.Length != 0)) && (ifeatureWorkspace_0 is IHistoricalWorkspace))
                {
                    IHistoricalVersion version = (ifeatureWorkspace_0 as IHistoricalWorkspace).FindHistoricalVersionByName(string_0);
                    if (version != null)
                    {
                        this.ChangeVersion(igraphicsContainer_0, ifeatureWorkspace_0, version as IFeatureWorkspace);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void ChangeHistoricalVersionByTimeStamp(IGraphicsContainer igraphicsContainer_0, IFeatureWorkspace ifeatureWorkspace_0, object object_0)
        {
            try
            {
                if ((((igraphicsContainer_0 != null) && (ifeatureWorkspace_0 != null)) && (object_0 != null)) && (ifeatureWorkspace_0 is IHistoricalWorkspace))
                {
                    object obj2;
                    object obj3;
                    (ifeatureWorkspace_0 as IWorkspace).ConnectionProperties.GetAllProperties(out obj2, out obj3);
                    IPropertySet connectionProperties = new PropertySetClass();
                    for (int i = 0; i < ((System.Array) obj2).Length; i++)
                    {
                        switch (((System.Array) obj2).GetValue(i).ToString().ToUpper())
                        {
                            case "SERVER":
                                connectionProperties.SetProperty("SERVER", ((System.Array) obj3).GetValue(i));
                                break;

                            case "INSTANCE":
                                connectionProperties.SetProperty("INSTANCE", ((System.Array) obj3).GetValue(i));
                                break;

                            case "DATABASE":
                                connectionProperties.SetProperty("DATABASE", ((System.Array) obj3).GetValue(i));
                                break;

                            case "USER":
                                connectionProperties.SetProperty("USER", ((System.Array) obj3).GetValue(i));
                                break;

                            case "PASSWORD":
                                connectionProperties.SetProperty("PASSWORD", ((System.Array) obj3).GetValue(i));
                                break;

                            case "AUTHENTICATION_MODE":
                                connectionProperties.SetProperty("AUTHENTICATION_MODE", ((System.Array) obj3).GetValue(i));
                                break;
                        }
                    }
                    connectionProperties.SetProperty("HISTORICAL_TIMESTAMP", object_0);
                    connectionProperties.GetAllProperties(out obj2, out obj3);
                    IWorkspaceFactory factory = new SdeWorkspaceFactoryClass();
                    IWorkspace workspace = factory.Open(connectionProperties, 0);
                    if (workspace != null)
                    {
                        workspace.ConnectionProperties.GetAllProperties(out obj2, out obj3);
                        this.ChangeVersion(igraphicsContainer_0, ifeatureWorkspace_0, workspace as IFeatureWorkspace);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void ChangeVersion(IGraphicsContainer igraphicsContainer_0, IFeatureWorkspace ifeatureWorkspace_0, IFeatureWorkspace ifeatureWorkspace_1)
        {
            try
            {
                if (((igraphicsContainer_0 != null) && (ifeatureWorkspace_0 != null)) && (ifeatureWorkspace_1 != null))
                {
                    if (this.method_6(ifeatureWorkspace_0))
                    {
                        MessageBox.Show("当前版本在编辑状态，不能改变版本！");
                    }
                    else
                    {
                        IMap map;
                        IMapAdmin2 admin;
                        ICollectionTableVersionChanges changes = new EnumTableVersionChangesClass();
                        try
                        {
                            (ifeatureWorkspace_1 as IVersion).RefreshVersion();
                        }
                        catch (Exception exception1)
                        {
                            exception1.ToString();
                        }
                        if (igraphicsContainer_0 is IMap)
                        {
                            map = igraphicsContainer_0 as IMap;
                            map.ClearSelection();
                            admin = map as IMapAdmin2;
                            admin.FireChangeVersion(ifeatureWorkspace_0 as IVersion, ifeatureWorkspace_1 as IVersion);
                            this.method_5(map, ifeatureWorkspace_0, ifeatureWorkspace_1, changes);
                            this.method_4(map as IStandaloneTableCollection, ifeatureWorkspace_0, ifeatureWorkspace_1, changes);
                            this.method_0(admin, changes as IEnumTableVersionChanges);
                            changes.RemoveAll();
                            (map as IActiveView).Refresh();
                        }
                        else
                        {
                            igraphicsContainer_0.Reset();
                            for (IElement element = igraphicsContainer_0.Next(); element != null; element = igraphicsContainer_0.Next())
                            {
                                if (element is IMapFrame)
                                {
                                    map = (element as IMapFrame).Map;
                                    map.ClearSelection();
                                    admin = map as IMapAdmin2;
                                    admin.FireChangeVersion(ifeatureWorkspace_0 as IVersion, ifeatureWorkspace_1 as IVersion);
                                    this.method_5(map, ifeatureWorkspace_0, ifeatureWorkspace_1, changes);
                                    this.method_4(map as IStandaloneTableCollection, ifeatureWorkspace_0, ifeatureWorkspace_1, changes);
                                    this.method_0(admin, changes as IEnumTableVersionChanges);
                                    changes.RemoveAll();
                                    (map as IActiveView).Refresh();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                exception2.ToString();
            }
        }

        public void ChangeVersion(IMap imap_0, IFeatureWorkspace ifeatureWorkspace_0, IFeatureWorkspace ifeatureWorkspace_1)
        {
            try
            {
                if ((((imap_0 != null) && (ifeatureWorkspace_0 != null)) && (ifeatureWorkspace_1 != null)) && !this.method_6(ifeatureWorkspace_0))
                {
                    ICollectionTableVersionChanges changes = new EnumTableVersionChangesClass();
                    (ifeatureWorkspace_1 as IVersion).RefreshVersion();
                    imap_0.ClearSelection();
                    IMapAdmin2 admin = imap_0 as IMapAdmin2;
                    admin.FireChangeVersion(ifeatureWorkspace_0 as IVersion, ifeatureWorkspace_1 as IVersion);
                    this.method_5(imap_0, ifeatureWorkspace_0, ifeatureWorkspace_1, changes);
                    this.method_4(imap_0 as IStandaloneTableCollection, ifeatureWorkspace_0, ifeatureWorkspace_1, changes);
                    this.method_0(admin, changes as IEnumTableVersionChanges);
                    changes.RemoveAll();
                    (imap_0 as IActiveView).Refresh();
                }
            }
            catch
            {
            }
        }

        public void ChangeVersionByName(IGraphicsContainer igraphicsContainer_0, IFeatureWorkspace ifeatureWorkspace_0, string string_0)
        {
            try
            {
                if ((((igraphicsContainer_0 != null) && (ifeatureWorkspace_0 != null)) && (string_0.Length != 0)) && (ifeatureWorkspace_0 is IVersionedWorkspace))
                {
                    IVersionedWorkspace workspace = ifeatureWorkspace_0 as IVersionedWorkspace;
                    string versionName = (workspace as IVersion).VersionName;
                    IPropertySet connectionProperties = (ifeatureWorkspace_0 as IWorkspace).ConnectionProperties;
                    try
                    {
                        connectionProperties.GetProperty("Version").ToString();
                    }
                    catch
                    {
                    }
                    IVersion version = workspace.FindVersion(string_0);
                    if (version == null)
                    {
                    }
                    this.ChangeVersion(igraphicsContainer_0, ifeatureWorkspace_0, version as IFeatureWorkspace);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void method_0(IMapAdmin2 imapAdmin2_0, IEnumTableVersionChanges ienumTableVersionChanges_0)
        {
            try
            {
                ITable table;
                ITable table2;
                ienumTableVersionChanges_0.Reset();
                ienumTableVersionChanges_0.Next(out table, out table2);
                while (table == null)
                {
                Label_0014:
                    if (0 == 0)
                    {
                        return;
                    }
                    if (table is IFeatureClass)
                    {
                        imapAdmin2_0.FireChangeFeatureClass(table as IFeatureClass, table2 as IFeatureClass);
                    }
                    else
                    {
                        imapAdmin2_0.FireChangeTable(table, table2);
                    }
                    ienumTableVersionChanges_0.Next(out table, out table2);
                }
                goto Label_0014;
            }
            catch
            {
            }
        }

        private ITable method_1(IFeatureWorkspace ifeatureWorkspace_0, IDataset idataset_0)
        {
            try
            {
                if (idataset_0 is IRelationshipClass)
                {
                    return (ifeatureWorkspace_0.OpenRelationshipClass(idataset_0.Name) as ITable);
                }
                if (idataset_0 is IFeatureClass)
                {
                    return (ifeatureWorkspace_0.OpenFeatureClass(idataset_0.Name) as ITable);
                }
                return ifeatureWorkspace_0.OpenTable(idataset_0.Name);
            }
            catch
            {
            }
            return null;
        }

        private void method_2(IDisplayRelationshipClass idisplayRelationshipClass_0, IRelQueryTable irelQueryTable_0)
        {
            try
            {
                IRelationshipClass relationshipClass = irelQueryTable_0.RelationshipClass;
                if (relationshipClass != null)
                {
                    idisplayRelationshipClass_0.DisplayRelationshipClass(relationshipClass, idisplayRelationshipClass_0.JoinType);
                }
            }
            catch
            {
            }
        }

        private bool method_3(IDataset idataset_0, IFeatureWorkspace ifeatureWorkspace_0, IFeatureWorkspace ifeatureWorkspace_1, ICollectionTableVersionChanges icollectionTableVersionChanges_0)
        {
            try
            {
                if (idataset_0 is IRelQueryTableManage)
                {
                    (idataset_0 as IRelQueryTableManage).VersionChanged(ifeatureWorkspace_0 as IVersion, ifeatureWorkspace_1 as IVersion, icollectionTableVersionChanges_0 as IEnumTableVersionChanges);
                    icollectionTableVersionChanges_0.Add(idataset_0 as ITable, idataset_0 as ITable);
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        private void method_4(IStandaloneTableCollection istandaloneTableCollection_0, IFeatureWorkspace ifeatureWorkspace_0, IFeatureWorkspace ifeatureWorkspace_1, ICollectionTableVersionChanges icollectionTableVersionChanges_0)
        {
            try
            {
                IDisplayTable table = null;
                for (int i = 0; i < istandaloneTableCollection_0.StandaloneTableCount; i++)
                {
                    IStandaloneTable table2 = istandaloneTableCollection_0.get_StandaloneTable(i);
                    if (table2 is IDisplayTable)
                    {
                        table = table2 as IDisplayTable;
                        if (this.method_3(table.DisplayTable as IDataset, ifeatureWorkspace_0, ifeatureWorkspace_1, icollectionTableVersionChanges_0))
                        {
                            this.method_2(table2 as IDisplayRelationshipClass, table.DisplayTable as IRelQueryTable);
                        }
                    }
                    IDataset dataset = table2.Table as IDataset;
                    if (dataset != null)
                    {
                        this.method_3(dataset, ifeatureWorkspace_0, ifeatureWorkspace_1, icollectionTableVersionChanges_0);
                        if (dataset.Workspace == ifeatureWorkspace_0)
                        {
                            ITable newTable = this.method_1(ifeatureWorkspace_1, dataset);
                            if (newTable != null)
                            {
                                table2.Table = newTable;
                                icollectionTableVersionChanges_0.Add(dataset as ITable, newTable);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void method_5(IMap imap_0, IFeatureWorkspace ifeatureWorkspace_0, IFeatureWorkspace ifeatureWorkspace_1, ICollectionTableVersionChanges icollectionTableVersionChanges_0)
        {
            try
            {
                IEnumLayer layer = imap_0.get_Layers(null, true);
                if (layer != null)
                {
                    for (ILayer layer2 = layer.Next(); layer2 != null; layer2 = layer.Next())
                    {
                        if (layer2 is IDisplayTable)
                        {
                            IDisplayTable table = layer2 as IDisplayTable;
                            if (this.method_3(table.DisplayTable as IDataset, ifeatureWorkspace_0, ifeatureWorkspace_1, icollectionTableVersionChanges_0))
                            {
                                this.method_2(layer2 as IDisplayRelationshipClass, table.DisplayTable as IRelQueryTable);
                            }
                        }
                        if (layer2 is IFeatureLayer)
                        {
                            IFeatureLayer layer3 = layer2 as IFeatureLayer;
                            IDataset featureClass = layer3.FeatureClass as IDataset;
                            if (featureClass != null)
                            {
                                this.method_3(featureClass, ifeatureWorkspace_0, ifeatureWorkspace_1, icollectionTableVersionChanges_0);
                                if (featureClass.Workspace == ifeatureWorkspace_0)
                                {
                                    IFeatureClass class2 = this.method_1(ifeatureWorkspace_1, featureClass) as IFeatureClass;
                                    if (class2 != null)
                                    {
                                        layer3.FeatureClass = class2;
                                        icollectionTableVersionChanges_0.Add(featureClass as ITable, class2 as ITable);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private bool method_6(IFeatureWorkspace ifeatureWorkspace_0)
        {
            IWorkspaceEdit edit = ifeatureWorkspace_0 as IWorkspaceEdit;
            return ((edit != null) && edit.IsBeingEdited());
        }
    }
}

