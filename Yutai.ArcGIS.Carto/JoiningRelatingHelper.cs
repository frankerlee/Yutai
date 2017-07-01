using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto
{
    internal class JoiningRelatingHelper
    {
        public static void JoinTableLayer(ILayer ilayer_0, string string_0, ITable itable_0, string string_1)
        {
            try
            {
                IAttributeTable table = ilayer_0 as IAttributeTable;
                if (table != null)
                {
                    ITable attributeTable = table.AttributeTable;
                    if (itable_0 is IStandaloneTable)
                    {
                        itable_0 = (itable_0 as IStandaloneTable).Table;
                    }
                    IMemoryRelationshipClassFactory factory = new MemoryRelationshipClassFactoryClass();
                    IRelationshipClass relClass = factory.Open("TabletoLayer", (IObjectClass) itable_0, string_1,
                        (IObjectClass) attributeTable, string_0, "forward", "backward",
                        esriRelCardinality.esriRelCardinalityOneToMany);
                    ((IDisplayRelationshipClass) ilayer_0).DisplayRelationshipClass(relClass,
                        esriJoinType.esriLeftOuterJoin);
                }
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void JoinTableLayer(ITable itable_0, string string_0, ITable itable_1, string string_1)
        {
            try
            {
                ITable attributeTable = null;
                ITable displayTable = null;
                IDisplayTable table3 = itable_0 as IDisplayTable;
                if (table3 != null)
                {
                    displayTable = table3.DisplayTable;
                }
                if (displayTable is IRelQueryTable)
                {
                    attributeTable = displayTable;
                }
                else
                {
                    IAttributeTable table4 = itable_0 as IAttributeTable;
                    if (table4 == null)
                    {
                        return;
                    }
                    attributeTable = table4.AttributeTable;
                }
                if (itable_1 is IStandaloneTable)
                {
                    itable_1 = (itable_1 as IStandaloneTable).Table;
                }
                IMemoryRelationshipClassFactory factory = new MemoryRelationshipClassFactoryClass();
                IRelationshipClass relClass = factory.Open("TabletoLayer", (IObjectClass) itable_1, string_1,
                    (IObjectClass) attributeTable, string_0, "forward", "backward",
                    esriRelCardinality.esriRelCardinalityOneToMany);
                ((IDisplayRelationshipClass) itable_0).DisplayRelationshipClass(relClass, esriJoinType.esriLeftOuterJoin);
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void RelateTableLayer(ILayer ilayer_0, string string_0, ITable itable_0, string string_1)
        {
            try
            {
                IAttributeTable table = ilayer_0 as IAttributeTable;
                if (table != null)
                {
                    ITable attributeTable = table.AttributeTable;
                    if (itable_0 is IStandaloneTable)
                    {
                        itable_0 = (itable_0 as IStandaloneTable).Table;
                    }
                    IMemoryRelationshipClassFactory factory = new MemoryRelationshipClassFactoryClass();
                    IRelationshipClass relationshipClass = factory.Open("TabletoLayer", (IObjectClass) itable_0,
                        string_1, (IObjectClass) attributeTable, string_0, "forward", "backward",
                        esriRelCardinality.esriRelCardinalityOneToMany);
                    ((IRelationshipClassCollectionEdit) ilayer_0).AddRelationshipClass(relationshipClass);
                }
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void RelateTableLayer(string string_0, ITable itable_0, string string_1, ITable itable_1,
            string string_2)
        {
            try
            {
                IAttributeTable table = itable_0 as IAttributeTable;
                if (table != null)
                {
                    ITable attributeTable = table.AttributeTable;
                    IMemoryRelationshipClassFactory factory = new MemoryRelationshipClassFactoryClass();
                    IRelationshipClass relationshipClass = factory.Open(string_0, (IObjectClass) itable_1, string_2,
                        (IObjectClass) attributeTable, string_1, "forward", "backward",
                        esriRelCardinality.esriRelCardinalityOneToMany);
                    ((IRelationshipClassCollectionEdit) itable_0).AddRelationshipClass(relationshipClass);
                }
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static bool TableIsJoinLayer(ILayer ilayer_0, ITable itable_0)
        {
            try
            {
                IDisplayTable table = ilayer_0 as IDisplayTable;
                if (table != null)
                {
                    ITable displayTable = table.DisplayTable;
                    string str = (itable_0 as IDataset).Name.ToLower();
                    while (displayTable is IRelQueryTable)
                    {
                        IRelQueryTable table3 = displayTable as IRelQueryTable;
                        if ((table3.DestinationTable as IDataset).Name.ToLower() == str)
                        {
                            return true;
                        }
                        displayTable = table3.SourceTable;
                    }
                }
                return false;
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }

        public static bool TableIsJoinLayer(ITable itable_0, ITable itable_1)
        {
            try
            {
                IDisplayTable table = itable_0 as IDisplayTable;
                if (table != null)
                {
                    ITable displayTable = table.DisplayTable;
                    string str = (itable_1 as IDataset).Name.ToLower();
                    while (displayTable is IRelQueryTable)
                    {
                        IRelQueryTable table3 = displayTable as IRelQueryTable;
                        if ((table3.DestinationTable as IDataset).Name.ToLower() == str)
                        {
                            return true;
                        }
                        displayTable = table3.SourceTable;
                    }
                }
                return false;
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }

        public static bool TableIsRelateLayer(ILayer ilayer_0, ITable itable_0)
        {
            try
            {
                IRelationshipClassCollection classs = ilayer_0 as IRelationshipClassCollection;
                if (classs != null)
                {
                    IEnumRelationshipClass class2 = classs.FindRelationshipClasses(itable_0 as IObjectClass,
                        esriRelRole.esriRelRoleAny);
                    class2.Reset();
                    if (class2.Next() != null)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }

        public static bool TableIsRelateLayer(ITable itable_0, ITable itable_1)
        {
            try
            {
                IRelationshipClassCollection classs = itable_0 as IRelationshipClassCollection;
                if (classs != null)
                {
                    IEnumRelationshipClass class2 = classs.FindRelationshipClasses(itable_1 as IObjectClass,
                        esriRelRole.esriRelRoleAny);
                    class2.Reset();
                    if (class2.Next() != null)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }
    }
}