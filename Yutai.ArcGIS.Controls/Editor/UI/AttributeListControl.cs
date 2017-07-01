using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.CodeDomainEx;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Editor;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    internal partial class AttributeListControl : UserControl
    {
        private bool m_CanDo = false;
        private string m_EditFeildName = "";
        private IActiveView m_pActiveView = null;
        private IFeatureLayer m_pFeatureLayer = null;
        private IObject m_pObject = null;
        private VertXtraGrid m_pVertXtraGrid = null;

        public AttributeListControl()
        {
            this.InitializeComponent();
            this.m_pVertXtraGrid = new VertXtraGrid(this.gridControl1);
            this.gridView1.CellValueChanged += new CellValueChangedEventHandler(this.gridView1_CellValueChanged);
        }

        private void AttributeListControl_Load(object sender, EventArgs e)
        {
            EditorEvent.OnBeginSaveEditing += new EditorEvent.OnBeginSaveEditingHandler(this.EditorEvent_OnSaveEditing);
            EditorEvent.OnBeginStopEditing += new EditorEvent.OnBeginStopEditingHandler(this.EditorEvent_OnSaveEditing);
            this.gridView1.SelectionChanged += new SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            this.gridView1.LostFocus += new EventHandler(this.gridView1_LostFocus);
            this.Init();
            this.m_CanDo = true;
        }

        private void AttributeListControl_OnMenuClickEvent()
        {
            this.gridControl1.Focus();
        }

        private bool CheckFieldIsVisible(ILayer pLayer, IField pField)
        {
            if (pLayer is ILayerFields)
            {
                int index = (pLayer as ILayerFields).FindField(pField.Name);
                if (index != -1)
                {
                    return (pLayer as ILayerFields).get_FieldInfo(index).Visible;
                }
            }
            return true;
        }

        private void EditorEvent_OnSaveEditing()
        {
            base.Visible = false;
            base.Visible = true;
        }

        private bool FieldCanEdit(IField pField)
        {
            if (!pField.Editable)
            {
                return false;
            }
            if (this.LockFields != null)
            {
                if (this.LockFields.IndexOf(pField.Name) != -1)
                {
                    return false;
                }
                if (this.LockFields.IndexOf(pField.AliasName) != -1)
                {
                    return false;
                }
            }
            return true;
        }

        private string GetShapeString(IField pField)
        {
            string str = "";
            IGeometryDef geometryDef = pField.GeometryDef;
            if (geometryDef != null)
            {
                switch (geometryDef.GeometryType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        str = "点";
                        break;

                    case esriGeometryType.esriGeometryMultipoint:
                        str = "多点";
                        break;

                    case esriGeometryType.esriGeometryPolyline:
                        str = "线";
                        break;

                    case esriGeometryType.esriGeometryPolygon:
                        str = "多边形";
                        break;

                    case esriGeometryType.esriGeometryMultiPatch:
                        str = "多面";
                        break;
                }
                str = str + " ";
                if (geometryDef.HasZ)
                {
                    str = str + "Z";
                }
                if (geometryDef.HasM)
                {
                    str = str + "M";
                }
            }
            return str;
        }

        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (this.m_CanDo && (this.m_pObject != null))
            {
                object obj2;
                IDomain domain;
                ICodedValueDomain domain2;
                int num3;
                ISubtypes subtypes = this.m_pObject.Class as ISubtypes;
                GridEditorItem row = this.gridView1.GetRow(e.RowHandle) as GridEditorItem;
                int index = this.m_pObject.Fields.FindFieldByAliasName(row.Name);
                IField pField = this.m_pObject.Fields.get_Field(index);
                this.m_EditFeildName = pField.AliasName;
                if ((subtypes != null) && subtypes.HasSubtype)
                {
                    if (subtypes.SubtypeFieldName == pField.Name)
                    {
                        IEnumSubtype subtype = subtypes.Subtypes;
                        subtype.Reset();
                        int subtypeCode = 0;
                        for (string str = subtype.Next(out subtypeCode);
                            str != null;
                            str = subtype.Next(out subtypeCode))
                        {
                            if (e.Value.ToString() == str)
                            {
                                this.UpdateFieldValue(pField, subtypeCode);
                                break;
                            }
                        }
                    }
                    else if (e.Value.ToString() == "<空>")
                    {
                        obj2 = DBNull.Value;
                        this.UpdateFieldValue(pField, obj2);
                    }
                    else
                    {
                        domain = subtypes.get_Domain((this.m_pObject as IRowSubtypes).SubtypeCode, pField.Name);
                        if (domain is ICodedValueDomain)
                        {
                            domain2 = domain as ICodedValueDomain;
                            for (num3 = 0; num3 < domain2.CodeCount; num3++)
                            {
                                if (domain2.get_Name(num3) == e.Value.ToString())
                                {
                                    this.UpdateFieldValue(pField, domain2.get_Value(num3));
                                    break;
                                }
                            }
                        }
                        else
                        {
                            this.UpdateFieldValue(pField, e.Value);
                        }
                    }
                }
                else if (e.Value.ToString() == "<空>")
                {
                    obj2 = DBNull.Value;
                    this.UpdateFieldValue(pField, obj2);
                }
                else
                {
                    string name = (this.m_pObject.Class as IDataset).Name;
                    CodeDomainEx codeDomainEx = CodeDomainManage.GetCodeDomainEx(pField.Name, name);
                    if (codeDomainEx != null)
                    {
                        if ((codeDomainEx.ParentIDFieldName == null) || (codeDomainEx.ParentIDFieldName.Length == 0))
                        {
                            NameValueCollection codeDomain = codeDomainEx.GetCodeDomain();
                            for (num3 = 0; num3 < codeDomain.Count; num3++)
                            {
                                string str3 = codeDomain.Keys[num3];
                                if (str3 == e.Value.ToString())
                                {
                                    this.UpdateFieldValue(pField, codeDomain[str3]);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            this.UpdateFieldValue(pField, codeDomainEx.GetCodeByName(e.Value.ToString()));
                        }
                    }
                    else
                    {
                        domain = pField.Domain;
                        if (domain is ICodedValueDomain)
                        {
                            domain2 = domain as ICodedValueDomain;
                            for (num3 = 0; num3 < domain2.CodeCount; num3++)
                            {
                                if (domain2.get_Name(num3) == e.Value.ToString())
                                {
                                    this.UpdateFieldValue(pField, domain2.get_Value(num3));
                                    break;
                                }
                            }
                        }
                        else if (this.UpdateFieldValue(pField, e.Value))
                        {
                            this.m_CanDo = false;
                            row.Value = this.m_pObject.get_Value(index);
                            this.m_CanDo = true;
                        }
                    }
                }
            }
        }

        private void gridView1_LostFocus(object sender, EventArgs e)
        {
        }

        private void gridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Init()
        {
            this.gridControl1.Focus();
            this.m_pVertXtraGrid.Clear();
            int rowHandle = -1;
            if (this.m_pObject == null)
            {
                this.m_CanDo = true;
            }
            else
            {
                this.toolStripDropDownButton1.DropDownItemClicked +=
                    new ToolStripItemClickedEventHandler(this.toolStripDropDownButton1_DropDownItemClicked);
                try
                {
                    ITableAttachments attachments = (ITableAttachments) this.m_pObject.Class;
                    if (attachments != null)
                    {
                        if (attachments.HasAttachments)
                        {
                            this.toolStrip1.Visible = true;
                            this.InitAttachment();
                        }
                        else
                        {
                            this.toolStrip1.Visible = false;
                        }
                    }
                }
                catch
                {
                }
                IFields fields = this.m_pObject.Fields;
                string[] strArray = new string[2];
                ISubtypes subtypes = this.m_pObject.Class as ISubtypes;
                IDomain domain = null;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField pField = fields.get_Field(i);
                    if (this.CheckFieldIsVisible(this.m_pFeatureLayer, pField))
                    {
                        strArray[0] = pField.AliasName;
                        if (pField.Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            strArray[1] = this.GetShapeString(pField);
                            this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1], true);
                        }
                        else if (pField.Type == esriFieldType.esriFieldTypeOID)
                        {
                            strArray[1] = this.m_pObject.OID.ToString();
                            this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1], true);
                        }
                        else if (pField.Type == esriFieldType.esriFieldTypeBlob)
                        {
                            strArray[1] = "<二进制数据>";
                            this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1], true);
                        }
                        else
                        {
                            int num4;
                            double minValue;
                            double maxValue;
                            object obj2 = this.m_pObject.get_Value(i);
                            ICodedValueDomain domain2 = null;
                            IList list = new ArrayList();
                            if ((subtypes != null) && subtypes.HasSubtype)
                            {
                                if (subtypes.SubtypeFieldName == pField.Name)
                                {
                                    int num3;
                                    try
                                    {
                                        strArray[1] =
                                            subtypes.get_SubtypeName((this.m_pObject as IRowSubtypes).SubtypeCode);
                                    }
                                    catch
                                    {
                                        strArray[1] = obj2.ToString();
                                    }
                                    IEnumSubtype subtype = subtypes.Subtypes;
                                    subtype.Reset();
                                    for (string str = subtype.Next(out num3); str != null; str = subtype.Next(out num3))
                                    {
                                        list.Add(str);
                                    }
                                    this.m_pVertXtraGrid.AddComBoBox(strArray[0], strArray[1], list,
                                        !this.FieldCanEdit(pField));
                                }
                                else
                                {
                                    domain = subtypes.get_Domain((this.m_pObject as IRowSubtypes).SubtypeCode,
                                        pField.Name);
                                    if (domain is ICodedValueDomain)
                                    {
                                        domain2 = domain as ICodedValueDomain;
                                        if (pField.IsNullable)
                                        {
                                            list.Add("<空>");
                                        }
                                        strArray[1] = obj2.ToString();
                                        num4 = 0;
                                        while (num4 < domain2.CodeCount)
                                        {
                                            list.Add(domain2.get_Name(num4));
                                            if (obj2.ToString() == domain2.get_Value(num4).ToString())
                                            {
                                                strArray[1] = domain2.get_Name(num4);
                                            }
                                            num4++;
                                        }
                                        this.m_pVertXtraGrid.AddComBoBox(strArray[0], strArray[1], list,
                                            !this.FieldCanEdit(pField));
                                    }
                                    else if ((((pField.Type == esriFieldType.esriFieldTypeSmallInteger) ||
                                               (pField.Type == esriFieldType.esriFieldTypeSingle)) ||
                                              (pField.Type == esriFieldType.esriFieldTypeDouble)) ||
                                             (pField.Type == esriFieldType.esriFieldTypeInteger))
                                    {
                                        minValue = 0.0;
                                        maxValue = 0.0;
                                        if (domain is IRangeDomain)
                                        {
                                            minValue = (double) (domain as IRangeDomain).MinValue;
                                            maxValue = (double) (domain as IRangeDomain).MaxValue;
                                        }
                                        if (pField.Editable)
                                        {
                                            this.m_pVertXtraGrid.AddSpinEdit(strArray[0], obj2, false, minValue,
                                                maxValue);
                                        }
                                        else
                                        {
                                            this.m_pVertXtraGrid.AddTextEdit(strArray[0], obj2, true);
                                        }
                                    }
                                    else if (pField.Type == esriFieldType.esriFieldTypeDate)
                                    {
                                        this.m_pVertXtraGrid.AddDateEdit(strArray[0], obj2, !this.FieldCanEdit(pField));
                                    }
                                    else
                                    {
                                        strArray[1] = obj2.ToString();
                                        this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1],
                                            !this.FieldCanEdit(pField));
                                    }
                                }
                            }
                            else
                            {
                                domain = pField.Domain;
                                if (domain != null)
                                {
                                    if (domain is ICodedValueDomain)
                                    {
                                        domain2 = domain as ICodedValueDomain;
                                        if (pField.IsNullable)
                                        {
                                            list.Add("<空>");
                                        }
                                        strArray[1] = obj2.ToString();
                                        num4 = 0;
                                        while (num4 < domain2.CodeCount)
                                        {
                                            list.Add(domain2.get_Name(num4));
                                            if (obj2.ToString() == domain2.get_Value(num4).ToString())
                                            {
                                                strArray[1] = domain2.get_Name(num4);
                                            }
                                            num4++;
                                        }
                                        this.m_pVertXtraGrid.AddComBoBox(strArray[0], strArray[1], list,
                                            !this.FieldCanEdit(pField));
                                    }
                                    else if ((((pField.Type == esriFieldType.esriFieldTypeSmallInteger) ||
                                               (pField.Type == esriFieldType.esriFieldTypeSingle)) ||
                                              (pField.Type == esriFieldType.esriFieldTypeDouble)) ||
                                             (pField.Type == esriFieldType.esriFieldTypeInteger))
                                    {
                                        minValue = 0.0;
                                        maxValue = 0.0;
                                        if (domain is IRangeDomain)
                                        {
                                            minValue = (double) (domain as IRangeDomain).MinValue;
                                            maxValue = (double) (domain as IRangeDomain).MaxValue;
                                        }
                                        if (pField.Editable)
                                        {
                                            this.m_pVertXtraGrid.AddSpinEdit(strArray[0], obj2, false, minValue,
                                                maxValue);
                                        }
                                        else
                                        {
                                            this.m_pVertXtraGrid.AddTextEdit(strArray[0], obj2, true);
                                        }
                                    }
                                    else if (pField.Type == esriFieldType.esriFieldTypeDate)
                                    {
                                        this.m_pVertXtraGrid.AddDateEdit(strArray[0], obj2, !this.FieldCanEdit(pField));
                                    }
                                    else
                                    {
                                        strArray[1] = obj2.ToString();
                                        this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1],
                                            !this.FieldCanEdit(pField));
                                    }
                                }
                                else
                                {
                                    string name = (this.m_pObject.Class as IDataset).Name;
                                    CodeDomainEx codeDomainEx = CodeDomainManage.GetCodeDomainEx(pField.Name, name);
                                    if (codeDomainEx != null)
                                    {
                                        if ((codeDomainEx.ParentIDFieldName == null) ||
                                            (codeDomainEx.ParentIDFieldName.Length == 0))
                                        {
                                            NameValueCollection codeDomain = codeDomainEx.GetCodeDomain();
                                            if (pField.IsNullable)
                                            {
                                                list.Add("<空>");
                                            }
                                            strArray[1] = obj2.ToString();
                                            for (num4 = 0; num4 < codeDomain.Count; num4++)
                                            {
                                                string str3 = codeDomain.Keys[num4];
                                                list.Add(str3);
                                                if (obj2.ToString() == codeDomain[str3])
                                                {
                                                    strArray[1] = str3;
                                                }
                                            }
                                            this.m_pVertXtraGrid.AddComBoBox(strArray[0], strArray[1], list,
                                                !this.FieldCanEdit(pField));
                                        }
                                        else
                                        {
                                            strArray[1] = obj2.ToString();
                                            this.m_pVertXtraGrid.AddTreeviewComBoBox(strArray[0],
                                                codeDomainEx.FindName(strArray[1]), codeDomainEx,
                                                !this.FieldCanEdit(pField));
                                        }
                                    }
                                    else
                                    {
                                        if ((((pField.Type == esriFieldType.esriFieldTypeSmallInteger) ||
                                              (pField.Type == esriFieldType.esriFieldTypeSingle)) ||
                                             (pField.Type == esriFieldType.esriFieldTypeDouble)) ||
                                            (pField.Type == esriFieldType.esriFieldTypeInteger))
                                        {
                                            if (pField.Editable)
                                            {
                                                this.m_pVertXtraGrid.AddSpinEdit(strArray[0], obj2, false, 0.0, 0.0);
                                            }
                                            else
                                            {
                                                this.m_pVertXtraGrid.AddTextEdit(strArray[0], obj2, true);
                                            }
                                        }
                                        else if (pField.Type == esriFieldType.esriFieldTypeDate)
                                        {
                                            this.m_pVertXtraGrid.AddDateEdit(strArray[0], obj2,
                                                !this.FieldCanEdit(pField));
                                        }
                                        else
                                        {
                                            strArray[1] = obj2.ToString();
                                            this.m_pVertXtraGrid.AddTextEdit(strArray[0], strArray[1],
                                                !this.FieldCanEdit(pField));
                                        }
                                        if (strArray[0] == this.m_EditFeildName)
                                        {
                                            rowHandle = i;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (rowHandle >= 0)
                {
                    this.gridView1.SelectRow(rowHandle);
                }
                this.gridView1.Focus();
                this.m_CanDo = true;
                base.Parent.Focus();
            }
        }

        private void InitAttachment()
        {
            this.toolStripDropDownButton1.DropDownItems.Clear();
            if (this.m_pObject.Class is ITableAttachments)
            {
                ITableAttachments attachments = (ITableAttachments) this.m_pObject.Class;
                if (attachments.HasAttachments)
                {
                    ToolStripItem item;
                    IAttachmentManager attachmentManager = attachments.AttachmentManager;
                    ILongArray oids = new LongArrayClass();
                    oids.Add(this.m_pObject.OID);
                    IEnumAttachment attachmentsByParentIDs = attachmentManager.GetAttachmentsByParentIDs(oids, false);
                    attachmentsByParentIDs.Reset();
                    IAttachment attachment2 = null;
                    string[] strArray = new string[2];
                    int num = 0;
                    while ((attachment2 = attachmentsByParentIDs.Next()) != null)
                    {
                        item = new ToolStripButton(attachment2.Name)
                        {
                            Tag = attachment2
                        };
                        this.toolStripDropDownButton1.DropDownItems.Add(item);
                        num++;
                    }
                    item = new ToolStripButton("打开附件管理器");
                    this.toolStripDropDownButton1.DropDownItems.Add(item);
                    this.toolAttachmentLabel.Text = string.Format("附件({0})", num);
                }
            }
        }

        private void toolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag == null)
            {
                new frmAttachment {Object = this.m_pObject}.ShowDialog();
            }
            else
            {
                MessageBox.Show(e.ClickedItem.Text);
            }
        }

        private bool UpdateFieldValue(IField pField, object str)
        {
            if (this.m_pObject == null)
            {
                return false;
            }
            IWorkspaceEdit workspace = (this.m_pObject.Class as IDataset).Workspace as IWorkspaceEdit;
            bool flag = false;
            try
            {
                workspace.StartEditOperation();
                int index = this.m_pObject.Fields.FindField(pField.Name);
                this.m_pObject.set_Value(index, str);
                this.m_pObject.Store();
                workspace.StopEditOperation();
                try
                {
                    if ((this.m_pObject is IFeature) && ((this.m_pObject as IFeature).Shape != null))
                    {
                        this.m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null,
                            (this.m_pObject as IFeature).Shape.Envelope);
                    }
                }
                catch
                {
                }
                flag = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("输入数据格式错误");
                CErrorLog.writeErrorLog(null, exception, "");
            }
            return flag;
        }

        private void UpdateGrid()
        {
            IFields fields = this.m_pObject.Fields;
            ISubtypes subtypes = this.m_pObject.Class as ISubtypes;
            IDomain domain = null;
            ICodedValueDomain domain2 = null;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                IField field = fields.get_Field(i);
                if (((((field.Type != esriFieldType.esriFieldTypeGeometry) &&
                       (field.Type != esriFieldType.esriFieldTypeOID)) &&
                      (field.Type != esriFieldType.esriFieldTypeRaster)) && field.Editable) &&
                    (subtypes.SubtypeFieldName != field.Name))
                {
                    domain = subtypes.get_Domain((this.m_pObject as IRowSubtypes).SubtypeCode, field.Name);
                    if (domain is ICodedValueDomain)
                    {
                        IList list = new ArrayList();
                        domain2 = domain as ICodedValueDomain;
                        if (field.IsNullable)
                        {
                            list.Add("<空>");
                        }
                        for (int j = 0; j < domain2.CodeCount; j++)
                        {
                            list.Add(domain2.get_Name(j));
                        }
                        this.m_pVertXtraGrid.ChangeItem(i, ColumnAttribute.CA_COMBOBOX, list, 0.0, 0.0);
                    }
                    else if ((((field.Type == esriFieldType.esriFieldTypeSmallInteger) ||
                               (field.Type == esriFieldType.esriFieldTypeSingle)) ||
                              (field.Type == esriFieldType.esriFieldTypeDouble)) ||
                             (field.Type == esriFieldType.esriFieldTypeInteger))
                    {
                        double minValue = 0.0;
                        double maxValue = 0.0;
                        if (domain is IRangeDomain)
                        {
                            minValue = (double) (domain as IRangeDomain).MinValue;
                            maxValue = (double) (domain as IRangeDomain).MaxValue;
                            this.m_pVertXtraGrid.ChangeItem(i, ColumnAttribute.CA_SPINEDIT, null, 0.0, 0.0);
                        }
                        else
                        {
                            this.m_pVertXtraGrid.ChangeItem(i, ColumnAttribute.CA_TEXTEDIT, null, 0.0, 0.0);
                        }
                    }
                    else
                    {
                        this.m_pVertXtraGrid.ChangeItem(i, ColumnAttribute.CA_TEXTEDIT, null, 0.0, 0.0);
                    }
                }
            }
        }

        public IActiveView ActiveView
        {
            set { this.m_pActiveView = value; }
        }

        public IFeatureLayer FeatureLayer
        {
            set { this.m_pFeatureLayer = value; }
        }

        public List<string> LockFields { get; set; }

        public IObject SelectObject
        {
            set
            {
                this.m_pObject = value;
                if (this.m_pObject == null)
                {
                    this.m_pVertXtraGrid.Clear();
                }
                else if (this.m_CanDo)
                {
                    this.m_CanDo = false;
                    this.Init();
                    this.m_CanDo = true;
                }
            }
        }
    }
}