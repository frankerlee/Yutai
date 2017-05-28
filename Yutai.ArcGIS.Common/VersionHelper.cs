using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common
{
    public class VersionHelper
    {
        public VersionHelper()
        {
        }

        public static bool CheckVersionLocks(IVersion pVersion)
        {
            bool flag;
            IEnumLockInfo versionLocks = pVersion.VersionLocks;
            ILockInfo lockInfo = versionLocks.Next();
            StringBuilder stringBuilder = new StringBuilder();
            while (lockInfo != null)
            {
                stringBuilder.Append(string.Concat("数据库被", lockInfo.UserName, "所锁定!"));
                lockInfo = versionLocks.Next();
            }
            string str = stringBuilder.ToString();
            if (str.Equals(""))
            {
                flag = true;
            }
            else
            {
                MessageBox.Show(str, "检查数据库锁定", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                flag = false;
            }
            return flag;
        }

        public static bool CheckVersionName(List<string> pVersionNameList, string sVersionName)
        {
            bool flag;
            foreach (string str in pVersionNameList)
            {
                if (OtherHelper.GetRightName(str, ".").Equals(sVersionName.ToUpper()))
                {
                    flag = true;
                    return flag;
                }
            }
            flag = false;
            return flag;
        }

        public IFIDSet FindHistoricalDifferences(IWorkspace workspace, string historicalMarkerName, string tableName, esriDifferenceType differenceType)
        {
            IHistoricalWorkspace historicalWorkspace = (IHistoricalWorkspace)workspace;
            IHistoricalVersion historicalVersion = historicalWorkspace.FindHistoricalVersionByName(historicalWorkspace.DefaultMarkerName);
            IHistoricalVersion historicalVersion1 = historicalWorkspace.FindHistoricalVersionByName(historicalMarkerName);
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)historicalVersion;
            IFeatureWorkspace featureWorkspace1 = (IFeatureWorkspace)historicalVersion1;
            ITable table = featureWorkspace.OpenTable(tableName);
            ITable table1 = featureWorkspace1.OpenTable(tableName);
            IDifferenceCursor differenceCursor = ((IVersionedTable)table).Differences(table1, differenceType, null);
            IFIDSet fIDSetClass = new FIDSet();
            IRow row = null;
            int num = -1;
            differenceCursor.Next(out num, out row);
            while (num != -1)
            {
                fIDSetClass.Add(num);
                differenceCursor.Next(out num, out row);
            }
            fIDSetClass.Reset();
            return fIDSetClass;
        }

        public static IFIDSet FindVersionDifferences(IWorkspace workspace, string childVersionName, string parentVersionName, string tableName, esriDifferenceType differenceType)
        {
            IVersionedWorkspace versionedWorkspace = (IVersionedWorkspace)workspace;
            IVersion version = versionedWorkspace.FindVersion(childVersionName);
            IVersion version1 = versionedWorkspace.FindVersion(parentVersionName);
            IVersion commonAncestor = ((IVersion2)version).GetCommonAncestor(version1);
            ITable table = ((IFeatureWorkspace)version).OpenTable(tableName);
            ITable table1 = ((IFeatureWorkspace)commonAncestor).OpenTable(tableName);
            IDifferenceCursor differenceCursor = ((IVersionedTable)table).Differences(table1, differenceType, null);
            IFIDSet fIDSetClass = new FIDSet();
            IRow row = null;
            int num = -1;
            differenceCursor.Next(out num, out row);
            while (num != -1)
            {
                fIDSetClass.Add(num);
                differenceCursor.Next(out num, out row);
            }
            fIDSetClass.Reset();
            return fIDSetClass;
        }

        public static IVersion GetDefaultVersion(IWorkspace pWS)
        {
            return (pWS as IVersionedWorkspace).DefaultVersion;
        }

        public static List<IVersion> GetParentVersionList(IVersion pVersion)
        {
            List<IVersion> versions = new List<IVersion>();
            if (pVersion.HasParent())
            {
                IVersionedWorkspace versionedWorkspace = pVersion as IVersionedWorkspace;
                IEnumVersionInfo ancestors = pVersion.VersionInfo.Ancestors;
                IVersionInfo versionInfo = ancestors.Next();
                while (versionInfo != null)
                {
                    versions.Add(versionedWorkspace.FindVersion(versionInfo.VersionName));
                    versionInfo = ancestors.Next();
                }
            }
            return versions;
        }

        public static List<string> GetParentVersionNameList(IVersion pVersion)
        {
            List<string> strs = new List<string>();
            if (pVersion.HasParent())
            {
                IEnumVersionInfo ancestors = pVersion.VersionInfo.Ancestors;
                for (IVersionInfo i = ancestors.Next(); i != null; i = ancestors.Next())
                {
                    strs.Add(i.VersionName);
                }
            }
            return strs;
        }

        public static IVersion GetVersion(IWorkspace pWS, string sVersionName, string sPws)
        {
            pWS = WorkspaceHelper.SwitchVersionWorkspace(pWS, sVersionName, sPws);
            return pWS as IVersionedWorkspace as IVersion;
        }

        public static List<IVersionInfo> GetVersionInfoList(IWorkspace pWS)
        {
            List<IVersionInfo> versionInfos = new List<IVersionInfo>();
            IEnumVersionInfo versions = (pWS as IVersionedWorkspace).Versions;
            for (IVersionInfo i = versions.Next(); i != null; i = versions.Next())
            {
                versionInfos.Add(i);
            }
            return versionInfos;
        }

        public static List<string> GetVersionNameList(List<IVersionInfo> pListVI)
        {
            List<string> strs = new List<string>();
            foreach (IVersionInfo versionInfo in pListVI)
            {
                strs.Add(versionInfo.VersionName);
            }
            return strs;
        }
    }
}
