using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

using Yutai.ArcGIS.Common.Symbol;
using Yutai.Shared;
using Array = System.Array;
using IBStringArray = Yutai.ArcGIS.Common.Symbol.IBStringArray;


public class MyStyleGallery : IStyleGallery, IStyleGalleryStorage
{
    // Fields
    private IArray iarray_0 = new ESRI.ArcGIS.esriSystem.Array();
    private IStyleGalleryClass[] istyleGalleryClass_0 = new IStyleGalleryClass[] {
        new MyMarkerSymbolStyleGalleryClass(), new MyLineSymbolStyleGalleryClass(), new MyFillSymbolStyleGalleryClass(), new MyColorSymbolStyleGalleryClass(), new MyTextSymbolStyleGalleryClass(), new MyColorRampsSymbolStyleGalleryClass(), new MyNorthArrowStyleGalleryClass(), new MyScaleBarStyleGalleryClass(), new MyScaleTextStyleGalleryClass(), new MyLinePatchStyleGalleryClass(), new MyAreaPatchStyleGalleryClass(), new MyBorderStyleGalleryClass(), new MyBackgroundStyleGalleryClass(), new MyShadowStyleGalleryClass(), new MyLegendItemStyleGalleryClass(), new MyMapGridStyleGalleryClass(),
        new MyLabelStyleGalleryClass(), new RepresentationMarkerStyleGalleryClass(), new RepresentationRuleStyleGalleryClass()
     };
    private string string_0 = "";

    // Methods
    public void AddFile(string string_1)
    {
        this.string_0 = string_1;
        this.iarray_0.Add(string_1);
    }

    public void AddItem(IStyleGalleryItem istyleGalleryItem_0)
    {
        if (!(this.string_0 == ""))
        {
            string str = this.method_2(istyleGalleryItem_0);
            if (str.Length != 0)
            {
                Guid guid;
                object obj2;
                IPersistStream item = (IPersistStream)istyleGalleryItem_0.Item;
                IMemoryBlobStream pstm = new MemoryBlobStream();
                IObjectStream stream3 = new ObjectStream
                {
                    Stream = pstm
                };
                item.GetClassID(out guid);
                item.Save(pstm, 1);
                ((IMemoryBlobStreamVariant)pstm).ExportToVariant(out obj2);
                System.Array array = (System.Array)obj2;
                byte[] buffer = new byte[array.Length + 0x10];
                guid.ToByteArray().CopyTo(buffer, 0);
                array.CopyTo(buffer, 0x10);
                obj2 = buffer;
                OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + this.string_0);
                connection.Open();
                OleDbCommand command = this.method_5(str, connection);
                OleDbParameterCollection parameters = command.Parameters;
                parameters["Name"].Value = istyleGalleryItem_0.Name;
                parameters["Category"].Value = istyleGalleryItem_0.Category;
                parameters["Object"].Value = obj2;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public void Clear()
    {
    }

    public void ImportStyle(string string_1)
    {
    }

    public void LoadStyle(string string_1, string string_2)
    {
    }

    private object method_0(Guid guid_0)
    {
        return Activator.CreateInstance(Type.GetTypeFromCLSID(guid_0));
    }

    private object method_1(string string_1)
    {
        string str = string_1;
        switch (str)
        {
            case null:
                break;

            case "Marker Symbols":
                return new MultiLayerMarkerSymbol();

            default:
                if (!(str == "Line Symbols"))
                {
                    if (str == "Fill Symbols")
                    {
                        return new MultiLayerFillSymbol();
                    }
                }
                else
                {
                    return new MultiLayerLineSymbol();
                }
                break;
        }
        return null;
    }

    private string method_2(IStyleGalleryItem istyleGalleryItem_0)
    {
        if (istyleGalleryItem_0.Item is IMarkerSymbol)
        {
            return "Marker Symbols";
        }
        if (istyleGalleryItem_0.Item is ILineSymbol)
        {
            return "Line Symbols";
        }
        if (istyleGalleryItem_0.Item is IFillSymbol)
        {
            return "Fill Symbols";
        }
        if (istyleGalleryItem_0.Item is IColor)
        {
            return "Colors";
        }
        if (istyleGalleryItem_0.Item is ISymbolBorder)
        {
            return "Borders";
        }
        if (istyleGalleryItem_0.Item is ISymbolShadow)
        {
            return "Shadows";
        }
        if (istyleGalleryItem_0.Item is ITextSymbol)
        {
            return "Text Symbols";
        }
        if (istyleGalleryItem_0.Item is ISymbolBackground)
        {
            return "Backgrounds";
        }
        if (istyleGalleryItem_0.Item is ILegendItem)
        {
            return "Legend Items";
        }
        if (istyleGalleryItem_0.Item is INorthArrow)
        {
            return "North Arrows";
        }
        if (istyleGalleryItem_0.Item is IScaleBar)
        {
            return "Scale Bars";
        }
        if (istyleGalleryItem_0.Item is IScaleText)
        {
            return "Scale Texts";
        }
        if (istyleGalleryItem_0.Item is IRepresentationMarker)
        {
            return "Representation Markers";
        }
        if (istyleGalleryItem_0.Item is IRepresentationRuleItem)
        {
            return "Representation Rules";
        }
        if (istyleGalleryItem_0.Item is IColorRamp)
        {
            return "Color Ramps";
        }
        return "";
    }

    private void method_3(string string_1, string string_2, IList ilist_0)
    {
        Exception exception;
        try
        {
            OleDbConnection selectConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + string_2);
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [" + string_1 + "]", selectConnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            selectConnection.Close();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                try
                {
                    IStyleGalleryItem item = new MyStyleGalleryItem();
                    ((MyStyleGalleryItem)item).ItemID = (int)dataSet.Tables[0].Rows[i]["ID"];
                    item.Name = (string)dataSet.Tables[0].Rows[i]["Name"];
                    item.Category = (string)dataSet.Tables[0].Rows[i]["Category"];
                    IMemoryBlobStream pstm = new MemoryBlobStream();
                    object obj2 = dataSet.Tables[0].Rows[i]["Object"];
                    Array array = (Array)obj2;
                    int num2 = array.Length - 0x10;
                    byte[] b = new byte[0x10];
                    int index = 0;
                    while (index < 0x10)
                    {
                        b[index] = (byte)array.GetValue(index);
                        index++;
                    }
                    Guid guid = new Guid(b);
                    IPersistStream stream2 = (IPersistStream)this.method_0(guid);
                    byte[] buffer2 = new byte[num2];
                    for (index = 0; index < num2; index++)
                    {
                        buffer2[index] = (byte)array.GetValue((int)(index + 0x10));
                    }
                    ((IMemoryBlobStreamVariant)pstm).ImportFromVariant(buffer2);
                    stream2.Load(pstm);
                    item.Item = stream2;
                    ilist_0.Add(item);
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    Logger.Current.Error("", exception, "");
                }
            }
        }
        catch (Exception exception2)
        {
            exception = exception2;
            Logger.Current.Error("", exception, "");
            MessageBox.Show(exception.Message);
        }
    }

