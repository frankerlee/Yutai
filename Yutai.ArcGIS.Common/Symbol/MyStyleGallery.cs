using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geoprocessing;
using Yutai.Shared;
using Array = System.Array;


namespace Yutai.ArcGIS.Common.Symbol
{
   
        public class MyStyleGallery : IStyleGallery, IStyleGalleryStorage
        {
            private string string_0 = "";

            private IStyleGalleryClass[] istyleGalleryClass_0 = new IStyleGalleryClass[] { new MyMarkerSymbolStyleGalleryClass(), new MyLineSymbolStyleGalleryClass(), new MyFillSymbolStyleGalleryClass(), new MyColorSymbolStyleGalleryClass(), new MyTextSymbolStyleGalleryClass(), new MyColorRampsSymbolStyleGalleryClass(), new MyNorthArrowStyleGalleryClass(), new MyScaleBarStyleGalleryClass(), new MyScaleTextStyleGalleryClass(), new MyLinePatchStyleGalleryClass(), new MyAreaPatchStyleGalleryClass(), new MyBorderStyleGalleryClass(), new MyBackgroundStyleGalleryClass(), new MyShadowStyleGalleryClass(), new MyLegendItemStyleGalleryClass(), new MyMapGridStyleGalleryClass(), new MyLabelStyleGalleryClass(), new RepresentationMarkerStyleGalleryClass(), new RepresentationRuleStyleGalleryClass() };

            private IArray iarray_0 = new ESRI.ArcGIS.esriSystem.Array() as IArray;

            public bool CanUpdate(string string_1)
            {
                
                    bool flag;
                    flag = (this.TargetFile.Length <= 3 ? false : true);
                    return flag;
               
            }

