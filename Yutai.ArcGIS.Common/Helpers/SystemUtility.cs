using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Common.Helpers
{
    public class SystemUtility
    {
        /// <summary>
        /// 实现ESRI的对象复制方法
        /// </summary>
        /// <param name="pInObject">原始对象</param>
        /// <param name="pOverwriteObject">复制后的对象</param>
        public static void ObjectCopy(object pInObject, ref object pOverwriteObject)
        {
            IObjectCopy objectCopy = new ObjectCopy();
            object copyedObj = objectCopy.Copy(pInObject);
            objectCopy.Overwrite(copyedObj, ref pOverwriteObject);
        }
    }
}