    private void method_4(string string_1, string string_2, string string_3, IList ilist_0)
    {
        try
        {
            OleDbConnection selectConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + string_2);
            IDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [" + string_1 + "] Where Category = '" + string_3 + "'", selectConnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            selectConnection.Close();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                IStyleGalleryItem item = new MyStyleGalleryItem();
                ((MyStyleGalleryItem)item).ItemID = (int)dataSet.Tables[0].Rows[i]["ID"];
                item.Name = (string)dataSet.Tables[0].Rows[i]["Name"];
                item.Category = (string)dataSet.Tables[0].Rows[i]["Category"];
                IMemoryBlobStream pstm = new MemoryBlobStream();
                object obj2 = dataSet.Tables[0].Rows[i]["Object"];
                Array array = (Array)obj2;
                int num2 = array.Length - 0x10;
                byte[] b = new byte[0x10];
                int index = 0;
                while (index < 0x10)
                {
                    b[index] = (byte)array.GetValue(index);
                    index++;
                }
                Guid guid = new Guid(b);
                IPersistStream stream2 = (IPersistStream)this.method_0(guid);
                byte[] buffer2 = new byte[num2];
                for (index = 0; index < num2; index++)
                {
                    buffer2[index] = (byte)array.GetValue((int)(index + 0x10));
                }
                ((IMemoryBlobStreamVariant)pstm).ImportFromVariant(buffer2);
                stream2.Load(pstm);
                item.Item = stream2;
                ilist_0.Add(item);
            }
        }
        catch
        {
        }
    }

    private OleDbCommand method_5(string string_1, OleDbConnection oleDbConnection_0)
    {
        OleDbCommand command = new OleDbCommand("INSERT INTO [" + string_1 + "] ([Name], [Category], [Object])  Values (?, ?, ?)", oleDbConnection_0);
        OleDbParameterCollection parameters = command.Parameters;
        parameters.Add("Name", OleDbType.Char);
        parameters.Add("Category", OleDbType.Char);
        parameters.Add("Object", OleDbType.Binary);
        return command;
    }

    private OleDbCommand method_6(string string_1, OleDbConnection oleDbConnection_0)
    {
        OleDbCommand command = new OleDbCommand("Update [" + string_1 + "] Set [Name] = ?, [Category] = ?, [Object] = ? Where [ID] = ?", oleDbConnection_0);
        OleDbParameterCollection parameters = command.Parameters;
        parameters.Add("Name", OleDbType.Char);
        parameters.Add("Category", OleDbType.Char);
        parameters.Add("Object", OleDbType.Binary);
        parameters.Add("ID", OleDbType.Integer);
        return command;
    }

    private OleDbCommand method_7(string string_1, OleDbConnection oleDbConnection_0)
    {
        OleDbCommand command = new OleDbCommand("Delete From [" + string_1 + "] Where [ID] = ?", oleDbConnection_0);
        command.Parameters.Add("ID", OleDbType.Integer);
        return command;
    }

    public void RemoveFile(string string_1)
    {
        for (int i = 0; i < this.iarray_0.Count; i++)
        {
            string str = (string)this.iarray_0.get_Element(i);
            if (str.ToUpper() == string_1.ToUpper())
            {
                this.iarray_0.Remove(i);
                break;
            }
        }
    }

    public void RemoveItem(IStyleGalleryItem istyleGalleryItem_0)
    {
        if (!(this.string_0 == ""))
        {
            string str = this.method_2(istyleGalleryItem_0);
            if ((str.Length != 0) && (istyleGalleryItem_0.ID != -1))
            {
                Guid guid;
                object obj2;
                IPersistStream item = (IPersistStream)istyleGalleryItem_0.Item;
                IMemoryBlobStream pstm = new MemoryBlobStream();
                IObjectStream stream3 = new ObjectStream
                {
                    Stream = pstm
                };
                item.GetClassID(out guid);
                item.Save(pstm, 1);
                ((IMemoryBlobStreamVariant)pstm).ExportToVariant(out obj2);
                Array array = (Array)obj2;
                byte[] buffer = new byte[array.Length + 0x10];
                guid.ToByteArray().CopyTo(buffer, 0);
                array.CopyTo(buffer, 0x10);
                obj2 = buffer;
                OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + this.string_0);
                connection.Open();
                OleDbCommand command = this.method_7(str, connection);
                command.Parameters["ID"].Value = istyleGalleryItem_0.ID;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public void SaveStyle(string string_1, string string_2, string string_3)
    {
    }

    public void UpdateItem(IStyleGalleryItem istyleGalleryItem_0)
    {
        if (!(this.string_0 == ""))
        {
            string str = this.method_2(istyleGalleryItem_0);
            if ((str.Length != 0) && (istyleGalleryItem_0.ID != -1))
            {
                Guid guid;
                object obj2;
                IPersistStream item = (IPersistStream)istyleGalleryItem_0.Item;
                IMemoryBlobStream pstm = new MemoryBlobStream();
                IObjectStream stream3 = new ObjectStream
                {
                    Stream = pstm
                };
                item.GetClassID(out guid);
                item.Save(pstm, 1);
                ((IMemoryBlobStreamVariant)pstm).ExportToVariant(out obj2);
                System.Array array = (System.Array)obj2;
                byte[] buffer = new byte[array.Length + 0x10];
                guid.ToByteArray().CopyTo(buffer, 0);
                array.CopyTo(buffer, 0x10);
                obj2 = buffer;
                OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + this.string_0);
                connection.Open();
                OleDbCommand command = this.method_6(str, connection);
                OleDbParameterCollection parameters = command.Parameters;
                parameters["ID"].Value = istyleGalleryItem_0.ID;
                parameters["Name"].Value = istyleGalleryItem_0.Name;
                parameters["Category"].Value = istyleGalleryItem_0.Category;
                parameters["Object"].Value = obj2;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    // Properties
    public bool this[string string_1]
    {
        get
        {
            return (this.TargetFile.Length > 3);
        }
    }

    //public IEnumBSTR this[string string_1]
    //{
    //    get
    //    {

    //        if (string_1 == null) throw new ArgumentNullException(nameof(string_1));
    //        IEnumBSTR mbstr = new EnumBSTRClass();
    //        if (this.iarray_0.Count != 0)
    //        {
    //            try
    //            {
    //                int index = 0;
    //                Label_0022:
    //                if (index >= this.iarray_0.Count)
    //                {
    //                    return mbstr;
    //                }
    //                string str = this.iarray_0.get_Element(index).ToString();
    //                OleDbConnection selectConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + str);
    //                selectConnection.Open();
    //                IDbDataAdapter adapter = new OleDbDataAdapter("SELECT DISTINCT(Category) FROM [" + string_1 + "]", selectConnection);
    //                DataSet dataSet = new DataSet();
    //                adapter.Fill(dataSet);
    //                selectConnection.Close();
    //                int num2 = 0;
    //                while (true)
    //                {
    //                    if (num2 < dataSet.Tables[0].Rows.Count)
    //                    {
    //                        try
    //                        {
    //                            (mbstr as IBStringArray).AddString(dataSet.Tables[0].Rows[num2][0].ToString());
    //                        }
    //                        catch
    //                        {
    //                        }
    //                    }
    //                    else
    //                    {
    //                        index++;
    //                        goto Label_0022;
    //                    }
    //                    num2++;
    //                }
    //            }
    //            catch
    //            {
    //            }
    //        }
    //        return mbstr;
    //    }

    //}

    //public IStyleGalleryClass this[int int_0]
    //{
    //    get
    //    {
    //        if ((int_0 < 0) || (int_0 >= this.istyleGalleryClass_0.Length))
    //        {
    //            return null;
    //        }
    //        return this.istyleGalleryClass_0[int_0];
    //    }
    //}

    public int ClassCount
    {
        get
        {
            return this.istyleGalleryClass_0.Length;

        }
    }

    public IStyleGalleryClass get_Class(int int_0)
    {
        if ((int_0 < 0) || (int_0 >= this.istyleGalleryClass_0.Length))
        {
            return null;
        }
        return this.istyleGalleryClass_0[int_0];

    }

    public IEnumBSTR get_Categories(string string_1)
    {
        IEnumBSTR mbstr = (IEnumBSTR) new EnumBSTRClass();
        if (this.iarray_0.Count != 0)
        {
            try
            {
                int index = 0;
                Label_0022:
                if (index >= this.iarray_0.Count)
                {
                    return mbstr;
                }
                string str = this.iarray_0.get_Element(index).ToString();
                OleDbConnection selectConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + str);
                selectConnection.Open();
                IDbDataAdapter adapter = new OleDbDataAdapter("SELECT DISTINCT(Category) FROM [" + string_1 + "]", selectConnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                selectConnection.Close();
                int num2 = 0;
                while (true)
                {
                    if (num2 < dataSet.Tables[0].Rows.Count)
                    {
                        try
                        {
                            (mbstr as IBStringArray).AddString(dataSet.Tables[0].Rows[num2][0].ToString());
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        index++;
                        goto Label_0022;
                    }
                    num2++;
                }
            }
            catch
            {
            }
        }
        return mbstr;

    }

    public IEnumStyleGalleryItem get_Items(string ClassName, string styleSet, string category)
    {
        styleSet = styleSet.Trim();
        category = category.Trim();
        IList list = new ArrayList();
        try
        {
            if (styleSet.Length == 0)
            {
                for (int i = 0; i < this.iarray_0.Count; i++)
                {
                    string str = (string)this.iarray_0.get_Element(i);
                    if (category.Length == 0)
                    {
                        this.method_3(ClassName, str, list);
                    }
                    else
                    {
                        this.method_4(ClassName, str, category, list);
                    }
                }
            }
            else if (category.Length == 0)
            {
                this.method_3(ClassName, styleSet, list);
            }
            else
            {
                this.method_4(ClassName, styleSet, category, list);
            }
        }
        catch
        {
            MessageBox.Show("无法识别的符号库文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        return new MyEnumStyleGalleryItem { m_pEnumerator = list.GetEnumerator() };

    }

    public string DefaultStylePath
    {
        get
        {
            return null;
        }
    }

    public string this[int int_0]
    {
        get
        {
            return (this.iarray_0.get_Element(int_0) as string);
        }
    }

    public int FileCount
    {
        get
        {
            return this.iarray_0.Count;
        }
    }

    public string get_File(int index)
    {
        throw new NotImplementedException();
    }

    public IEnumStyleGalleryItem this[string string_1, string string_2, string string_3]
    {
        get
        {
            string_2 = string_2.Trim();
            string_3 = string_3.Trim();
            IList list = new ArrayList();
            try
            {
                if (string_2.Length == 0)
                {
                    for (int i = 0; i < this.iarray_0.Count; i++)
                    {
                        string str = (string)this.iarray_0.get_Element(i);
                        if (string_3.Length == 0)
                        {
                            this.method_3(string_1, str, list);
                        }
                        else
                        {
                            this.method_4(string_1, str, string_3, list);
                        }
                    }
                }
                else if (string_3.Length == 0)
                {
                    this.method_3(string_1, string_2, list);
                }
                else
                {
                    this.method_4(string_1, string_2, string_3, list);
                }
            }
            catch
            {
                MessageBox.Show("无法识别的符号库文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return new MyEnumStyleGalleryItem { m_pEnumerator = list.GetEnumerator() };
        }
    }

    public bool get_CanUpdate(string path)
    {
        return true;
    }

    public string TargetFile
    {
        get
        {
            return this.string_0;
        }
        set
        {
            this.string_0 = value;
        }
    }
}