            public IEnumBSTR Categories(string string_1)
            {

                IEnumBSTR enumBSTR;
                IEnumBSTR enumBSTRClass = new EnumBSTRClass();
                if (this.iarray_0.Count != 0)
                {
                    try
                    {
                        for (int i = 0; i < this.iarray_0.Count; i++)
                        {
                            string str = this.iarray_0.Element[i].ToString();
                            OleDbConnection oleDbConnection =
                                new OleDbConnection(string.Concat("Provider=Microsoft.Jet.OLEDB.4.0;Data source= ", str));
                            oleDbConnection.Open();
                            string str1 = string.Concat("SELECT DISTINCT(Category) FROM [", string_1, "]");
                            IDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(str1, oleDbConnection);
                            DataSet dataSet = new DataSet();
                            oleDbDataAdapter.Fill(dataSet);
                            oleDbConnection.Close();
                            for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
                            {
                                try
                                {
                                    (enumBSTRClass as IBStringArray).AddString(dataSet.Tables[0].Rows[j][0].ToString());
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    enumBSTR = enumBSTRClass;
                }
                else
                {
                    enumBSTR = enumBSTRClass;
                }
                return enumBSTR;
            }

            public IStyleGalleryClass Class(int int_0)
            {
                
                    IStyleGalleryClass istyleGalleryClass0;
                    if ((int_0 < 0 ? false : int_0 < (int)this.istyleGalleryClass_0.Length))
                    {
                        istyleGalleryClass0 = this.istyleGalleryClass_0[int_0];
                    }
                    else
                    {
                        istyleGalleryClass0 = null;
                    }
                    return istyleGalleryClass0;
               
            }

            public int ClassCount
            {
                get
                {
                    return (int)this.istyleGalleryClass_0.Length;
                }
            }

            public IStyleGalleryClass get_Class(int index)
            {
            IStyleGalleryClass istyleGalleryClass0;
            if ((index < 0 ? false : index < (int)this.istyleGalleryClass_0.Length))
            {
                istyleGalleryClass0 = this.istyleGalleryClass_0[index];
            }
            else
            {
                istyleGalleryClass0 = null;
            }
            return istyleGalleryClass0;

        }

        public IEnumBSTR get_Categories(string ClassName)
            {
            IEnumBSTR enumBSTR;
            IEnumBSTR enumBSTRClass = new EnumBSTRClass();
            if (this.iarray_0.Count != 0)
            {
                try
                {
                    for (int i = 0; i < this.iarray_0.Count; i++)
                    {
                        string str = this.iarray_0.Element[i].ToString();
                        OleDbConnection oleDbConnection =
                            new OleDbConnection(string.Concat("Provider=Microsoft.Jet.OLEDB.4.0;Data source= ", str));
                        oleDbConnection.Open();
                        string str1 = string.Concat("SELECT DISTINCT(Category) FROM [", ClassName, "]");
                        IDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(str1, oleDbConnection);
                        DataSet dataSet = new DataSet();
                        oleDbDataAdapter.Fill(dataSet);
                        oleDbConnection.Close();
                        for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
                        {
                            try
                            {
                                (enumBSTRClass as IBStringArray).AddString(dataSet.Tables[0].Rows[j][0].ToString());
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                catch
                {
                }
                enumBSTR = enumBSTRClass;
            }
            else
            {
                enumBSTR = enumBSTRClass;
            }
            return enumBSTR;
        }

            public IEnumStyleGalleryItem get_Items(string ClassName, string styleSet, string Category)
            {
                throw new NotImplementedException();
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
                    return this.iarray_0.Element[int_0] as string;
                }
            }

            public int FileCount
            {
                get
                {
                    return this.iarray_0.Count;
                }
            }

            public IEnumStyleGalleryItem this[string string_1, string string_2, string string_3]
            {
                get
                {
                    string_2 = string_2.Trim();
                    string_3 = string_3.Trim();
                    IList arrayLists = new ArrayList();
                    try
                    {
                        if (string_2.Length == 0)
                        {
                            for (int i = 0; i < this.iarray_0.Count; i++)
                            {
                                string element = (string)this.iarray_0.Element[i].ToString();
                                if (string_3.Length != 0)
                                {
                                    this.method_4(string_1, element, string_3, arrayLists);
                                }
                                else
                                {
                                    this.method_3(string_1, element, arrayLists);
                                }
                            }
                        }
                        else if (string_3.Length != 0)
                        {
                            this.method_4(string_1, string_2, string_3, arrayLists);
                        }
                        else
                        {
                            this.method_3(string_1, string_2, arrayLists);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("无法识别的符号库文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    return new MyEnumStyleGalleryItem()
                    {
                        m_pEnumerator = arrayLists.GetEnumerator()
                    };
                }
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

            public MyStyleGallery()
            {
            }

            public void AddFile(string string_1)
            {
                this.string_0 = string_1;
                this.iarray_0.Add(string_1);
            }

            public void AddItem(IStyleGalleryItem istyleGalleryItem_0)
            {
                Guid guid;
                object obj;
                if (this.string_0 != "")
                {
                    string str = this.method_2(istyleGalleryItem_0);
                    if (str.Length != 0)
                    {
                        IPersistStream item = (IPersistStream)istyleGalleryItem_0.Item;
                        IMemoryBlobStream memoryBlobStreamClass = new MemoryBlobStream();
                        (new ObjectStream()).Stream = memoryBlobStreamClass;
                        item.GetClassID(out guid);
                        item.Save(memoryBlobStreamClass, 1);
                        ((IMemoryBlobStreamVariant)memoryBlobStreamClass).ExportToVariant(out obj);
                        Array arrays = (Array)obj;
                        byte[] numArray = new byte[arrays.Length + 16];
                        guid.ToByteArray().CopyTo(numArray, 0);
                        arrays.CopyTo(numArray, 16);
                        obj = numArray;
                        OleDbConnection oleDbConnection = new OleDbConnection(string.Concat("Provider=Microsoft.Jet.OLEDB.4.0;Data source= ", this.string_0));
                        oleDbConnection.Open();
                        OleDbCommand oleDbCommand = this.method_5(str, oleDbConnection);
                        OleDbParameterCollection parameters = oleDbCommand.Parameters;
                        parameters["Name"].Value = istyleGalleryItem_0.Name;
                        parameters["Category"].Value = istyleGalleryItem_0.Category;
                        parameters["Object"].Value = obj;
                        oleDbCommand.ExecuteNonQuery();
                        oleDbConnection.Close();
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
                object multiLayerMarkerSymbolClass;
                string string1 = string_1;
                if (string1 != null)
                {
                    if (string1 == "Marker Symbols")
                    {
                        multiLayerMarkerSymbolClass = new MultiLayerMarkerSymbol();
                        return multiLayerMarkerSymbolClass;
                    }
                    else if (string1 == "Line Symbols")
                    {
                        multiLayerMarkerSymbolClass = new MultiLayerLineSymbol();
                        return multiLayerMarkerSymbolClass;
                    }
                    else
                    {
                        if (string1 != "Fill Symbols")
                        {
                            multiLayerMarkerSymbolClass = null;
                            return multiLayerMarkerSymbolClass;
                        }
                        multiLayerMarkerSymbolClass = new MultiLayerFillSymbol();
                        return multiLayerMarkerSymbolClass;
                    }
                }
                multiLayerMarkerSymbolClass = null;
                return multiLayerMarkerSymbolClass;
            }

            private string method_2(IStyleGalleryItem istyleGalleryItem_0)
            {
                string str;
                if (istyleGalleryItem_0.Item is IMarkerSymbol)
                {
                    str = "Marker Symbols";
                }
                else if (istyleGalleryItem_0.Item is ILineSymbol)
                {
                    str = "Line Symbols";
                }
                else if (istyleGalleryItem_0.Item is IFillSymbol)
                {
                    str = "Fill Symbols";
                }
                else if (istyleGalleryItem_0.Item is IColor)
                {
                    str = "Colors";
                }
                else if (istyleGalleryItem_0.Item is ISymbolBorder)
                {
                    str = "Borders";
                }
                else if (istyleGalleryItem_0.Item is ISymbolShadow)
                {
                    str = "Shadows";
                }
                else if (istyleGalleryItem_0.Item is ITextSymbol)
                {
                    str = "Text Symbols";
                }
                else if (istyleGalleryItem_0.Item is ISymbolBackground)
                {
                    str = "Backgrounds";
                }
                else if (istyleGalleryItem_0.Item is ILegendItem)
                {
                    str = "Legend Items";
                }
                else if (istyleGalleryItem_0.Item is INorthArrow)
                {
                    str = "North Arrows";
                }
                else if (istyleGalleryItem_0.Item is IScaleBar)
                {
                    str = "Scale Bars";
                }
                else if (istyleGalleryItem_0.Item is IScaleText)
                {
                    str = "Scale Texts";
                }
                else if (istyleGalleryItem_0.Item is IRepresentationMarker)
                {
                    str = "Representation Markers";
                }
                else if (!(istyleGalleryItem_0.Item is IRepresentationRuleItem))
                {
                    str = (!(istyleGalleryItem_0.Item is IColorRamp) ? "" : "Color Ramps");
                }
                else
                {
                    str = "Representation Rules";
                }
                return str;
            }

            private void method_3(string string_1, string string_2, IList ilist_0)
            {
                int j;
                Exception exception;
                try
                {
                    OleDbConnection oleDbConnection = new OleDbConnection(string.Concat("Provider=Microsoft.Jet.OLEDB.4.0;Data source= ", string_2));
                    string str = string.Concat("SELECT * FROM [", string_1, "]");
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(str, oleDbConnection);
                    DataSet dataSet = new DataSet();
                    oleDbDataAdapter.Fill(dataSet);
                    oleDbConnection.Close();
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        try
                        {
                            IStyleGalleryItem myStyleGalleryItem = new MyStyleGalleryItem();
                            ((MyStyleGalleryItem)myStyleGalleryItem).ItemID = (int)dataSet.Tables[0].Rows[i]["ID"];
                            myStyleGalleryItem.Name = (string)dataSet.Tables[0].Rows[i]["Name"];
                            myStyleGalleryItem.Category = (string)dataSet.Tables[0].Rows[i]["Category"];
                            IMemoryBlobStream memoryBlobStreamClass = new MemoryBlobStream();
                            object item = dataSet.Tables[0].Rows[i]["Object"];
                            Array arrays = (Array)item;
                            int length = arrays.Length - 16;
                            byte[] value = new byte[16];
                            for (j = 0; j < 16; j++)
                            {
                                value[j] = (byte)arrays.GetValue(j);
                            }
                            IPersistStream persistStream = (IPersistStream)this.method_0(new Guid(value));
                            byte[] numArray = new byte[length];
                            for (j = 0; j < length; j++)
                            {
                                numArray[j] = (byte)arrays.GetValue(j + 16);
                            }
                            ((IMemoryBlobStreamVariant)memoryBlobStreamClass).ImportFromVariant(numArray);
                            persistStream.Load(memoryBlobStreamClass);
                            myStyleGalleryItem.Item = persistStream;
                            ilist_0.Add(myStyleGalleryItem);
                        }
                        catch (Exception exception1)
                        {
                            exception = exception1;
                           // CErrorLog.writeErrorLog(this, exception, "");
                        }
                    }
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    //CErrorLog.writeErrorLog(this, exception, "");
                    MessageBox.Show(exception.Message);
                }
            }

            private void method_4(string string_1, string string_2, string string_3, IList ilist_0)
            {
                int j;
                try
                {
                    OleDbConnection oleDbConnection = new OleDbConnection(string.Concat("Provider=Microsoft.Jet.OLEDB.4.0;Data source= ", string_2));
                    string[] string1 = new string[] { "SELECT * FROM [", string_1, "] Where Category = '", string_3, "'" };
                    IDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(string.Concat(string1), oleDbConnection);
                    DataSet dataSet = new DataSet();
                    oleDbDataAdapter.Fill(dataSet);
                    oleDbConnection.Close();
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        IStyleGalleryItem myStyleGalleryItem = new MyStyleGalleryItem();
                        ((MyStyleGalleryItem)myStyleGalleryItem).ItemID = (int)dataSet.Tables[0].Rows[i]["ID"];
                        myStyleGalleryItem.Name = (string)dataSet.Tables[0].Rows[i]["Name"];
                        myStyleGalleryItem.Category = (string)dataSet.Tables[0].Rows[i]["Category"];
                        IMemoryBlobStream memoryBlobStreamClass = new MemoryBlobStream();
                        object item = dataSet.Tables[0].Rows[i]["Object"];
                        Array arrays = (Array)item;
                        int length = arrays.Length - 16;
                        byte[] value = new byte[16];
                        for (j = 0; j < 16; j++)
                        {
                            value[j] = (byte)arrays.GetValue(j);
                        }
                        IPersistStream persistStream = (IPersistStream)this.method_0(new Guid(value));
                        byte[] numArray = new byte[length];
                        for (j = 0; j < length; j++)
                        {
                            numArray[j] = (byte)arrays.GetValue(j + 16);
                        }
                        ((IMemoryBlobStreamVariant)memoryBlobStreamClass).ImportFromVariant(numArray);
                        persistStream.Load(memoryBlobStreamClass);
                        myStyleGalleryItem.Item = persistStream;
                        ilist_0.Add(myStyleGalleryItem);
                    }
                }
                catch
                {
                }
            }

            private OleDbCommand method_5(string string_1, OleDbConnection oleDbConnection_0)
            {
                string str = string.Concat("INSERT INTO [", string_1, "] ([Name], [Category], [Object])  Values (?, ?, ?)");
                OleDbCommand oleDbCommand = new OleDbCommand(str, oleDbConnection_0);
                OleDbParameterCollection parameters = oleDbCommand.Parameters;
                parameters.Add("Name", OleDbType.Char);
                parameters.Add("Category", OleDbType.Char);
                parameters.Add("Object", OleDbType.Binary);
                return oleDbCommand;
            }

            private OleDbCommand method_6(string string_1, OleDbConnection oleDbConnection_0)
            {
                string str = string.Concat("Update [", string_1, "] Set [Name] = ?, [Category] = ?, [Object] = ? Where [ID] = ?");
                OleDbCommand oleDbCommand = new OleDbCommand(str, oleDbConnection_0);
                OleDbParameterCollection parameters = oleDbCommand.Parameters;
                parameters.Add("Name", OleDbType.Char);
                parameters.Add("Category", OleDbType.Char);
                parameters.Add("Object", OleDbType.Binary);
                parameters.Add("ID", OleDbType.Integer);
                return oleDbCommand;
            }

            private OleDbCommand method_7(string string_1, OleDbConnection oleDbConnection_0)
            {
                string str = string.Concat("Delete From [", string_1, "] Where [ID] = ?");
                OleDbCommand oleDbCommand = new OleDbCommand(str, oleDbConnection_0);
                oleDbCommand.Parameters.Add("ID", OleDbType.Integer);
                return oleDbCommand;
            }

            public void RemoveFile(string string_1)
            {
                int num = 0;
                while (true)
                {
                    if (num >= this.iarray_0.Count)
                    {
                        break;
                    }
                    else if (((string)this.iarray_0.Element[num]).ToUpper() == string_1.ToUpper())
                    {
                        this.iarray_0.Remove(num);
                        break;
                    }
                    else
                    {
                        num++;
                    }
                }
            }

            public void RemoveItem(IStyleGalleryItem istyleGalleryItem_0)
            {
                Guid guid;
                object obj;
                if (this.string_0 != "")
                {
                    string str = this.method_2(istyleGalleryItem_0);
                    if (str.Length != 0 && istyleGalleryItem_0.ID != -1)
                    {
                        IPersistStream item = (IPersistStream)istyleGalleryItem_0.Item;
                        IMemoryBlobStream memoryBlobStreamClass = new MemoryBlobStream();
                        (new ObjectStream()).Stream = memoryBlobStreamClass;
                        item.GetClassID(out guid);
                        item.Save(memoryBlobStreamClass, 1);
                        ((IMemoryBlobStreamVariant)memoryBlobStreamClass).ExportToVariant(out obj);
                        Array arrays = (Array)obj;
                        byte[] numArray = new byte[arrays.Length + 16];
                        guid.ToByteArray().CopyTo(numArray, 0);
                        arrays.CopyTo(numArray, 16);
                        obj = numArray;
                        OleDbConnection oleDbConnection = new OleDbConnection(string.Concat("Provider=Microsoft.Jet.OLEDB.4.0;Data source= ", this.string_0));
                        oleDbConnection.Open();
                        OleDbCommand d = this.method_7(str, oleDbConnection);
                        d.Parameters["ID"].Value = istyleGalleryItem_0.ID;
                        d.ExecuteNonQuery();
                        oleDbConnection.Close();
                    }
                }
            }

            public void SaveStyle(string string_1, string string_2, string string_3)
            {
            }

            public void UpdateItem(IStyleGalleryItem istyleGalleryItem_0)
            {
                Guid guid;
                object obj;
                if (this.string_0 != "")
                {
                    string str = this.method_2(istyleGalleryItem_0);
                    if (str.Length != 0 && istyleGalleryItem_0.ID != -1)
                    {
                        IPersistStream item = (IPersistStream)istyleGalleryItem_0.Item;
                        IMemoryBlobStream memoryBlobStreamClass = new MemoryBlobStream();
                        (new ObjectStream()).Stream = memoryBlobStreamClass;
                        item.GetClassID(out guid);
                        item.Save(memoryBlobStreamClass, 1);
                        ((IMemoryBlobStreamVariant)memoryBlobStreamClass).ExportToVariant(out obj);
                        Array arrays = (Array)obj;
                        byte[] numArray = new byte[arrays.Length + 16];
                        guid.ToByteArray().CopyTo(numArray, 0);
                        arrays.CopyTo(numArray, 16);
                        obj = numArray;
                        OleDbConnection oleDbConnection = new OleDbConnection(string.Concat("Provider=Microsoft.Jet.OLEDB.4.0;Data source= ", this.string_0));
                        oleDbConnection.Open();
                        OleDbCommand oleDbCommand = this.method_6(str, oleDbConnection);
                        OleDbParameterCollection parameters = oleDbCommand.Parameters;
                        parameters["ID"].Value = istyleGalleryItem_0.ID;
                        parameters["Name"].Value = istyleGalleryItem_0.Name;
                        parameters["Category"].Value = istyleGalleryItem_0.Category;
                        parameters["Object"].Value = obj;
                        oleDbCommand.ExecuteNonQuery();
                        oleDbConnection.Close();
                    }
                }
            }

            public string get_File(int index)
            {
                throw new NotImplementedException();
            }

            bool IStyleGalleryStorage.get_CanUpdate(string path)
            {
                throw new NotImplementedException();
            }
        }
    }