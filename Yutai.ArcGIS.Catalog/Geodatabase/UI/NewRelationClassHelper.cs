using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class NewRelationClassHelper
    {
        public static string backwardLabel;
        public static esriRelCardinality Cardinality;
        public static string destForeignKey;
        public static IObjectClass DestinationClass;
        public static string destPrimaryKey;
        public static string forwardLabel;
        public static bool IsAttributed;
        public static bool IsComposite;
        public static IFeatureDataset m_pFeatureDataset;
        public static IWorkspace m_pWorkspace;
        public static esriRelNotification Notification;
        public static IObjectClass OriginClass;
        public static string OriginForeignKey;
        public static string OriginPrimaryKey;
        public static IFields relAttrFields;
        public static string relClassName;

        static NewRelationClassHelper()
        {
            old_acctor_mc();
        }

        public static IRelationshipClass CreateRelation()
        {
            try
            {
                if (m_pFeatureDataset != null)
                {
                    return (m_pFeatureDataset as IRelationshipClassContainer).CreateRelationshipClass(relClassName, OriginClass, DestinationClass, forwardLabel, backwardLabel, Cardinality, Notification, IsComposite, IsAttributed, relAttrFields, OriginPrimaryKey, destPrimaryKey, OriginForeignKey, destForeignKey);
                }
                if (m_pWorkspace != null)
                {
                    return (m_pWorkspace as IFeatureWorkspace).CreateRelationshipClass(relClassName, OriginClass, DestinationClass, forwardLabel, backwardLabel, Cardinality, Notification, IsComposite, IsAttributed, relAttrFields, OriginPrimaryKey, destPrimaryKey, OriginForeignKey, destForeignKey);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("创建关系类类失败");
                Logger.Current.Error("",exception, "创建关系类");
            }
            return null;
        }

        private static void old_acctor_mc()
        {
            m_pFeatureDataset = null;
            m_pWorkspace = null;
            relClassName = "";
            OriginClass = null;
            DestinationClass = null;
            forwardLabel = "";
            backwardLabel = "";
            Cardinality = esriRelCardinality.esriRelCardinalityOneToOne;
            Notification = esriRelNotification.esriRelNotificationNone;
            IsComposite = false;
            IsAttributed = false;
            relAttrFields = new FieldsClass();
            OriginPrimaryKey = "";
            destPrimaryKey = "";
            OriginForeignKey = "";
            destForeignKey = "";
        }
    }
}

