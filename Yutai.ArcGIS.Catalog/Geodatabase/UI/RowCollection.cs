using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class RowCollection
    {
        private bool bool_0 = false;
        private enumConflictType enumConflictType_0;
        private IConflictClass iconflictClass_0 = null;
        private int int_0 = 0;
        private IRow irow_0 = null;
        private IRow irow_1 = null;
        private IRow irow_2 = null;
        private IRow irow_3 = null;

        private void method_0()
        {
            if (this.iconflictClass_0 != null)
            {
                IVersionEdit edit = (this.iconflictClass_0 as IDataset).Workspace as IVersionEdit;
                IFeatureWorkspace workspace = edit as IFeatureWorkspace;
                IFeatureWorkspace reconcileVersion = edit.ReconcileVersion as IFeatureWorkspace;
                IFeatureWorkspace preReconcileVersion = edit.PreReconcileVersion as IFeatureWorkspace;
                IFeatureWorkspace startEditingVersion = edit.StartEditingVersion as IFeatureWorkspace;
                workspace.OpenTable((this.iconflictClass_0 as IDataset).Name);
                ITable table = reconcileVersion.OpenTable((this.iconflictClass_0 as IDataset).Name);
                ITable table2 = preReconcileVersion.OpenTable((this.iconflictClass_0 as IDataset).Name);
                ITable table3 = startEditingVersion.OpenTable((this.iconflictClass_0 as IDataset).Name);
                try
                {
                    this.irow_0 = (this.iconflictClass_0 as ITable).GetRow(this.int_0);
                }
                catch
                {
                }
                try
                {
                    this.irow_1 = table.GetRow(this.int_0);
                }
                catch
                {
                }
                try
                {
                    this.irow_2 = table2.GetRow(this.int_0);
                }
                catch
                {
                }
                try
                {
                    this.irow_3 = table3.GetRow(this.int_0);
                }
                catch
                {
                }
            }
        }

        public void Update(int int_1)
        {
            IQueryFilter filter;
            IFields fields;
            int num2;
            IWorkspaceEdit workspace = (this.iconflictClass_0 as IDataset).Workspace as IWorkspaceEdit;
            switch (int_1)
            {
                case 0:
                    if (this.irow_1 != null)
                    {
                        this.irow_0 = this.iconflictClass_0.RestoreRow(this.int_0);
                        fields = this.irow_0.Fields;
                        for (num2 = 0; num2 < fields.FieldCount; num2++)
                        {
                            if (fields.get_Field(num2).Editable)
                            {
                                this.irow_0.set_Value(num2, this.irow_1.get_Value(num2));
                            }
                        }
                        workspace.StartEditOperation();
                        this.irow_0.Store();
                        workspace.StopEditOperation();
                        break;
                    }
                    if (this.irow_0 != null)
                    {
                        filter = new QueryFilterClass
                        {
                            WhereClause =
                                (this.iconflictClass_0 as IObjectClass).OIDFieldName + " = " + this.int_0.ToString()
                        };
                        (this.iconflictClass_0 as ITable).DeleteSearchedRows(filter);
                        this.irow_0 = null;
                    }
                    break;

                case 1:
                    if (this.irow_2 != null)
                    {
                        if (this.irow_0 == null)
                        {
                            this.irow_0 = this.iconflictClass_0.RestoreRow(this.int_0);
                        }
                        fields = this.irow_0.Fields;
                        for (num2 = 0; num2 < fields.FieldCount; num2++)
                        {
                            if (fields.get_Field(num2).Editable)
                            {
                                this.irow_0.set_Value(num2, this.irow_2.get_Value(num2));
                            }
                        }
                        this.irow_0.Store();
                        break;
                    }
                    if (this.irow_0 != null)
                    {
                        filter = new QueryFilterClass
                        {
                            WhereClause =
                                (this.iconflictClass_0 as IObjectClass).OIDFieldName + " = " + this.int_0.ToString()
                        };
                        (this.iconflictClass_0 as ITable).DeleteSearchedRows(filter);
                        this.irow_0 = null;
                    }
                    break;

                case 2:
                    if (this.irow_0 == null)
                    {
                        this.irow_0 = this.iconflictClass_0.RestoreRow(this.int_0);
                    }
                    fields = this.irow_0.Fields;
                    for (num2 = 0; num2 < fields.FieldCount; num2++)
                    {
                        if (fields.get_Field(num2).Editable)
                        {
                            this.irow_0.set_Value(num2, this.irow_3.get_Value(num2));
                        }
                    }
                    this.irow_0.Store();
                    break;
            }
        }

        public IConflictClass ConflictClass
        {
            get { return this.iconflictClass_0; }
            set { this.iconflictClass_0 = value; }
        }

        public enumConflictType ConflictType
        {
            set { this.enumConflictType_0 = value; }
        }

        public IFields Fields
        {
            get
            {
                if (this.irow_3 != null)
                {
                    return this.irow_3.Fields;
                }
                return null;
            }
        }

        public int OID
        {
            get { return this.int_0; }
            set
            {
                this.int_0 = value;
                this.method_0();
            }
        }

        public IRow PreReconcileRow
        {
            get { return this.irow_2; }
        }

        public IRow ReconcileRow
        {
            get { return this.irow_1; }
        }

        public IRow StartEditingRow
        {
            get { return this.irow_3; }
        }
    }
}